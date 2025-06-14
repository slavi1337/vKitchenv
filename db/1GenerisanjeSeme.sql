-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`KORISNIK`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`KORISNIK` (
  `KorisnicnoIme` VARCHAR(45) NOT NULL,
  `Lozinka` VARCHAR(45) NOT NULL,
  `Ime` VARCHAR(45) NOT NULL,
  `Prezime` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`KorisnicnoIme`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`NAMIRNICA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`NAMIRNICA` (
  `NamirnicaIme` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`NamirnicaIme`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`JEDINICA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`JEDINICA` (
  `JedinicaID` VARCHAR(10) NOT NULL,
  `JedinicaBazna` VARCHAR(10) NOT NULL,
  `KonverzioniFaktor` DECIMAL(10,2) NOT NULL,
  PRIMARY KEY (`JedinicaID`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`KORISNIK_has_NAMIRNICA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`KORISNIK_has_NAMIRNICA` (
  `KORISNIK_KorisnicnoIme` VARCHAR(45) NOT NULL,
  `NAMIRNICA_NamirnicaIme` VARCHAR(45) NOT NULL,
  `Kolicina` DECIMAL(10,2) NOT NULL,
  `JedinicaID` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`KORISNIK_KorisnicnoIme`, `NAMIRNICA_NamirnicaIme`),
  INDEX `fk_KORISNIK_has_NAMIRNICA_NAMIRNICA1_idx` (`NAMIRNICA_NamirnicaIme` ASC) VISIBLE,
  INDEX `fk_KORISNIK_has_NAMIRNICA_KORISNIK_idx` (`KORISNIK_KorisnicnoIme` ASC) VISIBLE,
  INDEX `JedinicaID_idx` (`JedinicaID` ASC) VISIBLE,
  CONSTRAINT `fk_KORISNIK_has_NAMIRNICA_KORISNIK`
    FOREIGN KEY (`KORISNIK_KorisnicnoIme`)
    REFERENCES `mydb`.`KORISNIK` (`KorisnicnoIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_KORISNIK_has_NAMIRNICA_NAMIRNICA1`
    FOREIGN KEY (`NAMIRNICA_NamirnicaIme`)
    REFERENCES `mydb`.`NAMIRNICA` (`NamirnicaIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `JedinicaID`
    FOREIGN KEY (`JedinicaID`)
    REFERENCES `mydb`.`JEDINICA` (`JedinicaID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`RECEPT`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`RECEPT` (
  `ReceptID` INT NOT NULL AUTO_INCREMENT,
  `Autor_KorisnickoIme` VARCHAR(45) NOT NULL,
  `Naslov` VARCHAR(100) NOT NULL,
  `Opis` TEXT NOT NULL,
  `UputeZaPripremu` TEXT NOT NULL,
  PRIMARY KEY (`ReceptID`),
  INDEX `Autor_KorisnickoIme_idx` (`Autor_KorisnickoIme` ASC) VISIBLE,
  CONSTRAINT `Autor_KorisnickoIme`
    FOREIGN KEY (`Autor_KorisnickoIme`)
    REFERENCES `mydb`.`KORISNIK` (`KorisnicnoIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`RECEPT_has_NAMIRNICA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`RECEPT_has_NAMIRNICA` (
  `RECEPT_ReceptID` INT NOT NULL,
  `NAMIRNICA_NamirnicaIme` VARCHAR(45) NOT NULL,
  `Kolicina` DECIMAL(10,2) NOT NULL,
  `JedinicaID` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`RECEPT_ReceptID`, `NAMIRNICA_NamirnicaIme`),
  INDEX `fk_RECEPT_has_NAMIRNICA_NAMIRNICA1_idx` (`NAMIRNICA_NamirnicaIme` ASC) VISIBLE,
  INDEX `fk_RECEPT_has_NAMIRNICA_RECEPT1_idx` (`RECEPT_ReceptID` ASC) VISIBLE,
  INDEX `fk_KORISTENA_JEDINICA_idx` (`JedinicaID` ASC) VISIBLE,
  CONSTRAINT `fk_RECEPT_has_NAMIRNICA_RECEPT1`
    FOREIGN KEY (`RECEPT_ReceptID`)
    REFERENCES `mydb`.`RECEPT` (`ReceptID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_RECEPT_has_NAMIRNICA_NAMIRNICA1`
    FOREIGN KEY (`NAMIRNICA_NamirnicaIme`)
    REFERENCES `mydb`.`NAMIRNICA` (`NamirnicaIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_KORISTENA_JEDINICA`
    FOREIGN KEY (`JedinicaID`)
    REFERENCES `mydb`.`JEDINICA` (`JedinicaID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`NAPRAVLJEN_RECEPT`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`NAPRAVLJEN_RECEPT` (
  `KorisnickoIme` VARCHAR(45) NOT NULL,
  `ReceptID` INT NOT NULL,
  `DatumIVrijemePravljenja` DATETIME NOT NULL,
  PRIMARY KEY (`KorisnickoIme`, `ReceptID`, `DatumIVrijemePravljenja`),
  INDEX `JE_NAPRAVLJEN_idx` (`ReceptID` ASC) VISIBLE,
  CONSTRAINT `JE_NAPRAVIO`
    FOREIGN KEY (`KorisnickoIme`)
    REFERENCES `mydb`.`KORISNIK` (`KorisnicnoIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `JE_NAPRAVLJEN`
    FOREIGN KEY (`ReceptID`)
    REFERENCES `mydb`.`RECEPT` (`ReceptID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`KOMENTAR`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`KOMENTAR` (
  `KorisnickoIme` VARCHAR(45) NOT NULL,
  `ReceptID` INT NOT NULL,
  `DatumVrijeme` DATETIME NOT NULL,
  `Tekst` TEXT NOT NULL,
  PRIMARY KEY (`KorisnickoIme`, `ReceptID`, `DatumVrijeme`),
  INDEX `ZA_idx` (`ReceptID` ASC) VISIBLE,
  CONSTRAINT `OSTAVLJA_KOMENTAR`
    FOREIGN KEY (`KorisnickoIme`)
    REFERENCES `mydb`.`KORISNIK` (`KorisnicnoIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `KOMENTAR_ZA_RECEPT`
    FOREIGN KEY (`ReceptID`)
    REFERENCES `mydb`.`RECEPT` (`ReceptID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`OCJENA`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`OCJENA` (
  `KorisnickoIme` VARCHAR(45) NOT NULL,
  `ReceptID` INT NOT NULL,
  `OCJENA` INT NOT NULL,
  PRIMARY KEY (`KorisnickoIme`, `ReceptID`),
  INDEX `ZA_idx` (`ReceptID` ASC) VISIBLE,
  CONSTRAINT `OSTAVLJA_OCJENU`
    FOREIGN KEY (`KorisnickoIme`)
    REFERENCES `mydb`.`KORISNIK` (`KorisnicnoIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `OCJENA_ZA_RECEPT`
    FOREIGN KEY (`ReceptID`)
    REFERENCES `mydb`.`RECEPT` (`ReceptID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`OMILJENI_RECEPT`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`OMILJENI_RECEPT` (
  `KorisnickoIme` VARCHAR(45) NOT NULL,
  `ReceptID` INT NOT NULL,
  PRIMARY KEY (`KorisnickoIme`, `ReceptID`),
  INDEX `ZA_idx` (`ReceptID` ASC) VISIBLE,
  CONSTRAINT `OZNACIO`
    FOREIGN KEY (`KorisnickoIme`)
    REFERENCES `mydb`.`KORISNIK` (`KorisnicnoIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `ZA`
    FOREIGN KEY (`ReceptID`)
    REFERENCES `mydb`.`RECEPT` (`ReceptID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`PRACENJE`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`PRACENJE` (
  `Pratilac_KorisnickoIme` VARCHAR(45) NOT NULL,
  `Pracenik_KorisnickoIme` VARCHAR(45) NOT NULL,
  `DatumPocetka` DATE NOT NULL,
  PRIMARY KEY (`Pratilac_KorisnickoIme`, `Pracenik_KorisnickoIme`),
  INDEX `fk_KORISNIK_has_KORISNIK_KORISNIK2_idx` (`Pracenik_KorisnickoIme` ASC) VISIBLE,
  INDEX `fk_KORISNIK_has_KORISNIK_KORISNIK1_idx` (`Pratilac_KorisnickoIme` ASC) VISIBLE,
  CONSTRAINT `fk_KORISNIK_has_KORISNIK_KORISNIK1`
    FOREIGN KEY (`Pratilac_KorisnickoIme`)
    REFERENCES `mydb`.`KORISNIK` (`KorisnicnoIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_KORISNIK_has_KORISNIK_KORISNIK2`
    FOREIGN KEY (`Pracenik_KorisnickoIme`)
    REFERENCES `mydb`.`KORISNIK` (`KorisnicnoIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`OBAVJESTENJE`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`OBAVJESTENJE` (
  `ObavjestenjeID` INT NOT NULL AUTO_INCREMENT,
  `Primatelj_KorisnickoIme` VARCHAR(45) NOT NULL,
  `ReceptID` INT NOT NULL,
  `DatumVrijeme` DATETIME NOT NULL,
  `Procitano` TINYINT NOT NULL DEFAULT 0,
  `Tekst` TEXT NULL,
  PRIMARY KEY (`ObavjestenjeID`),
  INDEX `PRIMA_idx` (`Primatelj_KorisnickoIme` ASC) VISIBLE,
  INDEX `ZA_idx` (`ReceptID` ASC) VISIBLE,
  CONSTRAINT `PRIMA`
    FOREIGN KEY (`Primatelj_KorisnickoIme`)
    REFERENCES `mydb`.`KORISNIK` (`KorisnicnoIme`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `OBAVJESTENJE_ZA_RECEPT`
    FOREIGN KEY (`ReceptID`)
    REFERENCES `mydb`.`RECEPT` (`ReceptID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
