using System;
using System.Collections.Generic;
using System.Text;

namespace Foundation
{
    /// <summary>
    /// IContactInfo interface.  Inherits from ICompanyContact, IAddress and ICountryPhone
    /// </summary>
    public interface IContactInfo:ICompanyContact,IAddress,ICountryPhone
    {
        //nothing goes in here, it just inherits from the ICompanyContact
        //IAddress and ICountryPhone interfaces.  We use this so we can just
        //inherit this in our classes and this will in turn inherit from 
        //the other interfaces (save us some typing!)
    }
}
