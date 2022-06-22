/*
Navicat MySQL Data Transfer

Source Server         : Eform
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-08-21 10:24:54
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `sms`
-- ----------------------------
DROP TABLE IF EXISTS `sms`;
CREATE TABLE `sms` (
  `SmsId` int(11) NOT NULL AUTO_INCREMENT,
  `PhoneNumber` char(15) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Số điện thoại nhận sms',
  `Message` text COLLATE utf8_unicode_ci NOT NULL COMMENT 'Nội dung sms',
  `IsSent` bit(1) DEFAULT b'0' COMMENT 'trạng thái đã gửi sms hay chưa',
  `DateCreated` datetime DEFAULT NULL COMMENT 'Ngày tạo sms',
  `DateSend` datetime DEFAULT NULL COMMENT 'Ngày gủi sms',
  `UserName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserSendId` int(11) DEFAULT NULL,
  `DocumentId` char(36) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DocumentCopyId` int(11) DEFAULT NULL,
  `NotifyConfigType` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HasSendFail` bit(1) NOT NULL,
  `CountSendFail` int(11) NOT NULL,
  `LinkApi` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TokenApi` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`SmsId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of sms
-- ----------------------------
