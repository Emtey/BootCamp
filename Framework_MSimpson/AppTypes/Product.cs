using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
using System.Runtime.Serialization;
using System.Collections;

namespace AppTypes
{
    /// <summary>
    /// Product Class, inherits IComparable
    /// </summary>
    [DeveloperInfo("Mark",Date="1/24/2007",Title="Developer")]
    [CustomDescription("Product Class")]
    [Serializable]
    public class Product:IComparable<Product>
    {
        #region Fields
        /// <summary>
        /// id field of the product.
        /// </summary>
        protected int _id;
        private string _productName;
        private int _supplierID;
        private int _categoryID;
        private string _quantityPerUnit;
        private decimal _unitPrice;
        private int _unitsInStock;
        private int _unitsOnOrder;
        private int _reOrderLevel; 
        #endregion

        #region Properties
        /// <summary>
        /// ID field properties.  Throws ArgumentOutOFRangeException if the value is not between 1 to 9,999.
        /// </summary>
        public virtual int ID
        {
            get { return _id; }
            set
            {
                if (value < 1 && value > 9999)
                    throw new ArgumentOutOfRangeException("ID must be between 1 and 9,999.");
                else
                    _id = value;
            }
        }
        /// <summary>
        /// Product name field properties.  Calls throwsException if the product name is not 1 to 40 chars in length.
        /// </summary>
        public virtual string ProductName
        {
            get { return _productName; }
            set
            {
                if (value == null || value.Trim().Length < 1 && value.Trim().Length > 40)
                    throwException("Product Name", 40);
                else
                    _productName = value;
            }
        }
        /// <summary>
        /// SupplierID field properties.  Throws ArgumentOutOfRangeException if supplier id is not between 1 to 99,999
        /// </summary>
        public virtual int SupplierID
        {
            get { return _supplierID; }
            set
            {
                if (Convert.ToInt32(value) < 1 && Convert.ToInt32(value) > 99999)
                    throw new ArgumentOutOfRangeException("Supplier ID must be between 1 and 99,999");
                else
                    _supplierID = value;
            }
        }
        /// <summary>
        /// CategoryID field properties
        /// </summary>
        public virtual int CategoryID
        {
            get {return _categoryID;}
            set {_categoryID = value;}
        }
        /// <summary>
        /// QuantityPerUnit field properties.  Calls throwException if length is not between 1 and 20.
        /// </summary>
        public virtual string QuantityPerUnit
        {
            get { return _quantityPerUnit; }
            set
            {
                if (value == null || value.Trim().Length < 1 && value.Trim().Length > 20)
                    throwException("Product Name", 40);
                else
                    _quantityPerUnit = value;
            }
        }
        /// <summary>
        /// UnitPrice field properties
        /// </summary>
        public virtual decimal UnitPrice
        {
            get { return _unitPrice; }
            set { _unitPrice = value; }
        }
        /// <summary>
        /// UnitsInStock field properties
        /// </summary>
        public virtual int UnitsInStock
        {
            get { return _unitsInStock; }
            set { _unitsInStock = value; }
        }
        /// <summary>
        /// UnitsOnOrder field properties
        /// </summary>
        public virtual int UnitsOnOrder
        {
            get { return _unitsOnOrder; }
            set {_unitsOnOrder = value;}
        }
        /// <summary>
        /// ReOrderLevel field properties
        /// </summary>
        public virtual int ReOrderLevel
        {
            get { return _reOrderLevel; }
            set { _reOrderLevel = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default Constructor for Product Class
        /// </summary>
        public Product()
        {
        }
        /// <summary>
        /// Non-Default constructor for Product class.
        /// </summary>
        /// <param name="iD">Product ID (int)</param>
        /// <param name="productName">ProductName (string)</param>
        /// <param name="supplierID">Supplier ID (int)</param>
        /// <param name="categoryID">Category ID (int)</param>
        /// <param name="quantityPerUnit">Quantity per unit (string)</param>
        /// <param name="unitPrice">Unit Price (decimal)</param>
        /// <param name="unitsInStock">Units in STock (int)</param>
        /// <param name="unitsOnOrder">Units on order (int)</param>
        /// <param name="reOrderLevel">Reorder level (int)</param>
        public Product(int iD, string productName, int supplierID, int categoryID, string quantityPerUnit, decimal unitPrice,
            int unitsInStock, int unitsOnOrder, int reOrderLevel)
        {
            ID = iD;
            ProductName = productName;
            SupplierID = supplierID;
            CategoryID = categoryID;
            QuantityPerUnit = quantityPerUnit;
            UnitPrice = unitPrice;
            UnitsInStock = unitsInStock;
            UnitsOnOrder = unitsOnOrder;
            ReOrderLevel = reOrderLevel;
        }
        /// <summary>
        /// Product constructor that accepts a Product Struct.
        /// </summary>
        /// <param name="prodStruct">Product Struct</param>
        public Product(DataAccess.ProductStruct prodStruct)
        {
            ID = prodStruct.ID;
            ProductName = prodStruct.ProductName;
            SupplierID = Convert.ToInt32(prodStruct.SupplierId);
            CategoryID = Convert.ToInt32(prodStruct.CategoryID);
            QuantityPerUnit = prodStruct.QuantityPerUnit;
            UnitPrice = prodStruct.UnitPrice;
            UnitsInStock = prodStruct.UnitsInStock;
            UnitsOnOrder = prodStruct.UnitsOnOrder;
            ReOrderLevel = prodStruct.ReOrderLevel;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        /// <returns>Supplier ID, Type and Company Name</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Product Name: ");
            sb.Append(ProductName);
            sb.Append("\tCategory ID: ");
            sb.Append(CategoryID.ToString());
            sb.Append("\tSupplier ID ");
            sb.Append(SupplierID.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// Returns values in the collections.
        /// </summary>
        /// <returns>IEnumerator object</returns>
        public IEnumerator PropertyAndValuesCollection()
        {
            yield return ("Product ID: " + ID.ToString());
            yield return ("Product Name: " + ProductName);
            yield return ("Supplier ID: " + SupplierID.ToString());
            yield return ("Category ID: " + CategoryID.ToString());
            yield return ("Quantity Per Unit: " + QuantityPerUnit);
            yield return ("Unit Price: " + UnitPrice.ToString("c"));
            yield return ("Units in Stock: " + UnitsInStock.ToString());
            yield return ("Units On Order: " + UnitsOnOrder.ToString());
            yield return ("ReOrder Level: " + ReOrderLevel.ToString());
        }

        /// <summary>
        /// Takes two objects and compares them for equality
        /// </summary>
        /// <param name="obj">Object to use for comparison</param>
        /// <returns>True = compare, False = not compare</returns>
        public override bool Equals(object obj)
        {
            //check to see if we have a null object
            if (obj == null) 
                return false;
            //obj isnt null, lets see if its a product
            if (!(obj is Product)) 
                return false;

            if (((Product)obj).ID == this.ID)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Overload of the == operator.
        /// </summary>
        /// <param name="o1">Product object</param>
        /// <param name="o2">Product Object</param>
        /// <returns>True = match, False = no match</returns>
        static public bool operator ==(Product o1, Product o2)
        {
            if ((object)o1 == null)
                return false;
            return o1.Equals(o2);
        }

        /// <summary>
        /// Overload of the != operator
        /// </summary>
        /// <param name="o1">Product Object</param>
        /// <param name="o2">Product Object</param>
        /// <returns>True = no match, False = match</returns>
        static public bool operator !=(Product o1, Product o2)
        {
            if ((object)o1 == null)
                return false;
            return !o1.Equals(o2);
        }

        /// <summary>
        /// Gets the ID of current object.
        /// </summary>
        /// <returns>the product ID</returns>
        public override int GetHashCode()
        {
            return ID;
        }

        /// <summary>
        /// Check to see if Object1 is greater than object2
        /// we only check ID here.
        /// </summary>
        /// <param name="o1">Product Object</param>
        /// <param name="o2">Product Object</param>
        /// <returns>True if 1st object is greater than second, otherwise its false.</returns>
        public static bool operator >(Product o1, Product o2)
        {
            if (o1 != null && o2 != null)
                return o1.ID > o2.ID;
            else
                return false;
        }

        /// <summary>
        /// Check to see if Object1 is less than object2
        /// we only check ID here.
        /// </summary>
        /// <param name="o1">Product Object</param>
        /// <param name="o2">Product Object</param>
        /// <returns>True if 1st object is less than second, otherwise its false.</returns>
        public static bool operator <(Product o1, Product o2)
        {
            if (o1 != null && o2 != null)
                return o1.ID < o2.ID;
            else
                return false;
        }

        /// <summary>
        /// Check to see if Object1 is less than or equal to object2
        /// we only check ID here.
        /// </summary>
        /// <param name="o1">Product Object</param>
        /// <param name="o2">Product Object</param>
        /// <returns>True if 1st object is less than or equal to second, otherwise its false.</returns>
        public static bool operator <=(Product o1, Product o2)
        {
            if (o1 != null && o2 != null)
                return o1.ID <= o2.ID;
            else
                return false;
        }

        /// <summary>
        /// Check to see if Object1 is greater than or equal to object2
        /// we only check ID here.
        /// </summary>
        /// <param name="o1">Product Object</param>
        /// <param name="o2">Product Object</param>
        /// <returns>True if 1st object is greater than or equal to second, otherwise its false.</returns>
        public static bool operator >=(Product o1, Product o2)
        {
            if (o1 != null && o2 != null)
                return o1.ID >= o2.ID;
            else
                return false;
        }

        //just using this to format an out of range exception.
        private void throwException(string name, int max)
        {
            string exceptionString;
            exceptionString = string.Format("{0} must be no more than {1} characters and is required.", name, max);
            throw new ArgumentOutOfRangeException(exceptionString);
        }

        /// <summary>
        /// Calls Class method in SortByCategoryID to sort
        /// by category ID.
        /// </summary>
        /// <returns>Icomparer object</returns>
        public static IComparer<Product> GetSortByCategoryID()
        {
            return new SortByCategoryID();
        }

        /// <summary>
        /// Calls Class method in SortBySupplierID to sort
        /// by Supplier ID.
        /// </summary>
        /// <returns>Icomparer object</returns>
        public static IComparer<Product> GetSortBySupplierID()
        {
            return new SortBySupplierID();
        }

        /// <summary>
        /// Takes a Product class, and converts into the ProductStruct.
        /// </summary>
        /// <param name="p">Product class</param>
        /// <returns>Product Struct</returns>
        public static explicit operator ProductStruct(Product p)
        {
            ProductStruct ps = new ProductStruct(p.ID, p.ProductName,p.SupplierID.ToString(),p.CategoryID.ToString(),p.QuantityPerUnit,p.UnitPrice,p.UnitsInStock,
                p.UnitsOnOrder,p.ReOrderLevel);
            return ps;
        }
        
        #endregion

        #region IComparable<Product> Members
        /// <summary>
        /// Implementation of CompareTo method, inherited from IComparable.
        /// Compares one object to another.
        /// </summary>
        /// <param name="other">A Product object</param>
        /// <returns>1 if this is greater than Product, 0 if they are equal and -1 if this less than Product</returns>
        public int CompareTo(Product other)
        {
            if (other == null)
                return 1;
            if (!(other is Product))
                throw new ArgumentException("Object must be Product");
            Product otherObject = (Product)other;
            return this.ID.CompareTo(otherObject.ID);
        }

        #endregion

        #region Class SortBySupplierID
        private class SortBySupplierID : IComparer<Product>
        {
            #region IComparer<Product> Members
            /// <summary>
            /// Compare x to y, sort by SUpplier ID.
            /// </summary>
            /// <param name="x">Product</param>
            /// <param name="y">Product</param>
            /// <returns>integer</returns>
            public int Compare(Product x, Product y)
            {
                if (x == null && y == null)
                    return 0;
                if (x != null && y != null && x is Product && y is Product)
                {
                    Product xAsP = (Product)x;
                    Product yAsP = (Product)y;
                    if (xAsP.SupplierID.CompareTo(yAsP.SupplierID) == 0)
                        return 0;
                    else if (xAsP.SupplierID.CompareTo(yAsP.SupplierID) > 0)
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

        #region Class SortByCategoryID
        private class SortByCategoryID : IComparer<Product>
        {
            #region IComparer<Product> Members

            public int Compare(Product x, Product y)
            {
                if (x == null & y == null)
                    return 0;
                if (x != null && y != null && x is Product && y is Product)
                {
                    Product xAsP = (Product)x;
                    Product yAsP = (Product)y;
                    if (xAsP.CategoryID.CompareTo(yAsP.CategoryID) == 0)
                        return 0;
                    else if (xAsP.CategoryID.CompareTo(yAsP.CategoryID) > 0)
                        return 1;
                    else
                        return -1;
                }
                else
                    throw new ArgumentException("Not Comparable");
            }

            #endregion
        }
        #endregion
    }
}
