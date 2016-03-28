using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IndexDictionaryUnitTests.SampleObjects;
using IndexedCollections;
using IndexedCollections.Exceptions;
using System.Collections.Generic;
using IndexedCollections.DataStructures;
using IndexedCollections.Definitions;

namespace IndexDictionaryUnitTests
{
    [TestClass]
    public class TestDictionary
    {

        #region Members

        IndexedDictionary<int,HasKey> items1000;

        #endregion

        #region Init

        [TestInitialize]
        public void Init()
        {
            items1000 = new IndexedDictionary<int,HasKey>();
            for(int i =0;i< 1000; i++)
            {
                HasKey obj = new HasKey() { Id = i };
                if (i % 2 == 0)
                    obj.Name = "Lolik";
                else
                    obj.Name = "Bolik";

                obj.LastName = "Tolik_" + i.ToString();
                items1000.Add(obj);
            }
        }

        #endregion

        #region TestKeyAttributeNotExists

        [TestMethod]
        public void TestKeyAttributeNotExists()
        {
            Exception resultException = null;
            try
            {
                NoKey noKey = new NoKey();
                IndexedDictionary<int,NoKey> dictionary = new IndexedDictionary<int,NoKey>();
            }
            catch (Exception ex)
            {
                resultException = ex;
            }
            Assert.IsInstanceOfType(resultException, typeof(NoKeyPropertyException));

        }

        #endregion

        #region TestKeyAttributeExists

        [TestMethod]
        public void TestKeyAttributeExists()
        {
            //Arrange
            HasKey hasKey = new HasKey();
            hasKey.Id = 56;
            hasKey.Name = "Jackson";
            hasKey.LastName = "Bolik";
            IndexedDictionary<int,HasKey> dictionary = new IndexedDictionary<int,HasKey>();
            //Act
            dictionary.Add(hasKey);

            //Assert
            Assert.AreEqual(dictionary.Count, 1,"The dictionary failed to add item");
        }

        #endregion

        #region TestGetItemsByIndexTemplateORKeyNotInvolved

        [TestMethod]
        public void TestGetItemsByIndexTemplateORKeyNotInvolved()
        {
            //Arrange
            HasKey hasKey1 = new HasKey() { Id = 1, Name = "Jason" ,LastName = "Bolik"};
            HasKey hasKey2 = new HasKey() { Id = 2, Name = "Jason", LastName = "Lolik"};
            HasKey hasKey3 = new HasKey() { Id = 3, Name = "John" ,LastName = "Tolik"};
            HasKey hasKey4 = new HasKey() { Id = 4, Name = "John", LastName = "Alkogolik" };

            IndexedDictionary<int,HasKey> dictionary = new IndexedDictionary<int,HasKey>();
            dictionary.Add(hasKey1);
            dictionary.Add(hasKey2);
            dictionary.Add(hasKey3);
            dictionary.Add(hasKey4);

            //Act

            HasKey[] result = dictionary.GetByTemplate(new HasKey() { Id = 0, Name = "Jason",LastName="Alkogolik" },LogicOperator.OR,false);
            
            //Asert
            Assert.AreEqual(result.Length, 3, "Failed to match values");
        }

        #endregion

        #region TestGetItemsByIndexTemplateANDKeyNotInvolvedForNotMatching

        [TestMethod]
        public void TestGetItemsByIndexTemplateANDKeyNotInvolvedForNotMatching()
        {
            //Arrange
            HasKey hasKey1 = new HasKey() { Id = 1, Name = "Jason", LastName = "Bolik" };
            HasKey hasKey2 = new HasKey() { Id = 2, Name = "Jason", LastName = "Lolik" };
            HasKey hasKey3 = new HasKey() { Id = 3, Name = "John", LastName = "Tolik" };
            HasKey hasKey4 = new HasKey() { Id = 4, Name = "John", LastName = "Alkogolik" };

            IndexedDictionary<int,HasKey> dictionary = new IndexedDictionary<int,HasKey>();
            dictionary.Add(hasKey1);
            dictionary.Add(hasKey2);
            dictionary.Add(hasKey3);
            dictionary.Add(hasKey4);

            //Act

            HasKey[] result = dictionary.GetByTemplate(new HasKey() { Id = 0, Name = "Jason", LastName = "Alkogolik" }, LogicOperator.AND,false);

            //Asert
            Assert.AreEqual(result.Length, 0, "Matched Values that should not match");
        }

