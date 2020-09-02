

USE library
GO

/* Drop the constraint if it already exists */

IF EXISTS 
   (SELECT * FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS  
    where CONSTRAINT_NAME = 'phone_no_rule')
  ALTER TABLE adult
     DROP CONSTRAINT phone_no_rule
GO

