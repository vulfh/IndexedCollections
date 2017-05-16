using IndexedCollections.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexDictionaryUnitTests.SampleObjects
{
    public class ImmutableKeyCorrect
    {
        [Key]
        public int Id { get; private set; }
    }
}
