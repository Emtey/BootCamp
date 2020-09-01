using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation
{
    /// <summary>
    /// IAddress Interface
    /// All properties must be implemented by inheriting class
    /// </summary>
    public interface IAddress
    {
        /// <summary>
        /// Interface for Address property
        /// Must be implemented by inheriting class
        /// </summary>
        string Address
        {
            get;
            set;
        }
        /// <summary>
        /// Interface for City property
        /// Must be implemented by inheriting class
        /// </summary>
        string City
        {
            get;
            set;
        }
        /// <summary>
        /// Interface for Region property
        /// Must be implemented by inheriting class
        /// </summary>
        string Region
        {
            get;
            set;
        }
        /// <summary>
        /// Interface for PostalCode property
        /// Must be implemented by inheriting class
        /// </summary>
        string PostalCode
        {
            get;
            set;
        }
    }
}
