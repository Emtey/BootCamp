using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation
{
    /// <summary>
    /// ICompanyContact Interface
    /// All properties must be implemented by inheriting class
    /// </summary>
    public interface ICompanyContact
    {
        /// <summary>
        /// Interface for ID property
        /// Must be implemented by inheriting class
        /// </summary>
        int ID
        {
            get;
            set;
        }
        /// <summary>
        /// Interface for CompanyName property
        /// Must be implemented by inheriting class
        /// </summary>
        string CompanyName
        {
            get;
            set;
        }
        /// <summary>
        /// Interface for ContactName property
        /// Must be implemented by inheriting class
        /// </summary>
        string ContactName
        {
            get;
            set;
        }
        /// <summary>
        /// Interface for ContactTitle property
        /// Must be implemented by inheriting class
        /// </summary>
        string ContactTitle
        {
            get;
            set;
        }

    }
}
