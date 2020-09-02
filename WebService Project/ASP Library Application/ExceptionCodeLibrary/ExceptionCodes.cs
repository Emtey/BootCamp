using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace ExceptionCodeLibrary
{
    /// <summary>
    /// Exception codes that are used with SoapExceptions.
    /// </summary>
    public class ExceptionCodes
    {
        /// <summary>
        /// define/declare the XmlQualifiedName of None to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName None =
            new XmlQualifiedName("None", "http://www.library.org/exceptionscodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of GenericException to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName GenericException =
            new XmlQualifiedName("GenericException", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of ItemNotFound to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName ItemNotFound =
            new XmlQualifiedName("ItemNotFound", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of NoSuchMember to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName NoSuchMember =
            new XmlQualifiedName("NoSuchMember", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of ItemNotOnLoan to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName ItemNotOnLoan =
            new XmlQualifiedName("ItemNotOnLoan", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of CheckInFailed to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName CheckInFailed =
            new XmlQualifiedName("CheckInFailed", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of ItemAlreadyOnLoan to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName ItemAlreadyOnLoan =
            new XmlQualifiedName("ItemAlreadyOnLoan", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of CheckOutFailed to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName CheckOutFailed =
            new XmlQualifiedName("CheckOutFailed", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of AddAdultFailed to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName AddAdultFailed =
            new XmlQualifiedName("AddAdultFailed", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of MissingAdultMember to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName MissingAdultMember =
            new XmlQualifiedName("MissingAdultMember", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of AddJuvenileFailed to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName AddJuvenileFailed =
            new XmlQualifiedName("AddJuvenileFailed", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of ItemNotLoanable to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName ItemNotLoanable =
            new XmlQualifiedName("ItemNotLoanable", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of RenewMemberFailed to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName RenewMemberFailed =
            new XmlQualifiedName("RenewMemberFailed", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of ConvertJuvenileFailed to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName ConvertJuvenileFailed =
            new XmlQualifiedName("ConvertJuvenileFailed", "http://www.library.org/exceptioncodes");

        /// <summary>
        /// define/declare the XmlQualifiedName of AddItemFailed to be used in SoapException processing.
        /// </summary>
        public static readonly XmlQualifiedName AddItemFailed =
            new XmlQualifiedName("AddItemFailed", "http://www.library.org/exceptioncodes");
    }
}
