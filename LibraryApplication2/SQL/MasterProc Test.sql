--Script to test stored procedures
USE [library]
GO
/***********************************************************
*  Test AddAdultMember
***********************************************************/

---test AddAdultMember with middle initial and phone as null
DECLARE	@return_value int,
		@ExpDte datetime,
		@memberID smallint,
		@errorCode int

EXEC	@return_value = [dbo].[usp_AddAdultMember]
		@LastName = N'Smith',
		@FirstName = N'Mark',
		@MiddleInitial = NULL,
		@Street = N'1234 East',
		@City = N'Dallas',
		@State = N'TX',
		@zip = N'12345',
		@PhoneNo = NULL,
		@ExpDte = @ExpDte OUTPUT,
		@memberID = @memberID OUTPUT,
		@errorCode = @errorCode OUTPUT

SELECT	@ExpDte as N'@ExpDte',
		@memberID as N'@memberID',
		@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
----test addadultmember with middle initial and phone as values
DECLARE	@return_value int,
		@ExpDte datetime,
		@memberID smallint,
		@errorCode int

EXEC	@return_value = [dbo].[usp_AddAdultMember]
		@LastName = N'Smith',
		@FirstName = N'Mark',
		@MiddleInitial = N'T',
		@Street = N'1234 East',
		@City = N'Dallas',
		@State = N'TX',
		@zip = N'12345',
		@PhoneNo = N'(123)123-1234',
		@ExpDte = @ExpDte OUTPUT,
		@memberID = @memberID OUTPUT,
		@errorCode = @errorCode OUTPUT

SELECT	@ExpDte as N'@ExpDte',
		@memberID as N'@memberID',
		@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
/*****************************************************
*    Test AddJuvenileMember
*****************************************************/
--AddJuvenileMember with MiddleInitial as null
DECLARE	@return_value int,
		@memberID smallint,
		@errorCode int,
        @bday datetime
select @bday = dateadd(yy,-2,getdate())

EXEC	@return_value = [dbo].[usp_AddJuvenileMember]
		@LastName = N'Thomas',
		@FirstName = N'Train',
		@MiddleInitial = NULL,
		@AdultMemberID = 1,
		@BirthDate = @bday,
		@memberID = @memberID OUTPUT,
		@errorCode = @errorCode OUTPUT

SELECT	@memberID as N'@memberID',
		@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value
GO

--AddJuvenileMember with MiddleInitial not null
DECLARE	@return_value int,
		@memberID smallint,
		@errorCode int,
        @bday datetime
select @bday = dateadd(yy,-2,getdate())

EXEC	@return_value = [dbo].[usp_AddJuvenileMember]
		@LastName = N'Thomas',
		@FirstName = N'Train',
		@MiddleInitial = N'D',
		@AdultMemberID = 1,
		@BirthDate = @bday,
		@memberID = @memberID OUTPUT,
		@errorCode = @errorCode OUTPUT

SELECT	@memberID as N'@memberID',
		@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value
GO
/*********************************************************
*  Test CheckOutItem
*********************************************************/
--Successfully checks out item
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckOutItem]
		@MemberNo = 1,
		@ISBN = 1,
		@copyNum = 1,
		@errorCode = @errorCode OUTPUT

--SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
--Returns error 50010 - Item not on loan
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckOutItem]
		@MemberNo = 1,
		@ISBN = 6,
		@copyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO

--Returns error 50020 - Item not loanable
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckOutItem]
		@MemberNo = 1,
		@ISBN = 2,
		@copyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value
GO

--Returns error 50030 - item not found
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckOutItem]
		@MemberNo = 1,
		@ISBN = 5000,
		@copyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
/***********************************************************
*  Test GetMemberByIsbnCopyNum
***********************************************************/
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetMemberByIsbnCopyNum]
		@ISBN = 1,
		@CopyNum = 1

SELECT	'Return Value' = @return_value

GO

/***********************************************************
*  Test usp_item
***********************************************************/
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_item]
		@memberId = 1

SELECT	'Return Value' = @return_value

GO
/***************************************************
*  Test CheckInItem
***************************************************/
--Returns error 50010 as item isbn 2 copynum 1 is not checked out.
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckInItem]
		@ISBN = 2,
		@CopyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
--Checks in an item.
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckInItem]
		@ISBN = 1,
		@CopyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
/*******************************************************
*  Test Get_Item
*******************************************************/
--Returns Item not found (50010) when ISBN is not found
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetItem]
		@ISBN = 5000,
		@CopyNum = 1

SELECT	'Return Value' = @return_value

GO
--Returns Item not found (50010) when CopyNum is not found
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetItem]
		@ISBN = 1,
		@CopyNum = 50

SELECT	'Return Value' = @return_value
GO
--Returns item from database
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetItem]
		@ISBN = 1,
		@CopyNum = 1

SELECT	'Return Value' = @return_value
GO
/*******************************************************
*  Test GetMemberByMemberID
*******************************************************/
--Returns Adult member information
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetMemberByMemberID]
		@MemberID = 1

SELECT	'Return Value' = @return_value

GO
--Returns Juvenile member information
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetMemberByMemberID]
		@MemberID = 2

SELECT	'Return Value' = @return_value

GO