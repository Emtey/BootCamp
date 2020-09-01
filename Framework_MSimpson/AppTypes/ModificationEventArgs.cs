using System;
using System.Collections.Generic;
using System.Text;


namespace AppTypes
{
    /// <summary>
    /// ModificationEventArgs class. Inherits from EventArgs.
    /// Holds the ModificationEventStatus and the obj that the event was raise by. 
    /// </summary>
    [DeveloperInfo("Mark",Date="1/23/2007",Title="Developer")]
    [CustomDescription("ModificationEventsArgs class, inherits from EventArgs")]
    [Serializable]
    public class ModificationEventArgs:EventArgs
    {
        private ModificationEventStatus modificationEventStatus;
        private object modifiedObjectReference;
        /// <summary>
        /// Property for ModificationEventStatus.
        /// </summary>
        public ModificationEventStatus ModificationEventStatus
        {
            get { return modificationEventStatus; }
        }
        /// <summary>
        /// Property for ModifiedObjectReference
        /// </summary>
        public object ModifiedObjectReference
        {
            get {return modifiedObjectReference;}
            set {modifiedObjectReference = value;}
        }
        /// <summary>
        /// Default constructor of ModificationEventArgs
        /// </summary>
        public ModificationEventArgs()
        {
        }
        /// <summary>
        /// Overidden Constructor of ModificationEventArgs 
        /// </summary>
        /// <param name="MES">ModificationEventStatus parm</param>
        /// <param name="obj">Object parm</param>
        public ModificationEventArgs(ModificationEventStatus MES, object obj)
        {
            this.modificationEventStatus = MES;
            this.ModifiedObjectReference = obj;
        }
        
    }
}
