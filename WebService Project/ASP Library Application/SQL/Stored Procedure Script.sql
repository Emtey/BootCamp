USE [library]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddAdultMember]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_AddAdultMember]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_AddAdultMember]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddItem]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_AddItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_AddItem]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddJuvenileMember]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_AddJuvenileMember]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_AddJuvenileMember]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckInItem]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CheckInItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CheckInItem]
GO
/****** Object:  StoredProcedure [dbo].[usp_CheckOutItem]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_CheckOutItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_CheckOutItem]
GO
/****** Object:  StoredProcedure [dbo].[usp_ConvertJuvenileToAdult]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_ConvertJuvenileToAdult]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_ConvertJuvenileToAdult]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetItem]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetItem]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetItem]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetMemberByIsbnCopyNum]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetMemberByIsbnCopyNum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetMemberByIsbnCopyNum]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetMemberByMemberID]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetMemberByMemberID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetMemberByMemberID]
GO
/****** Object:  StoredProcedure [dbo].[usp_item]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_item]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_item]
GO
/****** Object:  StoredProcedure [dbo].[usp_RenewMember]    Script Date: 03/16/2007 11:28:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_RenewMember]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_RenewMember]
GO

USE [library]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddAdultMember]    Script Date: 03/16/2007 11:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*********************************************************
*                   AddAdultMember script
*  This scripts add a member to the member table and then 
*  adds the corresponding row to the adult table.
*
* Input Parms:
*	LastName
*	FirstName
*	MiddleInitial 
*	Street
*	City
*	State
*	zip
*	PhoneNo 
*
* Output Parms
*	ExpDte 
*	memberID
*	errorCode 
*
* Error Codes:
*	
*********************************************************/

CREATE proc [dbo].[usp_AddAdultMember]
(@LastName varchar(15),
@FirstName varchar(15),
@MiddleInitial char(1),
@Street varchar(15),
@City varchar(15),
@State char(2),
@zip char(10),
@PhoneNo char(13),
@ExpDte Datetime output,
@memberID smallint output,
@errorCode int output)
AS
BEGIN TRANSACTION
--Insert into the members table
INSERT INTO member VALUES 
(@LastName, @FirstName,@MiddleInitial, null)
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--Get the memberID field for the members table that we
--created the record on.
SELECT @memberID = scope_identity()

--Get an expiration date to use for later.
SELECT @ExpDte = dateadd(yy,1,getdate())

--Insert into the adult table
INSERT INTO adult VALUES
(@memberID, @Street, @City, 
 @State, @Zip, @PhoneNo, @ExpDte)
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--it all worked if we got here, set the errorCode and commit.
SELECT @errorCode = @@error
COMMIT TRANSACTION








GO
/****** Object:  StoredProcedure [dbo].[usp_AddItem]    Script Date: 03/16/2007 11:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************
*  usp_AddItem
*
*  Adds an item to the database.
***********************************************************/

CREATE PROC	[dbo].[usp_AddItem]
(@isbn int,
@title varchar(63),
@author varchar(31),
@loanable char(1),
@errorCode int output)
AS

--declare some variables for us to use.
DECLARE @count int,
        @title_no int

--get the count of the copy_no for hte isbn, if its null
--then we know we are going to do a full insert
SELECT @count = max(copy_no + 1)
FROM copy
WHERE ISBN = @isbn

--set count to 1 if we have a null count.
IF @count is null
	SET @count = 1


BEGIN TRANSACTION
--We have an insert for a new item.
IF @count = 1
BEGIN    
    --insert into title table.
	INSERT INTO title values
    (@title,@author,null)
    SELECT @title_no = scope_identity();
    --check error code
	IF @@error <> 0
		BEGIN
			SELECT @errorCode = @@error
			ROLLBACK TRANSACTION
			RETURN @errorCode
		END
    --now insert into the items table
    INSERT INTO item VALUES
    (@isbn, @title_no,null,null,@loanable)
	--check error code
	IF @@error <> 0
		BEGIN
			SELECT @errorCode = @@error
			ROLLBACK TRANSACTION
			RETURN @errorCode
		END
	--now insert into the copy table
	INSERT INTO copy VALUES
	(@isbn,@count,@title_no,'N')
	--check error code
	IF @@error <> 0
		BEGIN
			SELECT @errorCode = @@error
			ROLLBACK TRANSACTION
			RETURN @errorCode
		END 
