/*
Creates all the user defined data types for the Library database
*/


USE library


IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'member_no')
    EXEC  sp_droptype  member_no

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'isbn')
     EXEC  sp_droptype  isbn

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'title_no')
     EXEC  sp_droptype  title_no

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'letter')
     EXEC  sp_droptype  letter

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'item_info')
     EXEC  sp_droptype  item_info

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'phonenumber')
    EXEC  sp_droptype  phonenumber

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'yes_no')
     EXEC  sp_droptype  yes_no

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'statecode')
     EXEC  sp_droptype  statecode

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'zipcode')
    EXEC  sp_droptype  zipcode

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'shortstring')
    EXEC  sp_droptype  shortstring

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'normstring')
     EXEC  sp_droptype  normstring

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'longstring')
     EXEC  sp_droptype  longstring

IF EXISTS (SELECT domain_name FROM information_schema.domains
           WHERE domain_schema = 'dbo' AND domain_name = 'remarks')
     EXEC  sp_droptype  remarks
GO

EXEC  sp_addtype  member_no,   'smallint'
EXEC  sp_addtype  isbn,        'int'
EXEC  sp_addtype  title_no,    'int'
EXEC  sp_addtype  letter,      'char(1)'
EXEC  sp_addtype  item_info,   'char(8)'
EXEC  sp_addtype  phonenumber, 'char(13)', 'NULL'
EXEC  sp_addtype  yes_no,      'char(1)'
EXEC  sp_addtype  statecode,   'char(2)'
EXEC  sp_addtype  zipcode,     'char(10)'
EXEC  sp_addtype  shortstring, 'varchar(15)'
EXEC  sp_addtype  normstring,  'varchar(31)'
EXEC  sp_addtype  longstring,  'varchar(63)'
EXEC  sp_addtype  remarks,     'varchar(255)'
GO

/* Display results */

SELECT domain_name
   FROM information_schema.domains
   ORDER BY domain_name
GO
