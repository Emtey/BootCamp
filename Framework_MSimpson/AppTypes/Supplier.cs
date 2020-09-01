using System;
using System.Collections.Generic;
using System.Text;
using Foundation;
using System.Runtime.Serialization;
using System.Collections;
using DataAccess;

namespace AppTypes
{
    /// <summary>
    /// The Supplier class holds info about suppliers.
    /// Its a sealed class so no other classes can inherit from it.
    /// It inherits from Foundation.Contact, IComparable and ISerializable.
    /// </summary>
    [DeveloperInfo("Mark S",Date="1/23/2007",Title="Developer")]
    [CustomDescription("Supplier Class")]
    [Serializable]
    public sealed class Supplier : Contact, IComparable, ISerializable
    {
        private string _homepage;
        private SupplierTypes _type;

        #region Properties
        /// <summary>
        /// Property for Homepage.  Throws ArgumentOutOfRangeException if the 
        /// length is not between 5 and 50.  Homepage can be null.
        /// </summary>
        public string Homepage
        {
            get { return _homepage; }
            set
            {
                if (value.Trim().Length < 5)
                    throw new ArgumentOutOfRangeException("Homepage can not be less than 5 characters");
                else if (value.Trim().Length > 50)
                    throw new ArgumentOutOfRangeException("Homepage can not be more than 50 characters");
                else
                    _homepage = value;
            }
        }

        /// <summary>
        /// Property for SupplierTypes.
        /// </summary>
        public SupplierTypes Type
        {
            get { return _type; }
            set { _type = value; }
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor for Supplier class.
        /// </summary>
        public Supplier() { }

        /// <summary>
        /// Non Default Constructor for Supplier (inherits from Contact)
        /// </summary>
        /// <param name="iD">The Supplier ID (int)</param>
        /// <param name="companyName">The Company Name (string)</param>
        /// <param name="contactName">The Contact Name (string)</param>
        /// <param name="contactTitle">The Contact Title (string)</param>
        /// <param name="address">The Contacts Address (string)</param>
        /// <param name="city">The Contacts city (string)</param>
        /// <param name="region">The contacts region (string)</param>
        /// <param name="postalCode">The contacts postal code (string)</param>
        /// <param name="country">The contacts country (string)</param>
        /// <param name="phone">The contacts phone number (string) </param>
        /// <param name="fax">The contacts fax number(string)</param>
        /// <param name="homePage">The suppliers homepage (string)</param>
        /// <param name="type">The type of supplier (SupplierTypes)</param>
        public Supplier(int iD, string companyName, string contactName,
            string contactTitle, string address, string city, string region,
            string postalCode, string country, string phone, string fax,
            string homePage, SupplierTypes type)
            : base(iD, companyName, contactName, contactTitle, address,
            city, region, postalCode, country, phone, fax)
        {
            Homepage = homePage;
            Type = type;
            ID = iD;
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Address = address;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
            Fax = fax;
        }
        /// <summary>
        /// Gets the supplier information from a struct and loads it.
        /// </summary>
        /// <param name="supStruct">The supplier struct</param>
        public Supplier(DataAccess.SupplierStruct supStruct)
        {   
            this.ID = supStruct.ID;
            this.CompanyName = supStruct.CompanyName;
            this.ContactName = supStruct.ContactName ;
            this.ContactTitle = supStruct.ContactTitle;
            this.Address = supStruct.Address;
            this.City = supStruct.City;
            this.Region = supStruct.Region;
            this.PostalCode = supStruct.PostalCode;
            this.Country = supStruct.Country;
            this.Phone = supStruct.Phone;
            this.Fax = supStruct.Fax;
            this.Homepage = supStruct.HomePage;

            //get the string for SupplierType from supStruct.SUpplierType
            //convert it to lower case.
            string supString = supStruct.SupplierType.ToLower();
            //now get the string for the enum values, converting them to lower case.
            string product = Convert.ToString(SupplierTypes.Product).ToLower();
            string service = Convert.ToString(SupplierTypes.Service).ToLower();
            string supply = Convert.ToString(SupplierTypes.Supply).ToLower();

            if (supString == product)
                this.Type = SupplierTypes.Product;
            else if (supString == service)
                this.Type = SupplierTypes.Service;
            else if (supString == supply)
                this.Type = SupplierTypes.Supply;
            else
                throw new Exception("Unknown supplier type!");
        }

        /// <summary>
        /// Constructor for serializing the class
        /// </summary>
        /// <param name="info">Stores teh data necessary to serialize or deserialize</param>
        /// <param name="context">describes source and destination</param>
        internal Supplier(SerializationInfo info, StreamingContext context)
        {
            this.ID = info.GetInt32("ID");
            this.CompanyName = info.GetString("CompanyName");
            this.ContactName = info.GetString("ContactName");
            this.ContactTitle = info.GetString("ContactTitle");
            this.Address = info.GetString("Address");
            this.City = info.GetString("City");
            this.Region = info.GetString("Region");
            this.PostalCode = info.GetString("PostalCode");
            this.Country = info.GetString("Country");
            this.Phone = info.GetString("Phone");
            this.Fax = info.GetString("Fax");
            this.Homepage = info.GetString("Homepage");
            this.Type = (SupplierTypes)info.GetValue("SupplierType", typeof(SupplierTypes));
        }
        #endregion

        #region Methods

        /// <summary>
        /// Override of the ToString method so we can supply our own.
        /// </summary>
        /// <returns>Supplier ID, Type and Company Name</returns>
        public override string ToString()
        {
            //return base.ToString();
            return string.Format("Supplier ID: {0,10}\tType: {1} \tCompany Name: {2}", ID, Type.ToString(), CompanyName);
        }
        /// <summary>
        /// Override of Contact.ContactInfo
        /// </summary>
        /// <returns>Returns a StringBuilder object</returns>
        public override StringBuilder ContactInfo()
        {
            StringBuilder myString = new StringBuilder();
            myString.AppendFormat (ContactName + "\n");
            myString.AppendFormat (ContactTitle + "\n");
            myString.AppendFormat (Address + "\n");
            myString.AppendFormat (City + "\n");
            myString.AppendFormat (Region + "\t" + PostalCode);

            return myString;
        }
        /// <summary>
        /// Gets the ID of current object.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ID;
        }

