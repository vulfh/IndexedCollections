using IndexedCollections.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexDictionaryUnitTests.SampleObjects
{
    public class ImmutableIndexCorrect
    {
        [Key]
        public int Id { get; private set; }

        [Index(unique:false,immutable:true)]
        public string Name { get; private set; }
    }
}
