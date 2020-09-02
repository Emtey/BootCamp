using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Entities
{
    /// <summary>
    /// LibrayException Class, extends Exception class.
    /// </summary>
    public class LibraryException:Exception, ISerializable
    {
        //fields
        private ErrorCode _libraryErrorCode;
        private Int16 _otherMemberID;

        #region Properties
        /// <summary>
        /// gets the LibraryErrorCode property
        /// </summary>
        public ErrorCode LibraryErrorCode
        {
            get { return _libraryErrorCode; }
        }

        /// <summary>
        /// Gets the OtherMemberID property
        /// </summary>
        public Int16 OtherMemberID
        {
            get { return _otherMemberID; }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the LibraryException class. 
        /// </summary>
        public LibraryException()
            : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of the LibraryException class with a specified error message. 
        /// </summary>
        /// <param name="message"></param>
        public LibraryException(string message):base(message)
        {
        }
        /// <summary>
        /// Initializes a new instance of the LibraryException class with serialized data. 
        /// </summary>
        /// <param name="info">info</param>
        /// <param name="context">context</param>
        public LibraryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            info.AddValue("MemID", OtherMemberID);
        }
        /// <summary>
        /// Initializes a new instance of the LibraryException class with a specified error message 
        /// and a reference to the inner exception that is the cause of this exception. 
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="innnerException">inner exception</param>
        public LibraryException(string message, Exception innnerException)
            : base(message, innnerException)
        {
        }
        /// <summary>
        /// Initializes a new instance of the LibraryException class with a specified ErrorCode. 
        /// </summary>
        /// <param name="errorCode">ErrorCode</param>
        public LibraryException(ErrorCode errorCode)
        {
            _libraryErrorCode = errorCode;
            _otherMemberID = 0;
        }
        /// <summary>
        /// Initializes a new instance of the LibraryException class with a specified ErrorCode and a specified error message. 
        /// </summary>
        /// <param name="errorCode">ErrorCode</param>
        /// <param name="message">message</param>
        public LibraryException(ErrorCode errorCode, string message):base(message)
        {
            _libraryErrorCode = errorCode;
            _otherMemberID = 0;
        }
        /// <summary>
        /// Initializes a new instance of the LibraryException class that indicates an item is already on loan to 
        /// another member when an attempt is made to check out the item. The ErrorCode will be set implicitly to 
        /// ErrorCode.ItemAlreadyOnLoan. 
        /// </summary>
        /// <param name="otherMemberID">Member Identifcation</param>
        /// <param name="message">message</param>
        public LibraryException(Int16 otherMemberID, string message):base(message)
        {
            _libraryErrorCode = ErrorCode.None;
            _otherMemberID = otherMemberID;
        }
        /// <summary>
        /// Initializes a new instance of the LibraryException class with a specified ErrorCode, a specified error message, 
        /// and a reference to the inner exception that is the cause of this exception 
        /// </summary>
        /// <param name="errorCode">ErrorCode</param>
        /// <param name="message">message</param>
        /// <param name="innerException">InnerException</param>
        public LibraryException(ErrorCode errorCode, string message, Exception innerException):
            base(message, innerException)
        {
            _libraryErrorCode = errorCode;
            _otherMemberID = 0;
        }
        #endregion

        #region ISerializable Members
        /// <summary>
        /// Used by the serialization mechanism to perform custom serialization of this exception object. 
        /// </summary>
        /// <param name="info">The SerializationInfo object. </param>
        /// <param name="context">The StreamingContext object.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            _otherMemberID = info.GetInt16("MemID");
            base.GetObjectData(info, context);
        }

        #endregion
    }
}
