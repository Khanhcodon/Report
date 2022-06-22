/*
Navicat MySQL Data Transfer

Source Server         : eGov
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-09-30 15:15:05
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for `printer`
-- ----------------------------
DROP TABLE IF EXISTS `printer`;
CREATE TABLE `printer` (
  `PrinterId` int(11) NOT NULL AUTO_INCREMENT,
  `PrinterName` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `ShareName` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `IsShared` bit(1) DEFAULT NULL,
  `IsActivated` bit(1) DEFAULT NULL,
  `IsFromComputer` bit(1) DEFAULT b'1',
  `UserIds` varchar(255) DEFAULT NULL,
  `DepartmentPositions` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`PrinterId`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of printer
-- ----------------------------
INSERT INTO `printer` VALUES ('8', 'Microsoft Print to PDF', '123', '\0', '\0', '', '[1365]', '[{\"DepartmentId\":158,\"PositionId\":\"0\"}]');
