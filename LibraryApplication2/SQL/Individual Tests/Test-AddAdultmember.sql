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

---Proof of functionality
select * from member, adult
where member.member_no = @memberID
and member.member_no = adult.member_no 

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

---Proof of functionality
select * from member, adult
where member.member_no = @memberID
and member.member_no = adult.member_no
