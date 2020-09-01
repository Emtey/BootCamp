using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace AppTypes
{
    /// <summary>
    /// Products Class inherits from IList
    /// </summary>
    [DeveloperInfo("Mark",Date="1/24/2007",Title="Developer")]
    [CustomDescription("Products Class")]
    [Serializable]
    public class Products: IList<Product>
    {
        /// <summary>
        /// Protect productsCollection List of products.
        /// </summary>
        protected List<Product> productsCollection;

        /// <summary>
        /// Default constructor for teh Products class
        /// </summary>
        public Products()
        {
            productsCollection = new List<Product>();
        }

        /// <summary>
        /// Event Declaration
        /// </summary>
        public event CollectionModifiedHandler CollectionModified; //declares event

        /// <summary>
        /// Method that takes in a ModificationEventArgs arguments,
        /// then raises the CollectionModified event.
        /// </summary>
        /// <param name="mea">ModificationEventArgs</param>
        public void OnCollectionModified(ModificationEventArgs mea)
        {
            //if there is no listener then ignore.
            if (CollectionModified != null)
                CollectionModified(this, mea); //raises event
            
        }

        #region IList<Product> Members
        /// <summary>
        /// GEts ths index of the product that was passed in.
        /// </summary>
        /// <param name="item">Product</param>
        /// <returns>int</returns>
        public int IndexOf(Product item)
        {  
            return productsCollection.IndexOf(item);
        }

        /// <summary>
        /// Inserts into the collection an certain item at a certain index
        /// </summary>
        /// <param name="index">index</param>
        /// <param name="item">Product</param>
        public void Insert(int index, Product item)
        {
            if (!productsCollection.Contains(item))
            {
                productsCollection.Insert(index, item);
                ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.successful, this);
                OnCollectionModified(e);
            }
            else
            {
                ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.unsuccessful, this);
                OnCollectionModified(e);
            }
        }
        /// <summary>
        /// Removes an item from the collection at specified index
        /// </summary>
        /// <param name="index">index</param>
        public void RemoveAt(int index)
        {
            int count = productsCollection.Count;
 
            productsCollection.RemoveAt(index);
            if (count == productsCollection.Count)
            {
                ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.successful, this);
                OnCollectionModified(e);
            }
            else
            {
                ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.unsuccessful, this);
                OnCollectionModified(e);
            }


        }

        /// <summary>
        /// Get/set the index property in the collection
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>Product</returns>
        public Product this[int index]
        {
            get
            {
                return productsCollection[index];
            }
            set
            {   
                if (!productsCollection.Contains(value))
                {
                    ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.successful, this);
                    OnCollectionModified(e);
                }
                else
                    productsCollection[index] = value;
            }
        }

        #endregion

        #region ICollection<Product> Members
        /// <summary>
        /// Add item to the collection list.
        /// </summary>
        /// <param name="item">Product</param>
        public void Add(Product item)
        {
            if (!productsCollection.Contains(item))
            {
                productsCollection.Add(item);
                ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.successful, this);
                OnCollectionModified(e);
            }
            else
            {   
                ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.unsuccessful, this);
                OnCollectionModified(e);
            }
        }
        /// <summary>
        /// Clear the list.
        /// </summary>
        public void Clear()
        {  
            productsCollection.Clear();
            ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.successful, this);
            OnCollectionModified(e);
        }

        /// <summary>
        /// Check to see if the collection list contains this item.
        /// </summary>
        /// <param name="item">Product</param>
        /// <returns>True or False</returns>
        public bool Contains(Product item)
        {
            return productsCollection.Contains(item);
        }

        /// <summary>
        /// Copies collection list to another list.
        /// </summary>
        /// <param name="array">The array name to copy the collection list to</param>
        /// <param name="arrayIndex">The index at which to start copying the collection.</param>
        public void CopyTo(Product[] array, int arrayIndex)
        {
            productsCollection.CopyTo(array,arrayIndex);
        }

        /// <summary>
        /// Gets the count of the productsCollection.
        /// </summary>
        public int Count
        {
            get { return productsCollection.Count; }
        }

        /// <summary>
        /// Readonly property, we are just setting it to always return false
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes an item from the productsCollection
        /// </summary>
        /// <param name="item">Product</param>
        /// <returns>true = success or false = failure</returns>
        public bool Remove(Product item)
        {
            if (productsCollection.Contains(item))
            {
                productsCollection.Remove(item);
                ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.successful, this);
                OnCollectionModified(e);
                return true;
            }
            else
            {
                ModificationEventArgs e = new ModificationEventArgs(ModificationEventStatus.unsuccessful, this);
                OnCollectionModified(e);
                return false;
            }
        }

        #endregion

        #region IEnumerable<Product> Members
        /// <summary>
        /// The GetEnumerator of the collection.  Basically just iterates through teh collection.
        /// </summary>
        /// <returns>IEnumerator index</returns>
        public IEnumerator<Product> GetEnumerator()
        {
            for (int i = 0; i < productsCollection.Count; i++)
                yield return this.productsCollection[i];
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Call the default constructor of Product to create new Product
        /// </summary>
        /// <returns></returns>
        public static Product CreateProduct()
        {
            return new Product();
        }

        /// <summary>
        /// Call the non default constructor of Product to create a new Product, when you know the values.
        /// </summary>
        /// <param name="iD">ID of the product (int)</param>
        /// <param name="productName">Name of the product (string)</param>
        /// <param name="supplierID">Supplier ID of the product (int)</param>
        /// <param name="categoryID">Category ID of the product (int)</param>
        /// <param name="quantityPerUnit">Quantity Per Unit of the product (string)</param>
        /// <param name="unitPrice">The unit price of the product (decimal)</param>
        /// <param name="unitsInStock">The units in stock of the product (int)</param>
        /// <param name="unitsOnOrder">The units on order of the product (int)</param>
        /// <param name="reOrderLevel">The reorder level of the product (int)</param>
        /// <returns></returns>
        public static Product CreateProduct(int iD, string productName, int supplierID, int categoryID,
            string quantityPerUnit, decimal unitPrice, int unitsInStock, int unitsOnOrder, int reOrderLevel)
        {
            return new Product(iD, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock,
                unitsOnOrder, reOrderLevel);
        }

        /// <summary>
        /// Call the sort method on teh productsCollection with no argument
        /// </summary>
        public void Sort()
        {
            productsCollection.Sort();
        }

        /// <summary>
        /// call the sort method on the productsCollection with an argument
        /// </summary>
        /// <param name="prod"></param>
        public void Sort(IComparer<Product> prod)
        {
            productsCollection.Sort(prod);
        }
    }
}
