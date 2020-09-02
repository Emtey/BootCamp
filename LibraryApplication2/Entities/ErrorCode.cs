using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    /// <summary>
    /// ErrorCode Enumeration
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// None - no errors encountered.
        /// </summary>
        None,
        /// <summary>
        /// GenericException - generic exception encoutered.
        /// </summary>
        GenericException,
        /// <summary>
        /// ItemNotFound - item was not found in the db
        /// </summary>
        ItemNotFound,
        /// <summary>
        /// NoSuchMember - Member not found in teh DB
        /// </summary>
        NoSuchMember,
        /// <summary>
        /// ItemNotOnLoan - Item is not checked out.
        /// </summary>
        ItemNotOnLoan,
        /// <summary>
        /// CheckInFailed - Check in failed.
        /// </summary>
        CheckInFailed,
        /// <summary>
        /// ItemAlreadyOnLoan - Item is already on loan
        /// </summary>
        ItemAlreadyOnLoan,
        /// <summary>
        /// CheckOutFailed - Check out failed.
        /// </summary>
        CheckOutFailed,
        /// <summary>
        /// AddAdultFailed - Add adult failed
        /// </summary>
        AddAdultFailed,
        /// <summary>
        /// Missing Adult Member - Adult member field on juvenile record is missing.
        /// </summary>
        MissingAdultMember,
        /// <summary>
        /// AddJuvenileFailed = Add juvenile failed.
        /// </summary>
        AddJuvenileFailed,
        /// <summary>
        /// ItemNotLoanable - Item is not loanable.
        /// </summary>
        ItemNotLoanable
    }
}
