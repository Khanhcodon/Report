/*
Navicat MySQL Data Transfer

Source Server         : bkav
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : eform_yenbai

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2020-10-22 09:24:49
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `sql_template`
-- ----------------------------
DROP TABLE IF EXISTS `sql_template`;
CREATE TABLE `sql_template` (
  `TemplateId` int(11) NOT NULL AUTO_INCREMENT,
  `DataSourceId` int(11) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `QueryString` varchar(255) NOT NULL,
  `DateModified` datetime NOT NULL,
  `UserCreatedId` int(11) NOT NULL,
  PRIMARY KEY (`TemplateId`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of sql_template
-- ----------------------------
INSERT INTO `sql_template` VALUES ('9', '4', '12 123123', 'select * from bmm_department', '2020-10-22 09:04:41', '5005');
