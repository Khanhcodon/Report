/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb_copy

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-10-07 11:47:35
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `dim_incatalogvalue`
-- ----------------------------
DROP TABLE IF EXISTS `incatalogvalue`;
CREATE TABLE `incatalogvalue` (
  `InCatalogValueId` char(36) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Id giá trị của 1 danh mục trên form',
  `InCatalogId` char(36) COLLATE utf8_unicode_ci DEFAULT NULL,
  `InCatalogValueName` varchar(255) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Giá trị',
  `Order` int(11) DEFAULT NULL,
  `InCatalogValueCode` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `ParentId` char(36) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Level` int(11) DEFAULT NULL,
  `Unit` char(36) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Type` char(36) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Description` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `PeriodTypeIds` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DisTypeIds` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Active` bit(1) DEFAULT NULL,
  `RegressionIds` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Threshold_min` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Threshold_max` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AllowAggregation` bit(1) DEFAULT NULL,
  `AggregationFormula` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `NumberPeriodReplace` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `AllowAggregationByPeriod` bit(1) DEFAULT NULL,
  `InCatalogIds` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`InCatalogValueId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=DYNAMIC COMMENT='Bảng thông tin giá trị của các danh mục chỉ tiêu';

-- ----------------------------
-- Records of dim_incatalogvalue
-- ----------------------------
INSERT INTO `incatalogvalue` VALUES ('0d162cd1-53e1-47d1-98f0-4194e705db8e', null, 'Diện tích và cơ cấu đất', '0', 'T0101', '85f093d2-ba89-4243-a567-5b1db2ee16ae', '0', '09901d43-9d7f-4ffc-8e87-90b6e2fbe041', '06345cf5-7b99-442f-a3e4-581be5459ffb', 'Diện tích và cơ cấu đất', null, null, '', null, 'Giới hạn nhỏ một', 'Giới hạn lớn một', '', 'MIN', '5', '', '[\"1aa9365c-dea5-4f8c-8c5d-467708cd501d\",\"4ce276c9-44cc-4e19-9504-42ea41cd3a20\"]');
INSERT INTO `incatalogvalue` VALUES ('4a25e81c-7f43-4c04-9edb-0f4df6d614a4', null, 'Xã hội, môi trường', '0', 'H03', null, '0', '09901d43-9d7f-4ffc-8e87-90b6e2fbe041', '45a396d9-eb0a-45db-951e-49af040f4674', 'Xã hội, môi trường', null, null, '', null, 'Giới hạn nhỏ một', 'Giới hạn lớn một', '', 'SUM', '1', '', '[\"00bb9712-bd9e-4012-aed2-4ee202b36f3e\",\"020729a9-1e9b-44a7-952d-e39db8febbf9\"]');
INSERT INTO `incatalogvalue` VALUES ('793e06ff-8c02-414c-8378-88026941f266', null, 'Sổ giáo viên mầm non', '1', 'H0302', '4a25e81c-7f43-4c04-9edb-0f4df6d614a4', '0', '533ef2e5-e339-4c12-ae6e-a621c19dd5b8', '45a396d9-eb0a-45db-951e-49af040f4674', 'Sổ giáo viên mầm non', null, null, '', null, 'Giới hạn nhỏ một', 'Giới hạn lớn một', '', 'MIN', '3', '', '[\"00bb9712-bd9e-4012-aed2-4ee202b36f3e\",\"020729a9-1e9b-44a7-952d-e39db8febbf9\"]');
INSERT INTO `incatalogvalue` VALUES ('85f093d2-ba89-4243-a567-5b1db2ee16ae', null, 'Đất đai, dân số', '0', 'T01', null, '0', '533022f4-8c81-4758-b551-fbd75ccf28f3', '06345cf5-7b99-442f-a3e4-581be5459ffb', 'Đất đai dân số', null, null, '', null, 'Giới hạn nhỏ một', 'Giới hạn lớn một', '', 'SUM', '4', '', '[\"1aa9365c-dea5-4f8c-8c5d-467708cd501d\",\"4ce276c9-44cc-4e19-9504-42ea41cd3a20\"]');
INSERT INTO `incatalogvalue` VALUES ('8f936235-a4ee-4cf5-910d-8ef06e39b029', null, 'Số trường, lớp, phòng học mầm non', '0', 'H0301', '4a25e81c-7f43-4c04-9edb-0f4df6d614a4', '0', '2be1edbb-5fbf-43aa-a562-6685e82c2080', '45a396d9-eb0a-45db-951e-49af040f4674', 'Số trường, lớp, phòng học mầm non', null, null, '', null, 'Giới hạn nhỏ một', 'Giới hạn lớn một', '', 'AVERAGE', '2', '', '[\"00bb9712-bd9e-4012-aed2-4ee202b36f3e\",\"020729a9-1e9b-44a7-952d-e39db8febbf9\",\"0644902a-eb07-4969-9fa2-d0fc438e132b\"]');
