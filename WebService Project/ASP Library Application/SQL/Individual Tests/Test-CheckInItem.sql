USE [library]
GO
/***************************************************
*  Test CheckInItem
***************************************************/
--Returns error 50010 as item isbn 1 copynum 1 is not checked out.
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckInItem]
		@ISBN = 1,
		@CopyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value

GO
--Checks in an item.
DECLARE	@return_value int,
		@errorCode int

EXEC	@return_value = [dbo].[usp_CheckInItem]
		@ISBN = 6,
		@CopyNum = 1,
		@errorCode = @errorCode OUTPUT

SELECT	@errorCode as N'@errorCode'

SELECT	'Return Value' = @return_value
--should have no rows here
select * from loan where isbn=6 and copy_no = 1
--on_loan field should be set to 'N'
select * from copy where isbn=6 and copy_no = 1
--an entry for this Isbn and COpy_no should appear
--with todays date as the in_date
select * from loanhist where isbn=6 and copy_no =1

GO