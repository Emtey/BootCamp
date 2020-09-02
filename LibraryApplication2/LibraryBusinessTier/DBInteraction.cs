using System;
using System.Collections.Generic;
using System.Text;
using LibraryDataAccess;
using Entities;

namespace LibraryBusinessTier
{
    /// <summary>
    /// DataBase interaction Class and methods.
    /// </summary>
    public class DBInteraction 
    {        
       
        /// <summary>
        /// NewItemAdded event
        /// </summary>
        public static event ItemsEventDelegate NewItemAdded;

        
        /// <summary>
        /// The method that will be raised for the NewItemAdded event
        /// </summary>
        /// <param name="args">ItemsEventsArgs</param>
        protected virtual void OnNewItemAdded(ItemsEventsArgs args)
        {
            if (NewItemAdded != null) NewItemAdded(args);
        }


        DataAccess myAccess = new DataAccess();

        /// <summary>
        /// Add a Juvenile member to the DB.
        /// </summary>
        /// <param name="member"></param>
        public void AddMember(JuvenileMember member)
        {
            try
            {
                myAccess.AddMember(member);
            }
            catch (LibraryException)
            {
                throw new LibraryException(ErrorCode.AddJuvenileFailed);
            }
        }

        /// <summary>
        /// Add an adult member to the DB
        /// </summary>
        /// <param name="member">Adult Member</param>
        public void AddMember(AdultMember member)
        {
            try
            {
                myAccess.AddMember(member);
            }
            catch (LibraryException ex)
            {
                if (ex.LibraryErrorCode == ErrorCode.AddAdultFailed)
                    throw new LibraryException(ErrorCode.AddAdultFailed);
                if (ex.LibraryErrorCode == ErrorCode.GenericException)
                    throw new LibraryException(ErrorCode.GenericException);
            }
        }

        /// <summary>
        /// Checks an item into the database.
        /// </summary>
        /// <param name="isbn">ISBN of the book</param>
        /// <param name="copyNumber">The copy number of the book.</param>
        public void CheckInItem(int isbn, short copyNumber)
        {
            try
            {
                Item temp = myAccess.GetItem(isbn, copyNumber);
                myAccess.CheckInItem(isbn, copyNumber);
                //raise the event.
                OnNewItemAdded(new ItemsEventsArgs(temp.MemberNumber));
            }
            catch (LibraryException e)
            {
                if (e.LibraryErrorCode == ErrorCode.ItemNotOnLoan)
                {
                    throw new LibraryException(ErrorCode.ItemNotOnLoan);
                }
                else
                    throw new LibraryException(ErrorCode.CheckInFailed);

            }
        }

        /// <summary>
        /// Checks out an item to a specific member.
        /// </summary>
        /// <param name="memberNumber">The Member ID number.</param>
        /// <param name="isbn">The ISBN number</param>
        /// <param name="copyNumber">The copy number of the book</param>
        public void CheckOutItem(short memberNumber, int isbn, short copyNumber)
        {
            try
            {
                myAccess.CheckOutItem(memberNumber, isbn, copyNumber);
                //raise the event.
                OnNewItemAdded(new ItemsEventsArgs(memberNumber));
            }
            catch (LibraryException ex)
            {
                if (ex.LibraryErrorCode == ErrorCode.CheckOutFailed) 
                    throw new LibraryException(ErrorCode.CheckOutFailed);
                if (ex.LibraryErrorCode == ErrorCode.ItemAlreadyOnLoan)
                    throw new LibraryException(ErrorCode.ItemAlreadyOnLoan);
                if (ex.LibraryErrorCode == ErrorCode.GenericException)
                    throw new LibraryException(ErrorCode.GenericException);
                if (ex.LibraryErrorCode == ErrorCode.ItemNotFound)
                    throw new LibraryException(ErrorCode.ItemNotFound);
                if (ex.LibraryErrorCode == ErrorCode.ItemNotLoanable)
                    throw new LibraryException(ErrorCode.ItemNotLoanable);
            }
        }

        /// <summary>
        /// Gets and item from the DB.
        /// </summary>
        /// <param name="isbn">The ISBN number</param>
        /// <param name="copyNumber">The copy number of the book</param>
        /// <returns>Item</returns>
        public Item GetItem(int isbn, short copyNumber)
        {
            try
            {
                return myAccess.GetItem(isbn, copyNumber); 
            }
            catch (LibraryException )
            {                
                throw new LibraryException(ErrorCode.ItemNotFound);
            }
        }

        /// <summary>
        /// Gets the items (books) that are checked out to a member.
        /// </summary>
        /// <param name="memberNumber">The Member's ID</param>
        /// <returns>Listing of Items</returns>
        public ItemsDataSet GetItems(short memberNumber)
        {
            ItemsDataSet myDataSet = new ItemsDataSet();

            try
            {
                myDataSet = myAccess.GetItems(memberNumber);
                return myDataSet;
            }
            catch (LibraryException)
            {
                throw new LibraryException(ErrorCode.None);
            }
            
        }

        /// <summary>
        /// Gets a member from the DB
        /// </summary>
        /// <param name="memberNumber">The members assigned number</param>
        /// <returns>Member instance</returns>
        public Member GetMember(short memberNumber)
        {
            Member myMember = new Member();
            //retrieve valid member;
            try
            {
                myMember = myAccess.GetMember(memberNumber);
                return myMember;
            }
            catch (LibraryException)
            {
                //post back if member is not on DB.
                throw new LibraryException(ErrorCode.NoSuchMember);
            }
        }
    }
}
