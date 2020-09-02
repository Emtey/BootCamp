USE [library]
GO
/*******************************************************
*  Test GetMemberByMemberID
*******************************************************/
--Returns Adult member information
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetMemberByMemberID]
		@MemberID = 337

SELECT	'Return Value' = @return_value

GO
--Returns Juvenile member information
DECLARE	@return_value int

EXEC	@return_value = [dbo].[usp_GetMemberByMemberID]
		@MemberID = 338

SELECT	'Return Value' = @return_value

GO
