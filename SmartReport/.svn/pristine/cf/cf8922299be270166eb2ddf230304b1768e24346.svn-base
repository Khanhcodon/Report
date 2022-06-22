/*
Navicat MySQL Data Transfer

Source Server         : egovcloud
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-07-24 16:38:08
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `backup_restore_history`
-- ----------------------------
DROP TABLE IF EXISTS `backup_restore_history`;
CREATE TABLE `backup_restore_history` (
  `BackupRestoreHistoryId` int(11) NOT NULL AUTO_INCREMENT,
  `Server` varchar(255) NOT NULL,
  `DatabaseName` varchar(255) NOT NULL,
  `Content` text,
  `Date` datetime NOT NULL,
  `IsBackup` bit(1) NOT NULL,
  `FileBackupUrl` varchar(1000) DEFAULT NULL,
  `DatabaseType` tinyint(4) NOT NULL,
  `FileName` varchar(255) DEFAULT NULL,
  `ShareFolderId` int(11) DEFAULT NULL,
  PRIMARY KEY (`BackupRestoreHistoryId`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of backup_restore_history
-- ----------------------------
