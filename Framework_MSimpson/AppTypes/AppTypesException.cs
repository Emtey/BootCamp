using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace AppTypes
{
    /// <summary>
    /// AppTypesException Class inherits from Exception
    /// </summary>
    [DeveloperInfo("Mark S", Date = "1/23/2007", Title = "Developer")]
    [CustomDescription("AppTypesException class")]
    [Serializable]
    public class AppTypesException : Exception
    {
        private string _lineNumber = "Exception at Line Number: ";

        /// <summary>
        /// property for LineNumber
        /// </summary>
        public string LineNumber
        {
            get { return _lineNumber; }
        }


        #region Constructors
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AppTypesException()
        {
        }
        /// <summary>
        /// Constructor that accepts a message
        /// </summary>
        /// <param name="message">A string message</param>
        public AppTypesException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// Constructor that accepts SErialization information.
        /// Gets the line number field from the info.
        /// </summary>
        /// <param name="info">Stores all the data needed to serialize or deserialize and object</param>
        /// <param name="context">describes the source and destination or serialized stream</param>
        public AppTypesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this._lineNumber = info.GetString("LineNumber");
        }
        /// <summary>
        /// Constructor that accepts a message and an Exception
        /// parses it through myMatch for the word ":line" extracts the
        /// characters (line number) and stores it in the myMatch
        /// field.  The we append it to the LineNumber, property and we
        /// are good to go.  Found it at site: http://www.regular-expressions.info/repeat.html
        /// </summary>
        /// <param name="message">A string message</param>
        /// <param name="ex">an exception</param>
        public AppTypesException(string message, Exception ex)
            : base(message, ex)
        {
            Match myMatch;
            myMatch = Regex.Match(ex.ToString(), @":line [0-9]+");
            _lineNumber = LineNumber + myMatch.ToString().Substring(6);
        }

        #endregion
    }
}

