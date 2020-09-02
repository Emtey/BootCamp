/*
This script file creates all of the tables for the Library database.
There are a total of nine tables.
Tables are dropped first in case this is a re-creation.
*/

USE library

IF OBJECT_ID('dbo.member') IS NOT NULL
    DROP TABLE dbo.member

IF OBJECT_ID('dbo.adult') IS NOT NULL
    DROP TABLE dbo.adult

IF OBJECT_ID('dbo.juvenile') IS NOT NULL
    DROP TABLE dbo.juvenile

IF OBJECT_ID('dbo.title') IS NOT NULL
    DROP TABLE dbo.title

IF OBJECT_ID('dbo.item') IS NOT NULL
    DROP TABLE dbo.item

IF OBJECT_ID('dbo.copy') IS NOT NULL
    DROP TABLE dbo.copy

IF OBJECT_ID('dbo.reservation') IS NOT NULL
    DROP TABLE dbo.reservation

IF OBJECT_ID('dbo.loan') IS NOT NULL
    DROP TABLE dbo.loan

IF OBJECT_ID('dbo.loanhist') IS NOT NULL
    DROP TABLE dbo.loanhist
GO

CREATE TABLE member
  (
     member_no        member_no IDENTITY(1,1)   NOT NULL
  ,  lastname         shortstring  NOT NULL 
  ,  firstname        shortstring  NOT NULL 
  ,  middleinitial    letter           NULL
  ,  photograph       image            NULL
  )

CREATE TABLE adult
  (
     member_no        member_no    NOT NULL
  ,  street           shortstring  NOT NULL
  ,  city             shortstring  NOT NULL
  ,  state            statecode    NOT NULL
  ,  zip              zipcode      NOT NULL
  ,  phone_no         phonenumber      NULL
  ,  expr_date        datetime     NOT NULL
  )

CREATE TABLE juvenile
  (
     member_no        member_no    NOT NULL
  ,  adult_member_no  member_no    NOT NULL
  ,  birth_date       datetime     NOT NULL
  )

CREATE TABLE title
  (
     title_no         title_no     IDENTITY(1,1) NOT NULL
  ,  title            longstring   NOT NULL
  ,  author           normstring   NOT NULL
  ,  synopsis         text             NULL
  )

CREATE TABLE item
  (
     isbn             isbn         NOT NULL
  ,  title_no         title_no     NOT NULL
  ,  translation      item_info        NULL
  ,  cover            item_info        NULL
  ,  loanable         yes_no           NULL
  )
-- You may notice that SQL Server Query Analyzer displays all references to the translation
-- column in the item table as keywords-this occurs because TRANSLATION is an ODBC 
-- keyword. Any scripts that reference the translation column can be executed, because 
-- TRANSLATION is not a Transact-SQL keyword.

CREATE TABLE copy
  (
     isbn             isbn         NOT NULL
  ,  copy_no          smallint     NOT NULL
  ,  title_no         title_no     NOT NULL
  ,  on_loan          yes_no       NOT NULL
  )

CREATE TABLE reservation
  (
     isbn             isbn         NOT NULL
  ,  member_no        member_no    NOT NULL
  ,  log_date         datetime         NULL
  ,  remarks          remarks          NULL
  )

CREATE TABLE loan
  (
     isbn             isbn         NOT NULL
  ,  copy_no          smallint     NOT NULL
  ,  title_no         title_no     NOT NULL
  ,  member_no        member_no    NOT NULL
  ,  out_date         datetime     NOT NULL
  ,  due_date         datetime     NOT NULL
  )

CREATE TABLE loanhist
  (
     isbn             isbn         NOT NULL
  ,  copy_no          smallint     NOT NULL
  ,  out_date         datetime     NOT NULL
  ,  title_no         title_no     NOT NULL
  ,  member_no        member_no    NOT NULL
  ,  due_date         datetime         NULL
  ,  in_date          datetime         NULL
  ,  fine_assessed    money            NULL
  ,  fine_paid        money            NULL
  ,  fine_waived      money            NULL
  ,  remarks          remarks          NULL
  )

GO

/* Display results */

SELECT table_name
  FROM information_schema.tables
  WHERE table_name IN (  'member'
                       , 'adult'
                       , 'juvenile'
                       , 'title'
                       , 'item'
                       , 'copy'
                       , 'reservation'
                       , 'loan'
                       , 'loanhist'
                      )
GO

BACKUP LOG library WITH TRUNCATE_ONLY
GO