        #endregion

        #region TestGetItemsByIndexTemplateANDKeyNotInvolved

        [TestMethod]
        public void TestGetItemsByIndexTemplateANDKeyNotInvolved()
        {
            //Arrange
            HasKey hasKey1 = new HasKey() { Id = 1, Name = "Jason", LastName = "Bolik" };
            HasKey hasKey2 = new HasKey() { Id = 2, Name = "Jason", LastName = "Lolik" };
            HasKey hasKey3 = new HasKey() { Id = 3, Name = "John", LastName = "Tolik" };
            HasKey hasKey4 = new HasKey() { Id = 4, Name = "John", LastName = "Alkogolik" };

            IndexedDictionary<int,HasKey> dictionary = new IndexedDictionary<int,HasKey>();
            dictionary.Add(hasKey1);
            dictionary.Add(hasKey2);
            dictionary.Add(hasKey3);
            dictionary.Add(hasKey4);

            //Act

            HasKey[] result = dictionary.GetByTemplate(new HasKey() { Id = 0, Name = "John", LastName = "Alkogolik" }, LogicOperator.AND,false);

            //Asert
            Assert.AreEqual(result.Length, 1, "Matched Values that should not match");
        }

        #endregion

        #region TestDuplicateKey

        [TestMethod]
        public void TestDuplicateKey()
        {
            //Arrange
            Exception resultException = null;
            HasKey hasKey = new HasKey();
            hasKey.Id = 1;
            hasKey.Name = "Jackson";
            hasKey.LastName = "Lolik";
            IndexedDictionary<int,HasKey> dictionary = new IndexedDictionary<int,HasKey>();
            dictionary.Add(hasKey);


            //Act
            try
            {
                HasKey hasKey2 = new HasKey();
                hasKey2.Id = 1;
                hasKey2.Name = "Jackson";
                hasKey2.LastName = "Tolik";
                dictionary.Add(hasKey2);
            }
            catch (Exception ex)
            {
                resultException = ex;
            }

            //Assert
            Assert.IsInstanceOfType(resultException, typeof(DuplicateKeyException),"A Duplicate Key exception should be raised !");
        }

        #endregion

        #region TestUniqueIndex

        [TestMethod]
        public void TestUniqueIndex()
        {
            //Arrange
            Exception resultException = null;
            HasKeyUniqueIndex hasKey = new HasKeyUniqueIndex();
            hasKey.Id = 1;
            hasKey.Name = "Jackson";
            hasKey.LastName = "Lolik";
            IndexedDictionary<int,HasKeyUniqueIndex> dictionary = new IndexedDictionary<int,HasKeyUniqueIndex>();
            dictionary.Add(hasKey);


            //Act
            try
            {
                HasKeyUniqueIndex hasKey2 = new HasKeyUniqueIndex();
                hasKey2.Id = 2;
                hasKey2.Name = "Tolik";
                hasKey2.LastName = "Lolik";
                dictionary.Add(hasKey2);
            }
            catch (Exception ex)
            {
                resultException = ex;
            }

            //Assert
            Assert.IsInstanceOfType(resultException, typeof(DuplicateUniqueIndexException), "A Duplicate Unique Index exception should be raised !");
        }

        #endregion

        #region TestContainsKey

        [TestMethod]
        public void TestContainsKey()
        {
            //Arrange
            HasKey hasKey = new HasKey();
            hasKey.Id = 1;
            hasKey.Name = "Jackson";
            hasKey.LastName = "Lolik";
            IndexedDictionary<int, HasKey> dictionary = new IndexedDictionary<int, HasKey>();
            HasKey hasKey2 = new HasKey();
            hasKey2.Id = 1;
            hasKey2.Name = "Tolik";
            hasKey2.LastName = "Lolik";
            dictionary.Add(hasKey);
            bool result = false;

            //Act
            result = dictionary.ContainsKey(hasKey2.Id);

            //Assert
            Assert.AreEqual(result, true, "Contained object was not identified!");
        }

