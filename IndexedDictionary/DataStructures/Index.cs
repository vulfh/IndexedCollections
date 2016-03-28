using IndexedCollections.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.DataStructures
{
    internal class Index
    {
        #region Members

        private Dictionary<int, List<int>> _values;

        private List<int> _indexRegistry;


        #endregion

        #region Properties

        public string Id { get; private set; }

        public PropertyInfo Property { get; private set; }

        public Dictionary<int, List<int>>.ValueCollection Values { get { return _values.Values; } }


        public bool Unique { get; private set; }

        #endregion

        #region Indexers

        public List<int> this[int idx]
        {
            get
            {
                if (_values.ContainsKey(idx))
                    return _values[idx];
                else
                    return null;
            }
        }

        #endregion

        #region Constructors

        public Index(string id,PropertyInfo property,bool unique)
        {
            Id = id;
            Property = property;
            Unique = unique;
            _values = new Dictionary<int, List<int>>();
            if (Unique)
                _indexRegistry = new List<int>();
        }

        #endregion

        #region Methods

        #region AddValue

        public void AddValue(int key, int index) 
        {
            List<int> indexValues = GetIndexByKey(key);
            if(indexValues != null)
            {
                indexValues.Add(index);
            }
            else
            {
                indexValues = new List<int>();
                indexValues.Add(index);
                _values.Add(key, indexValues);
            }
            if (Unique)
            {
                if (!_indexRegistry.Contains(index))
                {
                    _indexRegistry.Add(index);
                }
                else
                {
                    throw new DuplicateUniqueIndexException(new Exception(string.Concat("Index:",this.Property.Name)));
                }
            }
        }

        #endregion


        #region GetKeysByIndex
        
        public List<int> GetKeysByIndex<T>(T template)
        {
            int indexValue = Property.GetValue(template).GetHashCode();
            return GetKeysByIndex(indexValue);
        }

        #endregion

        #region GetKeysByIndex
       
        public List<int> GetKeysByIndex(int index)
        {
            return (from v in _values
                    where v.Value.Contains(index)
                    select v.Key).ToList();
        }

        #endregion

        #region GetKeysNotInIndex

        public List<int> GetKeysNotInIndex(int index)
        {
            return (from v in _values
                    where !v.Value.Contains(index)
                    select v.Key).ToList();
        }

        #endregion

        #region GetIndexByKey
        
        public List<int> GetIndexByKey(int key)
        {
            if (_values.ContainsKey(key))
            {
                return _values[key];
            }
            else
            {
                return null;
            }
        }

        #endregion


        #region RemoveByKey

        public void RemoveByKey(int keyHashCode)
        {
            if (_values.ContainsKey(keyHashCode))
            {
                _values.Remove(keyHashCode);
                if (Unique)
                {
                    _indexRegistry.Remove(keyHashCode);
                }
            }
        }

        #endregion

        #region GetAllKeys

        public IReadOnlyCollection<int> GetAllKeys()
        {
            ReadOnlyCollection<int> keys = null;

            if (_values != null)
                keys = new ReadOnlyCollection<int>(_values.Keys.ToList());

            return keys;
        }

        #endregion
        #endregion



    }
}
