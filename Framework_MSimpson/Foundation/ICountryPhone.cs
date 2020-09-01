using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation
{
    /// <summary>
    /// ICountryPhone Interface
    /// All properties must be implemented by inheriting class
    /// </summary>
    public interface ICountryPhone
    {
        /// <summary>
        /// Interface for Country property
        /// Must be implemented by inheriting class
        /// </summary>
        string Country
        {
            get;
            set;
        }
        /// <summary>
        /// Interface for Phone property
        /// Must be implemented by inheriting class
        /// </summary>
        string Phone
        {
            get;
            set;
        }
        /// <summary>
        /// Interface for Fax propery
        /// Must be implemented by inheriting class
        /// </summary>
        string Fax
        {
            get;
            set;
        }
    }
}
