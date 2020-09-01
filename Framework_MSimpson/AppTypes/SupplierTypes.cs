using System;
using System.Collections.Generic;
using System.Text;

namespace AppTypes
{
    /// <summary>
    /// SupplierTypes enumeration.
    /// </summary>
    [DeveloperInfo("Mark",Date="1/22/2007",Title="Developer")]
    [CustomDescription("Supplier Types enumeration")]
    public enum SupplierTypes
    {
        /// <summary>
        /// Product enumeration
        /// </summary>
            Product,
        /// <summary>
        /// Service enumeration
        /// </summary>
            Service,
        /// <summary>
        /// Supply enumeration
        /// </summary>
            Supply
    }
}
