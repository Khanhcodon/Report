/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2016-01-04 14:35:24
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for syncdoctype
-- ----------------------------
DROP TABLE IF EXISTS `syncdoctype`;
CREATE TABLE `syncdoctype` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InsideDocTypeId` char(36) DEFAULT NULL,
  `OutsideDocTypeId` char(36) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
