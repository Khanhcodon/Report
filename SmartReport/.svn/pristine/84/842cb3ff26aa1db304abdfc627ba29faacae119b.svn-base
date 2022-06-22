/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-09-23 10:32:06
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `extendedtime`
-- ----------------------------
DROP TABLE IF EXISTS `extendedtime`;
CREATE TABLE `renewals` (
  `RenewalsId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id của bảng gia hạn xử lý(gia hạn thêm thời gian xử lý)',
  `DocumentCopyId` int(11) NOT NULL COMMENT 'Văn bản/hồ sơ được gia hạn xử lý',
  `DocumentCopyIds` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `OldDateAppointed` datetime DEFAULT NULL COMMENT 'Thời gian trước khi gia hạn(thời gian xử lý ban đầu)',
  `RenewalsDays` int(11) NOT NULL COMMENT 'Số ngày gia hạn',
  `ApprovedRenewalsDays` int(11) DEFAULT NULL COMMENT 'Số ngày gia hạn được duyệt',
  `NewDateAppointed` datetime DEFAULT NULL COMMENT 'Thời gian sau khi gia hạn',
  `UserRequestedId` int(11) NOT NULL COMMENT 'Người xử lý gia hạn',
  `UserApprovedId` int(11) DEFAULT NULL COMMENT 'Người duyệt gia hạn',
  `Reason` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'Lý do gia hạn(của người xử lý gia hạn)',
  `ApprovedComment` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL COMMENT 'Ý kiến người duyệt',
  `IsApproved` bit(1) NOT NULL COMMENT 'Trạng thái gia hạn: False - Chưa duyệt, True - Đã duyệt',
  `RenewalsType` int(11) NOT NULL COMMENT 'Loại gia hạn: 1 - Gia hạn hẹn trả; 2 - gia hạn xử lý',
  PRIMARY KEY (`RenewalsId`)
) ENGINE=InnoDB AUTO_INCREMENT=24 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci COMMENT='Bảng lưu trữ lịch sử gia hạn xử lý';

