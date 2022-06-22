/*
Navicat MySQL Data Transfer

Source Server         : Eform
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-08-21 10:24:47
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `mail`
-- ----------------------------
DROP TABLE IF EXISTS `mail`;
CREATE TABLE `mail` (
  `MailId` int(11) NOT NULL AUTO_INCREMENT,
  `Subject` varchar(500) COLLATE utf8_unicode_ci NOT NULL,
  `Body` longtext COLLATE utf8_unicode_ci NOT NULL,
  `SendTo` varchar(500) COLLATE utf8_unicode_ci NOT NULL,
  `Signature` longtext COLLATE utf8_unicode_ci,
  `Header` longtext COLLATE utf8_unicode_ci,
  `Sender` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SenderDisplayName` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsBodyHtml` bit(1) DEFAULT b'1',
  `CarbonCopysStr` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AttachmentIdStr` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsSent` bit(1) DEFAULT b'0',
  `DateCreated` datetime DEFAULT NULL,
  `DateSend` datetime DEFAULT NULL,
  `UserSendId` int(11) DEFAULT NULL,
  `UserName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HasSentFail` bit(1) NOT NULL,
  `CountSentFail` int(11) DEFAULT NULL,
  `LinkApi` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `TokenApi` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`MailId`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of mail
-- ----------------------------
