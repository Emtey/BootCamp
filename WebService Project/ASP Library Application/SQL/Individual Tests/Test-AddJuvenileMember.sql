USE [library]
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
--Proof of functionality
select * from member, juvenile
where member.member_no = @memberID
and member.member_no = juvenile.member_no
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
--Proof of functionality
select * from member, juvenile
where member.member_no = @memberID
and member.member_no = juvenile.member_no
GO