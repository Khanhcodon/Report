/*
Navicat MySQL Data Transfer

Source Server         : egovcloud
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-07-24 16:39:18
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `backup_restore_config`
-- ----------------------------
DROP TABLE IF EXISTS `backup_restore_config`;
CREATE TABLE `backup_restore_config` (
  `BackupRestoreConfigId` int(11) NOT NULL AUTO_INCREMENT,
  `Server` varchar(255) NOT NULL,
  `DatabaseName` varchar(255) NOT NULL,
  `UserName` varchar(255) NOT NULL,
  `Password` varchar(255) DEFAULT NULL,
  `Port` int(11) DEFAULT NULL,
  `ShareFolderId` int(11) DEFAULT NULL,
  `Config` varchar(1000) DEFAULT NULL,
  `DatabaseType` tinyint(4) NOT NULL,
  PRIMARY KEY (`BackupRestoreConfigId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of backup_restore_config
-- ----------------------------
