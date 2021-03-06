USE [library]
GO
/*********************************************************
*  Test CheckOutItem
*********************************************************/
--Successfully checks out item
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckOutItem]
		@MemberNo = 338,
		@ISBN = 1,
		@copyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
--Returns error 50010 - Item not on loan
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckOutItem]
		@MemberNo = 338,
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
		@MemberNo = 338,
		@ISBN = 2,
		@copyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value
Go
--Returns error 50030 - item not found
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckOutItem]
		@MemberNo = 338,
		@ISBN = 5000,
		@copyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value