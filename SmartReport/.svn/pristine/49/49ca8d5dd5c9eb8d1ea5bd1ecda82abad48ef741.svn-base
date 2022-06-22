/*
Navicat MySQL Data Transfer

Source Server         : .
Source Server Version : 50622
Source Host           : localhost:3306
Source Database       : egovqn_sotttt

Target Server Type    : MYSQL
Target Server Version : 50622
File Encoding         : 65001

Date: 2016-12-03 13:48:46
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `calendar`
-- ----------------------------
DROP TABLE IF EXISTS `calendar`;
CREATE TABLE `calendar` (
  `CalendarId` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(300) COLLATE utf8_unicode_ci NOT NULL,
  `DepartmentPrimary` varchar(300) COLLATE utf8_unicode_ci DEFAULT NULL,
  `BeginTime` datetime NOT NULL,
  `EndTime` datetime DEFAULT NULL,
  `Location` varchar(300) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserJoin` varchar(300) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserCreatedId` int(11) NOT NULL,
  `IsAccepted` int(1) NOT NULL,
  `Note` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsSms` bit(1) NOT NULL,
  `Cause` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  PRIMARY KEY (`CalendarId`)
) ENGINE=InnoDB AUTO_INCREMENT=335 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;