/* 
Creates the Library database.
Sizes are specified in megabytes.
*/

USE master


/*  Drop the Library Database if it already exists  */
IF DB_ID('library') IS NOT NULL
BEGIN
    DROP DATABASE library
END


/*  Create the Database  */
CREATE DATABASE library ON PRIMARY
 (
  NAME=library_data, 
  FILENAME='C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\library.mdf', 
  SIZE=50MB,
  MAXSIZE=70MB,
  FILEGROWTH=1MB
  )
LOG ON 
  (
  NAME=library_log, 
  FILENAME='C:\Program Files\Microsoft SQL Server\MSSQL.1\MSSQL\Data\library.ldf', 
  SIZE=15MB,
  MAXSIZE=20MB,
  FILEGROWTH=1MB
  )

GO
