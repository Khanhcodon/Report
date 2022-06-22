/*
Navicat MySQL Data Transfer

Source Server         : eform
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-10-19 14:14:02
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `localitydepartmentvalue`
-- ----------------------------
DROP TABLE IF EXISTS `localitydepartmentvalue`;
CREATE TABLE `localitydepartmentvalue` (
  `LocalityDepartmentId` char(36) NOT NULL,
  `LocalityId` char(36) DEFAULT NULL,
  `DepartmentId` int(255) DEFAULT NULL,
  PRIMARY KEY (`LocalityDepartmentId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of localitydepartmentvalue
-- ----------------------------
INSERT INTO `localitydepartmentvalue` VALUES ('0e44249b-c327-47db-90d9-1cfc26283e0b', '6bb6e416-8534-4c95-b919-1854e8300f91', '3987');
INSERT INTO `localitydepartmentvalue` VALUES ('54bf549c-0d82-487e-89cc-46f1bf82866d', 'caba9839-7c60-4995-8497-3896631812a8', '3987');
INSERT INTO `localitydepartmentvalue` VALUES ('69e0520f-0ff3-4b23-93bd-2fd0c5d93c5f', '01a95f6a-8fa0-4ded-8b02-6becbe53caec', '3985');
INSERT INTO `localitydepartmentvalue` VALUES ('80990be2-6b0b-45ab-9c00-72d665116f2e', '6bb6e416-8534-4c95-b919-1854e8300f91', '4223');
INSERT INTO `localitydepartmentvalue` VALUES ('844d7a8d-e30a-48d8-8cd3-7a517d17140d', 'd68818b9-760a-43b1-aa90-6a04a8af5878', '4223');
INSERT INTO `localitydepartmentvalue` VALUES ('b1bf45bd-bd81-4f63-b82b-25e8cc5078ad', 'd68818b9-760a-43b1-aa90-6a04a8af5878', '3987');
INSERT INTO `localitydepartmentvalue` VALUES ('c582b61b-950c-483e-a6b7-ddd7691361b7', '28ef1132-4785-46ab-8325-0cabea34ddd2', '3987');
INSERT INTO `localitydepartmentvalue` VALUES ('d92866a1-2f41-4ac6-bc8f-23aa91153fa1', '140e5f82-8d36-4226-8835-d40afa00baf3', '3985');
