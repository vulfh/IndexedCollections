using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true)]
    public class IndexAttribute:Attribute
    {
        #region Properties

        public bool Unique { get; private set; }

        #endregion

        #region Constructors

        public IndexAttribute(bool unique)
        {
            Unique = unique;
        }

        public IndexAttribute() : this(false)
        {
            
        }
        #endregion
    }
}
