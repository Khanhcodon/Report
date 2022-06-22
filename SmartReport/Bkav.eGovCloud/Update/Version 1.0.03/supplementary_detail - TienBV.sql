/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud.vn

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-10-09 15:25:02
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `supplementary_detail`
-- ----------------------------
DROP TABLE IF EXISTS `supplementary_detail`;
CREATE TABLE `supplementary_detail` (
  `SupplementaryDetailId` int(11) NOT NULL,
  `SupplementaryId` int(11) NOT NULL,
  `UserSendId` int(11) NOT NULL,
  `UserSendName` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  `Comment` varchar(1000) COLLATE utf8_unicode_ci NOT NULL,
  `DateSend` datetime NOT NULL,
  `IsDeleted` bit(1) DEFAULT NULL,
  `UserDeletedId` int(11) DEFAULT NULL,
  `DateDeleted` datetime DEFAULT NULL,
  PRIMARY KEY (`SupplementaryDetailId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of supplementary_detail
-- ----------------------------

ALTER TABLE `supplementary_detail`
MODIFY COLUMN `SupplementaryDetailId`  int(11) NOT NULL AUTO_INCREMENT FIRST ;
