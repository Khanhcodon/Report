/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : egovls_tnmt

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2017-05-30 11:24:45
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `notifications`
-- ----------------------------
DROP TABLE IF EXISTS `notifications`;
CREATE TABLE `notifications` (
  `NotificationId` int(11) NOT NULL AUTO_INCREMENT,
  `GroupId` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  `Title` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `Body` varchar(1500) COLLATE utf8_unicode_ci NOT NULL,
  `Avatar` varchar(300) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DateCreated` datetime NOT NULL,
  `AppName` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `JsonData` text COLLATE utf8_unicode_ci NOT NULL,
  `IsSystemNotify` bit(1) NOT NULL,
  `IsSent` bit(1) NOT NULL,
  `IsNew` bit(1) NOT NULL,
  `IsReaded` bit(1) NOT NULL,
  `IsDeleted` bit(1) NOT NULL,
  PRIMARY KEY (`NotificationId`)
) ENGINE=InnoDB AUTO_INCREMENT=19090 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
