USE [library]
GO
/***********************************************************
*  Test GetMemberByIsbnCopyNum
***********************************************************/
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetMemberByIsbnCopyNum]
		@ISBN = 6,
		@CopyNum = 1

SELECT	'Return Value' = @return_value

GO
