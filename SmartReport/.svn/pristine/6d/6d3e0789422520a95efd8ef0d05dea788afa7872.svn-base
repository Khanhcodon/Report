/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovbrvtadmin

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-12-04 15:44:31
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `account`
-- ----------------------------
DROP TABLE IF EXISTS `account`;
CREATE TABLE `account` (
  `AccountId` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `UsernameEmailDomain` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `DomainName` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `PasswordSalt` binary(16) NOT NULL,
  `PasswordHash` binary(64) NOT NULL,
  `PasswordLastModifiedOnDate` datetime DEFAULT NULL,
  `OpenId` varchar(1024) COLLATE utf8_unicode_ci DEFAULT NULL,
  `FullName` varchar(128) COLLATE utf8_unicode_ci NOT NULL,
  `Gender` bit(1) NOT NULL,
  `Phone` varchar(32) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Fax` varchar(32) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Address` mediumtext COLLATE utf8_unicode_ci,
  `IsActivated` bit(1) NOT NULL,
  `IsLockedOut` tinyint(1) NOT NULL,
  `LastLockoutDate` datetime DEFAULT NULL,
  `LastLoginDate` datetime DEFAULT NULL,
  `FailedPasswordAttemptCount` int(11) DEFAULT NULL,
  `FailedPasswordAttemptStart` datetime DEFAULT NULL,
  `CreatedByUserId` int(11) DEFAULT NULL,
  `CreatedOnDate` datetime DEFAULT NULL,
  `LastModifiedByUserId` int(11) DEFAULT NULL,
  `LastModifiedOnDate` datetime DEFAULT NULL,
  `Version` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `HasViewReport` bit(1) DEFAULT NULL,
  PRIMARY KEY (`AccountId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of account
-- ----------------------------
-- INSERT INTO `account` VALUES ('1', 'egovadmin', 'egovadmin@bkav.com', 'bkav.com', 0x7F241C01E5CEA497FEAB5E0BCF3730A9, 0xEF147E4FFB7DB07FB11658AABB6B10975B2A2799CCF67ABC2A67E7987A324FBAE473D90F0BBCC16214928802E37E7F6C976CB954704DA9F5E4B9EDF056953F5B, '2015-05-29 11:52:27', null, 'Administrator', '', null, null, null, '', '0', null, '2015-12-07 09:00:02', null, null, null, '2015-05-29 11:52:27', null, null, '2015-12-07 09:01:14');

-- ----------------------------
-- Table structure for `accountpasswordhistory`
-- ----------------------------
DROP TABLE IF EXISTS `accountpasswordhistory`;
CREATE TABLE `accountpasswordhistory` (
  `AccountPasswordHistoryId` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` int(11) NOT NULL,
  `Username` longtext,
  `PasswordSalt` binary(16) NOT NULL,
  `PasswordHash` binary(64) NOT NULL,
  `CreatedOnDate` datetime NOT NULL,
  PRIMARY KEY (`AccountPasswordHistoryId`),
  KEY `AccountId` (`AccountId`) USING BTREE,
  CONSTRAINT `accountpasswordhistory_ibfk_1` FOREIGN KEY (`AccountId`) REFERENCES `account` (`AccountId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of accountpasswordhistory
-- ----------------------------

-- ----------------------------
-- Table structure for `account_domain`
-- ----------------------------
DROP TABLE IF EXISTS `account_domain`;
CREATE TABLE `account_domain` (
  `AccountDomainId` int(11) NOT NULL AUTO_INCREMENT,
  `AccountId` int(11) NOT NULL,
  `DomainId` int(11) NOT NULL,
  PRIMARY KEY (`AccountDomainId`),
  KEY `AccountId` (`AccountId`) USING BTREE,
  KEY `DomainId` (`DomainId`) USING BTREE,
  CONSTRAINT `account_domain_ibfk_1` FOREIGN KEY (`AccountId`) REFERENCES `account` (`AccountId`),
  CONSTRAINT `account_domain_ibfk_2` FOREIGN KEY (`DomainId`) REFERENCES `domain` (`DomainId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of account_domain
-- ----------------------------

-- ----------------------------
-- Table structure for `connection`
-- ----------------------------
DROP TABLE IF EXISTS `connection`;
CREATE TABLE `connection` (
  `ConnectionId` int(11) NOT NULL AUTO_INCREMENT,
  `DatabaseTypeId` tinyint(3) unsigned NOT NULL,
  `ConnectionName` varchar(300) COLLATE utf8_unicode_ci NOT NULL,
  `ServerName` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `IseGovOnlineDb` bit(1) NULL,
  `Username` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `Database` varchar(64) COLLATE utf8_unicode_ci NOT NULL,
  `Port` smallint(6) DEFAULT NULL,
  `ConnectionRaw` mediumtext COLLATE utf8_unicode_ci,
  `IsHsmcDb` bit(1) NULL,
  PRIMARY KEY (`ConnectionId`),
  KEY `DatabaseTypeId` (`DatabaseTypeId`) USING BTREE,
  CONSTRAINT `connection_ibfk_1` FOREIGN KEY (`DatabaseTypeId`) REFERENCES `databasetype` (`DatabaseTypeId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of connection
-- ----------------------------

-- ----------------------------
-- Table structure for `customer`
-- ----------------------------
DROP TABLE IF EXISTS `customer`;
CREATE TABLE `customer` (
  `CustomerId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(300) NOT NULL,
  `Email` varchar(150) NOT NULL,
  `Phone` varchar(15) NOT NULL,
  `Address` varchar(500) NOT NULL,
  `CustomerType` int(11) NOT NULL,
  `Province` varchar(128) NOT NULL,
  `District` varchar(128) NOT NULL,
  `Commune` varchar(128) NOT NULL,
  `IsActivated` bit(1) NOT NULL,
  PRIMARY KEY (`CustomerId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of customer
-- ----------------------------

-- ----------------------------
-- Table structure for `databasetype`
-- ----------------------------
DROP TABLE IF EXISTS `databasetype`;
CREATE TABLE `databasetype` (
  `DatabaseTypeId` tinyint(3) unsigned NOT NULL,
  `DatabaseTypeName` varchar(128) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`DatabaseTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of databasetype
-- ----------------------------
INSERT INTO `databasetype` VALUES ('1', 'SqlServer');
INSERT INTO `databasetype` VALUES ('2', 'MySql');
INSERT INTO `databasetype` VALUES ('3', 'Oracle');

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
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of domain
-- ----------------------------

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
  KEY `DomainId` (`DomainId`) USING BTREE,
  CONSTRAINT `domainalias_ibfk_1` FOREIGN KEY (`DomainId`) REFERENCES `domain` (`DomainId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of domainalias
-- ----------------------------

-- ----------------------------
-- Table structure for `log`
-- ----------------------------
DROP TABLE IF EXISTS `log`;
CREATE TABLE `log` (
  `LogId` int(11) NOT NULL AUTO_INCREMENT,
  `LogType` int(11) NOT NULL,
  `ShortMessage` mediumtext COLLATE utf8_unicode_ci NOT NULL,
  `FullMessage` mediumtext COLLATE utf8_unicode_ci,
  `RequestJson` mediumtext COLLATE utf8_unicode_ci,
  `IpAddress` varchar(15) COLLATE utf8_unicode_ci DEFAULT NULL,
  `CreatedOnDate` datetime NOT NULL,
  PRIMARY KEY (`LogId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of log
-- ----------------------------

-- ----------------------------
-- Table structure for `resource`
-- ----------------------------
DROP TABLE IF EXISTS `resource`;
CREATE TABLE `resource` (
  `ResourceId` int(11) NOT NULL AUTO_INCREMENT,
  `ResourceKey` text COLLATE utf8_unicode_ci NOT NULL,
  `ResourceValue` longtext COLLATE utf8_unicode_ci,
  PRIMARY KEY (`ResourceId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of resource
-- ----------------------------

-- ----------------------------
-- Table structure for `server`
-- ----------------------------
DROP TABLE IF EXISTS `server`;
CREATE TABLE `server` (
  `ServerId` int(11) NOT NULL AUTO_INCREMENT,
  `PublicDomain` varchar(255) NOT NULL,
  `PrivateIp` varchar(15) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ServerId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of server
-- ----------------------------

-- ----------------------------
-- Table structure for `setting`
-- ----------------------------
DROP TABLE IF EXISTS `setting`;
CREATE TABLE `setting` (
  `SettingId` int(11) NOT NULL AUTO_INCREMENT,
  `SettingKey` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `SettingValue` mediumtext COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`SettingId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of setting
-- ----------------------------

-- ----------------------------
-- Table structure for `site`
-- ----------------------------
DROP TABLE IF EXISTS `site`;
CREATE TABLE `site` (
  `SiteId` int(11) NOT NULL AUTO_INCREMENT,
  `SiteName` varchar(150) NOT NULL,
  `ServerId` int(11) NOT NULL,
  PRIMARY KEY (`SiteId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of site
-- ----------------------------

-- ----------------------------
-- Table structure for `timerjob`
-- ----------------------------
DROP TABLE IF EXISTS `timerjob`;
CREATE TABLE `timerjob` (
  `TimerJobId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(500) COLLATE utf8_unicode_ci NOT NULL,
  `TimerJobType` int(11) NOT NULL,
  `TimerJobConfig` mediumtext COLLATE utf8_unicode_ci,
  `DomainId` int(11) NOT NULL,
  `DateLastJobRun` datetime DEFAULT NULL,
  `JobInterval` datetime NOT NULL,
  `DateNextJobStartAfter` datetime NOT NULL,
  `DateNextJobStartBefore` datetime NOT NULL,
  `ScheduleType` int(11) NOT NULL,
  `ScheduleConfig` varchar(1000) COLLATE utf8_unicode_ci NOT NULL,
  `IsActive` bit(1) NOT NULL,
  `IsRunning` bit(1) NOT NULL,
  PRIMARY KEY (`TimerJobId`),
  KEY `DomainId` (`DomainId`) USING BTREE,
  CONSTRAINT `timerjob_ibfk_1` FOREIGN KEY (`DomainId`) REFERENCES `domain` (`DomainId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of timerjob
-- ----------------------------

-- ----------------------------
-- Table structure for `timertemplate`
-- ----------------------------
DROP TABLE IF EXISTS `timertemplate`;
CREATE TABLE `timertemplate` (
  `TimerTemplateId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(250) COLLATE utf8_unicode_ci NOT NULL,
  `Query` mediumtext COLLATE utf8_unicode_ci NOT NULL,
  `Template` mediumtext COLLATE utf8_unicode_ci,
  `DateCreated` datetime NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`TimerTemplateId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of timertemplate
-- ----------------------------
