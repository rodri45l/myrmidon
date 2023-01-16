-- MariaDB dump 10.19  Distrib 10.9.4-MariaDB, for Linux (x86_64)
--
-- Host: localhost    Database: myrmidon
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `AppointmentUser`
--

DROP TABLE IF EXISTS `AppointmentUser`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AppointmentUser`
(
    `AppointmentId` int                                                   NOT NULL,
    `Id`            char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    PRIMARY KEY (`AppointmentId`, `Id`),
    KEY             `Id` (`Id`),
    CONSTRAINT `AppointmentUsers_ibfk_1` FOREIGN KEY (`AppointmentId`) REFERENCES `appointments` (`appointment_id`),
    CONSTRAINT `AppointmentUsers_ibfk_2` FOREIGN KEY (`Id`) REFERENCES `User` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AppointmentUser`
--

LOCK
TABLES `AppointmentUser` WRITE;
/*!40000 ALTER TABLE `AppointmentUser` DISABLE KEYS */;
/*!40000 ALTER TABLE `AppointmentUser` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `AspNetRoleClaims`
--

DROP TABLE IF EXISTS `AspNetRoleClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetRoleClaims`
(
    `Id`         int                                                   NOT NULL AUTO_INCREMENT,
    `RoleId`     char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    `ClaimType`  longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    PRIMARY KEY (`Id`),
    KEY          `IX_AspNetRoleClaims_RoleId` (`RoleId`),
    CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoleClaims`
--

LOCK
TABLES `AspNetRoleClaims` WRITE;
/*!40000 ALTER TABLE `AspNetRoleClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoleClaims` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `AspNetRoles`
--

DROP TABLE IF EXISTS `AspNetRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetRoles`
(
    `Id`               char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    `Name`             varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
    `NormalizedName`   varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetRoles`
--

LOCK
TABLES `AspNetRoles` WRITE;
/*!40000 ALTER TABLE `AspNetRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetRoles` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `AspNetUserClaims`
--

DROP TABLE IF EXISTS `AspNetUserClaims`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserClaims`
(
    `Id`         int                                                   NOT NULL AUTO_INCREMENT,
    `UserId`     char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    `ClaimType`  longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    `ClaimValue` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    PRIMARY KEY (`Id`),
    KEY          `IX_AspNetUserClaims_UserId` (`UserId`),
    CONSTRAINT `FK_AspNetUserClaims_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserClaims`
--

LOCK
TABLES `AspNetUserClaims` WRITE;
/*!40000 ALTER TABLE `AspNetUserClaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserClaims` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `AspNetUserLogins`
--

DROP TABLE IF EXISTS `AspNetUserLogins`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserLogins`
(
    `UserId`              char(36) CHARACTER SET ascii COLLATE ascii_general_ci         NOT NULL,
    `LoginProvider`       varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `ProviderKey`         varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `ProviderDisplayName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    KEY                   `IX_AspNetUserLogins_UserId` (`UserId`),
    CONSTRAINT `FK_AspNetUserLogins_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserLogins`
--

LOCK
TABLES `AspNetUserLogins` WRITE;
/*!40000 ALTER TABLE `AspNetUserLogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserLogins` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `AspNetUserRoles`
--

DROP TABLE IF EXISTS `AspNetUserRoles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserRoles`
(
    `UserId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    `RoleId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    PRIMARY KEY (`UserId`, `RoleId`),
    KEY      `IX_AspNetUserRoles_RoleId` (`RoleId`),
    CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `AspNetRoles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_AspNetUserRoles_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserRoles`
--

LOCK
TABLES `AspNetUserRoles` WRITE;
/*!40000 ALTER TABLE `AspNetUserRoles` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserRoles` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `AspNetUserTokens`
--

DROP TABLE IF EXISTS `AspNetUserTokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AspNetUserTokens`
(
    `UserId`        char(36) CHARACTER SET ascii COLLATE ascii_general_ci         NOT NULL,
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `Name`          varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `Value`         longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_AspNetUserTokens_User_UserId` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AspNetUserTokens`
--

LOCK
TABLES `AspNetUserTokens` WRITE;
/*!40000 ALTER TABLE `AspNetUserTokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `AspNetUserTokens` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `Facts`
--

DROP TABLE IF EXISTS `Facts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Facts`
(
    `fact_id`    int                                                           NOT NULL AUTO_INCREMENT,
    `fact`       varchar(511) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `last_shown` datetime DEFAULT NULL,
    PRIMARY KEY (`fact_id`),
    UNIQUE KEY `fact_id` (`fact_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Facts`
--

LOCK
TABLES `Facts` WRITE;
/*!40000 ALTER TABLE `Facts` DISABLE KEYS */;
/*!40000 ALTER TABLE `Facts` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `Journal_entry`
--

DROP TABLE IF EXISTS `Journal_entry`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Journal_entry`
(
    `journal_entry_id` int                                                            NOT NULL AUTO_INCREMENT,
    `journal_entry`    varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `date`             datetime                                                       NOT NULL,
    `Id`               char(36) CHARACTER SET ascii COLLATE ascii_general_ci          NOT NULL,
    PRIMARY KEY (`journal_entry_id`),
    KEY                `Id1` (`Id`),
    CONSTRAINT `Journal_entry_ibfk_1` FOREIGN KEY (`Id`) REFERENCES `User` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Journal_entry`
--

LOCK
TABLES `Journal_entry` WRITE;
/*!40000 ALTER TABLE `Journal_entry` DISABLE KEYS */;
/*!40000 ALTER TABLE `Journal_entry` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `Mood`
--

DROP TABLE IF EXISTS `Mood`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Mood`
(
    `mood_id` int                                                   NOT NULL AUTO_INCREMENT,
    `rating`  int                                                   NOT NULL,
    `date`    datetime                                              NOT NULL,
    `Id`      char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    PRIMARY KEY (`mood_id`),
    KEY       `Id2` (`Id`),
    CONSTRAINT `Mood_ibfk_1` FOREIGN KEY (`Id`) REFERENCES `User` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Mood`
--

LOCK
TABLES `Mood` WRITE;
/*!40000 ALTER TABLE `Mood` DISABLE KEYS */;
/*!40000 ALTER TABLE `Mood` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `Patient`
--

DROP TABLE IF EXISTS `Patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Patient`
(
    `patient_id`      char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    `medical_history` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
    `Id`              char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    PRIMARY KEY (`patient_id`),
    KEY               `Id3` (`Id`),
    CONSTRAINT `Patient_ibfk_1` FOREIGN KEY (`Id`) REFERENCES `User` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Patient`
--

LOCK
TABLES `Patient` WRITE;
/*!40000 ALTER TABLE `Patient` DISABLE KEYS */;
/*!40000 ALTER TABLE `Patient` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `PatientTherapist`
--

DROP TABLE IF EXISTS `PatientTherapist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PatientTherapist`
(
    `PatientId`   char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    `TherapistId` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    PRIMARY KEY (`PatientId`, `TherapistId`),
    KEY           `therapist_id` (`TherapistId`),
    CONSTRAINT `PatientTherapist_ibfk_1` FOREIGN KEY (`PatientId`) REFERENCES `Patient` (`patient_id`) ON DELETE CASCADE,
    CONSTRAINT `PatientTherapist_ibfk_2` FOREIGN KEY (`TherapistId`) REFERENCES `Therapist` (`therapist_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PatientTherapist`
--

LOCK
TABLES `PatientTherapist` WRITE;
/*!40000 ALTER TABLE `PatientTherapist` DISABLE KEYS */;
/*!40000 ALTER TABLE `PatientTherapist` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `Tension`
--

DROP TABLE IF EXISTS `Tension`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Tension`
(
    `tension_id` int                                                   NOT NULL AUTO_INCREMENT,
    `rating`     int                                                   NOT NULL,
    `date`       datetime                                              NOT NULL,
    `Id`         char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    PRIMARY KEY (`tension_id`),
    KEY          `Id4` (`Id`),
    CONSTRAINT `Tension_ibfk_1` FOREIGN KEY (`Id`) REFERENCES `User` (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tension`
--

LOCK
TABLES `Tension` WRITE;
/*!40000 ALTER TABLE `Tension` DISABLE KEYS */;
/*!40000 ALTER TABLE `Tension` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `Therapist`
--

DROP TABLE IF EXISTS `Therapist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Therapist`
(
    `therapist_id` char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    `Id`           char(36) CHARACTER SET ascii COLLATE ascii_general_ci NOT NULL,
    PRIMARY KEY (`therapist_id`),
    KEY            `Id5` (`Id`),
    CONSTRAINT `Therapist_ibfk_1` FOREIGN KEY (`Id`) REFERENCES `User` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Therapist`
--

LOCK
TABLES `Therapist` WRITE;
/*!40000 ALTER TABLE `Therapist` DISABLE KEYS */;
/*!40000 ALTER TABLE `Therapist` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `User`
(
    `name`                 varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `surname`              varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `birth_date`           datetime                                                      NOT NULL,
    `postal_code`          varchar(12) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci  NOT NULL,
    `address`              varchar(320) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `sex`                  tinyint(1) NOT NULL,
    `gender`               varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci  DEFAULT NULL,
    `deleted`              tinyint(1) NOT NULL,
    `Id`                   char(36) CHARACTER SET ascii COLLATE ascii_general_ci         NOT NULL,
    `UserName`             varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
    `NormalizedUserName`   varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
    `Email`                varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
    `NormalizedEmail`      varchar(256) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
    `EmailConfirmed`       tinyint(1) NOT NULL,
    `PasswordHash`         longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    `SecurityStamp`        longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    `ConcurrencyStamp`     longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    `PhoneNumber`          longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
    `PhoneNumberConfirmed` tinyint(1) NOT NULL,
    `TwoFactorEnabled`     tinyint(1) NOT NULL,
    `LockoutEnd`           datetime(6) DEFAULT NULL,
    `LockoutEnabled`       tinyint(1) NOT NULL,
    `AccessFailedCount`    int                                                           NOT NULL,
    PRIMARY KEY (`Id`),
    UNIQUE KEY `Id6` (`Id`),
    UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
    KEY                    `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK
TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
INSERT INTO `User`
VALUES ('string', 'string', '2023-01-16 06:32:34', 'string', 'string', 1, 'string', 0,
        '08daf78b-7ead-4887-8334-be94870d4a8f', 'string', 'STRING', 'string@sdaf.com', 'STRING@SDAF.COM', 0,
        'AQAAAAIAAYagAAAAENSd5J7LGOnJnpbvTjnQVCX6RIkmQfAQ0hPBQu+fwkWnqSNvNyBneZfVX8b3bGYUMg==',
        'GRY5EI6FTH3FOJUU2QT7FFWXQDPIGKBQ', '005d4aed-6e0a-41ea-a67e-d6d86e65827a', 'string', 0, 0, NULL, 1, 0);
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `__EFMigrationsHistory`
--

DROP TABLE IF EXISTS `__EFMigrationsHistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `__EFMigrationsHistory`
(
    `MigrationId`    varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci  NOT NULL,
    PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__EFMigrationsHistory`
--

LOCK
TABLES `__EFMigrationsHistory` WRITE;
/*!40000 ALTER TABLE `__EFMigrationsHistory` DISABLE KEYS */;
INSERT INTO `__EFMigrationsHistory`
VALUES ('20230115171125_IdentityUserMigration', '7.0.2'),
       ('20230116061843_IdentityUserMigration2', '7.0.2');
/*!40000 ALTER TABLE `__EFMigrationsHistory` ENABLE KEYS */;
UNLOCK
TABLES;

--
-- Table structure for table `appointments`
--

DROP TABLE IF EXISTS `appointments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `appointments`
(
    `appointment_id` int      NOT NULL AUTO_INCREMENT,
    `date`           datetime NOT NULL,
    `notes`          varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
    PRIMARY KEY (`appointment_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointments`
--

LOCK
TABLES `appointments` WRITE;
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;
UNLOCK
TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-01-16  7:40:26
