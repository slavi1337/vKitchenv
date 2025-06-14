USE mydb;

DROP TRIGGER IF EXISTS trg_INV_BEFORE_INSERT;
DROP TRIGGER IF EXISTS trg_INV_BEFORE_UPDATE;
DROP TRIGGER IF EXISTS trg_REC_BEFORE_INSERT;
DROP TRIGGER IF EXISTS trg_REC_BEFORE_UPDATE;
DROP TRIGGER IF EXISTS trg_Recept_AfterInsert_Notif;

DROP VIEW IF EXISTS vw_SkladisteKorisnika;
DROP VIEW IF EXISTS vw_ReceptHeader;
DROP VIEW IF EXISTS vw_ReceptSastojciEdit;
DROP VIEW IF EXISTS vw_IstorijaRecepata;
DROP VIEW IF EXISTS vw_NeprocitaneNotifikacije;
DROP VIEW IF EXISTS vw_KorisnikBasic;
DROP VIEW IF EXISTS vw_BrojRecepataAutora;

DROP PROCEDURE IF EXISTS sp_DodajKorisnika;
DROP PROCEDURE IF EXISTS sp_ProvjeriPrijavu;
DROP PROCEDURE IF EXISTS sp_PrikaziNamirniceKorisnika;
DROP PROCEDURE IF EXISTS sp_UpdateNamirnicuKorisniku;
DROP PROCEDURE IF EXISTS sp_DodajNamirnicuKorisniku;
DROP PROCEDURE IF EXISTS sp_ObrisiNamirnicuKorisniku;

DROP PROCEDURE IF EXISTS sp_GetReceptZaEdit;
DROP PROCEDURE IF EXISTS sp_UpdateRecept;
DROP PROCEDURE IF EXISTS sp_DodajSastojkeJSON;

DROP PROCEDURE IF EXISTS sp_GetProfilKorisnika;
DROP PROCEDURE IF EXISTS sp_DaLiPratim;
DROP PROCEDURE IF EXISTS sp_OtpratiKorisnika;
DROP PROCEDURE IF EXISTS sp_PratiKorisnika;
DROP PROCEDURE IF EXISTS sp_PrikaziRecepteAutora;

DROP PROCEDURE IF EXISTS sp_OznaciProcitano;
DROP PROCEDURE IF EXISTS sp_OznaciNeprocitano;
DROP PROCEDURE IF EXISTS sp_PrikaziSveNotifikacijeZaKorisnika;

DROP PROCEDURE IF EXISTS sp_PrikaziIstorijuRecepata;
DROP PROCEDURE IF EXISTS sp_DodajReceptSaNamirnicama;

DROP PROCEDURE IF EXISTS sp_JeOmiljeniRecept;
DROP PROCEDURE IF EXISTS sp_DodajOmiljeniRecept;
DROP PROCEDURE IF EXISTS sp_UkloniOmiljeniRecept;
DROP PROCEDURE IF EXISTS sp_PripremiRecept;
DROP PROCEDURE IF EXISTS sp_ObrisiRecept;
DROP PROCEDURE IF EXISTS sp_PrikaziSastojkeZaRecept;
DROP PROCEDURE IF EXISTS sp_PrikaziUputeZaRecept;

DROP PROCEDURE IF EXISTS sp_PretraziReceptePoNaslovu;
DROP PROCEDURE IF EXISTS sp_GetShoppingList;
DROP PROCEDURE IF EXISTS sp_FiltrirajRecepte;

# TRIGERI

DELIMITER //

-- 1.1  trg_INV_BEFORE_INSERT – standardizira količinu na baznu jedinicu pri INSERT-u u KORISNIK_has_NAMIRNICA
CREATE TRIGGER trg_INV_BEFORE_INSERT
BEFORE INSERT ON KORISNIK_has_NAMIRNICA
FOR EACH ROW
BEGIN
  DECLARE vBazna VARCHAR(10);  
  DECLARE vF DECIMAL(10,2);
  SELECT JedinicaBazna, KonverzioniFaktor
    INTO vBazna, vF 
    FROM JEDINICA 
    WHERE JedinicaID = NEW.JedinicaID;
  IF vBazna IS NOT NULL THEN
      SET NEW.Kolicina    = NEW.Kolicina * vF;
      SET NEW.JedinicaID  = vBazna;
  END IF;
