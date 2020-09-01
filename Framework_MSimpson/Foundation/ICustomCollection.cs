using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation
{
    /// <summary>
    /// ICustomCollection interface.  
    /// All properties and methods must be implemented by the inheriting class
    /// </summary>
    public interface ICustomCollection
    {
        /// <summary>
        /// Interface for the Count property of a Collection.
        /// Returns the amount of objects in a collection.
        /// </summary>
        int Count
        {
            get;
        }
        /// <summary>
        /// Interface for the Indexer of the current object.
        /// Must be implemented by inheriting class.
        /// </summary>
        /// <param name="index">The index of the current object.</param>
        /// <returns>Index</returns>
        object this[int index]
        {
            get;
            set;
        }

        //Methods
        #region Methods
        /// <summary>
        /// Add Method - Adds objects together returning their sum.
        /// Must be implemented by inheriting class.
        /// </summary>
        /// <param name="value">value to add.</param>
        /// <returns>Integer which is the sum.</returns>
        int Add(object value);
        /// <summary>
        /// Clear Method - Clears a collection.
        /// Must be implemented bu inheriting class.
        /// </summary>
        void Clear();
        /// <summary>
        /// Contains Method - Checks to see if a collection contains the object
        /// that was passed to it.  
        /// Must be implemented by inheriting class.
        /// </summary>
        /// <param name="value">Object that is being searched for in a collection</param>
        /// <returns>True if object is present, False if not present</returns>
        bool Contains(object value);
        /// <summary>
        /// IndexOf Method - Gets the index of an object in a collection.
        /// Must be implemented by inheriting class.
        /// </summary>
        /// <param name="value">Object in a collection that you want the index for.</param>
        /// <returns>Integer that represents the index location.</returns>
        int IndexOf(object value);
        /// <summary>
        /// Insert method - Inserts an object into a collection at a specified location.
        /// Must be implemented by inheriting class.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        void Insert(int index, object value);
        /// <summary>
        /// remove method - Removes an object from a collection.
        /// Must be implemented by inheriting class.
        /// </summary>
        /// <param name="value">Object that is to be removed from the collection</param>
        void Remove(object value);
        /// <summary>
        /// RemoveAt method - Removes an object at a given index from the collection.
        /// Must be implemented by inheriting class.
        /// </summary>
        /// <param name="index">The location of the object to remove in the collection</param>
        void RemoveAt(int index);
        /// <summary>
        /// CopyTo method - Copies the entire collection to another specified collection.
        /// Must be implemented by inheriting class.
        /// </summary>
        /// <param name="objArray">Name of the collection (object array) to which the collection is to be copied</param>
        void CopyTo(object[] objArray);
        /// <summary>
        /// Sort method - Sorts a collection into ascending order.
        /// Must be implemented by inheriting class.
        /// </summary>
        void Sort();
        /// <summary>
        /// overridden Sort method - Sorts the collection based on the IComparer object.
        /// </summary>
        /// <param name="comparer">IComparer object that specifies sorting order.</param>
        void Sort(System.Collections.IComparer comparer);
        #endregion
    }
}
