-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: localhost    Database: smart_med_db
-- ------------------------------------------------------
-- Server version	8.0.43

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `medicationlogs`
--

DROP TABLE IF EXISTS `medicationlogs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medicationlogs` (
  `log_id` int NOT NULL AUTO_INCREMENT,
  `schedule_id` int NOT NULL,
  `taken_date` date NOT NULL COMMENT '복용 날짜 (2023-10-25)',
  `is_taken` tinyint(1) DEFAULT '0' COMMENT '복용 여부 (True/False)',
  `taken_at` datetime DEFAULT NULL COMMENT '실제 복용 버튼 누른 시간',
  PRIMARY KEY (`log_id`),
  KEY `schedule_id` (`schedule_id`),
  CONSTRAINT `medicationlogs_ibfk_1` FOREIGN KEY (`schedule_id`) REFERENCES `schedules` (`schedule_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medicationlogs`
--

LOCK TABLES `medicationlogs` WRITE;
/*!40000 ALTER TABLE `medicationlogs` DISABLE KEYS */;
/*!40000 ALTER TABLE `medicationlogs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `medications`
--

DROP TABLE IF EXISTS `medications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medications` (
  `med_id` int NOT NULL AUTO_INCREMENT COMMENT '약품 고유 ID',
  `med_name` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '약품명 (예: 타이레놀)',
  `stock_quantity` int DEFAULT '0' COMMENT '현재 재고 수량 (남은 약)',
  `total_days` int DEFAULT '0' COMMENT '총 처방 일수 (예: 5일분)',
  `memo` text COLLATE utf8mb4_unicode_ci COMMENT '비고/메모',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`med_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medications`
--

LOCK TABLES `medications` WRITE;
/*!40000 ALTER TABLE `medications` DISABLE KEYS */;
INSERT INTO `medications` VALUES (1,'타이레놀',15,5,NULL,'2025-11-20 13:48:09'),(2,'아세트아미노펜',28,14,NULL,'2025-11-20 13:48:09'),(3,'세티리진',7,7,NULL,'2025-11-20 13:48:09'),(4,'약품1',100,0,NULL,'2025-11-20 15:27:29');
/*!40000 ALTER TABLE `medications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `schedules`
--

DROP TABLE IF EXISTS `schedules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `schedules` (
  `schedule_id` int NOT NULL AUTO_INCREMENT COMMENT '스케줄 고유 ID',
  `user_id` int NOT NULL COMMENT '사용자 ID (Users 테이블 참조)',
  `med_id` int NOT NULL COMMENT '약품 ID (Medications 테이블 참조)',
  `take_time` time NOT NULL COMMENT '복용 시간 (예: 08:00:00)',
  `daily_dosage` int DEFAULT '3' COMMENT '이 스케줄의 하루 총 복용량',
  PRIMARY KEY (`schedule_id`),
  KEY `user_id` (`user_id`),
  KEY `med_id` (`med_id`),
  CONSTRAINT `schedules_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  CONSTRAINT `schedules_ibfk_2` FOREIGN KEY (`med_id`) REFERENCES `medications` (`med_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `schedules`
--

LOCK TABLES `schedules` WRITE;
/*!40000 ALTER TABLE `schedules` DISABLE KEYS */;
INSERT INTO `schedules` VALUES (1,1,1,'08:00:00',3),(2,1,2,'12:30:00',2),(3,1,3,'19:00:00',1),(4,1,4,'08:00:00',6),(5,1,4,'13:00:00',6),(6,1,4,'19:00:00',6);
/*!40000 ALTER TABLE `schedules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `user_id` int NOT NULL AUTO_INCREMENT COMMENT '사용자 고유 ID',
  `user_name` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT '사용자 이름 (예: 유현호)',
  `created_at` datetime DEFAULT CURRENT_TIMESTAMP COMMENT '등록일자',
  `password` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL DEFAULT '0000',
  PRIMARY KEY (`user_id`,`user_name`,`password`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'유현호','2025-11-20 13:48:09','1234'),(2,'admin','2025-11-20 14:13:25','0728');
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

-- Dump completed on 2025-11-20 15:43:49