END//

-- 1.2  trg_INV_BEFORE_UPDATE – isto kao 1.1, ali za UPDATE
CREATE TRIGGER trg_INV_BEFORE_UPDATE
BEFORE UPDATE ON KORISNIK_has_NAMIRNICA
FOR EACH ROW
BEGIN
  DECLARE vBazna VARCHAR(10);  
  DECLARE vF DECIMAL(10,2);
  SELECT JedinicaBazna, KonverzioniFaktor
    INTO vBazna, vF 
    FROM JEDINICA 
    WHERE JedinicaID = NEW.JedinicaID;
  IF vBazna IS NOT NULL THEN
      SET NEW.Kolicina    = NEW.Kolicina * vF;
      SET NEW.JedinicaID  = vBazna;
  END IF;
END//

-- 1.3  trg_REC_BEFORE_INSERT – standardizira količinu sastojka pri INSERT-u u RECEPT_has_NAMIRNICA
CREATE TRIGGER trg_REC_BEFORE_INSERT
BEFORE INSERT ON RECEPT_has_NAMIRNICA
FOR EACH ROW
BEGIN
  DECLARE vBazna VARCHAR(10);  
  DECLARE vF DECIMAL(10,2);
  SELECT JedinicaBazna, KonverzioniFaktor
    INTO vBazna, vF 
    FROM JEDINICA 
    WHERE JedinicaID = NEW.JedinicaID;
  IF vBazna IS NOT NULL THEN
      SET NEW.Kolicina    = NEW.Kolicina * vF;
      SET NEW.JedinicaID  = vBazna;
  END IF;
END//

-- 1.4  trg_REC_BEFORE_UPDATE – isto kao 1.3, ali za UPDATE
CREATE TRIGGER trg_REC_BEFORE_UPDATE
BEFORE UPDATE ON RECEPT_has_NAMIRNICA
FOR EACH ROW
BEGIN
  DECLARE vBazna VARCHAR(10);  
  DECLARE vF DECIMAL(10,2);
  SELECT JedinicaBazna, KonverzioniFaktor
    INTO vBazna, vF 
    FROM JEDINICA 
    WHERE JedinicaID = NEW.JedinicaID;
  IF vBazna IS NOT NULL THEN
      SET NEW.Kolicina    = NEW.Kolicina * vF;
      SET NEW.JedinicaID  = vBazna;
  END IF;
END//

-- 1.5  trg_Recept_AfterInsert_Notif – po kreiranju recepta salje notifikacije svim follower-ima autora
CREATE TRIGGER trg_Recept_AfterInsert_Notif
AFTER INSERT ON RECEPT
FOR EACH ROW
BEGIN
  INSERT INTO OBAVJESTENJE (Primatelj_KorisnickoIme, ReceptID, DatumVrijeme, Tekst)
  SELECT Pratilac_KorisnickoIme,
         NEW.ReceptID,
         NOW(),
         CONCAT('Novi recept: ',NEW.Naslov,' (autor ',NEW.Autor_KorisnickoIme,')')
  FROM   PRACENJE
  WHERE  Pracenik_KorisnickoIme = NEW.Autor_KorisnickoIme;
END//

DELIMITER ;

# VIEWOVI

-- 2.1  vw_SkladisteKorisnika – sve namirnice korisnika sa kolicinom i jedinicom
CREATE VIEW vw_SkladisteKorisnika AS
SELECT 
    k.KORISNIK_KorisnicnoIme AS Korisnik,
    k.NAMIRNICA_NamirnicaIme AS Namirnica,
    k.Kolicina,
    k.JedinicaID,
    j.JedinicaBazna,
    j.KonverzioniFaktor
FROM KORISNIK_has_NAMIRNICA k
JOIN JEDINICA j ON k.JedinicaID = j.JedinicaID;

-- 2.2  vw_ReceptHeader – recept bez sastojaka (bez 2.3)
CREATE VIEW vw_ReceptHeader AS
SELECT ReceptID, Naslov, Opis, UputeZaPripremu
FROM   RECEPT;

-- 2.3  vw_ReceptSastojciEdit – recept sa sastojcima (bez 2.2)
CREATE VIEW vw_ReceptSastojciEdit AS
SELECT RECEPT_ReceptID AS ReceptID,
       NAMIRNICA_NamirnicaIme AS Namirnica,
       Kolicina,
       JedinicaID
