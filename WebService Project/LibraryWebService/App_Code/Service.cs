using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using Entities;
using LibraryBusinessTier;
using System.Xml.Serialization;
using ExceptionCodeLibrary;

using Microsoft.Web.Services3;

[WebService(Namespace = "http://library.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[Policy("LibraryPolicy")]
public class Service : System.Web.Services.WebService
{
    public Service ()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /// <summary>
    /// Add a member to the db.
    /// </summary>
    /// <param name="member">Member</param>
    [WebMethod(Description="Add Juvenile member to the database.")]
    public void AddJuvenileMember(ref JuvenileMember member)
    {
        
        try
        {
            DBInteraction db = new DBInteraction();
            db.AddMember(member);
        }
        catch (LibraryException ex)
        {
            throw new SoapException(ex.Message, ExceptionCodes.AddJuvenileFailed);
        }
    }

    /// <summary>
    /// Add a member to the db
    /// </summary>
    /// <param name="member">Member</param>
    [WebMethod(Description="Add Adult member to the database.")]
    public void AddAdultMember(ref AdultMember member)
    {
        try
        {
            DBInteraction db = new DBInteraction();
            db.AddMember(member);
        }
        catch (LibraryException ex)
        {
            throw new SoapException(ex.Message, ExceptionCodes.AddAdultFailed);
        }
    }

    /// <summary>
    /// Check in an item.
    /// </summary>
    /// <param name="isbn">ISBN</param>
    /// <param name="copyNumber">Copy Number</param>
    [WebMethod(Description="Check in an item.")]
    public void CheckInItem(int isbn, short copyNumber)
    {
        try
        {
            DBInteraction db = new DBInteraction();
            db.CheckInItem(isbn, copyNumber);
        }
        catch (LibraryException ex)
        {
            if (ex.LibraryErrorCode == ErrorCode.ItemNotOnLoan)
                throw new SoapException(ex.Message, ExceptionCodes.ItemNotOnLoan);
            else if (ex.LibraryErrorCode == ErrorCode.ItemNotFound)
                throw new SoapException(ex.Message, ExceptionCodes.ItemNotFound);
            else
                throw new SoapException(ex.Message, ExceptionCodes.GenericException);
        }
       
    }

    /// <summary>
    /// Check out an item.
    /// </summary>
    /// <param name="memberNumber">Member ID No.</param>
    /// <param name="isbn">ISBN</param>
    /// <param name="copyNumber">Copy Number</param>
    [WebMethod(Description="Check out an item.")]
    public void CheckOutItem(short memberNumber, int isbn, short copyNumber)
    {
        try
        {
            DBInteraction db = new DBInteraction();
            db.CheckOutItem(memberNumber, isbn, copyNumber);
        }
        catch (LibraryException ex)
        {
            if (ex.LibraryErrorCode == ErrorCode.CheckOutFailed)
                throw new SoapException(ex.Message,ExceptionCodes.CheckOutFailed);
            if (ex.LibraryErrorCode == ErrorCode.ItemAlreadyOnLoan)
                throw new SoapException(ex.Message, ExceptionCodes.ItemAlreadyOnLoan);
            if (ex.LibraryErrorCode == ErrorCode.GenericException)
                throw new SoapException(ex.Message, ExceptionCodes.GenericException);
            if (ex.LibraryErrorCode == ErrorCode.ItemNotFound)
                throw new SoapException(ex.Message, ExceptionCodes.ItemNotFound);
            if (ex.LibraryErrorCode == ErrorCode.ItemNotLoanable)
                throw new SoapException(ex.Message, ExceptionCodes.ItemNotLoanable);
        }
    }

    /// <summary>
    /// Gets an item from teh db
    /// </summary>
    /// <param name="isbn">ISBN</param>
    /// <param name="copyNumber">Copy Number</param>
    /// <returns>Item</returns>
    [WebMethod(Description = "Gets an item from the database.")]
    public Item GetItem(int isbn, short copyNumber)
    {
        try
        {
            DBInteraction db = new DBInteraction();
            return db.GetItem(isbn, copyNumber);
        }
        catch (LibraryException ex)
        {
            throw new SoapException(ex.Message, ExceptionCodes.ItemNotFound);
        }
    }

    /// <summary>
    /// Gets an Items data set (list of items) from teh db.
    /// </summary>
    /// <param name="memberNumber">Member ID</param>
    /// <returns>List of Items</returns>
    [WebMethod(Description="Gets a list of items from the database")]
    public ItemsDataSet GetItems(short memberNumber)
    {
        ItemsDataSet myDataSet = new ItemsDataSet();
        DBInteraction myDb = new DBInteraction();
        try
        {
            myDataSet = myDb.GetItems(memberNumber);
            return myDataSet;
        }
        catch (LibraryException ex)
        {
            throw new SoapException(ex.Message, ExceptionCodes.None);
        }
    }

    /// <summary>
    /// Gets a member from teh db.
    /// </summary>
    /// <param name="memberNumber">Member ID</param>
    /// <returns>Member</returns>
    [WebMethod(Description="Gets a member from the database.")]
    [XmlInclude(typeof(AdultMember))]
    [XmlInclude(typeof(JuvenileMember))]
    public Member GetMember(short memberNumber)
    {
        try
        {
            DBInteraction db = new DBInteraction();
            Member m = db.GetMember(memberNumber);
            return m;
        }
        catch (LibraryException ex)
        {
            throw new SoapException(ex.Message, ExceptionCodes.None );
        }
    }

    /// <summary>
    /// Renews the expiration date of a member.
    /// </summary>
    /// <param name="memberNumber">Member ID</param>
    [WebMethod(Description="Renews the expirations date of an expired membership.")]
    public void RenewMember(short memberNumber)
    {
        try
        {
            DBInteraction db = new DBInteraction();
            db.RenewMember(memberNumber);
        }
        catch (LibraryException ex)
        {
            throw new SoapException(ex.Message, ExceptionCodes.RenewMemberFailed);
        }
    }


    /// <summary>
    /// Converts a Juvenile member into an adult.
    /// </summary>
    /// <param name="memberNumber">Member ID.</param>
    [WebMethod(Description="Converts a juvenile member into an adult member.")]
    public void ConvertJuvenile(short memberNumber)
    {
        try
        {
            DBInteraction db = new DBInteraction();
            db.ConvertJuvenile(memberNumber);
        }
        catch (LibraryException ex)
        {
            throw new SoapException(ex.Message, ExceptionCodes.ConvertJuvenileFailed);
        }
    }

    /// <summary>
    /// Adds an item to the database.
    /// </summary>
    /// <param name="isbn">ISBN</param>
    /// <param name="title">title</param>
    /// <param name="author">author</param>
    /// <param name="loanable">loanable</param>
    [WebMethod(Description="Adds an item to the database.")]
    public void AddItem(int isbn, string title, string author, string loanable)
    {
        DBInteraction db = new DBInteraction();
        try
        {
            db.AddItem(isbn, title, author, loanable);
        }
        catch (LibraryException ex)
        {
            throw new SoapException(ex.Message, ExceptionCodes.AddItemFailed);
        }
    }
}
