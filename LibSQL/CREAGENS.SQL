/*
Build and load the interim tables that are used in generating
sample data.
These tables are not part of the database, they are just used for 
loading the sample data into the database.
Sample data for the entire database is generated in LOADDATA.SQL.  
In LOADDATA the TITLE table is loaded with sample data, all other 
tables are loaded by doing joins involving tables created by
this script, creatgens.sql.
*/

USE library

/* If the objects already exist (i.e. if this is a rebuild), drop them. */

IF OBJECT_ID('integer_table') IS NOT NULL
    DROP TABLE integer_table

IF OBJECT_ID('lastname_table') IS NOT NULL
    DROP TABLE lastname_table

IF OBJECT_ID('firstname_table') IS NOT NULL
    DROP TABLE firstname_table

IF OBJECT_ID('street_table') IS NOT NULL
    DROP TABLE street_table

IF OBJECT_ID('address_table') IS NOT NULL
    DROP TABLE address_table

IF OBJECT_ID('letter_table') IS NOT NULL
    DROP TABLE letter_table

IF OBJECT_ID('date_table') IS NOT NULL
    DROP TABLE date_table

IF OBJECT_ID('language_table') IS NOT NULL
    DROP TABLE language_table

GO


 /*  Will have one row each for integers 1 thru 100.  */
 /*  Used to generate up to 100 copies of each item.  */

CREATE TABLE integer_table
  (
  integer          INT          NOT NULL	identity(1,1)
  )

CREATE TABLE lastname_table   -- Will have 26 last names.
  (
  integer          INT          NOT NULL,
  lastname         VARCHAR(15)  NOT NULL 
  )

CREATE TABLE firstname_table  -- Will have 26 first names.
  (
  integer          INT          NOT NULL,
  firstname        VARCHAR(15)  NOT NULL 
  )

CREATE TABLE letter_table   --  Will have 26 initials. First letter of each last name.
  (
  integer          INT          NOT NULL,
  letter           CHAR(1)      NOT NULL
  )

CREATE TABLE street_table   --  Will have 23 streets.
  (
  integer          INT          NOT NULL,
  street           VARCHAR(18)  NOT NULL
  )

CREATE TABLE address_table  -- Will have 23 city / state / zips.
  (
  integer          INT          NOT NULL,
  city             VARCHAR(15)  NOT NULL,
  state            CHAR(2)      NOT NULL,
  zip              CHAR(10)     NOT NULL
  )

CREATE TABLE date_table     --  Will have 12 dates.
  (
  integer          INT          NOT NULL,
  date             DATETIME     NOT NULL
  )

CREATE TABLE language_table  -- Will have 10 languages.
  (
  integer          INT          NOT NULL,
  descr            CHAR(8)      NOT NULL
  )
GO

/* Display the result */

SELECT name
  FROM sysobjects
  WHERE type = 'U'
  AND name IN ( 'integer_table',
                'lastname_table',
                'firstname_table',
                'street_table',
                'address_table',
                'letter_table',
                'date_table',
                'language_table' ) 
  ORDER BY name
GO


/* Load lastname_table */

INSERT lastname_table     VALUES     (   1,   'Anderson')
INSERT lastname_table     VALUES     (   2,   'Barr')
INSERT lastname_table     VALUES     (   3,   'Brooke')
INSERT lastname_table     VALUES     (   4,   'Chen')
INSERT lastname_table     VALUES     (   5,   'Durkin')
INSERT lastname_table     VALUES     (   6,   'Erickson')
INSERT lastname_table     VALUES     (   7,   'Funk')
INSERT lastname_table     VALUES     (   8,   'Graff')
INSERT lastname_table     VALUES     (   9,   'Harui')
INSERT lastname_table     VALUES     (  10,   'Hightower')
INSERT lastname_table     VALUES     (  11,   'Jones')
INSERT lastname_table     VALUES     (  12,   'Kilwine')
INSERT lastname_table     VALUES     (  13,   'LaBrie')
INSERT lastname_table     VALUES     (  14,   'Martin')
INSERT lastname_table     VALUES     (  15,   'Miksovsky')
INSERT lastname_table     VALUES     (  16,   'Nash')
INSERT lastname_table     VALUES     (  17,   'O''Brian')
INSERT lastname_table     VALUES     (  18,   'Perry')
INSERT lastname_table     VALUES     (  19,   'Rothenberg')
INSERT lastname_table     VALUES     (  20,   'Rudd')
INSERT lastname_table     VALUES     (  21,   'Sherman')
INSERT lastname_table     VALUES     (  22,   'Smith')
INSERT lastname_table     VALUES     (  23,   'Thomas')
INSERT lastname_table     VALUES     (  24,   'Valentine')
INSERT lastname_table     VALUES     (  25,   'Wolfe-Hellene')
INSERT lastname_table     VALUES     (  26,   'Young')
GO