FROM   RECEPT_has_NAMIRNICA;

-- 2.4  vw_IstorijaRecepata – istorija pripremljenih recepata po korisniku
CREATE VIEW vw_IstorijaRecepata AS
SELECT nr.KorisnickoIme,
       nr.ReceptID,
       r.Naslov,
       r.Autor_KorisnickoIme,
       nr.DatumIVrijemePravljenja
FROM   NAPRAVLJEN_RECEPT nr
JOIN   RECEPT r ON r.ReceptID = nr.ReceptID;

-- 2.5  vw_NeprocitaneNotifikacije – sve notifikacije gdje Procitano = 0 (neprocitane)
CREATE VIEW vw_NeprocitaneNotifikacije AS
SELECT * FROM OBAVJESTENJE WHERE Procitano = 0;

-- 2.6  vw_KorisnikBasic – osnovni podaci o korisniku (bez lozinke)
CREATE VIEW vw_KorisnikBasic AS
SELECT KorisnicnoIme, Ime, Prezime
FROM   KORISNIK;

-- 2.7  vw_BrojRecepataAutora – broj recepata po autoru
CREATE VIEW vw_BrojRecepataAutora AS
SELECT Autor_KorisnickoIme AS Autor,
       COUNT(*)            AS BrojRecepata
FROM   RECEPT
GROUP  BY Autor_KorisnickoIme;

# PROCEDURE

DELIMITER //

-- 3.1  sp_DodajKorisnika – dodaje novog korisnika (provjera unikatnosti username-a)
CREATE PROCEDURE sp_DodajKorisnika(
    IN p_username VARCHAR(45),
    IN p_lozinka  VARCHAR(45),
    IN p_ime      VARCHAR(45),
    IN p_prezime  VARCHAR(45)
)
BEGIN
    DECLARE postoji INT;
    SELECT COUNT(*) INTO postoji 
    FROM KORISNIK 
    WHERE KorisnicnoIme = p_username;

    IF postoji > 0 THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Korisnicko ime vec postoji';
    ELSE
        INSERT INTO KORISNIK (KorisnicnoIme, Lozinka, Ime, Prezime)
        VALUES (p_username, p_lozinka, p_ime, p_prezime);
    END IF;
END//

-- 3.2  sp_ProvjeriPrijavu – provjerava da li username/lozinka postoje
CREATE PROCEDURE sp_ProvjeriPrijavu(
    IN  p_username VARCHAR(45),
    IN  p_lozinka  VARCHAR(45),
    OUT p_valid    BIT
)
BEGIN
    DECLARE broj INT;
    SELECT COUNT(*) INTO broj
    FROM KORISNIK
    WHERE KorisnicnoIme = p_username 
      AND Lozinka       = p_lozinka;

    SET p_valid = (broj = 1);
END//

-- 3.3  sp_PrikaziNamirniceKorisnika – vraca stanje namirnica korisnika (vw_SkladisteKorisnika)
CREATE PROCEDURE sp_PrikaziNamirniceKorisnika(IN p_username VARCHAR(45))
BEGIN
    SELECT Namirnica, Kolicina, JedinicaID
    FROM vw_SkladisteKorisnika
    WHERE Korisnik = p_username;
END//

-- 3.4  sp_UpdateNamirnicuKorisniku – update koliccine/jedinice
CREATE PROCEDURE sp_UpdateNamirnicuKorisniku(
    IN p_user      VARCHAR(45),
    IN p_namirnica VARCHAR(45),
    IN p_kol       DECIMAL(10,2),
    IN p_jed       VARCHAR(10)
)
BEGIN
#overrajduje postojecu namirnicu sa najnovijim upisom (NE VRSI SABIRANJE namirnica 
#kao sto je npr oduzimanje kod pravljenja recepta!!)
    UPDATE KORISNIK_has_NAMIRNICA
    SET    Kolicina   = p_kol,
           JedinicaID = p_jed
    WHERE  KORISNIK_KorisnicnoIme = p_user
      AND  NAMIRNICA_NamirnicaIme = p_namirnica;
END//

