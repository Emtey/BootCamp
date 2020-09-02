using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using System.Data;
using System.Data.SqlClient;
using Entities.ItemsDataSetTableAdapters;

namespace LibraryDataAccess
{
    /// <summary>
    /// DataAccess tier
    /// </summary>
    public class DataAccess
    {
        /// <summary>
        /// Default constructor for DataAccess Class.
        /// </summary>
        public DataAccess()
        {
        }
        /// <summary>
        /// Adds an adult member
        /// </summary>
        /// <param name="member">Member object</param>
        public void AddMember(AdultMember member)
        {
            try
            {
                //create Connection object
                using (SqlConnection cn = DBConnect())
                {
                    //create command object
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_AddAdultMember";

                        //add all the fields as parameters to pass into our AddAdultMember
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 15).Value = member.LastName;
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 15).Value = member.FirstName;
                        if (member.MiddleInitial != null)
                            cmd.Parameters.Add("@MiddleInitial", SqlDbType.Char, 1).Value = member.MiddleInitial;
                        else
                            cmd.Parameters.Add("@MiddleInitial", SqlDbType.Char, 1).Value = DBNull.Value;
                        cmd.Parameters.Add("@Street", SqlDbType.VarChar, 15).Value = member.Street;
                        cmd.Parameters.Add("@City", SqlDbType.VarChar, 15).Value = member.City;
                        cmd.Parameters.Add("@State", SqlDbType.Char, 2).Value = member.State;
                        cmd.Parameters.Add("@Zip", SqlDbType.Char, 10).Value = member.ZipCode;
                        if (member.PhoneNumber != null)
                            cmd.Parameters.Add("@PhoneNo", SqlDbType.Char, 13).Value = member.PhoneNumber;
                        else
                            cmd.Parameters.Add("@PhoneNo", SqlDbType.Char, 13).Value = DBNull.Value;
                        //cmd.Parameters.Add("@ExpDte", SqlDbType.DateTime).Value = member.ExpirationDate;
                        cmd.Parameters.Add("@ExpDte", SqlDbType.DateTime).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@memberID",SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@errorCode",SqlDbType.Int).Direction = ParameterDirection.Output;

                        //open connection
                        cn.Open();
                        //Execute command
                        cmd.ExecuteNonQuery();
                        //get the memberID of the newly added member
                        member.MemberID = (Int16)cmd.Parameters["@memberID"].Value;
                        member.ExpirationDate = (DateTime)cmd.Parameters["@ExpDte"].Value;
                        int errorCode = (int)cmd.Parameters["@errorCode"].Value;
                        if (errorCode != 0)
                        {
                            throw new LibraryException(ErrorCode.AddAdultFailed, "Error: Add Adult Failed.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new LibraryException(ErrorCode.GenericException, "Error:  Contact Technical Support.", ex);
            }
        }

