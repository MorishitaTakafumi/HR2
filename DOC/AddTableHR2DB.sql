-- HR2.sqlite3    SQLite3 DataBase

CREATE TABLE UmaHeader (
-- 0
    id 		INTEGER 	PRIMARY KEY AUTOINCREMENT,
-- 1
    name		varchar(50) NOT NULL,
-- 2
    father		varchar(50) NOT NULL,
-- 3
    mother		varchar(50) NOT NULL,
-- 4
   seibetu		INTEGER	NOT NULL,
-- 5
    birthday	DATETIME NOT NULL,
-- 6
    dt_update	DATETIME NOT NULL
);


CREATE TABLE UmaHist(
-- 0
    id 		INTEGER 	PRIMARY KEY AUTOINCREMENT,
-- 1
    uma_id		INTEGER NOT NULL,
-- 2
    dt		DATETIME NOT NULL,
-- 3
    jo_code	INTEGER NOT NULL,
-- 4
    race_name	varchar(50) NOT NULL,
-- 5
   type_code	INTEGER	NOT NULL,
-- 6
   kyori		INTEGER	NOT NULL,
-- 7
    baba		varchar(10) NOT NULL,
-- 8
    tosu		INTEGER	NOT NULL,
-- 9
    ninki		INTEGER NOT NULL,
-- 10
    cyakujun	INTEGER NOT NULL,
-- 11
    kisyu		varchar(50) NOT NULL,
-- 12
    hutan		REAL NOT NULL,
-- 13
    w		INTEGER NOT NULL,
-- 14
    secs		REAL NOT NULL,
-- 15
    href		varchar(100) NOT NULL
);

CREATE TABLE Kekka(
-- 0
    id 		INTEGER 	PRIMARY KEY AUTOINCREMENT,
-- 1
    race_header_id	INTEGER NOT NULL,
-- 2
    cyakujun	INTEGER NOT NULL,
-- 3
    umaban	INTEGER NOT NULL,
-- 4
    bamei		varchar(50) NOT NULL,
-- 5
    seirei		varchar(50) NOT NULL,
-- 6
    hutan		REAL NOT NULL,
-- 7
    kisyu		varchar(50) NOT NULL,
-- 8
    secs		REAL NOT NULL,
-- 9
    tocyu		varchar(50) NOT NULL,
-- 10
    agari		REAL NOT NULL,
-- 11
    agarisa		REAL NOT NULL,
-- 12
    cyakusa	REAL NOT NULL,
-- 13
    w		INTEGER NOT NULL,
-- 14
    zogen		INTEGER NOT NULL,
-- 15
    cyokyosi	varchar(50) NOT NULL,
-- 16
    ninki		INTEGER NOT NULL,
-- 17
    href		varchar(100) NOT NULL
);
