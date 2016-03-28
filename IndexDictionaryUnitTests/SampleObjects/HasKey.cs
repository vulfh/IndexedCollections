using IndexedCollections.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexDictionaryUnitTests.SampleObjects
{
    public class HasKey
    {
        #region PROPERTIES

        [Key]
        public int Id { get; set; }

        [Index]
        public string Name { get; set; }

        [Index]
        public string LastName { get; set; }

        #endregion
    }
}
