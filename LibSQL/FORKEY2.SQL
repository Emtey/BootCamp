/*
Add the foreign key constraints to the library database.  
If this is a rerun (and the constraint already exists), first drop the constraint.
For the purposes of this lab exercise, the WITH NOCHECK option
is used to improve the performance of this script.
Set the library database for database owner use only.
*/

USE master
EXEC sp_dboption library, 'dbo use only', TRUE
GO

/* 
Use the library database and set NOCOUNT on to eliminate
the message indicating the number of rows affected.
*/

USE library
SET NOCOUNT ON
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'adult_member_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE adult
     DROP CONSTRAINT adult_member_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'juvenile_member_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE juvenile
     DROP CONSTRAINT juvenile_member_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'juvenile_adult_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE juvenile
     DROP CONSTRAINT juvenile_adult_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'copy_item_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE copy
     DROP CONSTRAINT copy_item_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'copy_title_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE copy
     DROP CONSTRAINT copy_title_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'loan_copy_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE loan
     DROP CONSTRAINT loan_copy_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'loan_member_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE loan
     DROP CONSTRAINT loan_member_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'loan_title_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE loan
     DROP CONSTRAINT loan_title_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'loanhist_copy_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE loanhist
     DROP CONSTRAINT loanhist_copy_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'loanhist_title_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE loanhist
     DROP CONSTRAINT loanhist_title_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'reservation_item_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE reservation
     DROP CONSTRAINT reservation_item_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'reservation_member_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE reservation
     DROP CONSTRAINT reservation_member_link
GO

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'item_title_link'
      AND CONSTRAINT_TYPE = 'FOREIGN KEY')
  ALTER TABLE item
     DROP CONSTRAINT item_title_link
GO

/* Create Foreign Key Constraints for the Adult table */

ALTER TABLE adult WITH NOCHECK
   ADD CONSTRAINT adult_member_link FOREIGN KEY (member_no) REFERENCES member (member_no)
GO

/* Create Foreign Key Constraints for the Juvenile table */

ALTER TABLE juvenile WITH NOCHECK
   ADD CONSTRAINT juvenile_member_link FOREIGN KEY (member_no) REFERENCES member (member_no)

ALTER TABLE juvenile WITH NOCHECK
   ADD CONSTRAINT juvenile_adult_link  FOREIGN KEY (adult_member_no) REFERENCES adult (member_no)
GO

/* Create Foreign Key Constraints for the Copy table */

ALTER TABLE copy WITH NOCHECK
   ADD CONSTRAINT copy_item_link  FOREIGN KEY (isbn) REFERENCES item (isbn)

ALTER TABLE copy WITH NOCHECK
   ADD CONSTRAINT copy_title_link FOREIGN KEY (title_no) REFERENCES title (title_no)
GO

/* Create Foreign Key Constraints for the Loan table */

ALTER TABLE loan WITH NOCHECK
   ADD CONSTRAINT loan_copy_link  FOREIGN KEY (isbn, copy_no) REFERENCES copy (isbn, copy_no)

ALTER TABLE loan WITH NOCHECK
  ADD CONSTRAINT loan_member_link FOREIGN KEY (member_no) REFERENCES member (member_no)

ALTER TABLE loan WITH NOCHECK
  ADD CONSTRAINT loan_title_link  FOREIGN KEY (title_no) REFERENCES title (title_no)
GO

/* Create Foreign Key Constraints for the Loanhist table */

/*
Note that a Foreign key constraint is not created on the member_no
column. In this situation it may not be necessary to maintain
referencial integrity between the loanhist and member table because
the source data comes from the loan table and the loan table
already maintains referencial integrity.
This has been done intentionally so that a member can be deleted
without having to delete the member history information.
There are other ways to address this scenario. For example, you could
use an inactive member table instead of deleting the member.
*/

ALTER TABLE loanhist WITH NOCHECK
  ADD CONSTRAINT loanhist_copy_link   FOREIGN KEY (isbn, copy_no) REFERENCES copy (isbn, copy_no)

ALTER TABLE loanhist WITH NOCHECK
  ADD CONSTRAINT loanhist_title_link  FOREIGN KEY (title_no) REFERENCES title (title_no)
GO

/* Create Foreign Key Constraints for the Reservation table */

ALTER TABLE reservation WITH NOCHECK
  ADD CONSTRAINT reservation_item_link   FOREIGN KEY (isbn) REFERENCES item (isbn)

ALTER TABLE reservation WITH NOCHECK
  ADD CONSTRAINT reservation_member_link FOREIGN KEY (member_no) REFERENCES member (member_no)
GO

/* Create Foreign Key Constraint for the Item table */
ALTER TABLE item WITH NOCHECK
  ADD CONSTRAINT item_title_link FOREIGN KEY (title_no) REFERENCES title (title_no)
GO


/* Reset the database option and NOCOUNT */

USE master
EXEC sp_dboption library, 'dbo use only', FALSE
GO

USE library
SET NOCOUNT ON
GO

/* Select all the FK constraints that exist in the Library database */

PRINT 'FOREIGN KEYS CREATED:'
SELECT TABLE_NAME, CONSTRAINT_NAME
  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
  WHERE CONSTRAINT_TYPE = 'FOREIGN KEY'
