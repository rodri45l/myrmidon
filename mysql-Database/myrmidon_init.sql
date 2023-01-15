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
-- Table structure for table `AppointmentUsers`
--

DROP TABLE IF EXISTS `AppointmentUsers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `AppointmentUsers` (
  `appointment_id` int NOT NULL,
  `user_id` char(36) NOT NULL,
  PRIMARY KEY (`appointment_id`,`user_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `AppointmentUsers_ibfk_1` FOREIGN KEY (`appointment_id`) REFERENCES `appointments` (`appointment_id`),
  CONSTRAINT `AppointmentUsers_ibfk_2` FOREIGN KEY (`user_id`) REFERENCES `User` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `AppointmentUsers`
--

LOCK TABLES `AppointmentUsers` WRITE;
/*!40000 ALTER TABLE `AppointmentUsers` DISABLE KEYS */;
/*!40000 ALTER TABLE `AppointmentUsers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Facts`
--

DROP TABLE IF EXISTS `Facts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Facts` (
  `fact_id` int NOT NULL AUTO_INCREMENT,
  `fact` varchar(511) NOT NULL,
  `last_shown` datetime DEFAULT NULL,
  PRIMARY KEY (`fact`),
  UNIQUE KEY `fact_id` (`fact_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Facts`
--

LOCK TABLES `Facts` WRITE;
/*!40000 ALTER TABLE `Facts` DISABLE KEYS */;
/*!40000 ALTER TABLE `Facts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Journal_entry`
--

DROP TABLE IF EXISTS `Journal_entry`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Journal_entry` (
  `journal_entry_id` int NOT NULL AUTO_INCREMENT,
  `journal_entry` varchar(1000) NOT NULL,
  `date` datetime NOT NULL,
  `user_id` char(36) NOT NULL,
  PRIMARY KEY (`journal_entry_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `Journal_entry_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `User` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Journal_entry`
--

LOCK TABLES `Journal_entry` WRITE;
/*!40000 ALTER TABLE `Journal_entry` DISABLE KEYS */;
/*!40000 ALTER TABLE `Journal_entry` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Mood`
--

DROP TABLE IF EXISTS `Mood`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Mood` (
  `mood_id` int NOT NULL AUTO_INCREMENT,
  `rating` int NOT NULL,
  `date` datetime NOT NULL,
  `user_id` char(36) NOT NULL,
  PRIMARY KEY (`mood_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `Mood_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `User` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Mood`
--

LOCK TABLES `Mood` WRITE;
/*!40000 ALTER TABLE `Mood` DISABLE KEYS */;
/*!40000 ALTER TABLE `Mood` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Patient`
--

DROP TABLE IF EXISTS `Patient`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Patient` (
  `patient_id` char(36) NOT NULL,
  `medical_history` varchar(1000) DEFAULT NULL,
  `user_id` char(36) NOT NULL,
  PRIMARY KEY (`patient_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `Patient_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `User` (`user_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Patient`
--

LOCK TABLES `Patient` WRITE;
/*!40000 ALTER TABLE `Patient` DISABLE KEYS */;
/*!40000 ALTER TABLE `Patient` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `PatientTherapist`
--

DROP TABLE IF EXISTS `PatientTherapist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `PatientTherapist` (
  `patient_id` char(36) NOT NULL,
  `therapist_id` char(36) NOT NULL,
  PRIMARY KEY (`patient_id`,`therapist_id`),
  KEY `therapist_id` (`therapist_id`),
  CONSTRAINT `PatientTherapist_ibfk_1` FOREIGN KEY (`patient_id`) REFERENCES `Patient` (`patient_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `PatientTherapist_ibfk_2` FOREIGN KEY (`therapist_id`) REFERENCES `Therapist` (`therapist_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `PatientTherapist`
--

LOCK TABLES `PatientTherapist` WRITE;
/*!40000 ALTER TABLE `PatientTherapist` DISABLE KEYS */;
/*!40000 ALTER TABLE `PatientTherapist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Tension`
--

DROP TABLE IF EXISTS `Tension`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Tension` (
  `tension_id` int NOT NULL AUTO_INCREMENT,
  `rating` int NOT NULL,
  `date` datetime NOT NULL,
  `user_id` char(36) NOT NULL,
  PRIMARY KEY (`tension_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `Tension_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `User` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Tension`
--

LOCK TABLES `Tension` WRITE;
/*!40000 ALTER TABLE `Tension` DISABLE KEYS */;
/*!40000 ALTER TABLE `Tension` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Therapist`
--

DROP TABLE IF EXISTS `Therapist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Therapist` (
  `therapist_id` char(36) NOT NULL,
  `user_id` char(36) NOT NULL,
  PRIMARY KEY (`therapist_id`),
  KEY `user_id` (`user_id`),
  CONSTRAINT `Therapist_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `User` (`user_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Therapist`
--

LOCK TABLES `Therapist` WRITE;
/*!40000 ALTER TABLE `Therapist` DISABLE KEYS */;
/*!40000 ALTER TABLE `Therapist` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `User`
--

DROP TABLE IF EXISTS `User`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `User` (
  `user_id` char(36) NOT NULL,
  `name` varchar(100) NOT NULL,
  `surname` varchar(100) NOT NULL,
  `birth_date` datetime NOT NULL,
  `postal_code` varchar(12) NOT NULL,
  `email` varchar(320) NOT NULL,
  `address` varchar(320) NOT NULL,
  `phone` varchar(12) NOT NULL,
  `sex` tinyint(1) NOT NULL,
  `gender` varchar(20) DEFAULT NULL,
  `password` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_bin DEFAULT NULL,
  `UserType` enum('Patient','Therapist') NOT NULL DEFAULT 'Patient',
  `deleted` tinyint(1) NOT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `user_id` (`user_id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `User`
--

LOCK TABLES `User` WRITE;
/*!40000 ALTER TABLE `User` DISABLE KEYS */;
/*!40000 ALTER TABLE `User` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `appointments`
--

DROP TABLE IF EXISTS `appointments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `appointments` (
  `appointment_id` int NOT NULL AUTO_INCREMENT,
  `date` datetime NOT NULL,
  `notes` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`appointment_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointments`
--

LOCK TABLES `appointments` WRITE;
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `session_tokens`
--

DROP TABLE IF EXISTS `session_tokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `session_tokens` (
  `token_id` char(36) NOT NULL,
  `user_id` char(36) NOT NULL,
  `expiration_time` timestamp NOT NULL,
  `ip_address` varchar(45) NOT NULL,
  PRIMARY KEY (`token_id`),
  UNIQUE KEY `token_id` (`token_id`),
  UNIQUE KEY `user_id_idx` (`user_id`),
  KEY `token_id_idx` (`token_id`),
  CONSTRAINT `session_tokens_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `User` (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `session_tokens`
--

LOCK TABLES `session_tokens` WRITE;
/*!40000 ALTER TABLE `session_tokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `session_tokens` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-01-15  8:38:16