END    
--We have an insert for an already existing item.
ELSE
BEGIN
    SELECT @title_no = t.title_no
	FROM title t, copy c
	where c.isbn = @isbn and
          c.title_no = t.title_no

	INSERT INTO copy values (@isbn,
                             @count,
                             @title_no,						     
							 'N')
	--check error code
	IF @@error <> 0
		BEGIN
			SELECT @errorCode = @@error
			ROLLBACK TRANSACTION
			RETURN @errorCode
		END
END
SELECT @errorCode = @@error
COMMIT TRANSACTION






GO
/****** Object:  StoredProcedure [dbo].[usp_AddJuvenileMember]    Script Date: 03/16/2007 11:29:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/**********************************************************
*					AddJuvenileMember
*
* Adds a juvenile member to the member table and the 
* Juvenile table.
*
* Input Parms:
*	LastName
*	FirstName
*	MiddleInitial
*	AdultMemberID
*	BirthDate
* Output Parms:
*	memberID 
*	errorCode
**********************************************************/

CREATE proc [dbo].[usp_AddJuvenileMember]
(@LastName varchar(15),
@FirstName varchar(15),
@MiddleInitial char(1),
@AdultMemberID smallint,
@BirthDate datetime,
@memberID smallint output,
@errorCode int output)
AS
BEGIN TRANSACTION
--Insert into the member table.
INSERT INTO member VALUES 
(@LastName, @FirstName,@MiddleInitial, null)
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--Get the memberID field that was just inserted.
SELECT @memberID = scope_identity()

--Insert into the juvenile table.
INSERT INTO Juvenile VALUES
(@memberID,@AdultMemberID,@BirthDate)
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END
SELECT @errorCode = @@error
COMMIT TRANSACTION





GO
/****** Object:  StoredProcedure [dbo].[usp_CheckInItem]    Script Date: 03/16/2007 11:29:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/********************************************************
*					CheckInItem
* Checks in an item, removes the item from the loan table
* changes the on_loan field in the copy table and inserts
* a new line in the loanhist table.
*
* Input Parms:
*	ISBN 
*	CopyNum
*
* Output Parms:
*	errorCode
*
* Error Codes:
*	50010 - Item not checked out
********************************************************/

CREATE proc [dbo].[usp_CheckInItem]
(@ISBN int, 
@CopyNum smallint,
@errorCode int output)
AS
--check to see if item is checked out
IF NOT EXISTS
	(SELECT * FROM loan WHERE isbn = @ISBN and copy_no = @CopyNum)
	BEGIN
		SELECT @errorCode = 50010
		RETURN @errorCode
	END

--Create some variables that we will use later on.
DECLARE @titleNo int,
	    @member_no smallint,
		@out_date datetime,
		@due_date datetime

--Load the variables with the loan information that we will
--need to populate the loanhist record.
SELECT @titleNo = title_no,
       @member_no = member_no,
	   @out_date = out_date,
	   @due_date = due_date 
FROM loan 
WHERE isbn = @ISBN and copy_no = @CopyNum

BEGIN TRANSACTION
--Update the copy table, setting the on_loan field.
UPDATE copy SET on_loan = 'N'
WHERE copy.isbn = @ISBN and
      copy.copy_no = @CopyNum 
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--Insert into the loanhist table all the values we collected.
INSERT INTO loanhist VALUES
(@ISBN,@CopyNum,@out_date,@titleNo,@member_no,@due_date,getdate(),
null,null,null,null)
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--Now delete the item from the loan table
DELETE FROM loan
WHERE loan.isbn = @ISBN and
	  loan.copy_no = @CopyNum
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--if we made it down here all is good
SELECT @errorCode = @@error
COMMIT TRANSACTION


GO
/****** Object:  StoredProcedure [dbo].[usp_CheckOutItem]    Script Date: 03/16/2007 11:29:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/************************************************************
*				CheckOutItemQuery
*
*	Checks an item out.  Updates the copy table, inserts a 
* record into the loan table.
*
* Input Parms:
*	MemberNo
*	ISBN
*	copyNum
* Output Parms:
*	errorCode
*
* Error Messages:
*   50010 - Item on loan
*	50020 - Item is not loanable
*   50030 - Item not found
************************************************************/


