/*
Add a primary key constraint to the remaining tables in the library database
including the title and item tables where PK constraints were added earlier in the lab.
There are a total on nine PK contraints created. One for each table in the library database.
If this is a rerun (and the constraint already exists), drop the constraint.
Use the library database and set NOCOUNT on to eliminate
the message indicating the number of rows affected.
*/

USE library
SET NOCOUNT ON
GO


/* Check if any Primary Key constraints exist. If so drop them */

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'member_ident'
      AND CONSTRAINT_TYPE = 'PRIMARY KEY')
  ALTER TABLE member
      DROP CONSTRAINT member_ident

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'adult_ident'
      AND CONSTRAINT_TYPE = 'PRIMARY KEY')
  ALTER TABLE adult
     DROP CONSTRAINT adult_ident

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'juvenile_ident'
      AND CONSTRAINT_TYPE = 'PRIMARY KEY')
  ALTER TABLE juvenile
     DROP CONSTRAINT juvenile_ident

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'title_ident'
      AND CONSTRAINT_TYPE = 'PRIMARY KEY')
  ALTER TABLE title
     DROP CONSTRAINT title_ident

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'item_ident'
      AND CONSTRAINT_TYPE = 'PRIMARY KEY')
  ALTER TABLE item
     DROP CONSTRAINT item_ident

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'copy_ident'
      AND CONSTRAINT_TYPE = 'PRIMARY KEY')
  ALTER TABLE copy
     DROP CONSTRAINT copy_ident

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'loan_ident'
      AND CONSTRAINT_TYPE = 'PRIMARY KEY')
  ALTER TABLE loan
     DROP CONSTRAINT loan_ident

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'reservation_ident'
      AND CONSTRAINT_TYPE = 'PRIMARY KEY')
  ALTER TABLE reservation
     DROP CONSTRAINT reservation_ident

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    where CONSTRAINT_SCHEMA = 'dbo'
      AND CONSTRAINT_NAME = 'loanhist_ident'
      AND CONSTRAINT_TYPE = 'PRIMARY KEY')
  ALTER TABLE loanhist
     DROP CONSTRAINT loanhist_ident
GO

/* Create all of the PK constraints */

ALTER TABLE member
   ADD CONSTRAINT member_ident PRIMARY KEY NONCLUSTERED (member_no) WITH FILLFACTOR = 90

ALTER TABLE adult
   ADD CONSTRAINT adult_ident      PRIMARY KEY    CLUSTERED (member_no) WITH FILLFACTOR = 90

ALTER TABLE juvenile
   ADD CONSTRAINT juvenile_ident   PRIMARY KEY NONCLUSTERED (member_no) WITH FILLFACTOR = 95

ALTER TABLE title
   ADD CONSTRAINT title_ident        PRIMARY KEY NONCLUSTERED (title_no)

ALTER TABLE item
   ADD CONSTRAINT item_ident       PRIMARY KEY NONCLUSTERED (isbn)

ALTER TABLE copy
   ADD CONSTRAINT copy_ident      PRIMARY KEY NONCLUSTERED (isbn, copy_no)

ALTER TABLE loan
   ADD CONSTRAINT loan_ident        PRIMARY KEY NONCLUSTERED (isbn, copy_no) WITH FILLFACTOR = 75

ALTER TABLE reservation
   ADD CONSTRAINT reservation_ident PRIMARY KEY NONCLUSTERED (member_no, isbn)

ALTER TABLE loanhist
   ADD CONSTRAINT loanhist_ident    PRIMARY KEY    CLUSTERED (isbn, copy_no, out_date)
GO


/* Reset the NOCOUNT option */

SET NOCOUNT OFF
GO

/* Select all the PK constraints that exist in the Library database */

PRINT 'PRIMARY KEYS CREATED:'
SELECT TABLE_NAME, CONSTRAINT_NAME
  FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
  WHERE CONSTRAINT_TYPE = 'PRIMARY KEY'