/* Load firstname_table */

INSERT firstname_table    VALUES     (   1,   'Amy')
INSERT firstname_table    VALUES     (   2,   'Angela')
INSERT firstname_table    VALUES     (   3,   'Brian')
INSERT firstname_table    VALUES     (   4,   'Clair')
INSERT firstname_table    VALUES     (   5,   'Daniel')
INSERT firstname_table    VALUES     (   6,   'Darlene')
INSERT firstname_table    VALUES     (   7,   'Eva')
INSERT firstname_table    VALUES     (   8,   'Frank')
INSERT firstname_table    VALUES     (   9,   'Gary')
INSERT firstname_table    VALUES     (  10,   'Jose')
INSERT firstname_table    VALUES     (  11,   'Joshua')
INSERT firstname_table    VALUES     (  12,   'Karl')
INSERT firstname_table    VALUES     (  13,   'Katie')
INSERT firstname_table    VALUES     (  14,   'Linda')
INSERT firstname_table    VALUES     (  15,   'Mary Anne')
INSERT firstname_table    VALUES     (  16,   'Michael')
INSERT firstname_table    VALUES     (  17,   'Monica')
INSERT firstname_table    VALUES     (  18,   'Nate')
INSERT firstname_table    VALUES     (  19,   'Peter')
INSERT firstname_table    VALUES     (  20,   'Robert')
INSERT firstname_table    VALUES     (  21,   'Sarah')
INSERT firstname_table    VALUES     (  22,   'Shelly')
INSERT firstname_table    VALUES     (  23,   'Stephen')
INSERT firstname_table    VALUES     (  24,   'Teresa')
INSERT firstname_table    VALUES     (  25,   'Thom')
INSERT firstname_table    VALUES     (  26,   'William')
GO


/* Load letter_table */

INSERT letter_table    -- Column Letter is char(1), so lastname is truncated.
  SELECT integer, SUBSTRING(lastname,1,1) FROM lastname_table
GO


/*  Load street_table */

INSERT street_table    VALUES     (   1,   'Apple Lane')
INSERT street_table    VALUES     (   2,   'Bowery Estates')
INSERT street_table    VALUES     (   3,   'Cannery Row')
INSERT street_table    VALUES     (   4,   'Dogwood Drive')
INSERT street_table    VALUES     (   5,   'Elm Street')
INSERT street_table    VALUES     (   6,   'Fir Street')
INSERT street_table    VALUES     (   7,   'Goshen Road')
INSERT street_table    VALUES     (   8,   'The Highlands')
INSERT street_table    VALUES     (   9,   'Island Drive')
INSERT street_table    VALUES     (  10,   'James Road')
INSERT street_table    VALUES     (  11,   'King Street')
INSERT street_table    VALUES     (  12,   'Larch Mont Road')
INSERT street_table    VALUES     (  13,   'Maple Street')
INSERT street_table    VALUES     (  14,   'New Pike Road')
INSERT street_table    VALUES     (  15,   'Oak Drive')
INSERT street_table    VALUES     (  16,   'Pine Place')
INSERT street_table    VALUES     (  17,   'Queen Ann Drive')
INSERT street_table    VALUES     (  18,   'Race Track Road')
INSERT street_table    VALUES     (  19,   'Stoney Way')
INSERT street_table    VALUES     (  20,   'Tamarack Road')
INSERT street_table    VALUES     (  21,   'University Lane')
INSERT street_table    VALUES     (  22,   'Valley Road')
INSERT street_table    VALUES     (  23,   'Willow Road')
GO


/* Load address_table */