-- 3.5  sp_DodajNamirnicuKorisniku – INSERT ili UPDATE ako vec postoji
CREATE PROCEDURE sp_DodajNamirnicuKorisniku(
    IN p_user      VARCHAR(45),
    IN p_namirnica VARCHAR(45),
    IN p_kol       DECIMAL(10,2),
    IN p_jed       VARCHAR(10)
)
BEGIN
    INSERT INTO KORISNIK_has_NAMIRNICA
          (KORISNIK_KorisnicnoIme, NAMIRNICA_NamirnicaIme, Kolicina, JedinicaID)
    VALUES(p_user, p_namirnica, p_kol, p_jed)
    ON DUPLICATE KEY UPDATE
          Kolicina   = VALUES(Kolicina),
          JedinicaID = VALUES(JedinicaID);
END//

-- 3.6  sp_ObrisiNamirnicuKorisniku – brise namirnicu iz kuhinje
CREATE PROCEDURE sp_ObrisiNamirnicuKorisniku(
    IN p_user      VARCHAR(45),
    IN p_namirnica VARCHAR(45)
)
BEGIN
    DELETE FROM KORISNIK_has_NAMIRNICA
    WHERE  KORISNIK_KorisnicnoIme = p_user
      AND  NAMIRNICA_NamirnicaIme = p_namirnica;
END//

-- 3.7  sp_GetReceptZaEdit – vraca header + sastojke za recept (2.2 i 2.3)
CREATE PROCEDURE sp_GetReceptZaEdit(IN p_receptID INT)
BEGIN
    SELECT * FROM vw_ReceptHeader       WHERE ReceptID = p_receptID;
    SELECT * FROM vw_ReceptSastojciEdit WHERE ReceptID = p_receptID;
END//

-- 3.8  sp_UpdateRecept – mijenja naslov/opis/upute
CREATE PROCEDURE sp_UpdateRecept(
    IN p_receptID INT,
    IN p_naslov   VARCHAR(100),
    IN p_opis     TEXT,
    IN p_upute    TEXT
)
BEGIN
    UPDATE RECEPT
    SET Naslov = p_naslov,
        Opis   = p_opis,
        UputeZaPripremu = p_upute
    WHERE ReceptID = p_receptID;
END//

-- 3.9  sp_DodajSastojkeJSON – zamjenjuje sve sastojke prema JSON-u
CREATE PROCEDURE sp_DodajSastojkeJSON(
    IN p_receptID INT,
    IN p_JSON     JSON          -- [[namirnica,kolicina,jedID], …]
)
BEGIN
    DELETE FROM RECEPT_has_NAMIRNICA
    WHERE  RECEPT_ReceptID = p_receptID;

    INSERT INTO RECEPT_has_NAMIRNICA
          (RECEPT_ReceptID, NAMIRNICA_NamirnicaIme, Kolicina, JedinicaID)
    SELECT p_receptID,
           jt.nam,
           jt.kol,
           jt.jed
    FROM JSON_TABLE(
           p_JSON, '$[*]'
           COLUMNS (
             nam VARCHAR(45)   PATH '$[0]',
             kol DECIMAL(10,2) PATH '$[1]',
             jed VARCHAR(10)   PATH '$[2]'
           )) jt;
END//

-- 3.10 sp_GetProfilKorisnika – vraca podatke + broj followera/pratim
CREATE PROCEDURE sp_GetProfilKorisnika(IN p_user VARCHAR(45))
BEGIN
  SELECT 
    k.KorisnicnoIme,
    k.Ime,
    k.Prezime,
    (SELECT COUNT(*) FROM PRACENJE WHERE Pracenik_KorisnickoIme = p_user) AS BrojFollowera,
    (SELECT COUNT(*) FROM PRACENJE WHERE Pratilac_KorisnickoIme = p_user) AS BrojPratim
  FROM KORISNIK k
  WHERE k.KorisnicnoIme = p_user;
END//

-- 3.11 sp_DaLiPratim – vraca 0/1 da li p_me prati p_target
CREATE PROCEDURE sp_DaLiPratim(
    IN  p_me     VARCHAR(45),
    IN  p_target VARCHAR(45),
    OUT p_pratim BIT
)
BEGIN
    SELECT COUNT(*) = 1 INTO p_pratim
    FROM PRACENJE
    WHERE Pratilac_KorisnickoIme  = p_me
      AND Pracenik_KorisnickoIme = p_target;
END//

