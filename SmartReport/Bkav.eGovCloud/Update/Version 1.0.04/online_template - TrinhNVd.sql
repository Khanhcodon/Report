/*
Navicat MySQL Data Transfer

Source Server         : MySQL Test
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-11-10 09:50:14
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for online_template
-- ----------------------------
DROP TABLE IF EXISTS `online_template`;
CREATE TABLE `online_template` (
  `OnlineTemplateId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(250) NOT NULL,
  `FileId` int(11) DEFAULT NULL,
  `Description` longtext,
  PRIMARY KEY (`OnlineTemplateId`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='Biểu mẫu hành chính (HSMC)';