CREATE proc [dbo].[usp_CheckOutItem]
(@MemberNo	smallint,
@ISBN		int,
@copyNum	smallint,
@errorCode	int output)
AS
---Check to see if the item is on loan.
IF EXISTS
	(SELECT * FROM copy	WHERE isbn = @ISBN
	and copy_no = @copyNum
	and on_loan = 'Y')
	BEGIN
		SET @errorCode = 50010 --item on loan
		RETURN @errorCode
	END
---Check to see if the item is loanable
IF EXISTS
	(SELECT * FROM item WHERE isbn = @ISBN and loanable = 'N')
	BEGIN
		SET @errorCode = 50020 --item is not loanable
		RETURN @errorCode
	END
---check to see if the item exists
IF NOT EXISTS
	(SELECT * FROM copy WHERE isbn = @ISBN and copy_no = @copyNum)
	BEGIN
		set @errorCode = 50030 --item not found
		RETURN @errorCode
	END
DECLARE @title_no int
BEGIN TRANSACTION
---update the copy table
UPDATE copy SET on_loan = 'Y' 
WHERE isbn = @ISBN and copy_no = @copyNum
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

---get the title_no from the copy table
SELECT @title_no = title_no FROM copy 
WHERE isbn = @ISBN and copy_no = @copyNum
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--insert into the loan table
INSERT INTO loan VALUES
(@ISBN,@CopyNum,@title_no,@MemberNo,getdate(),
DATEADD(day, 14, getdate()))
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END
--set the return errorCode to the @@error
SELECT @errorCode = @@error
--commit the transaction!
COMMIT TRANSACTION
	


GO
/****** Object:  StoredProcedure [dbo].[usp_ConvertJuvenileToAdult]    Script Date: 03/16/2007 11:29:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*********************************************************
*  Convert Juvenile to Adult
*  Converts a juvenile to an adult record.
*********************************************************/
CREATE PROC [dbo].[usp_ConvertJuvenileToAdult]
(@memberID smallint,
@errorCode int output)
AS

--get some variables for us to work with
DECLARE @street varchar(15),
@city varchar(15),
@state char(2),
@zip char(10),
@phone_no char(13),
@expr_date datetime

--set the expiration date.
SELECT @expr_date = dateadd(yy,1,getdate())

SELECT  @street = street,
@city = city,
@state = state,
@zip = zip,
@phone_no = phone_no
FROM adult
INNER JOIN Juvenile ON
juvenile.Adult_member_no = adult.member_no
WHERE juvenile.member_no = @memberID

BEGIN TRANSACTION
--insert into the Adult table the values from above.
INSERT INTO Adult VALUES
(@memberID,@street,@city,@state,@zip,@phone_no,@expr_date)

--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--now get rid of the juvenile record.
DELETE FROM juvenile WHERE member_no = @memberID
--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--it all worked if we got here, set the errorCode and commit.
SELECT @errorCode = @@error
COMMIT TRANSACTION





GO
/****** Object:  StoredProcedure [dbo].[usp_GetItem]    Script Date: 03/16/2007 11:29:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*****************************************************
*						GetItem 
*
*  Gets the item from the item, copy, loan and title 
* tables.
*
* Input Parms:
*	ISBN
*	CopyNum
*
* Error Messages:
*   50010 - Item not found
*****************************************************/

CREATE proc [dbo].[usp_GetItem]
(@ISBN int,
@CopyNum smallint)
AS
--check if the item exists
IF NOT EXISTS
	(SELECT * FROM copy where isbn = @ISBN and copy_no = @CopyNum)
	BEGIN
		RETURN 50010
	END
--Get the item
SELECT	i.isbn,
		c.copy_no,
		t.title,
		t.author,
		l.member_no,
		l.out_date,
		l.due_date
FROM item i
inner join copy c on i.isbn = c.isbn
inner join title t on t.title_no = c.title_no
left outer join loan l on l.isbn = i.isbn
	and l.copy_no = c.copy_no
	and l.title_no = c.title_no
WHERE i.isbn = @ISBN and c.copy_no = @CopyNum
--check error code
IF @@error <> 0
	BEGIN
		RETURN @@error
	END
RETURN @@error
		



GO
/****** Object:  StoredProcedure [dbo].[usp_GetMemberByIsbnCopyNum]    Script Date: 03/16/2007 11:29:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/******************************************************
*				Get Member By Isbn CopyNum
*
*  Gets a member when an ISBN and copynumber are passed.
*
* Input Parms:
*   ISBN
*	CopyNum
*
******************************************************/

