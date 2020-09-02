/*       
This script loads sample data for the Library database.
The CREAGENS.SQL script must be run prior to executing this script.
*/


USE library
SET NOCOUNT ON
GO

/*  Throw out the old data */

TRUNCATE TABLE member
TRUNCATE TABLE adult
TRUNCATE TABLE juvenile
TRUNCATE TABLE title
TRUNCATE TABLE item
TRUNCATE TABLE copy
TRUNCATE TABLE loan
TRUNCATE TABLE loanhist
TRUNCATE TABLE reservation
GO


/* Load member, adult, and juvenile */

DECLARE @message char(255),  -- Prepare to record the elapsed time.
        @began datetime

SET @began = getdate()

/*
One half of the members are selected as adults, and the other half as juveniles.
The adults are updated with random address info. This is one of the few times 
you will ever use a cartesian product.
*/

INSERT member (lastname, firstname, middleinitial)
  SELECT  L.lastname 
	, F.firstname
	, M.letter
  FROM lastname_table L CROSS JOIN firstname_table F CROSS JOIN letter_table M

BACKUP TRAN library WITH TRUNCATE_ONLY
DELETE FROM member
   WHERE member_no > 10000

DBCC CHECKIDENT ('member', RESEED, 10000)

BACKUP TRAN library WITH TRUNCATE_ONLY

INSERT adult
	SELECT member_no
	  , ' '
	  , ' '
	  , ' '
	  , ' '
	  , NULL
	  , dateadd (day, member_no%365, GETDATE())
	FROM member
	WHERE member_no > isnull((SELECT max(member_no) from adult),0)
	AND member_no%2 = 1

BACKUP TRAN library WITH TRUNCATE_ONLY

UPDATE adult
	SET street = street_table.street 
	  , city  = address_table.city 
	  , state = address_table.state
	  , zip   = address_table.zip
	FROM adult, street_table, address_table 
	WHERE street_table.integer = (member_no % (SELECT count(*) FROM street_table) ) + 1
	AND address_table.integer = (member_no % (SELECT count(*) FROM address_table) ) + 1
	AND adult.street = ' '

BACKUP TRAN library WITH TRUNCATE_ONLY

INSERT juvenile
	SELECT member_no
	  , CASE
		WHEN member_no <= 7124 THEN (member_no -1)
		WHEN member_no <= 8522 THEN (member_no -7125)
		WHEN member_no <= 9504 THEN (member_no -8523)
		ELSE (member_no -9505)
	    END
	  , birth_date = dateadd(day, (((member_no)%18)-18)*365, GETDATE())
	FROM member
	WHERE member_no > isnull((SELECT max(member_no) from juvenile),0)
	AND member_no%2 = 0

BACKUP TRAN library WITH TRUNCATE_ONLY

SET @message = 'Time (in minutes:seconds) to load Member/Adult/Juvenile.  ' + convert( char(2), datediff(ss,@began,getdate())/60 ) + ':' + convert( char(2), datediff(ss,@began,getdate())%60 )    PRINT @message
GO

/* Load title */

DECLARE @message char(255),   -- Prepare to record the elapsed time.
        @began datetime

SET @began = getdate()

/*  Title numbers will be consecutive, beginning with 1. */

