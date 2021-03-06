USE [library]
GO
--Convert Juvenile member to an adult
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_ConvertJuvenileToAdult]
		@memberID = 12,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
--Add a New Item
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_AddItem]
		@isbn = 5007,
		@title = N'New Book',
		@author = N'New Author',
		@loanable = N'Y',
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
--Add a new item to an already existing one
USE [library]
GO

DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_AddItem]
		@isbn = 3,
		@title = N'doesnt matter',
		@author = N'doesnt matter',
		@loanable = N'Y',
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
--Renew an expired member
USE [library]
GO

DECLARE	@return_value int,
		@ExpDte datetime,
		@errorCode int

EXEC	@return_value = [dbo].[usp_RenewMember]
		@memberID = 1095,
		@ExpDte = @ExpDte OUTPUT,
		@errorCode = @errorCode OUTPUT

SELECT	@ExpDte as N'@ExpDte',
		@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO