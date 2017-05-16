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

        public bool Immutable { get; private set; }

        #endregion

        #region Constructors

        public IndexAttribute(bool unique,bool immutable = false)
        {
            Unique = unique;
            Immutable = immutable;
        }

        public IndexAttribute() : this(false)
        {
            
        }
        #endregion
    }
}
