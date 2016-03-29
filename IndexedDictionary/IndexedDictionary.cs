using IndexedCollections.Attributes;
using IndexedCollections.Exceptions;
using IndexedCollections.DataStructures;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IndexedCollections.Definitions;

namespace IndexedCollections
{
    public class IndexedDictionary<K,T> : IEnumerator<T>,IEnumerable<T> where T:class
    {
        #region Memebers

        private Dictionary<int, T> _items;
        private IndexRepository<T> _indexes;
        private Type _typeOfItem;
        private PropertyInfo _keyProperty;
        private T _current;
        private int _currentIndex;


        #endregion

        #region Properties
        /// <summary>
        /// Total amount a of items stored in the in the indexed dictionary.
        /// </summary>
        public int Count { get; private set; }

        
        /// <summary>
        /// Reserved property. No usage in the current version
        /// </summary>
        public int ParallelismDegree { get; set; }

        #endregion

        #region Indexers
        /// <summary>
        /// Retrieves item by  key.
        /// </summary>
        /// <param name="hashCode"></param>
        /// <returns></returns>
        public T this[K key]
        {
            get
            {
                if (key != null)
                {
                    var hashCode = key.GetHashCode();
                    if (_items.ContainsKey(hashCode))
                        return _items[hashCode];
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (key != null)
                {
                    var hashCode = key.GetHashCode();
                    if (_items.ContainsKey(hashCode))
                    {
                        throw new DuplicateKeyException();
                    }
                    else
                    {
                        _items[hashCode] = value;
                    }
                }
                else
                {
                    throw new Exception("Provided key is null!");
                }
            }
        }

        /// <summary>
        /// Retrieves the item with the key property  matching to the key property the provided template. If it does not exist, returns null.
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public T this[T template]
        {
            get
            {
                if (template != null)
                {
                    K key = GetKeyValue(template);
                    return this[key];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                K key = GetKeyValue(template);
                this[key] = value;
            }
        }

        #endregion

        #region Constructors

        public IndexedDictionary()
        {
            Initialize();
            _items = new Dictionary<int, T>();
            _keyProperty = GetKeyProperty();
        }


        #endregion

        #region Methods

        #region Add
        /// <summary>
        /// Adds item of type T with property marked with KeyAttribute. If object with the same key already exists in dictionary an exception will be thrown.
        /// </summary>
        /// <param name="item"> Item to add to indexed dictionary</param>
        public void Add(T item)
        {
            if (item != null)
            {
                object keyValue = _keyProperty.GetValue(item);
                int keyValueHashCode = keyValue.GetHashCode();
                if (!_items.ContainsKey(keyValueHashCode))
                {
                    _items.Add(keyValueHashCode, item);
                }
                else
                {
                    throw new DuplicateKeyException();
                }
                _indexes.IndexItem(item, keyValueHashCode);
                Count = _items.Count;
            }
            else
            {
                throw new Exception("The value you are trying to add is null ! Please assign value first !");
            }
        }
        #endregion

        #region GetByTemplate
       /// <summary>
       /// Gets array of items where atleast one of the indexed properties 
       /// matched the not null index properties in the template
       /// </summary>
       /// <param name="template">search pattern, where interested indexed properties are not null</param>
       /// <param name="loginOperator">Logic operation should be performed betweeen or on each index search property</param>
       /// <param name="encounterKey">Flag determines whether key provided in template should be involved in query</param>
       /// <returns></returns>
        public T[] GetByTemplate(T template, LogicOperator logicOperator,bool encounterKey = true)
        {
            List<T> result = new List<T>();
            object syncResult = new object();
            //Get relevant indexes (which are not nulls in tremplate)
            // Generate hascode of each relevant index proeprty
            //Retrieve key hashcodes of each code
            //Build array of indexs intersection

            List<int> keys = _indexes.Get(template, logicOperator);
            int? templateKeyHash = GetKeyHashOfTemplate(template);
            Action<T, int> addItem = null;

            if (templateKeyHash.HasValue && encounterKey)
            {
                addItem = new Action<T, int>((item, key) =>
                {
                    if (templateKeyHash.Value == key)
                    {
                        lock (syncResult)
                        {
                            result.Add(item);
                        }
                    }
                });
            }
            else
            {
                addItem = new Action<T, int>((item, key) => {
                    lock (syncResult)
                    {
                        result.Add(item);
                    }
                });
            }
            foreach (var key in keys.AsParallel())
            {
                addItem(_items[key], key);
            }
            

            return result.ToArray();
        }

        #endregion

        #region Contains
        /// <summary>
        /// Returns true if item with the same key and/or unique indxes already exists in the dictionary
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>

        public bool ContainsByTemplate(T candidate)
        {
            bool answer = false;
            int candidateKeyHash = _keyProperty.GetValue(candidate).GetHashCode();

            answer = TestContains(candidateKeyHash);

            if (answer == false)
            {
                answer = _indexes.Contains(candidate, candidateKeyHash);
            }
            return answer;
        }

        #endregion

        #region ContainsKey
        /// <summary>
        /// Returns true if  specified key already exists in dictionary
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(K key)
        {
            bool answer = false;
            int candidateKeyHash = key.GetHashCode();
            answer = TestContains(candidateKeyHash);
            return answer;
        }

        #endregion

        #region RemoveByKey
        /// <summary>
        /// Removes item from dictionary by specified hash code of the key. Returns true in case the item was found, false otherwise
        /// </summary>
        /// <param name="keyHashCode"></param>
        /// <returns></returns>
        public bool RemoveByKey(K key)
        {
            bool answer = false;
            int keyHashCode = key.GetHashCode();
            if (_items.ContainsKey(keyHashCode))
            {
                _items.Remove(keyHashCode);
                _indexes.RemoveByKey(keyHashCode);
                answer = true;
            }

            return answer;
        }

        /// <summary>
        /// Removes item from dictionary by key of the specified template. Returns true in case the item was found, false otherwise
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public bool RemoveByKey(T template)
        {
            return RemoveByKey(GetKeyValue(template));
        }

        #endregion

        #endregion

        #region Private Methods

        #region TestComtains

        private bool TestContains(int keyHashCode)
        {
            bool answer = false;
            foreach (var key in _items.Keys)
            {
                if (key == keyHashCode)
                {
                    answer = true;
                    break;
                }
            }
            return answer;
        }
        #endregion

        #region GetHashVectorOfCurrentItem

        private int [] GetHashVectorOfItem(T item)
        {
            int[] vector = new int[_indexes.Count + 1];//+1 for the key

            vector[0] = _keyProperty.GetValue(item).GetHashCode();
            for(int i =0;i< _indexes.Count; i++)
            {
                //if (_indexes.IndexItem.Unique)
                //{

                //}
            }

            return vector;
        }

        #endregion

        #region PerformAnd
        private List<T> PerformAnd(Index[] relevantIndexes, T template)
        {
            List<T> result = new List<T>();
            object syncResult = new object();

    


            foreach (Index currentIndex in relevantIndexes.AsParallel())
            {
                lock (syncResult)
                {
                    result.AddRange(Or(template, currentIndex));
                }
            }
            return result;

        }
        #endregion

        #region PerformOr
        private List<T> PerformOr(Index[] relevantIndexes, T template)
        {
            List<T> result = new List<T>();
            object syncResult = new object();
            foreach (Index currentIndex in relevantIndexes.AsParallel())
            {
                lock (syncResult)
                {
                    result.AddRange(Or(template, currentIndex));
                }
            }
            return result;
        }
        #endregion

        private List<T> Or(T template,Index currentIndex)
        {
            List<int> keys = currentIndex.GetKeysByIndex<T>(template);
            List<T> subset = (from v in _items
                              join k in keys
                              on v.Key equals k
                              select v.Value).ToList();
            return subset;
        }

        private void Initialize()
        {
           _typeOfItem = typeof(T);
           _keyProperty = GetKeyProperty();
           _indexes = new IndexRepository<T>();
        }

        
        private bool IsKeyProperty(PropertyInfo property)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(property,typeof(KeyAttribute));
            if (attributes != null && attributes.Length > 0)
                return true;
            else
                return false;

        }

        

        private PropertyInfo GetKeyProperty()
        {
            PropertyInfo[] properties = _typeOfItem.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (IsKeyProperty(property))
                    return property;

            }
            throw new NoKeyPropertyException();
        }
       
