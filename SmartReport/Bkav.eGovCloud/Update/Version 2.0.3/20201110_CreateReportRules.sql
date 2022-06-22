/*
Navicat MySQL Data Transfer

Source Server         : eform
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : eform

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2020-11-10 14:08:28
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `reportrules`
-- ----------------------------
DROP TABLE IF EXISTS `reportrules`;
CREATE TABLE `reportrules` (
  `ReportRuleId` int(11) NOT NULL AUTO_INCREMENT,
  `Code` varchar(255) DEFAULT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `ReportMode` varchar(255) DEFAULT NULL,
  `IsActive` bit(1) DEFAULT b'1',
  `DateCreate` datetime DEFAULT NULL,
  PRIMARY KEY (`ReportRuleId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of reportrules
-- ----------------------------
INSERT INTO `reportrules` VALUES ('1', 'VBQĐ', 'Văn bản quy định1', '[\"3\",\"4\"]', '', '2020-11-05 08:44:44');