-- 3.12 sp_OtpratiKorisnika – brise red iz PRACENJE
CREATE PROCEDURE sp_OtpratiKorisnika(IN p_me VARCHAR(45), IN p_target VARCHAR(45))
BEGIN
  DELETE FROM PRACENJE
  WHERE  Pratilac_KorisnickoIme = p_me
    AND  Pracenik_KorisnickoIme = p_target;
END//

-- 3.13 sp_PratiKorisnika – INSERT IGNORE u PRACENJE
CREATE PROCEDURE sp_PratiKorisnika(IN p_me VARCHAR(45), IN p_target VARCHAR(45))
BEGIN
  INSERT IGNORE INTO PRACENJE
        (Pratilac_KorisnickoIme, Pracenik_KorisnickoIme, DatumPocetka)
  VALUES(p_me, p_target, CURDATE());
END//

-- 3.14 sp_PrikaziRecepteAutora – svi recepti odredjenog autora
CREATE PROCEDURE sp_PrikaziRecepteAutora(IN p_autor VARCHAR(45))
BEGIN
    SELECT ReceptID, Naslov, Opis, Autor_KorisnickoIme
    FROM   RECEPT
    WHERE  Autor_KorisnickoIme = p_autor
    ORDER  BY ReceptID DESC;
END//

-- 3.15 sp_OznaciProcitano – postavlja Procitano = 1 za notifikaciju (oznacava da je procitana)
CREATE PROCEDURE sp_OznaciProcitano(IN p_notifID INT)
BEGIN
  UPDATE OBAVJESTENJE
     SET Procitano = 1
   WHERE ObavjestenjeID = p_notifID;
END//

-- 3.16 sp_OznaciNeprocitano – postavlja Procitano = 0 za notifikaciju (oznacava da je neprocitana)
CREATE PROCEDURE sp_OznaciNeprocitano(IN p_notifID INT)
BEGIN
  UPDATE OBAVJESTENJE
     SET Procitano = 0
   WHERE ObavjestenjeID = p_notifID;
END//

-- 3.17 sp_PrikaziSveNotifikacijeZaKorisnika – sve notifikacije (read + unread)
CREATE PROCEDURE sp_PrikaziSveNotifikacijeZaKorisnika(IN p_korisnik VARCHAR(45))
BEGIN
    SELECT 
      o.ObavjestenjeID,
      o.ReceptID,
      o.DatumVrijeme,
      o.Tekst,
      o.Procitano,
      r.Autor_KorisnickoIme AS Autor
    FROM OBAVJESTENJE o
    JOIN RECEPT r 
      ON o.ReceptID = r.ReceptID
    WHERE o.Primatelj_KorisnickoIme = p_korisnik
    ORDER BY o.DatumVrijeme DESC;
END//

-- 3.18 sp_PrikaziIstorijuRecepata – istorija pravljenja recepata za usera
CREATE PROCEDURE sp_PrikaziIstorijuRecepata(IN p_user VARCHAR(45))
BEGIN
    SELECT ReceptID, Naslov, Autor_KorisnickoIme, DatumIVrijemePravljenja
    FROM   vw_IstorijaRecepata
    WHERE  KorisnickoIme = p_user
    ORDER  BY DatumIVrijemePravljenja DESC;
END//

-- 3.19 sp_DodajReceptSaNamirnicama – INSERT recept + JSON sastojci
CREATE PROCEDURE sp_DodajReceptSaNamirnicama(
    IN pAutor  VARCHAR(45),
    IN pNaslov VARCHAR(100),
    IN pOpis   TEXT,
    IN pUpute  TEXT,
    IN pJSON   JSON            -- [[Namirnica, Kolicina, JedID], …]
)
BEGIN
  DECLARE vID INT;
  START TRANSACTION;
    INSERT INTO RECEPT (Autor_KorisnickoIme, Naslov, Opis, UputeZaPripremu)
    VALUES (pAutor, pNaslov, pOpis, pUpute);
    SET vID = LAST_INSERT_ID();

    INSERT INTO RECEPT_has_NAMIRNICA
          (RECEPT_ReceptID, NAMIRNICA_NamirnicaIme, Kolicina, JedinicaID)
    SELECT vID,
           jt.Namirnica,
           jt.Kolicina,
           jt.JedID
    FROM JSON_TABLE(
           pJSON, '$[*]'
           COLUMNS (
             Namirnica VARCHAR(45)  PATH '$[0]',
             Kolicina  DECIMAL(10,2) PATH '$[1]',
             JedID     VARCHAR(10)  PATH '$[2]'
           )) jt;
  COMMIT;
