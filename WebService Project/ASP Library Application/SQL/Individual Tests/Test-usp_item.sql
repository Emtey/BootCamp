USE [library]
GO
/***********************************************************
*  Test usp_item
***********************************************************/
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_item]
		@memberId = 338

SELECT	'Return Value' = @return_value

GO
