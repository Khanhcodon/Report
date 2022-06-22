/*
Navicat MySQL Data Transfer

Source Server         : eform
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-10-19 14:13:51
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `locality`
-- ----------------------------
DROP TABLE IF EXISTS `locality`;
CREATE TABLE `locality` (
  `LocalityId` char(36) NOT NULL,
  `LocalityName` varchar(255) DEFAULT NULL,
  `Type` int(11) DEFAULT NULL,
  `LocalityParent` varchar(255) DEFAULT NULL,
  `Description` varchar(1000) DEFAULT NULL,
  `Active` bit(1) DEFAULT NULL,
  `ParentId` char(36) DEFAULT NULL,
  PRIMARY KEY (`LocalityId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of locality
-- ----------------------------
INSERT INTO `locality` VALUES ('01a95f6a-8fa0-4ded-8b02-6becbe53caec', 'Thành phố Yên Bái', '1', 'Tỉnh Yên Bái', 'Tỉnh Yên Bái/Thành phố Yên Bái', '', '01e42df4-28de-4a4f-ae72-60e3d7ff710d');
INSERT INTO `locality` VALUES ('01e42df4-28de-4a4f-ae72-60e3d7ff710d', 'Tỉnh Yên Bái', '1', null, null, '', null);
INSERT INTO `locality` VALUES ('140e5f82-8d36-4226-8835-d40afa00baf3', 'Phường Đồng Tâm', '3', 'Thành phố Yên Bái', 'Tỉnh Yên Bái/Thành phố Yên Bái/Phường Đồng Tâm', '', '01a95f6a-8fa0-4ded-8b02-6becbe53caec');
INSERT INTO `locality` VALUES ('28ef1132-4785-46ab-8325-0cabea34ddd2', 'Huyện Văn Yên', '2', 'Tỉnh Yên Bái', 'Tỉnh Yên Bái/Huyện Văn Yên', '', '01e42df4-28de-4a4f-ae72-60e3d7ff710d');
INSERT INTO `locality` VALUES ('6bb6e416-8534-4c95-b919-1854e8300f91', 'Thị Trấn Mậu A', '3', 'Huyện Văn Yên', 'Tỉnh Yên Bái/Huyện Văn Yên/Thị Trấn Mậu A', '', 'd68818b9-760a-43b1-aa90-6a04a8af5878');
INSERT INTO `locality` VALUES ('caba9839-7c60-4995-8497-3896631812a8', 'Xã An Bình', '3', 'Huyện Văn Yên', 'Tỉnh Yên Bái/Huyện Văn Yên/Xã An Bình', '', 'd68818b9-760a-43b1-aa90-6a04a8af5878');
INSERT INTO `locality` VALUES ('d68818b9-760a-43b1-aa90-6a04a8af5878', 'Huyện Trấn Yên', '2', 'Tỉnh Yên Bái', 'Tỉnh Yên Bái/Huyện Trấn Yên', '', '01e42df4-28de-4a4f-ae72-60e3d7ff710d');