        /// <summary>
        /// Takes two objects and compares them for equality
        /// </summary>
        /// <param name="obj">Object to use for comparison</param>
        /// <returns>True = compare, False = not compare</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)  //check to see if its null
                return false;
            if (!(obj is Supplier))
                return false;
            //set the obj to a supplier.
            Supplier sup = (Supplier)obj;

            //now lets do some comparison!
            if (sup.ID == this.ID) 
                return true;  //we have a match return true!
            return false;  //no match return false.
        }

        /// <summary>
        /// Overload of the == operator.
        /// </summary>
        /// <param name="o1">Supplier object</param>
        /// <param name="o2">Supplier Object</param>
        /// <returns>True = match, False = no match</returns>
        static public bool operator ==(Supplier o1, Supplier o2)
        {
            if ((object)o1 == null)
                return false;
            return o1.Equals(o2);
        }

        /// <summary>
        /// Overload of the != operator
        /// </summary>
        /// <param name="o1">SUpplier Object</param>
        /// <param name="o2">Supplier Object</param>
        /// <returns>True = no match, False = match</returns>
        static public bool operator !=(Supplier o1, Supplier o2)
        {
            if ((object)o1 == null)
                return false;
            return !o1.Equals(o2);
        }

        /// <summary>
        /// Returns the Values in the collection.
        /// </summary>
        /// <returns></returns>
        public IEnumerator PropertyAndValuesCollection()
        {      
            yield return ("Property:\tID:\t\t" + ID.ToString());
            yield return ("Property:\tCompany Name:\t" + CompanyName);
            yield return ("Property:\tContact Name:\t" + ContactName);
            yield return ("Property:\tContact Title:\t" + ContactTitle);
            yield return ("Property:\tAddress:\t" + Address);
            yield return ("Property:\tCity:\t\t" + City);
            yield return ("Property:\tRegion:\t\t" + Region);
            yield return ("Property:\tPostal Code:\t" + PostalCode);
            yield return ("Property:\tCountry:\t" + Country);
            yield return ("Property:\tPhone:\t\t" + Phone);
            yield return ("Property:\tFax:\t\t" + Fax);
            yield return ("Property:\tHomepage:\t" + Homepage);
            yield return ("Property:\tSupplier Type:\t" + Type);
        }

        /// <summary>
        /// Calls Class method in SortByCountryREgionCity to sort
        /// by Country, region and city.
        /// </summary>
        /// <returns>Icomparer object</returns>
        public static IComparer GetSortByCountryRegionCity()
        {   
            return new SortByCountryRegionCity();
        }

        /// <summary>
        /// Calls class method in SortByTypeCompanyName to sort
        /// by type, and companyName.
        /// </summary>
        /// <returns>IComparer object</returns>
        public static IComparer GetSortByTypeCompanyName()
        {   
            return new SortByTypeCompanyName();
        }
        /// <summary>
        /// Takes a supplier class, and converts into the SupplierStruct.
        /// </summary>
        /// <param name="sup">Supplier class</param>
        /// <returns></returns>
        public static explicit operator SupplierStruct(Supplier sup)
        {
            SupplierStruct ss = new SupplierStruct(sup.ID, sup.CompanyName, sup.ContactName, sup.ContactTitle, sup.Address, sup.City,
                sup.Region, sup.PostalCode, sup.Country, sup.Phone, sup.Fax, sup.Homepage, sup.Type.ToString().ToUpper());
            return ss;
        }

