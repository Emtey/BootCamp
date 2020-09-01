using System;
using System.Collections.Generic;
using System.Text;

namespace AppTypes
{
    /// <summary>
    /// GenericCollection Class.
    /// Type T represents the generic parm
    /// </summary>
    /// <typeparam name="T">A generic type</typeparam>
    [Serializable]
    [DeveloperInfo("Mark",Date="1/24/2007",Title="Developer")]
    [CustomDescription("GenericCollection Class")]
    public class GenericCollection<T>:IList<T>
    {
        /// <summary>
        /// myList field of List"T" type.
        /// </summary>
        protected List<T> items;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GenericCollection()
        {
            items = new List<T>();
        }

        #region IList<T> Members
        /// <summary>
        /// Determines the index of the item in the collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>int for the type T.</returns>
        public int IndexOf(T item)
        {
            return items.IndexOf(item);
        }

        /// <summary>
        /// Inserts a value into the collection at the index
        /// </summary>
        /// <param name="index">The index at which you want to insert</param>
        /// <param name="item">The item you wish to insert</param>
        public void Insert(int index, T item)
        {
            items.Insert(index, item);
        }

        /// <summary>
        /// Removes an instance at index
        /// </summary>
        /// <param name="index">the index at which you want to remove</param>
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        /// <summary>
        /// Gets and sets the index from the list.
        /// </summary>
        /// <param name="index">the setter index value.</param>
        /// <returns>The index for type T</returns>
        public T this[int index]
        {
            get
            {
                return items[index];
            }
            set
            {
                items[index] = value;
            }
        }

        #endregion

        #region ICollection<T> Members

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item">The collection type</param>
        public void Add(T item)
        {
            items.Add(item);
        }

        /// <summary>
        /// CLears the contents of the collection
        /// </summary>
        public void Clear()
        {
            items.Clear();
        }

        /// <summary>
        /// Scans the list to see if it contains the item.
        /// </summary>
        /// <param name="item">the type of collection</param>
        /// <returns>true = collection contains item, false = collection does not contain item.</returns>
        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        /// <summary>
        /// Copies from one array to another.
        /// </summary>
        /// <param name="array">The receiving array</param>
        /// <param name="arrayIndex">Index at which to start copy operation</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Count property returns number of items in the collection.
        /// </summary>
        public int Count
        {
            get { return items.Count; }
        }

        /// <summary>
        /// speicifies if the collection is ReadOnly.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes the item from the collection
        /// </summary>
        /// <param name="item">the type of collection</param>
        /// <returns>true = successful, false = failure</returns>
        public bool Remove(T item)
        {
            return items.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns>IEnumerator of type T</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < items.Count; i++)
                yield return this.items[i];
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