END//

-- 3.20 sp_JeOmiljeniRecept – OUT bit da li je recept omiljen
CREATE PROCEDURE sp_JeOmiljeniRecept(
    IN  p_korisnik VARCHAR(45),
    IN  p_receptID INT,
    OUT p_is       BIT
)
BEGIN
    DECLARE cnt INT;
    SELECT COUNT(*) INTO cnt
    FROM OMILJENI_RECEPT
    WHERE KorisnickoIme = p_korisnik
      AND ReceptID      = p_receptID;
    SET p_is = (cnt = 1);
END//

-- 3.21 sp_DodajOmiljeniRecept – INSERT IGNORE u OMILJENI_RECEPT
CREATE PROCEDURE sp_DodajOmiljeniRecept(
    IN p_korisnik VARCHAR(45),
    IN p_receptID INT
)
BEGIN
    INSERT IGNORE INTO OMILJENI_RECEPT (KorisnickoIme, ReceptID)
    VALUES (p_korisnik, p_receptID);
END//

-- 3.22 sp_UkloniOmiljeniRecept – DELETE iz OMILJENI_RECEPT
CREATE PROCEDURE sp_UkloniOmiljeniRecept(
    IN p_korisnik VARCHAR(45),
    IN p_receptID INT
)
BEGIN
    DELETE FROM OMILJENI_RECEPT
    WHERE KorisnickoIme = p_korisnik
      AND ReceptID      = p_receptID;
END//

-- 3.23 sp_PripremiRecept – provjera sastojaka, umanji zalihe, log napravljeno
CREATE PROCEDURE sp_PripremiRecept(
    IN pKorisnik VARCHAR(45),
    IN pReceptID INT
)
BEGIN
  DECLARE vCnt INT;

  SELECT COUNT(*) INTO vCnt
  FROM   RECEPT_has_NAMIRNICA rh
  LEFT  JOIN KORISNIK_has_NAMIRNICA kh
         ON kh.NAMIRNICA_NamirnicaIme = rh.NAMIRNICA_NamirnicaIme
        AND kh.KORISNIK_KorisnicnoIme = pKorisnik
  WHERE  rh.RECEPT_ReceptID = pReceptID
    AND (kh.Kolicina IS NULL OR kh.Kolicina < rh.Kolicina);

  IF vCnt > 0 THEN
     SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT='Nedovoljno sastojaka!!!';
  END IF;

  START TRANSACTION;
    UPDATE KORISNIK_has_NAMIRNICA kh
    JOIN   RECEPT_has_NAMIRNICA rh
           ON rh.NAMIRNICA_NamirnicaIme = kh.NAMIRNICA_NamirnicaIme
          AND rh.RECEPT_ReceptID        = pReceptID
    SET    kh.Kolicina = kh.Kolicina - rh.Kolicina
    WHERE  kh.KORISNIK_KorisnicnoIme = pKorisnik;

    INSERT INTO NAPRAVLJEN_RECEPT
          (KorisnickoIme, ReceptID, DatumIVrijemePravljenja)
    VALUES (pKorisnik, pReceptID, NOW());
  COMMIT;
END//

-- 3.24 sp_ObrisiRecept – brise recept + sve zavisne podatke 
CREATE PROCEDURE sp_ObrisiRecept(
    IN p_receptID INT,
    IN p_user     VARCHAR(45)
)
BEGIN
    IF (SELECT Autor_KorisnickoIme 
          FROM RECEPT 
          WHERE ReceptID = p_receptID) <> p_user
    THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Niste autor recepta';
    END IF;

    START TRANSACTION;
        DELETE FROM OMILJENI_RECEPT   WHERE ReceptID = p_receptID;
        DELETE FROM KOMENTAR          WHERE ReceptID = p_receptID;
        DELETE FROM OCJENA            WHERE ReceptID = p_receptID;
        DELETE FROM NAPRAVLJEN_RECEPT WHERE ReceptID = p_receptID;
        DELETE FROM OBAVJESTENJE      WHERE ReceptID = p_receptID;
        DELETE FROM RECEPT_has_NAMIRNICA WHERE RECEPT_ReceptID = p_receptID;
        DELETE FROM RECEPT WHERE ReceptID = p_receptID;
    COMMIT;
