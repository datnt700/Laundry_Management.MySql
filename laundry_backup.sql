-- MySQL dump 10.13  Distrib 8.0.31, for Win64 (x86_64)
--
-- Host: localhost    Database: laundry
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `location`
--

DROP TABLE IF EXISTS `location`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `location` (
  `location_id` int NOT NULL AUTO_INCREMENT,
  `location_name` varchar(45) NOT NULL,
  `coordinates` varchar(20) DEFAULT NULL,
  `is_active` bit(10) DEFAULT NULL,
  `user_id_host` int DEFAULT NULL,
  PRIMARY KEY (`location_id`),
  KEY `fk_location_users1_idx` (`user_id_host`),
  CONSTRAINT `fk_location_users1` FOREIGN KEY (`user_id_host`) REFERENCES `users` (`user_id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `location`
--

LOCK TABLES `location` WRITE;
/*!40000 ALTER TABLE `location` DISABLE KEYS */;
INSERT INTO `location` VALUES (1,'Hanoi','100000',_binary '\0',1),(2,'Hochiminh','70000',_binary '\0',2),(3,'Paris','75000',_binary '\0',3);
/*!40000 ALTER TABLE `location` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `machine`
--

DROP TABLE IF EXISTS `machine`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `machine` (
  `machine_id` int NOT NULL AUTO_INCREMENT,
  `machine_name` varchar(250) NOT NULL,
  `machine_type` tinyint DEFAULT NULL,
  `branch` varchar(250) DEFAULT NULL,
  `size` varchar(45) DEFAULT NULL,
  `is_active` bit(10) DEFAULT NULL,
  `status` tinyint DEFAULT NULL,
  `location_id` int DEFAULT NULL,
  PRIMARY KEY (`machine_id`),
  KEY `fk_machine_location1_idx` (`location_id`),
  CONSTRAINT `fk_machine_location1` FOREIGN KEY (`location_id`) REFERENCES `location` (`location_id`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=28 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine`
--

LOCK TABLES `machine` WRITE;
/*!40000 ALTER TABLE `machine` DISABLE KEYS */;
INSERT INTO `machine` VALUES (1,'Toshiba7800',1,'Toshiba','9kgabcd',_binary '\0',5,1),(3,'Adidasphat0700',1,'Adidas','25kg',_binary '\0',3,1),(8,'alao',1,'Nikee','15kg',_binary '\0',4,2),(9,'hihihihi',1,'Nike','15kg',_binary '\0',4,2),(12,'lslsls',1,'hahaha','25kg',NULL,4,NULL),(13,'sdfdsf',1,'hahaha','25kg',NULL,4,NULL),(18,'Adidasphat0700',1,'Nike','15kg',NULL,4,NULL),(19,'Adidasphat0700',1,'Nike','15kg',NULL,4,NULL),(20,'trongdat',1,'toshiba','25kg',NULL,3,NULL),(21,'trongdat',1,'toshiba','25kg',NULL,23,NULL),(22,'dat',1,'1','1',NULL,1,NULL),(23,'dat',1,'11','1',NULL,1,NULL),(26,'hieuth',1,'shizuka','9kgabcd',NULL,1,NULL),(27,'test',1,'1','1',NULL,3,NULL);
/*!40000 ALTER TABLE `machine` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `machine_history`
--

DROP TABLE IF EXISTS `machine_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `machine_history` (
  `history_id` int NOT NULL AUTO_INCREMENT,
  `user_id` int DEFAULT NULL,
  `time_use` time DEFAULT NULL,
  `status` tinyint DEFAULT NULL,
  `money` bigint DEFAULT NULL,
  `location_id` int DEFAULT NULL,
  `machine_id` int DEFAULT NULL,
  PRIMARY KEY (`history_id`),
  KEY `fk_machine_history_users1_idx` (`user_id`),
  KEY `fk_machine_history_location1_idx` (`location_id`),
  KEY `fk_machine_history_machine1_idx` (`machine_id`),
  CONSTRAINT `fk_machine_history_location` FOREIGN KEY (`location_id`) REFERENCES `location` (`location_id`) ON UPDATE CASCADE,
  CONSTRAINT `fk_machine_history_machine` FOREIGN KEY (`machine_id`) REFERENCES `machine` (`machine_id`) ON UPDATE CASCADE,
  CONSTRAINT `fk_machine_history_users` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine_history`
--

LOCK TABLES `machine_history` WRITE;
/*!40000 ALTER TABLE `machine_history` DISABLE KEYS */;
/*!40000 ALTER TABLE `machine_history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `machine_mode`
--

DROP TABLE IF EXISTS `machine_mode`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `machine_mode` (
  `machine_mode_id` int NOT NULL,
  `service_mode_id` int DEFAULT NULL,
  `machine_id` int DEFAULT NULL,
  PRIMARY KEY (`machine_mode_id`),
  KEY `fk_machine_mode_service_mode1_idx` (`service_mode_id`),
  KEY `fk_machine_mode_machine1_idx` (`machine_id`),
  CONSTRAINT `fk_machine_mode_machine1` FOREIGN KEY (`machine_id`) REFERENCES `machine` (`machine_id`) ON UPDATE CASCADE,
  CONSTRAINT `fk_machine_mode_service_mode1` FOREIGN KEY (`service_mode_id`) REFERENCES `service_mode` (`mode_id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `machine_mode`
--

LOCK TABLES `machine_mode` WRITE;
/*!40000 ALTER TABLE `machine_mode` DISABLE KEYS */;
/*!40000 ALTER TABLE `machine_mode` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role`
--

DROP TABLE IF EXISTS `role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role` (
  `role_id` int NOT NULL AUTO_INCREMENT,
  `role_name` varchar(100) NOT NULL,
  `is_active` bit(10) DEFAULT NULL,
  PRIMARY KEY (`role_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role`
--

LOCK TABLES `role` WRITE;
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` VALUES (1,'Admin',_binary '\0'),(2,'User',_binary '\0');
/*!40000 ALTER TABLE `role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `role_user`
--

DROP TABLE IF EXISTS `role_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `role_user` (
  `role_user_id` int NOT NULL,
  `role_id` int DEFAULT NULL,
  `user_id` int DEFAULT NULL,
  PRIMARY KEY (`role_user_id`),
  KEY `fk_role_user_role1_idx` (`role_id`),
  KEY `fk_role_user_users1_idx` (`user_id`),
  CONSTRAINT `fk_role_user_role` FOREIGN KEY (`role_id`) REFERENCES `role` (`role_id`) ON UPDATE CASCADE,
  CONSTRAINT `fk_role_user_users` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `role_user`
--

LOCK TABLES `role_user` WRITE;
/*!40000 ALTER TABLE `role_user` DISABLE KEYS */;
INSERT INTO `role_user` VALUES (1,1,1),(2,2,2);
/*!40000 ALTER TABLE `role_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `service_mode`
--

DROP TABLE IF EXISTS `service_mode`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `service_mode` (
  `mode_id` int NOT NULL AUTO_INCREMENT,
  `mode_name` varchar(250) NOT NULL,
  `is_active` bit(10) DEFAULT NULL,
  `price_per_minute` double DEFAULT NULL,
  `price_per_size` double DEFAULT NULL,
  PRIMARY KEY (`mode_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `service_mode`
--

LOCK TABLES `service_mode` WRITE;
/*!40000 ALTER TABLE `service_mode` DISABLE KEYS */;
/*!40000 ALTER TABLE `service_mode` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `user_id` int NOT NULL AUTO_INCREMENT,
  `user_name` varchar(250) NOT NULL,
  `pass_hash` varchar(250) DEFAULT NULL,
  `salt` varchar(100) DEFAULT NULL,
  `phone_number` varchar(15) DEFAULT NULL,
  `account_type` tinyint DEFAULT NULL,
  `is_active` bit(10) DEFAULT NULL,
  `is_lock` bit(10) DEFAULT NULL,
  `create_date` datetime DEFAULT NULL,
  `money` bigint DEFAULT NULL,
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `email_UNIQUE` (`salt`)
) ENGINE=InnoDB AUTO_INCREMENT=37 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'ahahhaha','datbeo','1234','11111111',1,_binary '\0',_binary '\0\0','2021-02-15 09:55:12',250000),(2,'nam','namct','1212','0872309487',1,_binary '\0',_binary '\0\0','2022-03-10 10:50:01',50000),(3,'hhhhh','eHh4eHhmN2ZlNjU0NC00MmU2LTQ1ZmItOWIxMS0yMWE3M2UyNzYwOWE=','f7fe6544-42e6-45fb-9b11-21a73e27609a','0872309487',NULL,NULL,NULL,NULL,25000),(4,'datco','eXl5eXl5MjdkYzc2YzMtZjIwYS00NTc2LTllNDktYWNmYjdhMTA1ZTgz','27dc76c3-f20a-4576-9e49-acfb7a105e83','222',NULL,NULL,NULL,NULL,NULL),(7,'sadf','ZWVlZWVlNzIyZjllYWItODY2ZC00MGY5LTg3MzUtNjg2MDdjNDY2MWI5','722f9eab-866d-40f9-8735-68607c4661b9','333',NULL,NULL,NULL,NULL,NULL),(8,'ghjghj','dHR0dHR0OWQ3Mzg1MjMtZTZkMS00ZTQ1LTk0ZjgtYmY2Mzc3MzRjOGE4','9d738523-e6d1-4e45-94f8-bf637734c8a8','4444',NULL,NULL,NULL,NULL,NULL),(9,'kjh','dHR0dHR0MGJjNzJjZDItYjIyNS00YmMzLTg3ZTAtZmRiYzI3NDVjZTc2','0bc72cd2-b225-4bc3-87e0-fdbc2745ce76','5555',NULL,NULL,NULL,NULL,NULL),(10,'trshfg','ZmZmZmY4OWQyNWE4ZC1lYzYwLTRkZjgtYWQzZC04YTI5NjVhZWZiNGE=','89d25a8d-ec60-4df8-ad3d-8a2965aefb4a','66666',NULL,NULL,NULL,NULL,NULL),(11,'bnbnbnb','Ql1Z+vbz2fBd4+E+U+LAPhOPf6rtWe2rmtxWm2y7HiA=','3b5ef60b-c684-4fc1-8bd8-600c14cc9fa3','tttttt',NULL,NULL,NULL,NULL,NULL),(12,'dyudyu','aGhoaGhiOTIyYmU5MS0xNmVkLTRjYTMtYjI4MC05OTQwZmIzYWViZjE=','b922be91-16ed-4ca3-b280-9940fb3aebf1','77777',NULL,NULL,NULL,NULL,NULL),(13,'dat09',NULL,NULL,'99999999',NULL,NULL,NULL,'2022-12-15 17:20:03',NULL),(14,'dat09','aGhoaGhoMDVlNjZkODctNTUyMy00NTFmLWFmOTMtNjAwOGY5Mjc2MDZk','05e66d87-5523-451f-af93-6008f927606d','99999999',NULL,NULL,NULL,'2022-12-20 10:01:20',NULL),(15,'namct','MzAwMGEwNjM0ZWE0LTk2ZWItNDY0OS05ODU1LTZiNDAxODhhZTQyOQ==','a0634ea4-96eb-4649-9855-6b40188ae429','12341234',NULL,NULL,NULL,'2022-12-26 07:30:48',NULL),(16,'Hungcaro','dnZ2dnYwYTg0NGU2Ni05YTI1LTRlNjQtODQzZS1iYTcwOTZhNWIyOGI=','0a844e66-9a25-4e64-843e-ba7096a5b28b','111111',NULL,NULL,NULL,'2022-12-26 07:35:05',NULL),(17,'dat','YmVvYmJlOWMzNDAtNzk3OC00ZTE1LWE2MTUtNDlkZGYyOTk0YWZk','bbe9c340-7978-4e15-a615-49ddf2994afd','567',NULL,NULL,NULL,'2022-12-29 13:14:46',NULL),(18,'dat','YmVvMWY2YjE5ODYtNjZmMy00OTU0LWEyNjQtNWNjNzJiNzMzMGQ0','1f6b1986-66f3-4954-a264-5cc72b7330d4','567',NULL,NULL,NULL,'2022-12-29 13:14:46',NULL),(19,'dat','YmVvMjBkMWY3ZGQtNzJiMC00NjAyLTllYTYtMzAzM2FiZTM0ODAx','20d1f7dd-72b0-4602-9ea6-3033abe34801','567',NULL,NULL,NULL,'2022-12-29 13:14:46',NULL),(20,'dat','YmVvY2YzNGU1ZGUtMmNiMi00Yzg4LWE3MWQtNTgyNDkzODk3MTcz','cf34e5de-2cb2-4c88-a71d-582493897173','567',NULL,NULL,NULL,'2022-12-29 13:14:46',NULL),(21,'dat','YmVvNzE2NmRiZjYtMDg4NS00ZDI4LWE0YjctZjA2ZWEyNDYwNGM1','7166dbf6-0885-4d28-a4b7-f06ea24604c5','567',NULL,NULL,NULL,'2022-12-29 13:14:46',NULL),(22,'dat','YmVvNGYxZTE0ZjMtNDUyZi00NTQ3LTg0NDgtNzFhOGE2NmJmMzMz','4f1e14f3-452f-4547-8448-71a8a66bf333','567',NULL,NULL,NULL,'2022-12-29 13:14:46',NULL),(23,'dat','YmVvNjc5MmQxODgtZjNlYi00NmI1LTg3MmEtYjI1ZWU0NjMxMWIz','6792d188-f3eb-46b5-872a-b25ee46311b3','567',NULL,NULL,NULL,'2022-12-29 13:14:46',NULL),(24,'dat','YmVvMGY3MzBmYTgtMDZkNy00NjQ4LTkyMWQtMzc0ODNiNGY0OWY3','0f730fa8-06d7-4648-921d-37483b4f49f7','567',NULL,NULL,NULL,'2022-12-29 13:15:03',NULL),(25,'dat','YmVvYmFkYzQ3MGYtNGQwNS00M2MwLWIyMGUtZWMzMTUxOGZiMmZl','badc470f-4d05-43c0-b20e-ec31518fb2fe','7878',NULL,NULL,NULL,'2022-12-29 13:18:17',NULL),(26,'dat','YmVvZDYwMDY1Y2YtMzViMS00ZDk5LTllMGUtODZlOWIyYzA5NGRi','d60065cf-35b1-4d99-9e0e-86e9b2c094db','7878',NULL,NULL,NULL,'2022-12-29 13:19:24',NULL),(27,'hihihi','ZGF0YmVvM2NjNmFmYjktY2JmMi00NmM4LTlmYjEtZDI4YTA3OGEwZWQ4','3cc6afb9-cbf2-46c8-9fb1-d28a078a0ed8','676767',NULL,NULL,NULL,'2022-12-29 13:20:05',NULL),(28,'Marie','dHJvaXN1bjIyMmVkNzM1LWIxZTEtNDIxZS1iZTk3LWE2MGM3OTI1ZWUxMQ==','222ed735-b1e1-421e-be97-a60c7925ee11','91',NULL,NULL,NULL,'2022-12-29 13:30:21',NULL),(29,'kkkkk','ZGF0YmVvNzgwNTUyNjAtNTk4OC00Y2IxLWI3MWEtNzA3ZDI3MTJmOGYy','78055260-5988-4cb1-b71a-707d2712f8f2','676767',NULL,NULL,NULL,'2022-12-30 19:02:42',NULL),(30,'ohno','ZGF0YmVvMmE3NzdiYzYtNTA3Zi00YTk2LWE3YWItYjQ3OGUzMjI3N2Q1','2a777bc6-507f-4a96-a7ab-b478e32277d5','9090',NULL,NULL,NULL,'2022-12-30 20:25:43',NULL),(31,'hoa','cnVhNTA3ZDgzMzgtOWQwYi00YmUwLTgyODUtNGExOTMwMWZmYTAy','507d8338-9d0b-4be0-8285-4a19301ffa02','98',NULL,NULL,NULL,'2022-12-31 09:00:20',NULL),(32,'zfeg','ZXdld3dlMTdlOGMzMy1hYTIzLTRlMzctYjM4NC05YjUyM2E2ODRkZjY=','e17e8c33-aa23-4e37-b384-9b523a684df6','ssa',NULL,NULL,NULL,'2023-01-04 14:13:28',NULL),(33,'dat','Y29mOTdhZWU4Ny04NjUxLTRlM2YtYTg4YS1hMTg2MzU1M2E4OTE=','f97aee87-8651-4e3f-a88a-a1863553a891','99',NULL,NULL,NULL,'2023-01-04 15:36:04',NULL),(34,'hao','bWUwYjUzYzBkMS0zMDIwLTQ3MzItYmJmNy0zNTZkZTY2MjY5ODg=','0b53c0d1-3020-4732-bbf7-356de6626988','70',NULL,NULL,NULL,'2023-01-05 18:53:23',NULL),(35,'dat','YmVvYjA4OTFhYTQtZWZkZi00ODBmLWE3ZTgtNjEwYTM4NGM4Yzlj','b0891aa4-efdf-480f-a7e8-610a384c8c9c','99',NULL,NULL,NULL,'2023-02-12 08:26:17',NULL),(36,'hieu','aGlldWQ3MGUwMzA4LTFkOTgtNDE4MC1hZTAwLWFhYmM2NTg0ZjY5OQ==','d70e0308-1d98-4180-ae00-aabc6584f699','90',NULL,NULL,NULL,'2023-03-02 00:05:07',NULL);
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-03-03 13:53:04
