/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-11-13 15:58:55
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `doc_publish`
-- ----------------------------
DROP TABLE IF EXISTS `doc_publish`;
CREATE TABLE `doc_publish` (
  `DocumentPublishId` int(11) NOT NULL AUTO_INCREMENT,
  `DocumentId` char(36) NOT NULL,
  `DocumentCopyId` int(11) NOT NULL,
  `DoctypeId` char(36) NOT NULL,
  `DocCode` varchar(100) NOT NULL,
  `DatePublished` datetime NOT NULL,
  `AddressName` varchar(400) NOT NULL,
  `IsHsmc` bit(1) NOT NULL,
  `UserPublishId` int(11) NOT NULL,
  `UserPublishName` varchar(100) NOT NULL,
  `HasLienThong` bit(1) NOT NULL,
  `AddressId` int(11) DEFAULT NULL,
  `DateSent` datetime DEFAULT NULL,
  `IsPending` bit(1) NOT NULL,
  `HasRequireResponse` bit(1) NOT NULL,
  `DateAppointed` datetime DEFAULT NULL,
  `IsResponsed` bit(1) NOT NULL,
  `DateResponsed` datetime DEFAULT NULL,
  `DocCodeResponsed` varchar(100) DEFAULT NULL,
  `DocumentCopyResponsed` int(11) DEFAULT NULL,
  PRIMARY KEY (`DocumentPublishId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of doc_publish
-- ----------------------------
