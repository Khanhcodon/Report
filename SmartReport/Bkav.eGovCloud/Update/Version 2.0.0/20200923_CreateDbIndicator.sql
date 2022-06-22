/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb_copy

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-09-23 17:23:40
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `indicator`
-- ----------------------------
DROP TABLE IF EXISTS `indicator`;
CREATE TABLE `indicator` (
  `IndicatorId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `IndicatorName` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `IsActivated` bit(1) NOT NULL,
  `IndicatorDesctiption` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`IndicatorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of indicator
-- ----------------------------
