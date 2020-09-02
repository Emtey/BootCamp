using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Entities
{
    /// <summary>
    /// The Member Entity.
    /// </summary>
    public class Member
    {
        //fields:
        private Int16 _memberNo;
        private string _lastName;
        private string _firstName;
        private string _middleInitial;
        private string _city;
        private DateTime _expirationDate;
        private string _phoneNumber;
        private string _state;
        private string _street;
        private string _zipcode;

        #region Properties
        /// <summary>
        /// Gets memberNo property
        /// </summary>
        [XmlAttribute]
        [DefaultValue((short)0)]
        public short MemberID
        {
            get { return _memberNo; }
            set 
            {
                if (value > 0 && value <= 32767)
                    _memberNo = value;
                else
                    throw new ArgumentOutOfRangeException("Member ID must be betwen 1 and 32767.");
            }
        }
        /// <summary>
        /// gets/sets lastName property
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set 
            {
                if (value != null)
                {
                    if (value.Trim().Length <= 15 && value.Trim().Length > 0)
                        _lastName = value;
                    else
                        throw new ArgumentOutOfRangeException("Last Name must be between 1 and 15 characters long");
                }
                else
                    throw new ArgumentNullException("Last Name is required.");                
            }
        }
        
        /// <summary>
        /// gets/sets firstName property
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set 
            {
                if (value != null)
                {
                    if (value.Trim().Length <= 15 && value.Trim().Length > 0)
                        _firstName = value;
                    else
                        throw new ArgumentOutOfRangeException("First Name must be between 1 and 15 characters long");
                }
                else
                    throw new ArgumentNullException("First Name is required.");                
            } 
        }
        /// <summary>
        /// gets/sets middleInitial property
        /// </summary>
        public string MiddleInitial
        {
            get { return _middleInitial; }
            set
            {
                if (value != null)
                {
                    if (value.Trim().Length == 1)
                        _middleInitial = value;
                }
            } 
        }
        /// <summary>
        /// gets/set city property
        /// </summary>
        public string City
        {
            get { return _city; }
            set
            {
                if (value != null)
                {
                    if (value.Trim().Length <= 15 && value.Trim().Length > 0)
                        _city = value;
                    else
                        throw new ArgumentOutOfRangeException("City must be between 1 and 15 characters long");
                }
                else
                    throw new ArgumentNullException("City is required.");
            } 
        }
        /// <summary>
        ///  gets/sets ExpirationDate property
        /// </summary>
        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }
        /// <summary>
        /// gets/sets PhoneNumber property
        /// </summary>
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (value != null)
                {
                    if (value.Trim().Length <= 13 && value.Trim().Length > 0)
                        _phoneNumber = value;
                    //else
                    //    throw new ArgumentOutOfRangeException("Phone Number must be between 1 and 15 characters long");
                }
            } 
        }
        /// <summary>
        /// Gets/set State property
        /// </summary>
        public string State
        {
            get { return _state; }
            set
            {
                if (value != null)
                {
                    if (value.Trim().Length == 2)
                        _state = value;
                    else
                        throw new ArgumentOutOfRangeException("State must be 2 characters long");
                }
                else
                    throw new ArgumentNullException("State is required.");
            } 
        }

        /// <summary>
        /// get/set street property
        /// </summary>
        public string Street
        {
            get { return _street; }
            set
            {
                if (value != null)
                {
                    if (value.Trim().Length <= 15 && value.Trim().Length > 0)
                        _street = value;
                    else
                        throw new ArgumentOutOfRangeException("Street Address must be between 1 and 15 characters long");
                }
                else
                    throw new ArgumentNullException("Street Address is required.");
            } 
        }
        /// <summary>
        /// get/set zipcode property
        /// </summary>
        public string ZipCode
        {
            get { return _zipcode; }
            set
            {
                if (value != null)
                {
                    if (value.Trim().Length <= 10 && value.Trim().Length > 0)
                        _zipcode = value;
                    else
                        throw new ArgumentOutOfRangeException("Zip Code must be between 1 and 10 characters long");
                }
                else
                    throw new ArgumentNullException("Zip Code is required.");
            } 
        }
        #endregion

        /// <summary>
        /// The member constructor.
        /// </summary>
        public Member()
        {
        }
    }
}