        #endregion

        #region TestNotContainsKey

        [TestMethod]
        public void TestNotContainsKey()
        {
            //Arrange
            HasKey hasKey = new HasKey();
            hasKey.Id = 1;
            hasKey.Name = "Jackson";
            hasKey.LastName = "Lolik";
            IndexedDictionary<int, HasKey> dictionary = new IndexedDictionary<int, HasKey>();
            HasKey hasKey2 = new HasKey();
            hasKey2.Id = 2;
            hasKey2.Name = "Tolik";
            hasKey2.LastName = "Lolik";
            dictionary.Add(hasKey);
            bool result = false;

            //Act
            result = dictionary.ContainsKey(hasKey2.Id);
            //Assert
            Assert.AreEqual(result, false, "The object is not contained !");
        }

        #endregion

        #region TestContainsKeyByTemplateOnly

        [TestMethod]
        public void TestContainsKeyByTemplateOnly()
        {
            //Arrange
            HasKey hasKey = new HasKey();
            hasKey.Id = 1;
            hasKey.Name = "Jackson";
            hasKey.LastName = "Lolik";
            IndexedDictionary<int,HasKey> dictionary = new IndexedDictionary<int,HasKey>();
            HasKey hasKey2 = new HasKey();
            hasKey2.Id = 1;
            hasKey2.Name = "Tolik";
            hasKey2.LastName = "Lolik";
            dictionary.Add(hasKey);
            bool result = false;

            //Act
            result = dictionary.ContainsByTemplate(hasKey2);

            //Assert
            Assert.AreEqual(result, true, "Contained object was not identified!");
        }

        #endregion

        #region TestNotContainsKeyByTemplateOnly

        [TestMethod]
        public void TestNotContainsKeyByTemplateOnly()
        {
            //Arrange
            HasKey hasKey = new HasKey();
            hasKey.Id = 1;
            hasKey.Name = "Jackson";
            hasKey.LastName = "Lolik";
            IndexedDictionary<int,HasKey> dictionary = new IndexedDictionary<int,HasKey>();
            HasKey hasKey2 = new HasKey();
            hasKey2.Id = 2;
            hasKey2.Name = "Tolik";
            hasKey2.LastName = "Lolik";
            dictionary.Add(hasKey);
            bool result = false;

            //Act
            result = dictionary.ContainsByTemplate(hasKey2);
            //Assert
            Assert.AreEqual(result, false, "The object is not contained !");
        }

        #endregion

        #region TestContainsUniqueIndex

        [TestMethod]
        public void TestContainsUniqueIndex()
        {
            //Arrange
            HasKeyUniqueIndex hasKey = new HasKeyUniqueIndex();
            hasKey.Id = 1;
            hasKey.Name = "Jackson";
            hasKey.LastName = "Lolik";
            IndexedDictionary<int,HasKeyUniqueIndex> dictionary = new IndexedDictionary<int,HasKeyUniqueIndex>();
            HasKeyUniqueIndex hasKey2 = new HasKeyUniqueIndex();
            hasKey2.Id = 2;
            hasKey2.Name = "Tolik";
            hasKey2.LastName = "Lolik";
            dictionary.Add(hasKey);
            bool result = false;

            //Act
            result = dictionary.ContainsByTemplate(hasKey2);

            //Assert
            Assert.AreEqual(result, true, "Contained object was not identified!");
        }

        #endregion

        #region TestNotContainsUniqueIndex

        [TestMethod]
        public void TestNotContainsUniqueIndex()
        {
            //Arrange
            HasKeyUniqueIndex hasKey = new HasKeyUniqueIndex();
            hasKey.Id = 1;
            hasKey.Name = "Jackson";
            hasKey.LastName = "Lolik";
            IndexedDictionary<int,HasKeyUniqueIndex> dictionary = new IndexedDictionary<int,HasKeyUniqueIndex>();
            HasKeyUniqueIndex hasKey2 = new HasKeyUniqueIndex();
            hasKey2.Id = 2;
            hasKey2.Name = "Tolik";
            hasKey2.LastName = "Bolik";
            dictionary.Add(hasKey);
            bool result = false;

            //Act
            result = dictionary.ContainsByTemplate(hasKey2);
            //Assert
            Assert.AreEqual(result, false, "The object is not contained !");
        }

