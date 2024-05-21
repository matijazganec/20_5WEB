/*
SQLyog Community v13.2.1 (64 bit)
MySQL - 10.4.32-MariaDB : Database - webshop
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`webshop` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci */;

USE `webshop`;

/*Table structure for table `kategorija` */

DROP TABLE IF EXISTS `kategorija`;

CREATE TABLE `kategorija` (
  `idKategorija` int(11) NOT NULL AUTO_INCREMENT,
  `NazivKategorija` varchar(45) NOT NULL,
  PRIMARY KEY (`idKategorija`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `kategorija` */

insert  into `kategorija`(`idKategorija`,`NazivKategorija`) values 
(11,'Procesori'),
(12,'Grafičke kartice'),
(13,'RAM'),
(14,'Matične ploče'),
(15,'Diskovi'),
(16,'Napajanja'),
(17,'Hlađenje'),
(18,'Kučišta'),
(19,'Tipkovnice'),
(20,'Miševi'),
(21,'Monitori'),
(22,'Ostala periferija'),
(23,'Mrežne kartice'),
(24,'Zvučne kartice'),
(25,'Vanjski diskovi');

/*Table structure for table `korisnici` */

DROP TABLE IF EXISTS `korisnici`;

CREATE TABLE `korisnici` (
  `korisnicko_ime` varchar(30) NOT NULL,
  `email` varchar(50) NOT NULL,
  `prezime` varchar(255) NOT NULL,
  `ime` varchar(255) NOT NULL,
  `lozinka` varchar(255) NOT NULL,
  `ovlast` varchar(5) DEFAULT NULL,
  PRIMARY KEY (`korisnicko_ime`),
  KEY `FK_korisnici_ovlast` (`ovlast`),
  CONSTRAINT `FK_korisnici_ovlast` FOREIGN KEY (`ovlast`) REFERENCES `ovlasti` (`sifra`) ON DELETE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `korisnici` */

insert  into `korisnici`(`korisnicko_ime`,`email`,`prezime`,`ime`,`lozinka`,`ovlast`) values 
('admin','admin@net.hr','Zganec','Matija','jUPY60RIRBTWGhhlm0Q/v+UjmVENpGidU1K9ljHGxRs=','AD'),
('mod','pivanic@net.hr','Rator','Mode','9OGS0TpjNkgD0+dwSB1lpnsrlAZhsobZwZ5cQEtMOPo=','MO');

/*Table structure for table `kosarica` */

DROP TABLE IF EXISTS `kosarica`;

CREATE TABLE `kosarica` (
  `Id` int(10) NOT NULL AUTO_INCREMENT,
  `Naziv` varchar(50) DEFAULT NULL,
  `Cijena` decimal(10,0) DEFAULT NULL,
  `Kolicina` int(10) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `kosarica` */

insert  into `kosarica`(`Id`,`Naziv`,`Cijena`,`Kolicina`) values 
(1,'Intel i7',450,1),
(10,'NVidia RTX 4060Ti',650,1),
(13,'NVidia RTX 4060Ti',650,1),
(14,'AMD Radeon RX 6800',579,1);

/*Table structure for table `ovlasti` */

DROP TABLE IF EXISTS `ovlasti`;

CREATE TABLE `ovlasti` (
  `sifra` varchar(5) NOT NULL,
  `naziv` varchar(255) NOT NULL,
  PRIMARY KEY (`sifra`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `ovlasti` */

insert  into `ovlasti`(`sifra`,`naziv`) values 
('AD','Administrator'),
('MO','Moderator');

/*Table structure for table `proizvod` */

DROP TABLE IF EXISTS `proizvod`;

CREATE TABLE `proizvod` (
  `idProizvod` int(11) NOT NULL AUTO_INCREMENT,
  `Naziv` varchar(45) NOT NULL,
  `Opis` text DEFAULT NULL,
  `Proizvodac` varchar(45) DEFAULT NULL,
  `Kategorija` varchar(45) DEFAULT NULL,
  `Cijena` int(11) DEFAULT NULL,
  PRIMARY KEY (`idProizvod`),
  KEY `Kategorija` (`Kategorija`),
  KEY `Proizvodac` (`Proizvodac`)
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `proizvod` */

insert  into `proizvod`(`idProizvod`,`Naziv`,`Opis`,`Proizvodac`,`Kategorija`,`Cijena`) values 
(13,'AMD Ryzen 3 3200G','Proizvođač: AMD Socket AM4 Procesor tip AMD Ryzen 3 3200G Broj jezgri 4 Brzina: 3.6 GHz Cashe: 6 MB GPU AMD Radeon Vega 8 Graphics Hladnjak da','AMD','Procesori',95),
(14,'AMD Ryzen 5 5600G','SocketAM4 Procesor tipRyzen 5 Broj jezgri6 GPUDa HladnjakDa','AMD','Procesori',164),
(15,'AMD Ryzen 7 5800X3D','SocketAM4 Procesor tipRyzen 7 Broj jezgri8 GPUNe HladnjakNe','AMD','Procesori',400),
(16,'Intel Core i9-12900K',NULL,'Intel','Procesori',689),
(17,'AMD Ryzen 9 5950X',NULL,'AMD','Procesori',799),
(18,'Intel Core i9-11900K',NULL,'Intel','Procesori',529),
(19,'AMD Ryzen 9 5900X',NULL,'AMD','Procesori',549),
(20,'AMD Ryzen 7 5800X',NULL,'AMD','Procesori',389),
(21,'NVIDIA GeForce RTX 3080',NULL,'NVIDIA','Grafičke kartice',699),
(22,'NVIDIA GeForce RTX 3070',NULL,'NVIDIA','Grafičke kartice',499),
(23,'NVIDIA GeForce RTX 3060 Ti',NULL,'NVIDIA','Grafičke kartice',399),
(24,'AMD Radeon RX 6900 XT',NULL,'AMD','Grafičke kartice',999),
(25,'AMD Radeon RX 6800 XT',NULL,'AMD','Grafičke kartice',659),
(26,'AMD Radeon RX 6800',NULL,'AMD','Grafičke kartice',579),
(27,'Corsair Vengeance LPX 16GB','(2x8GB) DDR4 3200MHz','Corsair','RAM',79),
(28,'Kingston HyperX Fury 8GB','DDR4 2666MHz','Kingston','RAM',49),
(29,'Crucial Ballistix Sport LT 16GB','(2x8GB) DDR4 3000MHz','Crucial','RAM',85),
(30,'HyperX Predator RGB 32GB','(4x8GB) DDR4 3200MHz','HyperX','RAM',159),
(31,'ASUS ROG Strix Z590-E Gaming',NULL,'Asus','Matične ploče',349),
(32,'Gigabyte Aorus B550 Master',NULL,'Gigabyte','Matične ploče',289),
(33,'ASUS TUF Gaming B550M-Plus',NULL,'Asus','Matične ploče',129),
(34,'Samsung 970 EVO Plus','SSD','Samsung','Diskovi',129),
(35,'WD Blue SN550',NULL,'Western Digital','Diskovi',69),
(36,'Crucial MX500',NULL,'Crucial','Diskovi',89),
(37,'Corsair RM750x',NULL,'Corsair','Napajanja',120),
(38,'Seasonic Focus Plus 750 Platinum',NULL,'Seasonic','Napajanja',140),
(39,'NZXT H510',NULL,'NZXT','Kučišta',79),
(40,'Fractal Design Meshify C',NULL,'Fractal Design','Kučišta',89),
(41,'Logitech G502 Hero',NULL,'Logitech','Miševi',110),
(42,'Razer DeathAdder Elite',NULL,'Razer','Miševi',69),
(43,'Logitech G Pro X Mechanical Gaming Keyboard',NULL,'Logitech','Tipkovnice',149),
(44,'Corsair K95 RGB Platinum Mechanical Gaming Ke',NULL,'Corsair','Tipkovnice',199),
(45,'ASUS VG248QE',NULL,'Asus','Monitori',249),
(46,'Dell UltraSharp U2415',NULL,'Dell','Monitori',299);

/*Table structure for table `proizvodac` */

DROP TABLE IF EXISTS `proizvodac`;

CREATE TABLE `proizvodac` (
  `idProizvodac` int(11) NOT NULL AUTO_INCREMENT,
  `ProizvodacName` varchar(45) NOT NULL,
  PRIMARY KEY (`idProizvodac`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

/*Data for the table `proizvodac` */

insert  into `proizvodac`(`idProizvodac`,`ProizvodacName`) values 
(2,'Aoc'),
(3,'ASrock'),
(5,'Dell'),
(6,'HyperX'),
(12,'Samsung'),
(14,'Intel'),
(15,'AMD'),
(16,'NVIDIA'),
(17,'Asus'),
(18,'Gigabyte'),
(19,'MSI'),
(20,'Corsair'),
(21,'Kingston'),
(22,'Crucial'),
(23,'Western Digital'),
(24,'Seagate'),
(25,'Samsung'),
(26,'EVGA'),
(27,'Thermaltake'),
(28,'Cooler Master'),
(29,'NZXT'),
(30,'Fractal Design'),
(31,'Phanteks'),
(32,'Razer'),
(33,'Logitech'),
(34,'Seasonic');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
