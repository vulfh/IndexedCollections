using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.Exceptions
{
    public class NoKeyPropertyException:Exception
    {
        public NoKeyPropertyException()
            : base(Constants.NoKeyPropertyExceptionMessage)
        {
        }
        public NoKeyPropertyException(Exception ex)
            : base(Constants.NoKeyPropertyExceptionMessage, ex)
        {
        }
    }
}
