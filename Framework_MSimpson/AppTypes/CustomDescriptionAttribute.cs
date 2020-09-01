using System;
using System.Collections.Generic;
using System.Text;

namespace AppTypes
{
    /// <summary>
    /// Attribute CustomerDescriptionAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.All,AllowMultiple=false)]
    public class CustomDescriptionAttribute: Attribute
    {
        private string _description;
        /// <summary>
        /// Description property, this is read only.
        /// </summary>
        public string Description
        {
            get { return _description; }
        }

        /// <summary>
        /// Constructor for the CustomDescriptionAttribute attribute.
        /// </summary>
        /// <param name="description">The description</param>
        public CustomDescriptionAttribute(string description)
        {
            _description = description;
        }
    }
}