        private int GetHashCodeOfProperty(PropertyInfo property,T item)
        {
            object value = property.GetValue(item);
            return value.GetHashCode();
        }

        private K GetKeyValue(T value)
        {
            K key = default(K);
            key = (K)_keyProperty.GetValue(value);
            return key;
        }

        private T GetCurrent()
        {
            if (_current == null & _items.Count > 0)
            {
                _current = this._items.ElementAt(0).Value;
                _currentIndex = 0;
            }
            else if(_currentIndex >= _items.Count)
            {
                _current = null;
            }
            return _current;
        }

        private int? GetKeyHashOfTemplate(T template)
        {
            object keyValue = _keyProperty.GetValue(template);
            int? keyHash = null;
            if (keyValue != null)
            {
                keyHash = keyValue.GetHashCode();
            }
            return keyHash;
        }

        #endregion

        #region IEnumerator
        #region Current

        public T Current
        {
            get { return GetCurrent(); }
        }

        object IEnumerator.Current
        {
            get
            {
                return GetCurrent();
            }
        }


        #endregion


        #region MoveNext

        public bool MoveNext()
        {
            bool result = false;
            if (++_currentIndex < Count)
            {
                _current = _items.ElementAt(_currentIndex).Value;
                result = true;
            }
            return result;
        }

        #endregion

        #region Reset

        public void Reset()
        {
            _currentIndex = 0;
            _current = null;
        }

        #endregion

        #endregion

        #region IEnumerable impelementation

        #region GetEnumerator

        public IEnumerator<T> GetEnumerator()
        {
            return this as IEnumerator<T>;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this as IEnumerator;
        }

        #endregion

        #endregion

        #region IDisposable implementation

        #region Dispose

        public void Dispose()
        {
            _items.Clear();
            _indexes.Dispose();
        }

        #endregion

        #endregion
    }
}
