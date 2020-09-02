/*
**       CREATINDX3.SQL
**  
**  Create all Foreign Key indexes.
**
**  Drop the old indexes first.
**
**  All tables that are updated during the normal working day will be
**      given a FILLFACTOR.  Moderately volatile tables will have enough
**      free space for one new row.  Volatile tables, such as Loan, will
**      be given more free space.  Relatively static tables will be given
**      none.
**  
**  Don't forget to say 'USE library' first.
*/

USE library
SET NOCOUNT ON
GO

/*    
**  If the objects already exist (i.e. if this is a rebuild), drop them.
*/

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'juvenile_member_link')
    DROP INDEX  juvenile.juvenile_member_link
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'item_title_link')
    DROP INDEX  item.item_title_link
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'copy_title_link')
    DROP INDEX  copy.copy_title_link
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'loan_title_link')
    DROP INDEX  loan.loan_title_link
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'loan_member_link')
    DROP INDEX  loan.loan_member_link
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'reserve_item_link')
    DROP INDEX  reservation.reserve_item_link
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'loanhist_member_link')
    DROP INDEX  loanhist.loanhist_member_link
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'loanhist_title_link')
    DROP INDEX  loanhist.loanhist_title_link
GO

DUMP TRANSACTION library WITH TRUNCATE_ONLY
GO

/***************************  Create the indexes. ***************************/

/******  Member related indexes.  ******/

DECLARE @message char(255)  DECLARE @began datetime
SELECT @began = GETDATE()

CREATE	NONCLUSTERED INDEX juvenile_member_link ON juvenile (adult_member_no)

SELECT @message = 'Time (in minutes:seconds) to create Member related indexes.  '
     + CONVERT( char(2), DATEDIFF(ss,@began,GETDATE())/60 ) + ':'
     + CONVERT( char(2), DATEDIFF(ss,@began,GETDATE())%60 )
PRINT @message
GO

/******  Book related indexes.  ******/

DECLARE @message char(255)  DECLARE @began datetime  SELECT @began = GETDATE()

CREATE	   CLUSTERED INDEX item_title_link   ON item        (title_no)
CREATE     CLUSTERED INDEX copy_title_link   ON copy        (title_no)
CREATE     CLUSTERED INDEX loan_title_link   ON loan        (title_no)
CREATE  NONCLUSTERED INDEX loan_member_link  ON loan        (member_no)
    WITH FILLFACTOR = 75
CREATE     CLUSTERED INDEX reserve_item_link ON reservation (isbn)

SELECT @message = 'Time (in minutes:seconds) to create Book related indexes.  '
     + CONVERT( char(2), DATEDIFF(ss,@began,GETDATE())/60 ) + ':'
     + CONVERT( char(2), DATEDIFF(ss,@began,GETDATE())%60 )
PRINT @message
GO

DUMP TRANSACTION library WITH TRUNCATE_ONLY
GO


/******  Loan History related indexes.  ******/

DECLARE @message char(255)  DECLARE @began datetime  SELECT @began = GETDATE()

CREATE	NONCLUSTERED INDEX loanhist_member_link ON loanhist (member_no)
CREATE	NONCLUSTERED INDEX loanhist_title_link  ON loanhist (title_no)

SELECT @message = 'Time (in minutes:seconds) to create Loan History related indexes.  '
     + CONVERT( char(2), DATEDIFF(ss,@began,GETDATE())/60 ) + ':'
     + CONVERT( char(2), DATEDIFF(ss,@began,GETDATE())%60 )
PRINT @message
GO


/***************  Display the results  ***************/

PRINT 'CREATED INDEXES:'
SELECT name FROM sysindexes
   WHERE name IN (  'juvenile_member_link'
                   , 'item_title_link'
                   , 'copy_title_link'
                   , 'loan_title_link'
                   , 'loan_member_link'
                   , 'reserve_item_link'
                   , 'loanhist_member_link'
                   , 'loanhist_title_link'
                 )


PRINT 'SPACE REQUIRED:'
EXEC  sp_spaceused
EXEC  sp_spaceused juvenile
EXEC  sp_spaceused item
EXEC  sp_spaceused copy
EXEC  sp_spaceused loan
EXEC  sp_spaceused reservation
EXEC  sp_spaceused loanhist
GO


/*  Truncate the log.  */

DUMP TRANSACTION library WITH TRUNCATE_ONLY
GO

SET NOCOUNT OFF
GO
