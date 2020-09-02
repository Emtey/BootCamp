/*
Adds a DEFAULT constraint of "NY" for New York
to the state column in the adult table.
*/

USE library

/* Drop the constraint if it already exists */


IF OBJECT_ID('state_default') IS NOT NULL
  ALTER TABLE adult
     DROP CONSTRAINT state_default
GO

/* Add the constraint */

ALTER TABLE adult
   ADD CONSTRAINT state_default DEFAULT 'NY' FOR state
GO