END//

-- 3.25 sp_PrikaziSastojkeZaRecept – lista sastojaka za ReceptID
CREATE PROCEDURE sp_PrikaziSastojkeZaRecept(IN p_receptID INT)
BEGIN
    SELECT NAMIRNICA_NamirnicaIme, Kolicina, JedinicaID
    FROM RECEPT_has_NAMIRNICA
    WHERE RECEPT_ReceptID = p_receptID;
END//

-- 3.26 sp_PrikaziUputeZaRecept – opis + upute za recept
CREATE PROCEDURE sp_PrikaziUputeZaRecept(IN p_receptID INT)
BEGIN
    SELECT Opis, UputeZaPripremu
    FROM RECEPT
    WHERE ReceptID = p_receptID;
END//

-- 3.27 sp_PretraziReceptePoNaslovu – LIKE pretraga po naslovu
CREATE PROCEDURE sp_PretraziReceptePoNaslovu(IN p_term VARCHAR(100))
BEGIN
    SELECT ReceptID, Naslov, Opis, Autor_KorisnickoIme
    FROM   RECEPT
    WHERE  Naslov LIKE CONCAT('%', p_term, '%')
    ORDER  BY ReceptID DESC;
END//

-- 3.28 sp_GetShoppingList – sto nedostaje korisniku za recept/dodatna funkcija korpa za kupovinu
CREATE PROCEDURE sp_GetShoppingList(
    IN pKorisnik VARCHAR(45),
    IN pReceptID INT
)
BEGIN
  SELECT rh.NAMIRNICA_NamirnicaIme AS Namirnica,
         rh.Kolicina - IFNULL(kh.Kolicina,0) AS Nedostaje,
         rh.JedinicaID AS BaznaJed
  FROM   RECEPT_has_NAMIRNICA rh
  LEFT  JOIN KORISNIK_has_NAMIRNICA kh
         ON kh.NAMIRNICA_NamirnicaIme = rh.NAMIRNICA_NamirnicaIme
        AND kh.KORISNIK_KorisnicnoIme = pKorisnik
  WHERE  rh.RECEPT_ReceptID = pReceptID
    AND  (kh.Kolicina IS NULL OR kh.Kolicina < rh.Kolicina);
END//

-- 3.29 sp_FiltrirajRecepte – filter (dostupni/moji/omiljeni)
CREATE PROCEDURE sp_FiltrirajRecepte(
    IN p_korisnik      VARCHAR(45),
    IN p_dostupni_only TINYINT(1),
    IN p_moji_only     TINYINT(1),
    IN p_omiljeni_only TINYINT(1)
)
BEGIN
    CREATE TEMPORARY TABLE _rez
      SELECT r.ReceptID, r.Naslov, r.Opis, r.Autor_KorisnickoIme
      FROM   RECEPT r;

    IF p_omiljeni_only = 1 THEN
        DELETE FROM _rez
        WHERE ReceptID NOT IN (
            SELECT ReceptID
            FROM   OMILJENI_RECEPT
            WHERE  KorisnickoIme = p_korisnik
        );
    END IF;

    IF p_moji_only = 1 THEN
        DELETE FROM _rez 
        WHERE Autor_KorisnickoIme <> p_korisnik;
    END IF;

    IF p_dostupni_only = 1 THEN
        DELETE FROM _rez
        WHERE EXISTS (
               SELECT 1
               FROM   RECEPT_has_NAMIRNICA rh
               LEFT  JOIN KORISNIK_has_NAMIRNICA kh
                      ON kh.NAMIRNICA_NamirnicaIme = rh.NAMIRNICA_NamirnicaIme
                     AND kh.KORISNIK_KorisnicnoIme = p_korisnik
               WHERE  rh.RECEPT_ReceptID = _rez.ReceptID
                 AND (kh.Kolicina IS NULL OR kh.Kolicina < rh.Kolicina)
        );
    END IF;

    SELECT * FROM _rez ORDER BY ReceptID DESC;
    DROP TEMPORARY TABLE _rez;
END//

DELIMITER ;