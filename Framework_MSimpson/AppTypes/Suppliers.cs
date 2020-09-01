using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Runtime.Serialization;
using Foundation;


namespace AppTypes
{
    /// <summary>
    /// Suppliers Class.  Inherits from ICustomCollection and IEnumerable.
    /// </summary>
    [DeveloperInfo("Mark S",Date="1/23/2007",Title="Developer")]
    [CustomDescription("Suppliers class")]
    [Serializable]
    public class Suppliers: ICustomCollection,IEnumerable 
    {
        private ArrayList suppliersCollection;
        const string dupeMessage = "Duplicate Supplier.";
        const string typeMessage = "Type mismatch.";
       
        #region Constructors
        /// <summary>
        /// Default Constructor for Suppliers
        /// </summary>
        public Suppliers()
        {
            suppliersCollection = new ArrayList();
        }


        #endregion

        /// <summary>
        /// Creates a supplier object using the default constructor
        /// with no arguments.
        /// </summary>
        /// <returns>Supplier object</returns>
        public static Supplier CreateSupplier()
        {
            return new Supplier();
        }

        /// <summary>
        /// Creates a supplier object using a non default constructor
        /// with all the arguments.
        /// </summary>
        /// <param name="iD">ID of Supplier (int)</param>
        /// <param name="companyName">CompanyName of supplier (string)</param>
        /// <param name="contactName">Contact Name of supplier (string)</param>
        /// <param name="contactTitle">Contact Title of supplier (string)</param>
        /// <param name="address">Address of supplier (string)</param>
        /// <param name="city">City of supplier (string)</param>
        /// <param name="region">Region of supplier (string)</param>
        /// <param name="postalCode">PostalCode of supplier (string)</param>
        /// <param name="country">Country of supplier (string)</param>
        /// <param name="phone">Phone of supplier (string)</param>
        /// <param name="fax">Fax of supplier (string)</param>
        /// <param name="homePage">Homepage of supplier (string)</param>
        /// <param name="type">Type of supplier (SupplierTypes)</param>
        /// <returns>Supplier Object.</returns>
        public static Supplier CreateSupplier(int iD, string companyName, string contactName,
            string contactTitle, string address, string city, string region,
            string postalCode, string country, string phone, string fax,
            string homePage, SupplierTypes type)
        {
            return new Supplier(iD, companyName, contactName, contactTitle, address, city, region,
                postalCode, country, phone, fax, homePage, type);
        }

        #region IEnumerable Members

        /// <summary>
        /// IEnumerable GetEnumerator method.  Inherited from IEnumerator.
        /// </summary>
        /// <returns>Ienumerator object</returns>
        public IEnumerator GetEnumerator()
        {
            return new SupplierEnumerator(this);
        }

        #endregion

        /// <summary>
        /// Name enumerator used to iterate through a list of suppliers in reverse.
        /// </summary>
        /// <returns>Ienumerator object</returns>
        public IEnumerator GetReverseEnumerator()
        {
            for (int i = suppliersCollection.Count - 1; i >= 0; i--)
                yield return this.suppliersCollection[i];
        }

        /// <summary>
        /// Goes through the collection, looking for all the Supplier objects
        /// that have a type that matches the type passed in.
        /// </summary>
        /// <param name="type">THe SupplierType enum value</param>
        /// <returns>Ienumerator object</returns>
        public IEnumerator GetTypeEnumerator(SupplierTypes type)
        {
            foreach (Supplier s in suppliersCollection)
            {
                if (s.Type == type)
                    yield return s;
            }
        }
        

