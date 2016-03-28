using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.Exceptions
{
   
    public class DuplicateUniqueIndexException : Exception
    {
        public DuplicateUniqueIndexException()
            : base(Constants.DuplicateUniqueIndexExceptionMessage)
        {
        }
        public DuplicateUniqueIndexException(Exception ex)
            : base(Constants.DuplicateUniqueIndexExceptionMessage, ex)
        {
        }
    }
}
