using IndexedCollections.Attributes;
using IndexedCollections.Definitions;
using IndexedCollections.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.DataStructures
{
    internal class IndexRepository<T>:IDisposable
    {
        #region Members
        private Dictionary<string, Index> _indexes;
        private string[] _indexKeys;
        private Type _typeOfItem;
        #endregion

        #region Properties

        public int Count {
            get
            {
                if (_indexes != null)
                    return _indexes.Count;
                else
                    return 0;
            }
        }

        #endregion

        #region Constructors

        public IndexRepository()
        {
            _typeOfItem = typeof(T);
            _indexes = new Dictionary<string, Index>();
            SetupIndexedProperties();
            SetupIndexKeys();
        }

       

        #endregion

        #region Methods

        #region Get

        public List<int> Get(T template,LogicOperator logicOperator)
        {
            int?[] indexMask = BuildIndexMask(template);
            List<int> result = null;
            switch (logicOperator)
            {
                case LogicOperator.OR:
                    result = PerformOr(indexMask);
                    break;
                case LogicOperator.AND:
                    result = PerformAnd(indexMask,false);
                    break;
                case LogicOperator.AND_IGNORE_NULLS:
                    result = PerformAnd(indexMask, true);
                    break;
                case LogicOperator.NOT:
                    result = PerformNot(indexMask);
                    break;
            }
            return result;
        }

        #endregion

        #region RemoveByKey

        public void RemoveByKey(int keyHashCode)
        {
            foreach(var index in _indexes)
            {
                index.Value.RemoveByKey(keyHashCode);
            }
        }

        #endregion

        #region IndexItem

        public void IndexItem(T item, int keyHashCode)
        {
            if (_indexes != null)
            {
                foreach (Index index in _indexes.Values)
                {
                    object indexValue = index.Property.GetValue(item);
                    int hashCode = indexValue.GetHashCode();
                    if (indexValue == null)
                        throw new IndexPropertyIsNullException(index.Property.Name);
                    index.AddValue(keyHashCode, hashCode);
                }
            }
        }

        #endregion


        #region Contains

        public bool Contains(T candidate,int keyHash)
        {
            bool answer = false;
            foreach (var ind in _indexes)
            {
                if (ind.Value.Unique)
                {
                    var candidateIndexHash = ind.Value.Property.GetValue(candidate).GetHashCode();
                    var index = ind.Value.GetKeysByIndex(candidateIndexHash);
                    if (index != null && index.Count>0)
                    {
                        answer = true;
                        break;
                    }
                }
            }
            return answer;
        }
        #endregion

        #region IDisposable implementation

        #region Dispose

        public void Dispose()
        {
            _indexes.Clear();
        }

        #endregion

        #endregion

        #endregion

        #region Private Methods

        #region SetupIndexKeys
        private void SetupIndexKeys()
        {
            _indexKeys = new string[_indexes.Count];
            for (int i = 0; i < _indexes.Count; i++)
            {
                _indexKeys[i] = _indexes.ElementAt(i).Key;
            }
        }
        #endregion

        #region PerformOr
        private List<int> PerformOr(int?[] indexMask)
        {
            List<int> result = new List<int>();
            var sync = new object();
            IndexMaskItem[] im = new IndexMaskItem[indexMask.Length];
            for (int i = 0; i < indexMask.Length; i++)
            {
                if (indexMask[i].HasValue)
                {
                    IndexMaskItem item = new IndexMaskItem() { Value = indexMask[i].Value ,Index = i};
                    im[i] = item;
                }
                else
                {
                    im[i] = null;
                }

            }
            Func<IndexMaskItem,List<int>> selectKeysByndex = new Func<IndexMaskItem,List<int>>((index) => {
                if (index != null)
                {
                   return _indexes[_indexKeys[index.Index]].GetKeysByIndex(index.Value);
                }
                else
                {
                    return null;
                }

            
            });
           
                foreach (var index in im.AsParallel())
                {
                    List<int> keys = selectKeysByndex(index);
                    if (keys != null)
                    {
                        lock (sync)
                        {
                            foreach(var key in keys)
                            {
                                if (!result.Contains(key))
                                {
                                    result.Add(key);
                                }
                            }
                        }
                    }
                }
            return result;

        }
        #endregion

        #region PerformAnd
        private List<int> PerformAnd(int?[] indexMask,bool ignoreNulls)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < indexMask.Length; i++)
            {
                var index = indexMask[i];
                if (index.HasValue)
                {
                    List<int> keys = _indexes[_indexKeys[i]].GetKeysByIndex(index.Value);
                    if (keys != null)
                    {
                        if (i == 0)
                        {
                            result.AddRange(keys);
                        }
                        else
                        {
                            IEnumerable<int> temp = result.Intersect(keys);
                            result =  temp.ToList();
                        }
                    }
                }
                else
                {
                    if (ignoreNulls)
                    {
                        var keys = _indexes[_indexKeys[i]].GetAllKeys();
                        if (keys != null)
                        {
                            if (i == 0)
                            {
                                result.AddRange(keys);
                            }
                            else
                            {
                                IEnumerable<int> temp = result.Intersect(keys);
                                result = temp.ToList();
                            }
                        }
                    }
                }
            }
            return result;

        }
        #endregion

        #region PerformNot
        private List<int> PerformNot(int?[] indexMask)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < indexMask.Length; i++)
            {
                var index = indexMask[i];
                if (index.HasValue)
                {
                    List<int> keys = _indexes[_indexKeys[i]].GetKeysNotInIndex(index.Value);
                    if (keys != null)
                    {
                        if (i == 0)
                        {
                            result.AddRange(keys);
                        }
                        else
                        {
                            IEnumerable<int> temp = result.Intersect(keys);
                            result = temp.ToList();
                        }
                    }
                }
            }
            return result;

        }
        #endregion

        #region BuildIndexMask
        private int?[] BuildIndexMask(T template)
        {
            int?[] mask = new int?[_indexes.Count];
            int indexCounter = 0;
            foreach (var index in _indexes)
            {
                object indexedProperty = index.Value.Property.GetValue(template);
                if ( indexedProperty!= null)
                {
                    mask[indexCounter] = indexedProperty.GetHashCode();
                }
                else
                {
                    mask[indexCounter] = null;
                }
                indexCounter++;
            }
            return mask;
        }
        #endregion

        #region SetupIndexedProperties

        private void SetupIndexedProperties()
        {
            List<PropertyInfo> indexedProperties = new List<PropertyInfo>();
            PropertyInfo[] properties = _typeOfItem.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                Tuple<bool, bool,bool> isIdexedProperty = IsIndexProperty(property);
                if (isIdexedProperty.Item1)
                {
                    indexedProperties.Add(property);
                    _indexes.Add(property.Name, new Index(property.Name, property,isIdexedProperty.Item2));
                }
            }
        }
        #endregion

        #region IsIndexProperty
        private Tuple<bool,bool,bool> IsIndexProperty(PropertyInfo property)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(property, typeof(IndexAttribute));
            bool isIndexProperty = false;
            bool isUniqueIndex = false;
            bool isImmutable = false;
            if (attributes != null && attributes.Length > 0)
            {
                isIndexProperty = true;
                if ((attributes[0] as IndexAttribute).Unique)
                    isUniqueIndex = true;
                if ((attributes[0] as IndexAttribute).Immutable)
                {
                    isImmutable = true;
                    if (property.CanWrite)
                        throw new Exceptions.IndexShouldBeImmutableException("PropertyName:" + property.Name);
                }


            }
            return new Tuple<bool, bool, bool>(isIndexProperty, isUniqueIndex, isImmutable);
        }

       
        #endregion

        #endregion
    }
}
