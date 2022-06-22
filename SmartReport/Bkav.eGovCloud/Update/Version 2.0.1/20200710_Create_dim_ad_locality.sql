/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb_copy

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-10-07 11:46:52
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `dim_ad_locality`
-- ----------------------------
DROP TABLE IF EXISTS `dim_ad_locality`;
CREATE TABLE `dim_ad_locality` (
  `LocalityId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `LocalityName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `ParentId` char(36) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` int(11) DEFAULT NULL,
  `Description` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsActive` bit(1) DEFAULT NULL,
  `Id` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`LocalityId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of dim_ad_locality
-- ----------------------------
INSERT INTO `dim_ad_locality` VALUES ('140e5f82-8d36-4226-8835-d40afa00baf3', 'mnui', null, '3', 'bui', '', 'mnui');
INSERT INTO `dim_ad_locality` VALUES ('28ef1132-4785-46ab-8325-0cabea34ddd2', 'Văn phòng UBND Thành phố', 'd68818b9-760a-43b1-aa90-6a04a8af5878', '2', 'Văn phòng UBND Thành Phố', '', 'Thành phố Hồ Chí Minh');
INSERT INTO `dim_ad_locality` VALUES ('6bb6e416-8534-4c95-b919-1854e8300f91', 'Sở Ngoại vụ', 'd68818b9-760a-43b1-aa90-6a04a8af5878', '1', 'Sở Ngoại vụ', '', 'Thành phố Hồ Chí Minh');
INSERT INTO `dim_ad_locality` VALUES ('caba9839-7c60-4995-8497-3896631812a8', 'Sở Kế hoạch và đầu tư', 'd68818b9-760a-43b1-aa90-6a04a8af5878', '1', 'Sở Kế hoạch và Đầu tư', '', 'Thành phố Hồ Chí Minh');
INSERT INTO `dim_ad_locality` VALUES ('d68818b9-760a-43b1-aa90-6a04a8af5878', 'Hồ Chí Minh', null, '3', 'Hồ Chí Minh', '', 'Thành phố Hồ Chí Minh');
