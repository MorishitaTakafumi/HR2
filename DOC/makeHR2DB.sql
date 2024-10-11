-- HR2.sqlite3    SQLite3 DataBase

CREATE TABLE RaceHeader (
-- 0
    id 		INTEGER 	PRIMARY KEY AUTOINCREMENT,
-- 1
    dt		DATETIME NOT NULL,
-- 2
   class_code	INTEGER	NOT NULL,
-- 3
   type_code	INTEGER	NOT NULL,
-- 4
   kyori		INTEGER	NOT NULL,
-- 5
    jo_code	INTEGER NOT NULL,
-- 6
    race_name	varchar(50) NOT NULL,
-- 7
    race_no	INTEGER NOT NULL,
-- 8
    tosu		INTEGER	NOT NULL
);


CREATE TABLE AnaVal (
-- 0
    id 		INTEGER 	PRIMARY KEY AUTOINCREMENT,
-- 1
    rhead_id	INTEGER NOT NULL,
-- 2
    cyakujun	INTEGER NOT NULL,
-- 3
    spanScore	INTEGER NOT NULL,
-- 4
    agarisa1	REAL	NOT NULL,
-- 5
    cyakusa1	REAL	NOT NULL,
-- 6
    agarisa2	REAL	NOT NULL,
-- 7
    cyakusa2	REAL	NOT NULL,
-- 8
    agarisa3	REAL	NOT NULL,
-- 9
    cyakusa3	REAL	NOT NULL,
-- 10
    agarisa4	REAL	NOT NULL,
-- 11
    cyakusa4	REAL	NOT NULL,
-- 12
    dateScore	INTEGER NOT NULL,
-- 13
    kyoriScore	INTEGER NOT NULL,
-- 14
    bamei		varchar(50) NOT NULL,
-- 15
    ninki		INTEGER NOT NULL,
-- 16
    flags		INTEGER NOT NULL
);