INSERT title (title, author) VALUES ('Last of the Mohicans',                                'James Fenimore Cooper')
INSERT title (title, author) VALUES ('The Village Watch-Tower',                             'Kate Douglas Wiggin')
INSERT title (title, author) VALUES ('Self Help; Conduct & Perseverance',                   'Samuel Smiles')
INSERT title (title, author) VALUES ('Songs of a Savoyard',                                 'W. S. Gilbert')
INSERT title (title, author) VALUES ('Fall of the House of Usher',                          'Edgar Allen Poe')
INSERT title (title, author) VALUES ('The Cook''s Decameron',                               'Mrs. W. G. Water')
INSERT title (title, author) VALUES ('Poems',                                               'Wilfred Owen')
INSERT title (title, author) VALUES ('The Cherry Orchard',                                  'Anton Checkov')
INSERT title (title, author) VALUES ('Wayfarers',                                           'Knut Hamsun')
INSERT title (title, author) VALUES ('The Night-Born',                                      'Jack London')
INSERT title (title, author) VALUES ('Lemon',                                               'Motojirou')
INSERT title (title, author) VALUES ('Walking',                                             'Henry David Thoreau')
INSERT title (title, author) VALUES ('The Water-Babies',                                    'Charles Kingsley')
INSERT title (title, author) VALUES ('Improvement of Understanding',                        'Spinoza')
INSERT title (title, author) VALUES ('The Dictionary of the Khazars',                       'Milorad Pavic')
INSERT title (title, author) VALUES ('The First Men In The Moon',                           'H.G. Wells')
INSERT title (title, author) VALUES ('Ballads of a Bohemian',                               'Robert W. Service')
INSERT title (title, author) VALUES ('War and Peace',                                       'Lev Tolstoy')
INSERT title (title, author) VALUES ('The Phantom of the Opera',                            'Gaston Leroux')
INSERT title (title, author) VALUES ('The Unbearable Lightness of Being',                   'Milan Kundera')
INSERT title (title, author) VALUES ('Le Petit Prince',                                     'Antoine de Saint-Exupery')
INSERT title (title, author) VALUES ('The Master and Margarita',                            'Mikhael Bulgakov')
INSERT title (title, author) VALUES ('De La Terre a La Lune',                               'Jules Verne')
INSERT title (title, author) VALUES ('Tao Teh King',                                        'Lau-Tzu')
INSERT title (title, author) VALUES ('The Black Tulip',                                     'Alexandre Dumas')
INSERT title (title, author) VALUES ('The Adventures of Robin Hood',                        'Howard Pyle')
INSERT title (title, author) VALUES ('Lady Susan',                                          'Jane Austen')
INSERT title (title, author) VALUES ('The Voyage of the Beagle',                            'Charles Darwin')
INSERT title (title, author) VALUES ('Misalliance',                                         'George Bernard Shaw')
INSERT title (title, author) VALUES ('A Tale of Two Cities',                                'Charles Dickens')
INSERT title (title, author) VALUES ('Oliver Twist',                                        'Charles Dickens')
INSERT title (title, author) VALUES ('The Call of the Wild',                                'Jack London')
INSERT title (title, author) VALUES ('The First 100,000 Prime Numbers',                     'Unknown')
INSERT title (title, author) VALUES ('The Legend of Sleepy Hollow',                         'Irving, Washington')
INSERT title (title, author) VALUES ('The Return of Sherlock Holmes',                       'Sir Arthur Conan Doyle')
INSERT title (title, author) VALUES ('The Scarlet Letter',                                  'Nathaniel Hawthorne')
INSERT title (title, author) VALUES ('Treasure Island',                                     'Robert Louis Stevenson')
INSERT title (title, author) VALUES ('Les Miserables',                                      'Victor Marie Hugo')
INSERT title (title, author) VALUES ('Moby Dick',                                           'Herman Melville')
INSERT title (title, author) VALUES ('Moll Flanders',                                       'Daniel Defoe')
INSERT title (title, author) VALUES ('Sense and Sensibility',                               'Jane Austen')
INSERT title (title, author) VALUES ('Vanity Fair',                                         'William Makepeace Thackeray')
INSERT title (title, author) VALUES ('Emma',                                                'Jane Austen')
INSERT title (title, author) VALUES ('Adventures of Huckleberry Finn',                      'Mark Twain')
INSERT title (title, author) VALUES ('Candide',                                             'Voltaire')
INSERT title (title, author) VALUES ('The Complete Works of William Shakespeare',           'William Shakespeare')
INSERT title (title, author) VALUES ('The Crossing',                                        'Winston Churchill')
INSERT title (title, author) VALUES ('History of the Decline and Fall of the Roman Empire', 'Edward Gibbon')
INSERT title (title, author) VALUES ('Julius Caesar''s Commentaries on the Gallic War',     'Julius Caesar')
INSERT title (title, author) VALUES ('Frankenstein',                                        'Mary Wollstonecraft Shelley')


/*
Load item.
Every title (50), every language (10), both covers(2).
Therefore, there will be 1000 items.
ISBNs will be consecutive, beginning with 1.
*/

INSERT item
   SELECT ((title.title_no-1)*10)+language_table.integer
     ,   title.title_no
     ,   language_table.descr
     ,   'HARDBACK'
     ,   'Y'
   FROM title, language_table

INSERT item
   SELECT ((title.title_no-1)*10)+language_table.integer+500
     , title.title_no
     , language_table.descr
     , 'SOFTBACK'
     , 'Y'
   FROM title, language_table

/*  Randomly make a small portion of  the hard cover books non-loanable.    */

UPDATE item
   SET loanable = 'N'
   WHERE (isbn*title_no) = 0
   AND cover = 'HARDBACK'

BACKUP TRAN library WITH TRUNCATE_ONLY

/*
Load copy.
Adjust the number of copies here. There will be MAX_COPIES
copies of each item. There are 1000 items in the database.
The maximum that MAX_COPIES can be is 100.
*/

DECLARE @max_copies int
SET     @max_copies = 10

INSERT copy
   SELECT isbn, integer, title_no, 'N' 
   FROM item, integer_table
   WHERE integer <= @max_copies

BACKUP TRAN library WITH TRUNCATE_ONLY
SET @message = 'Time (in minutes:seconds) to load Title/Item/Copy.  ' + convert( char(2), datediff(ss,@began,getdate())/60 ) + ':' + convert( char(2), datediff(ss,@began,getdate())%60 )    PRINT @message
GO

/*
Randomly loan out twenty percent of the copies.  For performance
reasons, first mark the copies as loaned then loan them out.
*/

DECLARE @message char(255),   -- Prepare to record the elapsed time.
        @began datetime

SET @began = getdate()

UPDATE copy
   SET on_loan = 'Y'
   WHERE (isbn+copy_no)%5  =  CONVERT(int, (RAND() * 5) )

