/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb_copy

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-10-07 11:47:09
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `dim_categorydisaggregations`
-- ----------------------------
DROP TABLE IF EXISTS `dim_categorydisaggregations`;
CREATE TABLE `dim_categorydisaggregations` (
  `CategoryDisaggregationId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `IndicatorId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `CategoryDisaggregationName` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `CategoryDisaggregationCode` varchar(255) COLLATE utf8_unicode_ci NOT NULL,
  `IsActivated` bit(1) NOT NULL,
  `OrderType` int(11) NOT NULL,
  PRIMARY KEY (`CategoryDisaggregationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci ROW_FORMAT=DYNAMIC;

-- ----------------------------
-- Records of dim_categorydisaggregations
-- ----------------------------
INSERT INTO `dim_categorydisaggregations` VALUES ('0a610c5b-a9f8-4b83-8ddb-7d3cc65197f4', '7befb13e-9084-40a8-86f9-6e12c109ce35', 'Trung học phổ thông1', 'TD011', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('0bec3758-c172-4adf-9cef-b75562479d9d', 'ca0f787c-927c-4947-83cc-3081705122d9', 'Trung cấp51', 'TD061', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('154fb91e-5222-460c-b331-9174a4fbd855', '541e3e6c-70a8-4a8a-b80a-976fb7e53607', 'testHiepns111', 'lND1112', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('27c66998-dec6-464c-8159-00726d7454ae', 'fd293161-a950-401f-ac21-5619f7e00459', 'Trung cấp111', 'TD121', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('2ab4ae79-c3ff-417b-b03c-6aeb591532cb', '267eacf0-cf14-4fd1-8623-8b4b01eb0f17', 'Hoạt động kinh doanh bất động sản1', 'NKT062', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('323214eb-0de4-47e3-92b4-94fb81245f0f', 'b902abc7-5ffe-4d6c-8f48-b35685873ee8', 'Trung cấp31', 'TD041', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('348616b3-8f15-488d-82a0-4e2484d948fb', 'de098d09-295a-457b-826f-f1073c2dd632', 'Trung cấp61', 'TD071', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('3e9f4a92-c78d-4c2d-b3c4-20740afce9fe', '6985f76d-fc4d-4196-b213-05301655a57b', 'Bán buôn, bán lẻ; Sửa chữa ô tô, mô tô, xe máy và xe có động cơ khác1', 'NKT0712', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('57aae2ba-6533-49ab-be5e-7531f45899f6', 'f79ea783-abab-4b5e-baa7-6e78df8e63f5', 'Trung cấp101', 'TD111', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('5956f0ce-70f3-4b63-bb80-2cf288fbd4f0', '311df173-c0d0-4780-a377-1a7532e7b89d', 'Đất có mặt nước ven biển1', 'LĐ42', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('65ed991f-7944-4c7c-9749-3ab020ac5d5d', '93804ae5-585c-4fe1-b589-919da66d478f', 'Trung cấp21', 'TD031', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('7cb558db-2cc7-44fc-86ec-865515c8ced4', '1ea2eb6e-762f-40ff-96c5-a5388f0e398b', 'Đất chưa sử dụng1', 'LĐ32', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('80f1a16a-d5c7-4fbc-8211-e8290e042e03', 'ed0d5427-cc4a-4d29-9584-e3f7df96df7c', 'Trung cấp81', 'TD091', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('942ca937-e24b-4d89-915f-1831aee2e510', 'eab57195-5cf3-41c9-82f3-d2d437418615', 'Trung cấp71', 'TD081', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('9a2fbaea-ff0b-4c05-9b25-7a0ebf0a46d6', 'be7e2b92-e7e0-4428-87ca-f584332c322f', 'Trung cấp41', 'TD051', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('a0e3e8c9-a0a7-4061-add2-9171fc0cc721', '381ca741-baf5-4731-adf9-f13c48ba0edb', 'Sản xuất và phân phối điện, khí đốt, nước nóng, hơi nước và điều hòa không khí1', 'NKT042', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('a5eed137-8c74-4196-a496-46faf446e3dc', '12e0aa1f-1f3e-4be2-8650-38eb743be8ab', 'Đất nông nghiệp1', 'LĐ12', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('c1df22a7-9aa3-4d91-be08-0c34f5101a14', '5e054ef4-43c0-45fa-b72b-f178a2d48898', 'Cung cấp nước, hoạt động quản lý và xử lý rác thải, nước thải1', 'NKT052', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('ceb5ce7b-3112-4c74-8edf-24222e489613', '341bfd51-2145-4055-8e4d-618648e5f4c6', 'Nông nghiệp, lâm nghiệp và thủy sản1', 'NKT012', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('d48f1bc9-b1e8-4faf-a84d-4addaf62840e', 'f63a949b-4a2f-40ef-8a8f-f386eeefd2d3', 'Trung cấp91', 'TD101', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('dba813c8-b321-40b3-8247-477007d31a7c', '7d838cab-22c8-49d7-98f9-e3b29a79285d', 'Trung cấp11', 'TD021', '', '0');
INSERT INTO `dim_categorydisaggregations` VALUES ('e3ab9a84-7b9a-4494-af2b-088cd92ca339', '3633ade6-0fa8-4732-9b31-7085e36248f8', 'Đất phi nông nghiệp1', 'LĐ22', '', '0');
