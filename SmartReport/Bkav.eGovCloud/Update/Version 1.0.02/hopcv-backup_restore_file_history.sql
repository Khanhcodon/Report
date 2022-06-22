/*
Navicat MySQL Data Transfer

Source Server         : egovcloud
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-07-24 16:38:24
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `backup_restore_file_history`
-- ----------------------------
DROP TABLE IF EXISTS `backup_restore_file_history`;
CREATE TABLE `backup_restore_file_history` (
  `BackupRestoreFileHistoryId` int(11) NOT NULL AUTO_INCREMENT,
  `PathFolder` varchar(255) NOT NULL,
  `FileBackupUrl` varchar(500) DEFAULT NULL,
  `FileName` varchar(255) DEFAULT NULL,
  `Date` datetime NOT NULL,
  `IsBackup` bit(1) NOT NULL,
  `Content` varchar(1000) DEFAULT NULL,
  `ShareFolderId` int(11) NOT NULL,
  PRIMARY KEY (`BackupRestoreFileHistoryId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of backup_restore_file_history
-- ----------------------------
