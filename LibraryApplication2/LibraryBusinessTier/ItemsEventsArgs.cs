using System;
using System.Collections.Generic;
using System.Text;
using Entities;

namespace LibraryBusinessTier
{
    /// <summary>
    /// Create a delegate
    /// </summary>
    /// <param name="args">ItemsEventDelegate</param>   
    public delegate void ItemsEventDelegate(ItemsEventsArgs args);

    /// <summary>
    /// Create a Create an event args object to communicate details about the event
    /// </summary>
    public class ItemsEventsArgs:EventArgs
    {
        private short _memberId;
        /// <summary>
        /// memberID property
        /// </summary>
        public short memberID
        {
            get { return _memberId; }
        }
        /// <summary>
        /// verriden constructor
        /// </summary>
        /// <param name="memberID">memberID</param>
        public ItemsEventsArgs(short memberID)
        {
            _memberId = memberID;
        }
    }
}
