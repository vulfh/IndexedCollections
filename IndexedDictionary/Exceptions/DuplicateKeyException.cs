using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.Exceptions
{
    public class DuplicateKeyException:Exception
    {
        public DuplicateKeyException()
            : base(Constants.DuplicateKeyExceptionMessage)
        {
        }
        public DuplicateKeyException(Exception ex)
            : base(Constants.DuplicateKeyExceptionMessage, ex)
        {
        }

    }
}
