/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb_copy

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-10-07 11:47:21
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `dim_disaggregation`
-- ----------------------------
DROP TABLE IF EXISTS `dim_disaggregation`;
CREATE TABLE `dim_disaggregation` (
  `IndicatorId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `IndicatorName` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `IsActivated` bit(1) NOT NULL,
  `IndicatorDesctiption` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`IndicatorId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of dim_disaggregation
-- ----------------------------
INSERT INTO `dim_disaggregation` VALUES ('12e0aa1f-1f3e-4be2-8650-38eb743be8ab', 'Thôn/ấp/bản/tổ dân phố', '', 'Thôn/ấp/bản/tổ dân phố');
INSERT INTO `dim_disaggregation` VALUES ('1ea2eb6e-762f-40ff-96c5-a5388f0e398b', 'Nội dung kinh tế', '', 'Nội dung kinh tế');
INSERT INTO `dim_disaggregation` VALUES ('267eacf0-cf14-4fd1-8623-8b4b01eb0f17', 'Loại hình trang trại', '', 'Loại hình trang trại');
INSERT INTO `dim_disaggregation` VALUES ('311df173-c0d0-4780-a377-1a7532e7b89d', 'Trồng mới/cho sản phẩm', '', 'Trồng mới/cho sản phẩm');
INSERT INTO `dim_disaggregation` VALUES ('341bfd51-2145-4055-8e4d-618648e5f4c6', 'Thôn/ấp/bản/tổ dân phốrv', '', 'Thôn/ấp/bản/tổ dân phốr');
INSERT INTO `dim_disaggregation` VALUES ('3633ade6-0fa8-4732-9b31-7085e36248f8', 'Ngành kinh tế', '', 'Ngành kinh tế');
INSERT INTO `dim_disaggregation` VALUES ('381ca741-baf5-4731-adf9-f13c48ba0edb', 'Loại hình kinh tế', '', 'Loại hình kinh tế');
INSERT INTO `dim_disaggregation` VALUES ('541e3e6c-70a8-4a8a-b80a-976fb7e53607', 'Loại đấtrv', '', 'Loại đấtr');
INSERT INTO `dim_disaggregation` VALUES ('5e054ef4-43c0-45fa-b72b-f178a2d48898', 'Loại hình kinh tếrv', '', 'Loại hình kinh tếr');
INSERT INTO `dim_disaggregation` VALUES ('6985f76d-fc4d-4196-b213-05301655a57b', 'Ngành kinh tếrv', '', 'Ngành kinh tếr');
INSERT INTO `dim_disaggregation` VALUES ('7befb13e-9084-40a8-86f9-6e12c109ce35', 'Hiện trạng sử dụng', '', 'Hiện trạng sử dụng');
INSERT INTO `dim_disaggregation` VALUES ('7d838cab-22c8-49d7-98f9-e3b29a79285d', 'Giới tínhrv', '', 'Giới tínhr');
INSERT INTO `dim_disaggregation` VALUES ('93804ae5-585c-4fe1-b589-919da66d478f', 'Phòng học phân tổ thêm kiên cố/bán kiên cố/nhà tạm', '', 'Phòng học phân tổ thêm kiên cố/bán kiên cố/nhà tạm');
INSERT INTO `dim_disaggregation` VALUES ('b902abc7-5ffe-4d6c-8f48-b35685873ee8', 'Nội dung kinh tếrv', '', 'Nội dung kinh tếr');
INSERT INTO `dim_disaggregation` VALUES ('be7e2b92-e7e0-4428-87ca-f584332c322f', 'Trồng mới/cho sản phẩmrv', '', 'Trồng mới/cho sản phẩmr');
INSERT INTO `dim_disaggregation` VALUES ('ca0f787c-927c-4947-83cc-3081705122d9', 'Giới tính', '', 'Giới tính');
INSERT INTO `dim_disaggregation` VALUES ('de098d09-295a-457b-826f-f1073c2dd632', 'Hiện trạng sử dụngrv', '', 'Hiện trạng sử dụngr');
INSERT INTO `dim_disaggregation` VALUES ('eab57195-5cf3-41c9-82f3-d2d437418615', 'Loại đất', '', 'Loại đất');
INSERT INTO `dim_disaggregation` VALUES ('ed0d5427-cc4a-4d29-9584-e3f7df96df7c', 'rtv', '', 'rtv');
INSERT INTO `dim_disaggregation` VALUES ('f63a949b-4a2f-40ef-8a8f-f386eeefd2d3', 'Loại hình trang trạirv', '', 'Loại hình trang trạir');
INSERT INTO `dim_disaggregation` VALUES ('f79ea783-abab-4b5e-baa7-6e78df8e63f5', 'rtvrv', '', 'rtvr');
INSERT INTO `dim_disaggregation` VALUES ('fd293161-a950-401f-ac21-5619f7e00459', 'Phòng học phân tổ thêm kiên cố/bán kiên cố/nhà tạmrv', '', 'Phòng học phân tổ thêm kiên cố/bán kiên cố/nhà tạmr');
