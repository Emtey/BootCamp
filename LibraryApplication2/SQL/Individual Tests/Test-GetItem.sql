USE [library]
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
select * from item where isbn = 5000
GO
--Returns Item not found (50010) when CopyNum is not found
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetItem]
		@ISBN = 1,
		@CopyNum = 50

SELECT	'Return Value' = @return_value
select * from copy where isbn = 1 and copy_no = 50
GO
--Returns item from database
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetItem]
		@ISBN = 1,
		@CopyNum = 1

SELECT	'Return Value' = @return_value
GO