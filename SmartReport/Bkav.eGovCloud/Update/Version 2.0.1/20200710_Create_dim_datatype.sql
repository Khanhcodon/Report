/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb_copy

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-10-07 11:47:15
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `dim_datatype`
-- ----------------------------
DROP TABLE IF EXISTS `dim_datatype`;
CREATE TABLE `dim_datatype` (
  `dataTypeId` char(36) NOT NULL,
  `nameID` varchar(255) DEFAULT NULL,
  `dataTypeName` varchar(255) DEFAULT NULL,
  `dataTypeDescription` varchar(255) DEFAULT NULL,
  `distribute` varchar(255) DEFAULT NULL,
  `IsActivated` bit(1) DEFAULT NULL,
  PRIMARY KEY (`dataTypeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of dim_datatype
-- ----------------------------
INSERT INTO `dim_datatype` VALUES ('06345cf5-7b99-442f-a3e4-581be5459ffb', 'CT', 'Chính thức', 'Số liệu chính thức', 'Loại số liệu hệ thống', '');
INSERT INTO `dim_datatype` VALUES ('364d89e0-57fa-4f8f-a6ac-e5c610a710d9', 'UT', 'Ước tính', 'Số liệu ước tính', 'Loại số liệu hệ thống', '');
INSERT INTO `dim_datatype` VALUES ('45a396d9-eb0a-45db-951e-49af040f4674', 'KCT', 'Không chính thức', 'Số liệu không chính thức', 'Loại số liệu đơn vị', '');
INSERT INTO `dim_datatype` VALUES ('cec27785-32c6-44f9-bcf8-5021a7c7847a', 'TH', 'Thực hiện', 'Số liệu thực hiện', 'Loại số liệu hệ thống', '');
INSERT INTO `dim_datatype` VALUES ('ef693bd7-e71d-48b1-8f55-aea55fd73049', 'KH', 'Kế hoạch', 'Số liệu kế hoạch', 'Loại số liệu hệ thống', '');
