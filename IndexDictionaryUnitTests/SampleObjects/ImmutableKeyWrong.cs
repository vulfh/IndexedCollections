using IndexedCollections.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexDictionaryUnitTests.SampleObjects
{
    public class ImmutableKeyWrong
    {
        [Key(true)]
        public int Id { get; set; }
    }
}