        #endregion

        #region IComparable Members

        /// <summary>
        /// Compares current object to another object
        /// </summary>
        /// <param name="obj">the object to be compared to</param>
        /// <returns>int</returns>
        public int CompareTo(object obj)
        {
            if (obj == null) 
                return 1;
            if (!(obj is Supplier)) 
                throw new ArgumentException("Object must be Supplier");
            Supplier otherObject = (Supplier)obj;
            return this.ID.CompareTo(otherObject.ID);
        }

        #endregion

        #region Class SortByCountryRegionCity
        /// <summary>
        /// SortByCountryRegionCity, inner class that sorts Suppliers.
        /// Inherits from IComparer.
        /// </summary>
        public class SortByCountryRegionCity : IComparer
        {
            #region IComparer Members
            /// <summary>
            /// Compares two objects and determines their order by Country, Region and City
            /// </summary>
            /// <param name="x">Supplier object</param>
            /// <param name="y">Supplier Object</param>
            /// <returns> positive number if x greater than y, 0 if x == y, negative number if x less than y</returns>
            public int Compare(object x, object y)
            {
                if (x == null && y == null) return 0;
                if (x != null && y != null && x is Supplier && y is Supplier)
                {
                    Supplier xAsSup = (Supplier)x;
                    Supplier yAsSup = (Supplier)y;
                    if (xAsSup.Country.CompareTo(yAsSup.Country) == 0)
                    {
                        if (xAsSup.Region.CompareTo(yAsSup.Region) == 0)
                        {
                            if (xAsSup.City.CompareTo(yAsSup.City) == 0)
                                return 0;
                            else
                                if (xAsSup.City.CompareTo(yAsSup.City) > 0)
                                    return 1;
                                else
                                    return -1;
                        }
                        else if (xAsSup.Region.CompareTo(yAsSup.Region) > 0)
                            return 1;
                        else
                            return -1;
                    }
                    else if (xAsSup.Country.CompareTo(yAsSup.Country) > 0)
                        return 1;
                    else
                        return -1;
                }
                else
                {
                    throw new ArgumentException("Not comparable");
                }
            }

            #endregion
        } 
        #endregion

        #region Class SortByTypeCompanyName
        /// <summary>
        /// SortByTypeCompanyName inner class that sorts suppliers by Type and Company Name.
        /// Inherits IComparer.
        /// </summary>
        public class SortByTypeCompanyName : IComparer
        {
            #region IComparer Members
            /// <summary>
            /// Compares two objects and determines their order.
            /// </summary>
            /// <param name="x">Supplier Object</param>
            /// <param name="y">Supplier Object</param>
            /// <returns>positive number if x greater than y, 0 if x == y, negative number if x less than y</returns>
            public int Compare(object x, object y)
            {
                if (x == null && y == null) return 0;
                if (x != null && y != null && x is Supplier && y is Supplier)
                {
                    Supplier xAsSup = (Supplier)x;
                    Supplier yAsSup = (Supplier)y;
                    if (xAsSup.Type.ToString().CompareTo(yAsSup.Type.ToString()) == 0)
                    {
                        if (xAsSup.CompanyName.CompareTo(yAsSup.CompanyName) == 0)
                            return 0;
                        else if (xAsSup.CompanyName.CompareTo(yAsSup.CompanyName) > 0)
                            return 1;
                        else
                            return -1;
                    }
                    else if (xAsSup.Type.ToString().CompareTo(yAsSup.Type.ToString()) > 0)
                        return 1;
                    else
                        return -1;
                }
                else
                {
                    throw new ArgumentException("Not comparable");
                }
            }

            #endregion
        } 
        #endregion

        #region ISerializable Members

        /// <summary>
        /// Gets teh object data from teh serialize stream.
        /// </summary>
        /// <param name="info">Stores all data needed to serialize and deserialize</param>
        /// <param name="context">describes source and destination of a given stream</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", this.ID);
            info.AddValue("CompanyName", this.CompanyName);
            info.AddValue("ContactName", this.ContactName);
            info.AddValue("ContactTitle", this.ContactTitle);
            info.AddValue("Address", this.Address);
            info.AddValue("City", this.City);
            info.AddValue("Region", this.Region);
            info.AddValue("PostalCode", this.PostalCode);
            info.AddValue("Country", this.Country);
            info.AddValue("Phone", this.Phone);
            info.AddValue("Fax", this.Fax);
            info.AddValue("Homepage", this.Homepage);
            info.AddValue("SupplierType", this.Type, typeof(SupplierTypes));
        }

        #endregion

    }
}