CREATE proc [dbo].[usp_GetMemberByIsbnCopyNum]
(@ISBN int, @CopyNum smallint) AS
--Select the appropriate member information from the 
--appropriate tables depending on whether the member is
--a juvenile or an adult.
SELECT	m.member_no,
		m.lastname,
		m.firstname,
		m.middleinitial,
		a.street,
		a.city,
		a.state,
		a.zip,
		a.phone_no,
		a.expr_date,
		NULL as birth_date,
		NULL as adult_member_no
FROM member m
inner join adult a on m.member_no = a.member_no
inner join loan l on l.member_no = m.member_no
WHERE l.isbn = @ISBN and
      l.copy_no = @CopyNum
UNION ALL
SELECT m.member_no,
		m.lastname,
		m.firstname,
		m.middleinitial,
		a.street,
		a.city,
		a.state,
		a.zip,
		a.phone_no,
		a.expr_date,
		j.birth_date,
		j.adult_member_no
FROM member m
Inner join juvenile j on j.member_no = m.member_no
inner join adult a on j.adult_member_no = a.member_no 
inner join loan l on l.member_no = m.member_no
WHERE l.isbn = @ISBN and
      l.copy_no = @CopyNum



GO
/****** Object:  StoredProcedure [dbo].[usp_GetMemberByMemberID]    Script Date: 03/16/2007 11:29:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************
*					GetMemberByMemberID
*
*  Gets the member information when a member ID is passed.
*
* Input Parms:
*	MemberID
***********************************************************/

CREATE proc [dbo].[usp_GetMemberByMemberID] 
(@MemberID smallint) 
AS
--Gets the member information from the member table
--and the juvenile or adult table based on if the member
--is a juvenile or adult.
SELECT	m.member_no,
		m.lastname,
		m.firstname,
		m.middleinitial,
		a.street,
		a.city,
		a.state,
		a.zip,
		a.phone_no,
		a.expr_date,
		NULL as birth_date,
		NULL as adult_member_no
FROM member m
inner join adult a on m.member_no = a.member_no
WHERE m.member_no = @MemberID
UNION ALL
SELECT m.member_no,
		m.lastname,
		m.firstname,
		m.middleinitial,
		a.street,
		a.city,
		a.state,
		a.zip,
		a.phone_no,
		a.expr_date,
		j.birth_date,
		j.adult_member_no
FROM member m
Inner join juvenile j on j.member_no = m.member_no
inner join adult a on j.adult_member_no = a.member_no 
WHERE m.member_no = @MemberID
		

GO
/****** Object:  StoredProcedure [dbo].[usp_item]    Script Date: 03/16/2007 11:29:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/**********************************************************
* usp_item
*  Gets and item from the database
**********************************************************/


CREATE proc [dbo].[usp_item] (@memberId smallint) as
SELECT  item.isbn, 
        item.title_no,
        item.translation,
        item.cover, 
        item.loanable,
        title.title_no,
        title.title,
        title.author,
        title.synopsis,
        loan.isbn, 
        loan.copy_no,
        loan.title_no,
        loan.member_no,
        loan.out_date,
        loan.due_date,
		copy.on_loan		
FROM  item
INNER JOIN title ON item.title_no = title.title_no
INNER JOIN loan ON item.isbn = loan.isbn
INNER JOIN copy on copy.isbn = item.isbn and
                   copy.copy_no = loan.copy_no
where loan.member_no = @memberId




GO
/****** Object:  StoredProcedure [dbo].[usp_RenewMember]    Script Date: 03/16/2007 11:29:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/******************************************************
* RenewMember
* Renews a member's expiration date.
******************************************************/

CREATE PROC [dbo].[usp_RenewMember] 
(@memberID smallint,
@ExpDte datetime output,
@errorCode int output) AS

BEGIN TRANSACTION
--Get an expiration date to use for later.
SELECT @ExpDte = dateadd(yy,1,getdate())

--Update the adult member's expiration date
UPDATE adult SET expr_date = @ExpDte
WHERE member_no = @memberID

--check error code
IF @@error <> 0
	BEGIN
		SELECT @errorCode = @@error
		ROLLBACK TRANSACTION
		RETURN @errorCode
	END

--it all worked if we got here, set the errorCode and commit.
SELECT @errorCode = @@error
COMMIT TRANSACTION
