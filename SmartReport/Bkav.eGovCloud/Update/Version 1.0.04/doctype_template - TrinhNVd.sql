/*
Navicat MySQL Data Transfer

Source Server         : MySQL Test
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-11-10 09:57:52
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for doctype_template
-- ----------------------------
DROP TABLE IF EXISTS `doctype_template`;
CREATE TABLE `doctype_template` (
  `DoctypeTemplateId` int(11) NOT NULL AUTO_INCREMENT,
  `DoctypeId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Name` varchar(255) NOT NULL,
  `OnlineTemplateId` int(11) DEFAULT NULL,
  PRIMARY KEY (`DoctypeTemplateId`),
  KEY `OnlineTemplateId` (`OnlineTemplateId`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;
