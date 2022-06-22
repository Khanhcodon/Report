/*
Navicat MySQL Data Transfer

Source Server         : bkav
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : eform_yenbai

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2020-10-22 09:24:30
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `datafieldtemplate`
-- ----------------------------
DROP TABLE IF EXISTS `datafieldtemplate`;
CREATE TABLE `datafieldtemplate` (
  `DataFieldTemplateId` int(11) NOT NULL AUTO_INCREMENT,
  `TemplateId` int(11) DEFAULT NULL,
  `FieldName` varchar(255) DEFAULT NULL,
  `Datatype` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`DataFieldTemplateId`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of datafieldtemplate
-- ----------------------------
INSERT INTO `datafieldtemplate` VALUES ('15', '9', 'DepartmentId', 'Int');
INSERT INTO `datafieldtemplate` VALUES ('16', '9', 'DepartmentCode', 'String');
INSERT INTO `datafieldtemplate` VALUES ('17', '9', 'ParentDepartmentId', 'Int');
INSERT INTO `datafieldtemplate` VALUES ('18', '9', 'ParentDepartmentCode', 'String');
INSERT INTO `datafieldtemplate` VALUES ('19', '9', 'DepartmentName', 'String');