INSERT address_table    VALUES     (   1,   'Juneau'        , 'AK', '99801')
INSERT address_table    VALUES     (   2,   'Montgomery'    , 'AL', '36100')
INSERT address_table    VALUES     (   3,   'Phoenix'       , 'AZ', '85011')
INSERT address_table    VALUES     (   4,   'Sacramento'    , 'CA', '94203')
INSERT address_table    VALUES     (   5,   'Denver'        , 'CO', '80201')
INSERT address_table    VALUES     (   6,   'Washington'    , 'DC', '20510-0001')
INSERT address_table    VALUES     (   7,   'Tallahassee'   , 'FL', '32301')
INSERT address_table    VALUES     (   8,   'Atlanta'       , 'GA', '30026')
INSERT address_table    VALUES     (   9,   'Honolulu'      , 'HI', '96800-3017')
INSERT address_table    VALUES     (  10,   'Springfield'   , 'IL', '62700')
INSERT address_table    VALUES     (  11,   'Baton Rouge'   , 'LA', '70800')
INSERT address_table    VALUES     (  12,   'Boston'        , 'MA', '02138-7190')
INSERT address_table    VALUES     (  13,   'Annapolis'     , 'MD', '21400')
INSERT address_table    VALUES     (  14,   'Trenton'       , 'NJ', '08600')
INSERT address_table    VALUES     (  15,   'Carson City'   , 'NV', '89701')
INSERT address_table    VALUES     (  16,   'Albany'        , 'NY', '12200-2837')
INSERT address_table    VALUES     (  17,   'Salem'         , 'OR', '97301')
INSERT address_table    VALUES     (  18,   'Harrisburg'    , 'PA', '17100')
INSERT address_table    VALUES     (  19,   'Austin'        , 'TX', '73301')
INSERT address_table    VALUES     (  20,   'Salt Lake City', 'UT', '84100')
INSERT address_table    VALUES     (  21,   'Richmond'      , 'VA', '23173-1823')
INSERT address_table    VALUES     (  22,   'Olympia'       , 'WA', '98507')
INSERT address_table    VALUES     (  23,   'Charleston'    , 'WV', '25300')
GO


/*  Load date_table */

INSERT date_table       VALUES     (   1,   '01/01/98'  )
INSERT date_table       VALUES     (   2,   '02/02/98'  )
INSERT date_table       VALUES     (   3,   '03/03/98'  )
INSERT date_table       VALUES     (   4,   '04/04/98'  )
INSERT date_table       VALUES     (   5,   '05/05/98'  )
INSERT date_table       VALUES     (   6,   '06/06/98'  )
INSERT date_table       VALUES     (   7,   '07/07/98'  )
INSERT date_table       VALUES     (   8,   '08/08/98'  )
INSERT date_table       VALUES     (   9,   '09/09/98'  )
INSERT date_table       VALUES     (  10,   '10/10/98'  )
INSERT date_table       VALUES     (  11,   '11/11/98'  )
INSERT date_table       VALUES     (  12,   '12/12/98'  )
GO


/* Load language_table */

INSERT language_table     VALUES     (  1,   'ARABIC'    )
INSERT language_table     VALUES     (  2,   'CHINESE'   )
INSERT language_table     VALUES     (  3,   'ENGLISH'   )
INSERT language_table     VALUES     (  4,   'FRENCH'    )
INSERT language_table     VALUES     (  5,   'GERMAN'    )
INSERT language_table     VALUES     (  6,   'JAPANESE'  )
INSERT language_table     VALUES     (  7,   'KOREAN'    )
INSERT language_table     VALUES     (  8,   'LATIN'     )
INSERT language_table     VALUES     (  9,   'PORTUGES'  )
INSERT language_table     VALUES     ( 10,   'SPANISH'   )
GO


/* Load integer_table */

/*
There are two methods to load a table with incrementing
values. One method is to use the code in the commented
section below that uses a WHILE loop combined using
SELECT MAX(col) which loops through and incremments a
count that does multi row inserts into the table.
***************************************************
**   INSERT integer_table VALUES (1)  -- Initialize starting value.
**
**    WHILE (SELECT MAX(integer) FROM integer_table) < 100
**    BEGIN
**    INSERT integer_table
**        SELECT integer + (SELECT MAX(integer) FROM integer_table)
**        FROM integer_table
**    END
*****************************************************************************
The second method also uses a WHILE loop but instead of
coding the increment routine you can use the IDENTITY
property combined with the key words "DEFAULT VALUES"
used in the INSERT statement. This does require that
your table.column be defined with the IDENTITY property.
The first method, shown above, is faster but more complex to
write. The second method, used below, is simpler to write and
read, but slower. And without the BEGIN/COMMIT TRANSACTION it
is an additional 33% slower.
*******************************************************************************
**    BEGIN TRANSACTION
**    WHILE @@identity < 100
**       OR (SELECT MAX(integer) FROM integer_table) IS NULL
**      BEGIN
**         INSERT integer_table DEFAULT VALUES
**      END
**    COMMIT TRANSACTION
**
**    DELETE FROM integer_table
**       WHERE integer > 100
**   ****************************************************************
*/

BEGIN TRANSACTION
    WHILE @@identity < 100
       OR (SELECT MAX(integer) FROM integer_table) IS NULL
      BEGIN
         INSERT integer_table DEFAULT VALUES
      END
    COMMIT TRANSACTION

    DELETE FROM integer_table
       WHERE integer > 100

/* Verify results */

SELECT * FROM integer_table
SELECT * FROM lastname_table
SELECT * FROM firstname_table
SELECT * FROM letter_table
SELECT * FROM street_table
SELECT * FROM address_table
SELECT * FROM date_table
SELECT * FROM language_table
GO
