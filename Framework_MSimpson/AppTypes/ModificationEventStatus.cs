using System;
using System.Collections.Generic;
using System.Text;

namespace AppTypes
{
    /// <summary>
    /// ModificationEventStatus enumeration
    /// </summary>
    [DeveloperInfo("Mark",Date="1/23/2007",Title="Developer")]
    [CustomDescription("ModificationEventStatus enumeration")]
    [Serializable]
    public enum ModificationEventStatus
    {
        /// <summary>
        /// Unsuccesful
        /// </summary>
        unsuccessful,
        /// <summary>
        /// successful
        /// </summary>
        successful
    }
}
