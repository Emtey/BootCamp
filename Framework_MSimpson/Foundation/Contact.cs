using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation
{
    /// <summary>
    /// Base Class for Contact information.
    /// Inherits from IContactInfo, which in turn inherits from
    /// IAddress, IcompanyContact, and ICountryPhone.
    /// This class must implement all properties and methods from the 
    /// afore mentioned interfaces.
    /// </summary>
    public abstract class Contact: IContactInfo
    {
        #region Fields
        /// <summary>
        /// Stores the ID data as an int.
        /// </summary>
        protected int _iD;
        /// <summary>
        /// Stores the CompanyName data as a string.
        /// </summary>
        protected string _companyName;
        /// <summary>
        /// Stores the ContactName data as a string.
        /// </summary>
        protected string _contactName;
        /// <summary>
        /// Stores the ContactTitle data as a string.
        /// </summary>
        protected string _contactTitle;
        /// <summary>
        /// Stores the Address data as a string.
        /// </summary>
        protected string _address;
        /// <summary>
        /// Stores the City data as a string.
        /// </summary>
        protected string _city;
        /// <summary>
        /// Stores the Region data as a string.
        /// </summary>
        protected string _region;
        /// <summary>
        /// Stores the PostalCode data as a string.
        /// </summary>
        protected string _postalCode;
        /// <summary>
        /// Stores the Country data as a string.
        /// </summary>
        protected string _country;
        /// <summary>
        /// Stores the Phone data as a string.
        /// </summary>
        protected string _phone;
        /// <summary>
        /// Stores the Fax data as a string.
        /// </summary>
        protected string _fax; 
        #endregion

        #region ICompanyContact Members
        /// <summary>
        /// Property for ID.  Inherited from ICompanyContact.
        /// Throws ArguementsOutOfRangeException when the ID is not 1 to 99,999.
        /// </summary>
        public virtual int ID
        {
            get
            {
                return _iD;
            }
            set
            {
                if (value < 1 || value > 99999)
                    throw new ArgumentOutOfRangeException("ID","ID must be between 1 and 99,999.");
                else
                    _iD = value;
            }
        }
        /// <summary>
        /// Property for CompanyName.  Inhertied from ICompanyContact.
        /// Throws ArguementOutOfRangeException through the throwException method
        /// when CompanyName is not 1 to 40 characters long.
        /// </summary>
        public virtual string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                if (value == null || value.Trim().Length < 1 || value.Trim().Length > 40)
                    throwException("Company Name", 40);
                else
                    _companyName = value;
            }
        }
        /// <summary>
        /// Property for ContactName.  Inhertied from ICompanyContact.
        /// Throws ArguementOutOfRangeException through the throwException method 
        /// when ContactName is not 1 to 30 characters long.
        /// </summary>
        public virtual string ContactName
        {
            get
            {
                return _contactName;
            }
            set
            {
                if (value == null || value.Trim().Length < 1 || value.Trim().Length > 30 )
                    throwException("Contact Name", 30);
                else
                    _contactName = value;
                    
            }
        }
        /// <summary>
        /// Property for ContactTitle.  Inhertied from ICompanyContact.
        /// Throws ArguementOutOfRangeException through the throwException method
        /// when ContactTitle is not 1 to 30 characters long.  
        /// </summary>
        public virtual string ContactTitle
        {
            get
            {
                return _contactTitle; 
            }
            set
            {
                if (value == null || value.Trim().Length < 1 || value.Trim().Length > 30)
                    throwException("Contact Title", 30);
                else
                    _contactTitle = value;
            }
        }

        #endregion

        #region IAddress Members
        /// <summary>
        /// Property for Address.  Inhertied from IAddress.
        /// Throws ArguementOutOfRangeException through the throwException method
        /// when Address is not 1 to 60 characters long. 
        /// </summary>
        public virtual string Address
        {
            get
            {
                return _address;
            }
            set
            {      
                if ( value == null || value.Trim().Length < 1 || value.Trim().Length > 60)
                    throwException("Address", 60);
                else
                    _address = value;
            }
        }
        /// <summary>
        /// Property for City.  Inhertied from IAddress.
        /// Throws ArguementOutOfRangeException through the throwException method 
        /// when City is not 1 to 15 characters long. 
        /// </summary>
        public virtual string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (value == null || value.Trim().Length < 1 || value.Trim().Length > 15) 
                    throwException("City", 15);
                else
                    _city = value;
            }
        }
        /// <summary>
        /// Property for Region.  Inhertied from IAddress.
        /// Throws ArguementOutOfRangeException through the throwException method 
        /// when Region is not 1 to 15 characters long. 
        /// </summary>
        public virtual string Region
        {
            get
            {
                return _region;
            }
            set
            {
                if (value == null || value.Trim().Length < 1 || value.Trim().Length > 15)
                    throwException("Region", 15);
                else
                    _region = value;
            }
        }
        /// <summary>
        /// Property for PostalCode. Inhertied from IAddress.
        /// Throws ArguementOutOfRangeException through the throwException method 
        /// when PostalCode is not 1 to 10 characters long.  
        /// </summary>
        public virtual string PostalCode
        {
            get
            {
                return _postalCode;
            }
            set
            {
                if ( value == null || value.Trim().Length < 1 || value.Trim().Length > 10)
                    throwException("Postal Code", 10);
                else
                    _postalCode = value;
            }
        }

        #endregion

        #region ICountryPhone Members
        /// <summary>
        /// Property for Country. Inhertied from ICountryPhone.
        /// Throws ArguementOutOfRangeException through the throwException method 
        /// when Country is not 1 to 15 characters long.  
        /// </summary>
        public virtual string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (value == null || value.Trim().Length < 1 && value.Trim().Length > 15)
                    throwException("Country", 15);
                else
                    _country = value;
            }
        }
        /// <summary>
        /// Property for Phone.  Inhertied from ICountryPhone.
        /// Throws ArguementOutOfRangeException through the throwException method 
        /// when Phone is not 1 to 15 characters long.  
        /// </summary>
        public virtual string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (value == null || value.Trim().Length < 1 && value.Trim().Length > 24) 
                    throwException("Phone", 24);
                else
                    _phone = value;
            }
        }
        /// <summary>
        /// Property for Fax.  Inhertied from ICountryPhone.
        /// Throws ArguementOutOfRangeException through the throwException method 
        /// when Fax is not 1 to 15 characters long.  
        /// </summary>
        public virtual string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                if (value == null || value.Trim().Length < 1 || value.Trim().Length > 24)
                    throwException("Fax", 24);
                else
                    _fax = value;
            }
        }

        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor for the Contact Class
        /// </summary>
        public Contact() { }

        /// <summary>
        /// Non-Default Constructor for the Contact Class.
        /// </summary>
        /// <param name="ID">ID of Contact(int)</param>
        /// <param name="CompanyName">CompanyName of contact(string)</param>
        /// <param name="ContactName">ContactName of contact(string)</param>
        /// <param name="ContactTitle">ContactTitle of contact(string)</param>
        /// <param name="Address">Address of contact(string)</param>
        /// <param name="City">City of contact (string)</param>
        /// <param name="Region">Region of contact (string)</param>
        /// <param name="PostalCode">PostalCode of contact (string)</param>
        /// <param name="Country">Country of contact (string)</param>
        /// <param name="Phone">Phone number of contact (string)</param>
        /// <param name="Fax">Fax number of contact (string)</param>
        public Contact(int ID, string CompanyName, string ContactName, string ContactTitle, string Address,
                        string City, string Region, string PostalCode, string Country, string Phone, string Fax)
        {
            this.ID = ID;
            this.CompanyName = CompanyName;
            this.ContactName = ContactName;
            this.ContactTitle = ContactTitle;
            this.Address = Address;
            this.City = City;
            this.Region = Region;
            this.PostalCode = PostalCode;
            this.Country = Country;
            this.Phone = Phone;
            this.Fax = Fax;
        } 
        #endregion

        /// <summary>
        /// Abstract method ContactInfo.  Another class can provide the 
        /// implementation for this if it wants to.
        /// </summary>
        /// <returns>Returns StringBuilder object</returns>
        public abstract StringBuilder ContactInfo();

        //just using this to format an out of range exception.
        //made it private because nothing else needs access to it.
        private void throwException(string name, int max)
        {
            string exceptionString;
            exceptionString = string.Format("{0} must be no more than {1} characters and is required.", name, max);
            throw new ArgumentOutOfRangeException(name,exceptionString);
        }

    }
}
