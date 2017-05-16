using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexedCollections.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple=true)]
    public class KeyAttribute:Attribute
    {


        #region Properties

        public bool Immutable { get; set; }

        #endregion

        #region Constructors

        public KeyAttribute(bool Immutable = false)
        {
            this.Immutable = Immutable;
        }

        #endregion
    }
}
