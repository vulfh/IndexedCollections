using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.Exceptions
{
    public class IndexPropertyIsNullException:Exception
    {
        public IndexPropertyIsNullException(string propertyName)
            : base(string.Format(Constants.IndexedPropertyIsNull,propertyName))
        {
        }
        public IndexPropertyIsNullException(string propertyName,Exception ex)
            : base(string.Format(Constants.IndexedPropertyIsNull,propertyName), ex)
        {
        }
    }
}