        /// <summary>
        /// Adds a juvenile member
        /// </summary>
        /// <param name="member">Member object</param>
        public void AddMember(JuvenileMember member)
        {
            try
            {
                //create Connection object
                using (SqlConnection cn = DBConnect())
                {
                    //create command object
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_AddJuvenileMember";

                        //add all the fields as parameters to pass into our AddAdultMember
                        cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 15).Value = member.LastName;
                        cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 15).Value = member.FirstName;
                        if (member.MiddleInitial != null)
                            cmd.Parameters.Add("@MiddleInitial", SqlDbType.Char, 1).Value = member.MiddleInitial;
                        else
                            cmd.Parameters.Add("@MiddleInitial", SqlDbType.Char, 1).Value = DBNull.Value;
                        cmd.Parameters.Add("@AdultMemberID", SqlDbType.SmallInt).Value = member.AdultMemberID;
                        cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime).Value = member.BirthDate;
                        cmd.Parameters.Add("@memberID", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("@errorCode", SqlDbType.Int).Direction = ParameterDirection.Output;

                        //open connection
                        cn.Open();
                        //Execute command
                        cmd.ExecuteNonQuery();
                        //get the memberID of the newly added member
                        member.MemberID = (Int16)cmd.Parameters["@memberID"].Value;
                        int errorCode = (int)cmd.Parameters["@errorCode"].Value;
                        if (errorCode != 0)
                        {
                            throw new LibraryException(ErrorCode.AddAdultFailed, "Error: Add Juvenile Failed.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new LibraryException(ErrorCode.GenericException, "Error:  Contact Technical Support.", ex);
            }
        }
        /// <summary>
        /// Checks in an item
        /// </summary>
        /// <param name="isbn">ISBN </param>
        /// <param name="copyNumber">Copy Number</param>
        public void CheckInItem(Int32 isbn, Int16 copyNumber)
        {
            try
            {
                //create Connection object
                using (SqlConnection cn = DBConnect())
                {
                    //create command object
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_CheckInItem";

                        cmd.Parameters.Add("@ISBN", SqlDbType.Int).Value = isbn;
                        cmd.Parameters.Add("@CopyNum", SqlDbType.SmallInt).Value = copyNumber;
                        cmd.Parameters.Add("@errorCode", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cn.Open();
                        cmd.ExecuteNonQuery();

                        int errorCode = (int)cmd.Parameters["@errorCode"].Value;
                        if (errorCode != 0)
                            throw new LibraryException(ErrorCode.CheckInFailed, "Error:  Failed Check In");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new LibraryException(ErrorCode.GenericException, "Error:  Contact Technical Support.", ex);
            }
        }
        /// <summary>
        /// Checks an item out
        /// </summary>
        /// <param name="memberNumber">Member ID</param>
        /// <param name="isbn">ISBN</param>
        /// <param name="copyNum">Copy Number</param>
        public void CheckOutItem(Int16 memberNumber, Int32 isbn, Int16 copyNum)
        {
            try
            {
                //create Connection object
                using (SqlConnection cn = DBConnect())
                {
                    //create command object
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_CheckOutItem";

                        cmd.Parameters.Add("@MemberNo", SqlDbType.SmallInt).Value = memberNumber;
                        cmd.Parameters.Add("@ISBN", SqlDbType.Int).Value = isbn;
                        cmd.Parameters.Add("@copyNum", SqlDbType.SmallInt).Value = copyNum;
                        cmd.Parameters.Add("@errorCode", SqlDbType.Int).Direction = ParameterDirection.Output;

                        cn.Open();
                        cmd.ExecuteNonQuery();
                        int errorCode = (int)cmd.Parameters["@errorCode"].Value;
                        switch (errorCode)
                        {
                            case 0: //no problems
                                break;
                            case 50010:
                                throw new LibraryException(ErrorCode.ItemAlreadyOnLoan, "Error: Item already on loan");
                            case 50020:
                                throw new LibraryException(ErrorCode.ItemNotLoanable, "Error: Item is not loanable");
                            case 50030:
                                throw new LibraryException(ErrorCode.ItemNotFound, "Error: Item not found");
                            default:
                                throw new LibraryException(ErrorCode.CheckOutFailed, "Error:  Failed Check Out");
                        }   
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new LibraryException(ErrorCode.GenericException, "Error:  Contact Technical Support.", ex);
            }
        }
        /// <summary>
        /// Retreives an item.
        /// </summary>
        /// <param name="isbn">ISBN</param>
        /// <param name="copyNumber">Copy Number</param>
        /// <returns>Item Object</returns>
        public Item GetItem(Int32 isbn, Int16 copyNumber)
        {
            try
            {
                //create Connection object
                using (SqlConnection cn = DBConnect())
                {
                    //create command object
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetItem";

                        cmd.Parameters.Add("@ISBN", SqlDbType.Int).Value = isbn;
                        cmd.Parameters.Add("@CopyNum", SqlDbType.SmallInt).Value = copyNumber;
                        cmd.Parameters.Add("@errorCode", SqlDbType.Int);
                        cmd.Parameters["@errorCode"].Direction = ParameterDirection.ReturnValue;

                        //open connection
                        cn.Open();
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                       
                        if (reader.Read())
                        {
                            Int32 ISBN = (Int32)reader["isbn"];
                            Int16 copyNo = (Int16)reader["copy_no"];
                            string title = (string)reader["title"];
                            string author = (string)reader["author"];
                            Int16 member_no = 0;
                            if (!reader.IsDBNull(4))
                                member_no = (Int16)reader["member_no"];
                            DateTime out_date = DateTime.MinValue;
                            if (!reader.IsDBNull(5))
                                out_date = (DateTime)reader["out_date"];
                            DateTime due_date = DateTime.MinValue;
                            if (!reader.IsDBNull(6))
                                due_date = (DateTime)reader["due_date"];
                            
                            Item item = new Item(ISBN, copyNo, title, author, member_no, out_date, due_date);

                            return item;
                        }
                        reader.Close();
                        int errorCode = (int)cmd.Parameters["@errorCode"].Value;
                        if (errorCode == 50010)
                            throw new LibraryException(ErrorCode.ItemNotFound, "Item not found");
                        else
                            throw new LibraryException(ErrorCode.GenericException, "Error: Contact Technical Support.");
                    }
                }                
            }
            catch (SqlException ex)
            {
                throw new LibraryException(ErrorCode.GenericException, "Error:  Contact Technical Support.", ex);
            }
        }
        /// <summary>
        /// Retrieves items.
        /// </summary>
        /// <param name="memberNumber">Member ID</param>
        /// <returns>Items Data Set</returns>
        public ItemsDataSet GetItems(Int16 memberNumber)
        {
            ItemsTableAdapter ita = new ItemsTableAdapter();
            ItemsDataSet ids = new ItemsDataSet();
            ita.Fill(ids.Items,memberNumber);
            return ids;
        }
        /// <summary>
        /// Retrieves a member
        /// </summary>
        /// <param name="memberNumber">Member ID</param>
        /// <returns>member object</returns>
        public Member GetMember(Int16 memberNumber)
        {
            try
            {
                //create Connection object
                using (SqlConnection cn = DBConnect())
                {
                    //create command object
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetMemberByMemberID";

                        //load the parameter list
                        cmd.Parameters.Add("@MemberID", SqlDbType.SmallInt).Value = memberNumber;

                        //open connection
                        cn.Open();
                        //execute command
                        SqlDataReader reader = cmd.ExecuteReader();
                        
                        if (reader.Read())
                        {
                            //check the birth_date field, if its null, then we have an adult.
                            if (reader.IsDBNull(10))
                            {
                                AdultMember member = new AdultMember();
                                member.MemberID = (Int16)reader["member_no"];
                                member.LastName = (string)reader["lastname"];
                                member.FirstName = (string)reader["firstname"];
                                if (!reader.IsDBNull(3))
                                    member.MiddleInitial = (string)reader["middleinitial"];
                                member.Street = (string)reader["street"];
                                member.City = (string)reader["city"];
                                member.State = (string)reader["state"];
                                member.ZipCode = (string)reader["zip"];
                                if (!reader.IsDBNull(8))
                                    member.PhoneNumber = (string)reader["phone_no"];
                                member.ExpirationDate = reader.GetDateTime(9);
                                return member;
                            }
                            else
                            {
                                JuvenileMember member = new JuvenileMember();                                
                                member.MemberID = (Int16)reader["member_no"];
                                member.LastName = (string)reader["lastname"];
                                member.FirstName = (string)reader["firstname"];
                                if (!reader.IsDBNull(3))
                                    member.MiddleInitial = (string)reader["middleinitial"];
                                member.Street = (string)reader["street"];
                                member.City = (string)reader["city"];
                                member.State = (string)reader["state"];
                                member.ZipCode = (string)reader["zip"];
                                if (!reader.IsDBNull(8))
                                    member.PhoneNumber = (string)reader["phone_no"];
                                member.ExpirationDate = (DateTime)reader["expr_date"];
                                member.BirthDate = (DateTime)reader["birth_date"];
                                member.AdultMemberID = (Int16)reader["adult_member_no"];
                                return member;
                            }                           
                        }
                        else
                            throw new LibraryException(ErrorCode.NoSuchMember, "Member not found");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new LibraryException(ErrorCode.GenericException, "Error:  Contact Technical Support.", ex);
            }
        }
        /// <summary>
        /// Retrieves a member
        /// </summary>
        /// <param name="isbn">ISBN</param>
        /// <param name="copyNum">Copy Number</param>
        /// <returns>Member object</returns>
        public Member GetMember(Int32 isbn, Int16 copyNum)
        {
            try
            {
                //create Connection object
                using (SqlConnection cn = DBConnect())
                {
                    //create command object
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_GetMemberByIsbnCopyNum";

                        //load the parameter list
                        cmd.Parameters.Add("@ISBN", SqlDbType.Int).Value = isbn;
                        cmd.Parameters.Add("@CopyNum", SqlDbType.SmallInt).Value = copyNum;
                        

                        //open connection
                        cn.Open();
                        //execute command
                        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                        
                        if (reader.Read())
                        {
                            //check the birth_date field, if its null, then we have an adult.
                            if (reader.IsDBNull(10))
                            {
                                AdultMember member = new AdultMember();
                                member.MemberID = (Int16)reader["member_no"];
                                member.LastName = (string)reader["lastname"];
                                member.FirstName = (string)reader["firstname"];
                                if (!reader.IsDBNull(3))
                                    member.MiddleInitial = (string)reader["middleinitial"];
                                member.Street = (string)reader["street"];
                                member.City = (string)reader["city"];
                                member.State = (string)reader["state"];
                                member.ZipCode = (string)reader["zip"];
                                if (!reader.IsDBNull(8))
                                    member.PhoneNumber = (string)reader["phone_no"];
                                member.ExpirationDate = (DateTime)reader["expr_date"];
                                return member;
                            }
                            else
                            {
                                JuvenileMember member = new JuvenileMember();
                                member.MemberID = (Int16)reader["member_no"];
                                member.LastName = (string)reader["lastname"];
                                member.FirstName = (string)reader["firstname"];
                                if (!reader.IsDBNull(3))
                                    member.MiddleInitial = (string)reader["middleinitial"];
                                member.Street = (string)reader["street"];
                                member.City = (string)reader["city"];
                                member.State = (string)reader["state"];
                                member.ZipCode = (string)reader["zip"];
                                if (!reader.IsDBNull(8))
                                    member.PhoneNumber = (string)reader["phone_no"];
                                member.ExpirationDate = (DateTime)reader["expr_date"];
                                member.BirthDate = (DateTime)reader["birth_date"];
                                member.AdultMemberID = (Int16)reader["adult_member_no"];
                                return member;
                            }
                        }
                        else
                            throw new LibraryException(ErrorCode.NoSuchMember, "Member not found");
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new LibraryException(ErrorCode.GenericException, "Error:  Contact Technical Support.", ex);
            }
        }

        private SqlConnection DBConnect()
        {
            string Connection = @"server=.;database=Library;integrated security=true";
            SqlConnection cn = new SqlConnection(Connection);
            return cn;
        }
    }
}
       
   