INSERT loan
   SELECT isbn
     , copy_no
     , title_no
     , ((isbn+(copy_no*47))/2)*13
     , dateadd(day, ((isbn*copy_no)%16)-16, GETDATE())
     , dateadd(day, ((isbn*copy_no)%16)-2,  GETDATE())
   FROM copy
   WHERE on_loan = 'Y'

BACKUP TRAN library WITH TRUNCATE_ONLY
SET @message = 'Time (in minutes:seconds) to load Loan.  ' + convert( char(2), datediff(ss,@began,getdate())/60 ) + ':' + convert( char(2), datediff(ss,@began,getdate())%60 )    PRINT @message
GO

/* Load reservation */

DECLARE @message char(255),   -- Prepare to record the elapsed time.
        @began datetime

SET @began = getdate()

/*
Just a very small percentage of titles and copies.
For performance reasons, produce a mini-member and
a mini-item table, and then join these mini tables. 
*/

SELECT member_no
   INTO #member
   FROM member
   WHERE member_no % 17 = 1

SELECT isbn
   INTO #item
   FROM item
   WHERE isbn % 7 = 1

INSERT reservation
   SELECT isbn
     , member_no
     , GETDATE()
     , NULL
   FROM #member, #item
   WHERE member_no % 3 = isbn % 41

BACKUP TRAN library WITH TRUNCATE_ONLY
SET @message = 'Time (in minutes:seconds) to load Reservation.  ' + convert( char(2), datediff(ss,@began,getdate())/60 ) + ':' + convert( char(2), datediff(ss,@began,getdate())%60 )    PRINT @message
GO

/* 
Load loanhist
Generate loan history.  Do it by using the current loans as the basis.
Since there is an almost complete turnover of loans every two weeks,
replicate the Loan table into LoanHist for every two weeks worth of
history, changing ISBNs and Member Numbers as you go.  Since the ISBNs
have changed, the Title Numbers in LoanHist will have to be resynched.
For performance reasons, an index is used.
*/


CREATE UNIQUE CLUSTERED INDEX item_ident ON item (isbn)
GO

DECLARE @message char(255),   -- Prepare to record the elapsed time.
        @began datetime

SET @began = getdate()

/* 
Adjust the amount of history here. The number of loans in LoanHist will be equal
to the number of rows in the Loan table times @max_cycles.  Each 'cycle' equals
two weeks of loans; or 26 cycles per year. 
*/

DECLARE @max_cycles int    SET @max_cycles = 26
DECLARE @cycle int         SET @cycle = 1
DECLARE @noof_members int  SElecT @noof_members = (SELECT count(*) FROM member)

WHILE (@cycle <= @max_cycles)
BEGIN
    INSERT loanhist
       SELECT isbn
         , copy_no  
         , dateadd(week,-(@cycle*2),out_date)
         , 0          
         , (convert(int,ceiling((rand((isbn*copy_no))*(isbn*copy_no)*10)))%@noof_members)+1
         , dateadd(day, 14, dateadd(week,-(@cycle*2),out_date))
         , dateadd(day, (isbn*copy_no) % 16, dateadd(week,-(@cycle*2),out_date))
         , null
         , null
         , null
         , null
       FROM loan

    UPDATE loanhist 
       SET title_no = item.title_no
       FROM item, loanhist
       WHERE item.isbn = loanhist.isbn
       AND loanhist.title_no = 0

    BACKUP TRAN library WITH TRUNCATE_ONLY
    
    SET @cycle = @cycle + 1
END
    

/* Update the LoanHist table to include fine information for a few loans */

UPDATE loanhist
   SET fine_assessed = convert(money,member_no%10)
   WHERE (isbn+copy_no)%37 = 0

UPDATE loanhist
   SET fine_paid = fine_assessed
   WHERE fine_assessed <> 0
   AND member_no%2 = 0

UPDATE loanhist
   SET fine_paid = fine_assessed / ((member_no%9)+1)
   WHERE fine_assessed <> 0
   AND member_no%22 = 0

UPDATE loanhist
   SET fine_waived = fine_assessed - fine_paid
   WHERE fine_assessed <> 0
   AND fine_paid <> fine_assessed
   AND member_no%4 = 0

BACKUP TRAN library WITH TRUNCATE_ONLY
    
SET @message = 'Time (in minutes:seconds) to load LoanHist.  ' + convert( char(2), datediff(ss,@began,getdate())/60 ) + ':' + convert( char(2), datediff(ss,@began,getdate())%60 )    PRINT @message
GO

DROP INDEX  item.item_ident
GO


/* Check for success */

EXEC  sp_spaceused member
EXEC  sp_spaceused adult
EXEC  sp_spaceused juvenile
EXEC  sp_spaceused title
EXEC  sp_spaceused item
EXEC  sp_spaceused copy
EXEC  sp_spaceused loan
EXEC  sp_spaceused loanhist
EXEC  sp_spaceused reservation
GO

SET NOCOUNT OFF
GO

