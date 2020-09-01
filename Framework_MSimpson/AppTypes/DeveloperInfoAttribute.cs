using System;
using System.Collections.Generic;
using System.Text;

namespace AppTypes
{
    /// <summary>
    /// Custom Attribute DeveloperInfoAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | 
          AttributeTargets.Interface | AttributeTargets.Enum, AllowMultiple=true)]
    public class DeveloperInfoAttribute:Attribute
    {
        private string _name; //required positional argument
        private string _title; //not required named argument
        private string _date;  //not required named argument

        /// <summary>
        /// DeveloperInfoAttribute Name Property.
        /// This is positional so its required and its read only.
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// DeveloperInfoAttribute Title property
        /// This is named, so its not required.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// DeveloperInfoAttribute Date property
        /// This is named so its not required.
        /// </summary>
        public string Date
        {
            //todo: Might want to format this date? 
            get { return _date; }
            set { _date = value; }
        }

        /// <summary>
        /// DeveloperInfoAttribute constructor.
        /// </summary>
        /// <param name="name">Required value since its only set through the constructor</param>
        public DeveloperInfoAttribute(string name)
        {
            _name = name;
        }

    }
}
