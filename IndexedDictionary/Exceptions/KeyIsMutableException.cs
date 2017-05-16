using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.Exceptions
{
    public class KeyShouldBeImmutableException:Exception
    {
        public KeyShouldBeImmutableException()
            : base(Constants.KeyIsNotImmutable)
        {
        }
        public KeyShouldBeImmutableException(Exception ex)
            : base(Constants.KeyIsNotImmutable, ex)
        {
        }
    }
}
