/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_yenbai

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2019-11-22 10:22:01
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for config_type
-- ----------------------------
DROP TABLE IF EXISTS `config_type`;
CREATE TABLE `config_type` (
  `TypeId` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `TypeName` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `IsActivated` bit(1) DEFAULT NULL,
  `TypeCode` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `ParentId` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `DisplayCode` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `PatternCode` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`TypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Records of config_type
-- ----------------------------
