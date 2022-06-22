/*
Navicat MySQL Data Transfer

Source Server         : egovcloud
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-07-24 16:36:42
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `share_folder`
-- ----------------------------
DROP TABLE IF EXISTS `share_folder`;
CREATE TABLE `share_folder` (
  `ShareFolderId` int(11) NOT NULL AUTO_INCREMENT,
  `ShareFolderUrl` varchar(500) NOT NULL,
  `UserName` varchar(255) DEFAULT NULL,
  `Password` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ShareFolderId`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
