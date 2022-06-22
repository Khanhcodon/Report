/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-11-19 09:21:56
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `connection`
-- ----------------------------
DROP TABLE IF EXISTS `connection`;
CREATE TABLE `connection` (
  `ConnectionId` int(11) NOT NULL AUTO_INCREMENT,
  `DatabaseTypeId` tinyint(3) unsigned NOT NULL,
  `ConnectionName` varchar(300) COLLATE utf8_unicode_ci NOT NULL,
  `Server` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `IseGovOnlineDb` bit(1) NOT NULL,
  `Username` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `Database` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `Port` smallint(6) DEFAULT NULL,
  `ConnectionRaw` mediumtext COLLATE utf8_unicode_ci,
  PRIMARY KEY (`ConnectionId`),
  KEY `DatabaseTypeId` (`DatabaseTypeId`),
  CONSTRAINT `DatabaseType_Connections` FOREIGN KEY (`DatabaseTypeId`) REFERENCES `databasetype` (`DatabaseTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of connection
-- ----------------------------
INSERT INTO `connection` VALUES ('25', '2', 'egovdev.bkav.com', '10.2.78.125', '', 'root', '123456', 'egovcloud_lastest', '3306', 'server=10.2.78.125;User Id=root;password=123456;database=egovcloud_lastest;Convert Zero Datetime=True;Character Set=utf8;Persist Security Info=True;port=3306');

-- ----------------------------
-- Table structure for `domain`
-- ----------------------------
DROP TABLE IF EXISTS `domain`;
CREATE TABLE `domain` (
  `DomainId` int(11) NOT NULL AUTO_INCREMENT,
  `ParentId` int(11) DEFAULT NULL,
  `DomainName` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `CustomerName` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Email` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `Phone` varchar(32) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Address` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CustomerType` bit(1) NOT NULL,
  `Province` varchar(128) COLLATE utf8_unicode_ci DEFAULT NULL,
  `District` varchar(128) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Commune` varchar(128) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Department` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsActivated` bit(1) NOT NULL,
  `CreatedByUserId` int(11) DEFAULT NULL,
  `CreatedOnDate` datetime DEFAULT NULL,
  `LastModifiedByUserId` int(11) DEFAULT NULL,
  `LastModifiedOnDate` datetime DEFAULT NULL,
  `Version` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `IsPrimary` bit(1) NOT NULL,
  `ConnectionId` int(11) NOT NULL,
  PRIMARY KEY (`DomainId`),
  KEY `ParentId` (`ParentId`),
  CONSTRAINT `Domain_DomainParent` FOREIGN KEY (`ParentId`) REFERENCES `domain` (`DomainId`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of domain
-- ----------------------------
INSERT INTO `domain` VALUES ('25', null, 'egovdev.bkav.com', 'egovdev.bkav.com', '', null, null, '', null, null, null, null, '', null, '2015-11-18 15:47:51', null, null, '2015-11-18 16:55:57', '', '25');

-- ----------------------------
-- Table structure for `domainalias`
-- ----------------------------
DROP TABLE IF EXISTS `domainalias`;
CREATE TABLE `domainalias` (
  `DomainAliasId` int(11) NOT NULL AUTO_INCREMENT,
  `DomainId` int(11) NOT NULL,
  `Alias` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `IsPrimary` bit(1) NOT NULL,
  `IsActivated` bit(1) NOT NULL,
  `CreatedByUserId` int(11) DEFAULT NULL,
  `CreatedOnDate` datetime DEFAULT NULL,
  `LastModifiedByUserId` int(11) DEFAULT NULL,
  `LastModifiedOnDate` datetime DEFAULT NULL,
  `Version` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`DomainAliasId`),
  KEY `DomainId` (`DomainId`),
  CONSTRAINT `DomainAlias_Domain` FOREIGN KEY (`DomainId`) REFERENCES `domain` (`DomainId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of domainalias
-- ----------------------------
INSERT INTO `domainalias` VALUES ('1', '25', 'egovdev.bkav.com', '', '', null, '2015-11-18 15:47:51', null, null, '2015-11-18 15:47:51');