        #region ICustomCollection Members
        /// <summary>
        /// Gets the number of suppliers in teh collection.  Inherited from ICustomCollection.
        /// </summary>
        public int Count
        {
            get { return suppliersCollection.Count; }
        }
        /// <summary>
        /// Returns the object at a specific index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>Supplier</returns>
        public object this[int index]
        {
            get
            {
                return suppliersCollection[index];
            }
            set
            {
                if (!(value is Supplier))
                    throw new ArgumentException(typeMessage);
                if (suppliersCollection.Contains(value))
                    throw new AppTypesException(dupeMessage);
                else
                    suppliersCollection[index] = value;
            }
        }
        /// <summary>
        /// Add method, adds an object to the array.
        /// </summary>
        /// <param name="value">Should be a Supplier object</param>
        /// <returns>returns the index if successful</returns>
        public int Add(object value)
        {
            if (!(value is Supplier))
                throw new ArgumentException(typeMessage);
            if (suppliersCollection.Contains(value))
            {
                throw new AppTypesException(dupeMessage);
            }
            else
                return suppliersCollection.Add(value);
        }
        /// <summary>
        /// Clears the collection list.
        /// </summary>
        public void Clear()
        {
            suppliersCollection.Clear();
        }
        /// <summary>
        /// Checks to see if the collectin list contains a certain object.
        /// </summary>
        /// <param name="value">Should be a Supplier object</param>
        /// <returns>Returns True if found</returns>
        public bool Contains(object value)
        {
            if (!(value is Supplier))
                throw new ArgumentException(typeMessage);
            else
                return suppliersCollection.Contains(value);
        }
        /// <summary>
        /// Scans to find the index of a certain object in the collection list.
        /// </summary>
        /// <param name="value">Should be a supplier object.</param>
        /// <returns>the index of the object</returns>
        public int IndexOf(object value)
        {
            if (!(value is Supplier))
                throw new ArgumentException(typeMessage);
            else
                return suppliersCollection.IndexOf(value);
        }
        /// <summary>
        /// Inserts an object at a given index in the collection list.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="value">Should be a supplier object.</param>
        public void Insert(int index, object value)
        {
            if (!(value is Supplier))
                throw new ArgumentException(typeMessage);
            if (suppliersCollection.Contains(value))
                throw new AppTypesException(dupeMessage);
            else
                suppliersCollection.Insert(index,value);
        }
        /// <summary>
        /// Removes an object from the collection list.
        /// </summary>
        /// <param name="value">Should be a supplier object.</param>
        public void Remove(object value)
        {
            if (!(value is Supplier))
                throw new ArgumentException(typeMessage);
            else
                suppliersCollection.Remove(value);
        }
        /// <summary>
        /// Removes an object at a specific object.
        /// </summary>
        /// <param name="index">The index</param>
        public void RemoveAt(int index)
        {
            suppliersCollection.RemoveAt(index);
        }
        /// <summary>
        /// Takes the entire collection and copies it to another array.
        /// </summary>
        /// <param name="objArray">The new array to copy the collection to</param>
        public void CopyTo(object[] objArray)
        {
            suppliersCollection.CopyTo(objArray);
        }
        /// <summary>
        /// Sorts the collection.
        /// </summary>
        public void Sort()
        {
            suppliersCollection.Sort();
        }
        /// <summary>
        /// Sorts the collection by a comparer object.
        /// </summary>
        /// <param name="comparer">Comparer object.</param>
        public void Sort(IComparer comparer)
        {
            suppliersCollection.Sort(comparer);
        }

        #endregion

        #region Private Class SupplierEnumerator

        private class SupplierEnumerator:IEnumerator
        {
            // Keeps track of the current position in the iteration.
            private int currentIndex = -1;

            // Holds a reference to the collection over which we are iterating.
            private Suppliers sups;

            /// <summary>
            /// INitialzes the reference to our collection
            /// </summary>
            /// <param name="sups">THe suppliers object</param>
            public SupplierEnumerator(Suppliers sups)
            {
                this.sups = sups;
            }

            #region IEnumerator Members

            /// <summary>
            /// Get the current index, inherits from IEnumerator
            /// </summary>
            public object Current
            {
                get
                {
                    if (currentIndex < 0 || currentIndex >= sups.suppliersCollection.Count)
                    {
                        throw new InvalidOperationException("Index was out of range");
                    }
                    else
                    {
                        return sups[currentIndex];
                    }
                }
            }

            /// <summary>
            /// Moves to the next index
            /// </summary>
            /// <returns>False if we are at the end of the collection, True if we have more objects in the collection.</returns>
            public bool MoveNext()
            {
                if (currentIndex == sups.suppliersCollection.Count)
                {
                    return false;
                }
                else if (currentIndex == sups.suppliersCollection.Count - 1)
                {
                    currentIndex++;
                    return false;
                }
                else
                {
                    currentIndex++;
                    return true;
                }
            }

            /// <summary>
            /// reset the current index back to -1
            /// </summary>
            public void Reset()
            {
                currentIndex = -1;
            }

            #endregion
        }
        #endregion
    }
}
