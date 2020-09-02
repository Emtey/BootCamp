@echo OFF
del bldlib.log
cls

echo ...Creating the Library database
osql /E /n /i creabase.sql >> bldlib.log

echo ...Creating user-defined datatypes in the Library database
osql /E /n /i creatyp3.sql >> bldlib.log

echo ...Creating tables in the Library database
osql /E /n /i creatab3.sql >> bldlib.log

echo ...Building temporary tables used to load the Library database
osql /E /n /i creagens.sql >> bldlib.log

echo ...Loading data into the Library database
osql /E /n /i loaddata.sql >> bldlib.log

echo ...Deleting temporary tables from the Library database
osql /E /n /i dropgens.sql >> bldlib.log

echo ...Creating Primary Key Constraints in the Library database
osql /E /n /i prikey2.sql >> bldlib.log

echo ...Creating Foreign Key Constraints in the Library database
osql /E /n /i forkey2.sql >> bldlib.log

echo ...Creating Check Constraint in the Library database
osql /E /n /i chkconst.sql >> bldlib.log

echo ...Creating Default Constraint in the Library database
osql /E /n /i defconst.sql >> bldlib.log

echo ...Creating Indexes in the Library database
osql /E /n /i creaind3.sql >> bldlib.log

echo ...Done creating the Library database.  Log file = bldlib.log.
