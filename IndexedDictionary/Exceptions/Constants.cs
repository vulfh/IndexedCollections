using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.Exceptions
{
    internal static class Constants
    {
        public  const string NoKeyPropertyExceptionMessage = "The class has no key property. Please consider mark any of the properties with KeyAttribute.";
        public  const string DuplicateKeyExceptionMessage = "The key already exists in dictionary.";
        public const string IndexedPropertyIsNull = "The indexed property {0} cannot be null.";
        public const string DuplicateUniqueIndexExceptionMessage = "The unique index already exist !";
    }
}
