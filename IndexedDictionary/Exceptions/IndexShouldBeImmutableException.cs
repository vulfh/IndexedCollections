using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.Exceptions
{
    public class IndexShouldBeImmutableException : Exception
    {
        public IndexShouldBeImmutableException()
            : base(Constants.IndexIsNotImmutable)
        {
        }
        public IndexShouldBeImmutableException(string message)
            :base(string.Concat(Constants.IndexIsNotImmutable, message))
        {
           
        }
        public IndexShouldBeImmutableException(Exception ex)
            : base(Constants.IndexIsNotImmutable, ex)
        {
        }
    }
}
