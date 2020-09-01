using System;
using System.Collections.Generic;
using System.Text;

namespace AppTypes
{
    /// <summary>
    /// Delegate used to define the event type to be raised anytime the collection is modified.
    /// </summary>
    /// <param name="sender">object</param>
    /// <param name="args">ModificatEventArgs</param>
    public delegate void CollectionModifiedHandler(object sender, ModificationEventArgs args);
    
}
