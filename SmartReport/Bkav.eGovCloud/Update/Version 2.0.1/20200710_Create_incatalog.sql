/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb_copy

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-10-07 11:47:29
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `dim_incatalog`
-- ----------------------------
DROP TABLE IF EXISTS `incatalog`;
CREATE TABLE `incatalog` (
  `InCatalogId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `InCatalogName` varchar(255) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Tên danh mục chỉ tiêu',
  `IsActivated` bit(1) DEFAULT NULL,
  `InCatalogKey` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`InCatalogId`) USING BTREE,
  KEY `CatalogId` (`InCatalogId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=DYNAMIC COMMENT='Bảng thông tin các danh mục chỉ tiêu';

-- ----------------------------
-- Records of dim_incatalog
-- ----------------------------
INSERT INTO `incatalog` VALUES ('00bb9712-bd9e-4012-aed2-4ee202b36f3e', 'Mùa vụ', '', null);
INSERT INTO `incatalog` VALUES ('020729a9-1e9b-44a7-952d-e39db8febbf9', 'Huyện Mù Cang Chải', '', null);
INSERT INTO `incatalog` VALUES ('0644902a-eb07-4969-9fa2-d0fc438e132b', 'Huyện Trấn Yên', '', null);
INSERT INTO `incatalog` VALUES ('1aa9365c-dea5-4f8c-8c5d-467708cd501d', 'Huyện Văn Chấn', '', null);
INSERT INTO `incatalog` VALUES ('4ce276c9-44cc-4e19-9504-42ea41cd3a20', 'Thành phố Yên Bái', '', null);
INSERT INTO `incatalog` VALUES ('518a1329-3f82-4304-aaab-4e78ab3a302d', 'Tỉnh Yên Bái', '', null);
INSERT INTO `incatalog` VALUES ('69501091-b51c-4081-b674-4e9a52adab07', 'Huyện Lục Yên', '', null);
INSERT INTO `incatalog` VALUES ('6d559e85-dd10-45be-99f6-5d1037bf453b', 'Huyện Văn Yên ', '', null);
INSERT INTO `incatalog` VALUES ('8aaa2736-2fac-4ee1-a345-f9a883264e4f', 'Thị xã Nghĩa Lộ', '', null);
INSERT INTO `incatalog` VALUES ('9f4532a9-185e-4914-bb25-2621da9ba4cc', 'Huyện Yên Bình', '', null);
INSERT INTO `incatalog` VALUES ('cc93ffcb-dcc9-4328-92e0-c0ae48738340', 'Cây nông nghiệp', '', null);
INSERT INTO `incatalog` VALUES ('fe4e81fd-e82e-438c-81f4-9a76c278b160', 'Huyện Trạm Tấu', '', null);
