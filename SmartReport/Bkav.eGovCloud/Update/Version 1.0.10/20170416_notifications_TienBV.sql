/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : bkavegov

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2017-04-18 15:54:11
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `notifications`
-- ----------------------------
DROP TABLE IF EXISTS `notifications`;
CREATE TABLE `notifications` (
  `NotificationId` int(11) NOT NULL AUTO_INCREMENT,
  `UserSendName` varchar(50) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserId` int(11) DEFAULT NULL,
  `Title` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `Body` varchar(1500) COLLATE utf8_unicode_ci NOT NULL,
  `Avatar` varchar(300) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DateCreated` datetime NOT NULL,
  `AppName` varchar(150) COLLATE utf8_unicode_ci DEFAULT NULL,
  `JsonData` text COLLATE utf8_unicode_ci NOT NULL,
  `IsSystemNotify` bit(1) NOT NULL,
  `IsSent` bit(1) NOT NULL,
  `IsViewed` bit(1) NOT NULL,
  `IsReaded` bit(1) NOT NULL,
  PRIMARY KEY (`NotificationId`)
) ENGINE=InnoDB AUTO_INCREMENT=1320 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
