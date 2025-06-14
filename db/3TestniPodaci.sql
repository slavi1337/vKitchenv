USE mydb;

SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE OMILJENI_RECEPT;
TRUNCATE TABLE OBAVJESTENJE;
TRUNCATE TABLE NAPRAVLJEN_RECEPT;
TRUNCATE TABLE KORISNIK_has_NAMIRNICA;
TRUNCATE TABLE PRACENJE;
TRUNCATE TABLE RECEPT_has_NAMIRNICA;
TRUNCATE TABLE RECEPT;
TRUNCATE TABLE NAMIRNICA;
TRUNCATE TABLE JEDINICA;
TRUNCATE TABLE KORISNIK;
SET FOREIGN_KEY_CHECKS = 1;

/* Korisnici */
INSERT INTO KORISNIK (KorisnicnoIme,Lozinka,Ime,Prezime) VALUES
 ('slavi1337','pw','Slavisa','Covakusic'),
 ('ana','ana','Ana','Maric'),
 ('vuk','passwords','Vuk','Rmus'),
 ('anja1','anjaa','Anja','Ljiljak'),
 ('luka15','51akul','Luka','Trtic'),
 ('bazepodataka','bazePodAtAkA','Baze','Podataka');

/* Jedinice */
INSERT INTO JEDINICA (JedinicaID, JedinicaBazna, KonverzioniFaktor) VALUES
 ('g',   'g',   1),
 ('kg',  'g',   1000),
 ('ml',  'ml',  1),
 ('l',   'ml',  1000),
 ('dl', 'ml',  100),
 ('kom', 'kom',  1);

/* Dodavanje osnovnih namirnica - samo ove namirnice se mogu dodati korisnicima i u recepte!!!*/
INSERT INTO NAMIRNICA (NamirnicaIme) VALUES
 ('Brasno'), ('Secer'), ('So'), ('Ulje'), ('Mlijeko'),
 ('Jaje'), ('Kvasac'), ('Jogurt'), ('Sir'), ('Puter'),
 ('Cokolada'), ('Paradajz'), ('Origano'), ('Hljeb'), ('Piletina'),
 ('Susam'), ('Brokoli'), ('MaslinovoUlje'), ('Kakao'), ('Banana'), ('OvsenePahuljice');

/* Stanje namirnica korisnika - automatski se pretvara u baznu jedinicu(ako postoji konverzija u tabeli)*/
INSERT INTO KORISNIK_has_NAMIRNICA VALUES
 ('slavi1337','Brasno', 1.5,'kg'),
 ('slavi1337','Secer', 300,'g'),
 ('slavi1337','So', 100,'g'),
 ('slavi1337','Ulje', 2,'dl'),
 ('slavi1337','Mlijeko', 1.5,'l'),
 ('slavi1337','Jaje', 6,'kom'),
 ('slavi1337','Cokolada', 200,'g'),
 ('slavi1337', 'Banana', 6, 'kom'),
 ('slavi1337', 'OvsenePahuljice', 300, 'g'),
 ('slavi1337', 'Jogurt', 1, 'l'),

 ('ana','Jogurt', 2,'dl'),
 ('ana','Banana', 3,'kom'),
 ('ana','OvsenePahuljice', 150,'g'),

 ('vuk','Puter', 200,'g'),
 ('vuk','Brasno', 1,'kg'),
 ('vuk','Kvasac', 1,'kom'),
 ('vuk','Jogurt', 1,'l'),

 ('anja1','Sir', 300,'g'),
 ('anja1','Paradajz', 2,'kom'),
 ('anja1','Piletina', 500,'g'),
 ('anja1','Brokoli', 1,'kom'),
 
 ('luka15','Kvasac',1,'kom'),
 ('luka15','Brasno',0.5,'kg'),
 
 ('bazepodataka','Hljeb',2,'kom'),
 ('bazepodataka','Piletina',400,'g');

/* PRacenja */
INSERT INTO PRACENJE VALUES
 ('slavi1337','ana', CURDATE()),
 ('slavi1337','anja1', CURDATE()),
 ('vuk','ana', CURDATE()),
 ('anja1','slavi1337', CURDATE()),
 ('luka15','anja1', CURDATE()),
 ('bazepodataka','luka15', CURDATE());

/* Recepti – AKTIVIRAJU SE NOTIFIKACIJE za followere */

-- Recept 1: Palacinke (ana)
SET @jsonPala := '[ ["Brasno",200,"g"], ["Mlijeko",3,"dl"], ["Jaje",2,"kom"] ]';
CALL sp_DodajReceptSaNamirnicama('ana','Palacinke','Tanke, mekane','Sve umutiti i peci 2 min.', @jsonPala);

-- Recept 2: Coko-mufini (ana)
SET @jsonMuf := '[ ["Brasno",250,"g"], ["Secer",150,"g"], ["Cokolada",100,"g"], ["Jaje",2,"kom"] ]';
CALL sp_DodajReceptSaNamirnicama('ana','Coko-mufini','Socni mafini','Peci 20 min na 180C.', @jsonMuf);

-- Recept 3: Hljeb sa jogurtom (anja1)
SET @jsonHleb := '[ ["Brasno",0.3,"kg"], ["Jogurt",2,"dl"], ["Kvasac",1,"kom"] ]';
CALL sp_DodajReceptSaNamirnicama('anja1','Domaci hljeb','Mirisan i mekan','Umijesiti i peci 30 min.', @jsonHleb);

-- Recept 4: Zeleni smoothie (slavi1337)
SET @jsonSmoothie := '[ ["Banana",1,"kom"], ["OvsenePahuljice",50,"g"], ["Jogurt",2,"dl"] ]';
CALL sp_DodajReceptSaNamirnicama('slavi1337','Zeleni smoothie','Zdrav i brz','Sve izblendati.', @jsonSmoothie);

/* Priprema recepta */
SET @idPalacinke := (SELECT ReceptID FROM RECEPT WHERE Naslov='Palacinke');
SET @idHljeb := (SELECT ReceptID FROM RECEPT WHERE Naslov='Domaci hljeb');
SET @idSmoothie := (SELECT ReceptID FROM RECEPT WHERE Naslov='Zeleni smoothie');

CALL sp_PripremiRecept('slavi1337', @idPalacinke);
CALL sp_PripremiRecept('slavi1337', @idSmoothie);
CALL sp_PripremiRecept('vuk', @idHljeb);

/* Omiljeni recepti */
INSERT INTO OMILJENI_RECEPT VALUES
 ('slavi1337', @idPalacinke),
 ('slavi1337', @idHljeb),
 ('bazepodataka', @idSmoothie),
 ('vuk', @idPalacinke),
 ('anja1', @idPalacinke);

/* Dodatno spremljeni recepti (NAPRAVLJEN_RECEPT) */
INSERT INTO NAPRAVLJEN_RECEPT VALUES
 ('anja1', @idPalacinke, NOW()),
 ('luka15', @idSmoothie, NOW());