        #endregion

        #region TestIndexerByHashCode

        [TestMethod]
        public void TestIndexerByHashCode()
        {
            IndexedDictionary<int,HasKey> dictionary = new IndexedDictionary<int,HasKey>();
            HasKey item = new HasKey() { Id = 1, Name = "John",LastName="Lolik" };
            dictionary.Add(item);
            HasKey testItem = dictionary[item.Id.GetHashCode()];
            Assert.AreSame(item, testItem, "The object retrieved by hashcode is wrong.");
        }

        #endregion

        #region TestGetItemsByNotTemplate

        [TestMethod]
        public void TestGetItemsByNotTemplate()
        {
            HasKey hasKey1 = new HasKey() { Id = 1, Name = "Jason", LastName = "Bolik" };
            HasKey hasKey2 = new HasKey() { Id = 2, Name = "Jason", LastName = "Lolik" };
            HasKey hasKey3 = new HasKey() { Id = 3, Name = "John", LastName = "Tolik" };
            HasKey hasKey4 = new HasKey() { Id = 4, Name = "John", LastName = "Alkogolik" };

            IndexedDictionary<int,HasKey> dictionary = new IndexedDictionary<int,HasKey>();
            dictionary.Add(hasKey1);
            dictionary.Add(hasKey2);
            dictionary.Add(hasKey3);
            dictionary.Add(hasKey4);
            HasKey[] result  = dictionary.GetByTemplate(new HasKey(){Name="John",LastName="Tolik"},LogicOperator.NOT,false);
            Assert.AreEqual(result.Length, 2, "Not query should return 2 items only!");
        }

        #endregion

        #region TestGet1000ItemsByTemplate

        [TestMethod]
        public void TestGet1000ItemsByTemplate()
        {
            //Arrange
            HasKey[] result = items1000.GetByTemplate(new HasKey() { Name = "Lolik" }, LogicOperator.AND,false);
            //Assert
            Assert.AreEqual(result.Length, 500, "Should return 500 items of Items1000 dictionary!");
        }

        #endregion

        #region TestForEach

        [TestMethod]
        public void TestForEach()
        {
            //Act
            int count = 0;
            foreach (var i in items1000)
            {
                count = i.Id;
            }
            //Assert
            Assert.AreEqual(count, items1000.Count - 1, "Expected count == to amount of items in dictionary -1 !");
        }

        #endregion

        #region TestForEachOnEmpty

        [TestMethod]
        public void TestForEachOnEmpty()
        {
            //Arrange
            IndexedDictionary<int,HasKey> dict = new IndexedDictionary<int,HasKey>();
            int count = 0;
            
            //Arrange

            foreach(var i in dict)
            {
                count = i.Id;
            }

            //Assert
            Assert.AreEqual(count, 0, "Expected count == 0 !");

        }

        #endregion

        #region TestIndexerByKey

        [TestMethod]
        public void TestIndexerByKey()
        {
            //Arrange
            HasKey template = new HasKey() { Id = 25 };

            //Act
            HasKey result = items1000[template];

            //Assert
            Assert.AreEqual(result.Id, template.Id, "The returned value with the wrong key");
        }

        #endregion

        #region RemoveByKeyHashCode

        [TestMethod]
        public void RemoveByKeyHashCode()
        {
            //Arrange
            HasKey template = new HasKey() { Id = 25 };

            //Act
            var result = items1000.RemoveByKey(template.Id.GetHashCode());

            //Assert
            Assert.AreEqual(result, true, "The desired object was not removed from dictionary");
        }

        #endregion

        #region RemoveByKeyTemplate

        [TestMethod]
        public void RemoveByKeyTemplate()
        {
            //Arrange
            HasKey template = new HasKey() { Id = 26 };

            //Act
            var result = items1000.RemoveByKey(template);

            //Assert
            Assert.AreEqual(result, true, "The desired object was not removed from dictionary");
        }

        #endregion
    }
}
