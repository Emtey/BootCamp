using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    /// <summary>
    /// Item class.
    /// </summary>
    public class Item
    {
        //fields
        private Int32 _isbn;
        private Int16 _copy_num;
        private string _author;
        private string _title;
        private Int16 _member_no;
        private DateTime _out_date;
        private DateTime _due_date;
        private string _translation;
        private string _cover;
        private string _loanable;
        private string _synopsis;
        private string _on_loan;

        #region Properties
        /// <summary>
        /// get for ISBN Property
        /// </summary>
        public Int32 ISBN
        {
            get { return _isbn; }
            set { _isbn = value; }
        }
        /// <summary>
        /// get for CopyNumber property
        /// </summary>
        public Int16 CopyNumber
        {
            get { return _copy_num; }
            set { _copy_num = value; }
        }
        /// <summary>
        /// get for Author property
        /// </summary>
        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }
        /// <summary>
        /// get for Title property
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        /// <summary>
        /// get for MemberNumber property
        /// </summary>
        public Int16 MemberNumber
        {
            get { return _member_no; }
            set { _member_no = value; }
        }
        /// <summary>
        /// get for CheckOutDate property
        /// </summary>
        public DateTime CheckOutDate
        {
            get { return _out_date; }
            set { _out_date = value; }
        }
        /// <summary>
        /// get for DueDate property
        /// </summary>
        public DateTime DueDate
        {
            get { return _due_date; }
            set { _due_date = value; }
        }
        /// <summary>
        /// get for Translation property
        /// </summary>
        public string Translation
        {
            get { return _translation; }
            set { _translation = value; }
        }
        /// <summary>
        /// get for Cover property
        /// </summary>
        public string Cover
        {
            get { return _cover; }
            set { _cover = value; }
        }
        /// <summary>
        /// get for Loanable property
        /// </summary>
        public string Loanable
        {
            get { return _loanable; }
            set { _loanable = value; }
        }
        /// <summary>
        /// get for Synopsis property
        /// </summary>
        public string Synopsis
        {
            get { return _synopsis; }
            set { _synopsis = value; }
        }
        /// <summary>
        /// get for on_loan property
        /// </summary>
        public string On_loan
        {
            get { return _on_loan; }
            set { _on_loan = value; }
        }

        #endregion

        /// <summary>
        /// default constructor
        /// </summary>
        public Item()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isbn">ISBN number</param>
        /// <param name="copy_num">The copy number of the item.</param>
        /// <param name="title">The title of the item</param>
        /// <param name="author">The author of the item</param>
        /// <param name="member_no">Member Identification Number</param>
        /// <param name="out_date">Check Out Date</param>
        /// <param name="due_date">Date the item is due back</param>
        public Item(Int32 isbn, Int16 copy_num, string title, string author, Int16 member_no, DateTime out_date, DateTime due_date)
        {
            _isbn = isbn;
            _copy_num = copy_num;
            _title = title;
            _author = author;
            _member_no = member_no;
            _out_date = out_date;
            _due_date = due_date;
        }
    }
}
