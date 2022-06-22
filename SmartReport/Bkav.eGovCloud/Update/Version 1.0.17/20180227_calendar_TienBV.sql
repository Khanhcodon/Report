/*
Navicat MySQL Data Transfer

Source Server         : Localhost
Source Server Version : 50715
Source Host           : localhost:3306
Source Database       : egovbrvt_vpub

Target Server Type    : MYSQL
Target Server Version : 50715
File Encoding         : 65001

Date: 2018-02-27 14:49:17
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `calendar`
-- ----------------------------
DROP TABLE IF EXISTS `calendar`;
CREATE TABLE `calendar` (
  `CalendarId` int(11) NOT NULL AUTO_INCREMENT,
  `Title` varchar(2000) COLLATE utf8_unicode_ci NOT NULL,
  `BeginTime` datetime NOT NULL,
  `IsAccepted` bit(1) DEFAULT NULL,
  `Place` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserCreatedId` int(11) NOT NULL,
  `UserCreatedName` varchar(200) COLLATE utf8_unicode_ci NOT NULL,
  `DateCreated` datetime NOT NULL,
  `IsPrivate` bit(1) NOT NULL,
  `DepartmentIdExt` varchar(150) COLLATE utf8_unicode_ci NOT NULL,
  PRIMARY KEY (`CalendarId`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of calendar
-- ----------------------------
INSERT INTO `calendar` VALUES ('6', 'Dự lễ chào cờ đầu tuần', '2018-02-22 07:30:00', '', 'TTHC-CT', '486', 'Trần Thị Lý', '2018-02-22 17:01:51', '', '0');
INSERT INTO `calendar` VALUES ('7', 'Giao ban TTrTU để cho ý kiến về một số nội dung (cả ngày):', '2018-02-22 08:30:00', '', 'VPTU', '486', 'Trần Thị Lý', '2018-02-22 17:09:18', '', '0');
INSERT INTO `calendar` VALUES ('8', 'Dự Lễ Khánh thành Cảng Quốc tế Thị Vài', '2018-02-22 08:30:00', '', 'Cảng Quốc tế Thị Vài', '486', 'Trần Thị Lý', '2018-02-22 17:11:13', '', '0');
INSERT INTO `calendar` VALUES ('9', 'Nghe báo cáo Kế hoạch tổ chức họp mặt Doanh nghiệp và nhà đầu tư Xuân Mậu Tuất năm 2018', '2018-02-22 14:30:00', '', 'VPUBT (Phòng họp 302)', '486', 'Trần Thị Lý', '2018-02-22 17:13:29', '', '0');
INSERT INTO `calendar` VALUES ('12', 'Tiếp Doanh nghiệp', '2018-02-26 01:30:00', '', 'VPUBT (302)', '486', 'Trần Thị Lý', '2018-02-26 10:48:25', '', '0');
INSERT INTO `calendar` VALUES ('14', 'Tiếp Doanh nghiệp', '2018-02-26 08:30:00', '', 'VPUBT (302)', '719', 'Nguyễn Thanh Tùng', '2018-02-26 14:35:35', '', '0');
INSERT INTO `calendar` VALUES ('15', 'Họp BTC cuộc thi ý tưởng Quy hoạch tổng thể phát triển KH-XH tỉnh', '2018-02-26 08:00:00', '', 'VPUBT (307)', '719', 'Nguyễn Thanh Tùng', '2018-02-26 14:37:34', '', '0');
INSERT INTO `calendar` VALUES ('16', 'Dự lễ khởi công Nhà máy xử lý bụi KCN Phú Mỹ 3', '2018-02-26 09:00:00', '', 'KCN chuyên sâu Phú Mỹ 3', '719', 'Nguyễn Thanh Tùng', '2018-02-26 14:41:13', '', '0');
INSERT INTO `calendar` VALUES ('17', 'Tiếp Lãnh đạo Công ty TNHH Cảng Quốc tế Thị Vài', '2018-02-26 11:00:00', '', 'VPTU', '719', 'Nguyễn Thanh Tùng', '2018-02-26 14:44:06', '', '0');
INSERT INTO `calendar` VALUES ('18', 'Tiếp Công dân Nguyễn Quảng Thịnh, P8, TPVT về giải quyết khiếu nại', '2018-02-26 07:30:00', '', 'VPTU', '719', 'Nguyễn Thanh Tùng', '2018-02-26 14:45:56', '', '0');
INSERT INTO `calendar` VALUES ('19', 'Dự hội nghị triển khai công tác chuyển đề năm 2018 về xây dựng phong cách, tác phong công tác của người đứng đầu, của cán bộ đảng viên trong học tập và làm theo tư tưởng, đạo đức, phong cách Hồ Chí Minh', '2018-02-26 08:30:00', '', 'VPUBT (301)', '719', 'Nguyễn Thanh Tùng', '2018-02-26 14:48:02', '', '0');
INSERT INTO `calendar` VALUES ('20', 'Họp BCS Đảng UBND tỉnh', '2018-02-26 14:00:00', '', 'VPUBT (302)', '311', 'Phan Thanh Chí', '2018-02-26 14:57:08', '', '0');
INSERT INTO `calendar` VALUES ('21', 'Tiếp Tập đoàn Delta', '2018-02-26 15:30:00', '', 'VPTU', '311', 'Phan Thanh Chí', '2018-02-26 14:58:02', '', '0');

-- ----------------------------
-- Table structure for `calendar_detail`
-- ----------------------------
DROP TABLE IF EXISTS `calendar_detail`;
CREATE TABLE `calendar_detail` (
  `CalendarDetailId` int(11) NOT NULL AUTO_INCREMENT,
  `CalendarId` int(11) NOT NULL,
  `Content` varchar(1500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserPrimary` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Department` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Joined` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Note` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Attachments` varchar(200) COLLATE utf8_unicode_ci DEFAULT NULL,
  `UserVieweds` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`CalendarDetailId`),
  KEY `FK_Calendar` (`CalendarId`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=40 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of calendar_detail
-- ----------------------------
INSERT INTO `calendar_detail` VALUES ('10', '6', '', 'Chủ tịch, các PCT', 'Ban Quản lý TTHC-CT', 'Cán bộ, công chức làm việc tại Trung tâm HC-CT Tỉnh', '', null, null);
INSERT INTO `calendar_detail` VALUES ('11', '7', '1. Công tác cán bộ;', 'Đc Trình', '', 'Theo thư mời của Tỉnh ủy', '', null, null);
INSERT INTO `calendar_detail` VALUES ('12', '7', '2. Cho ý kiến về khu đất của dự án nghỉ dưỡng du lịch Bình Châu của Tỉnh Ủy;', 'Đc Long', 'Sở TNMT', '', 'Đc Thanh chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('13', '7', '3. Kế hoạch trồng rừng năm 2018;', 'Đc Quốc', 'Sở NNPTNT', '', 'Đc Chung chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('14', '7', '4. Giải pháp chống xuống cấp, chống đột Trung tâm Hành chính - Chính trị Tỉnh;', 'Đc Long', 'Ban QLTTHCCT, Sở Nội Vụ', '', 'Đc Tày chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('15', '8', '', 'Đc Quốc', 'Ban QLTTHC-CT', 'Theo thư mời của Cty TNHH Cảng Quốc tế Thị Vài', 'Đc Trâm chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('16', '9', '', 'Đc Long, Đc Vinh', 'Sở KHĐT', 'Sở KHĐT, TC, XD, CgT, NgV, NNPTNT, BQLKCN, Thuế, Đài PTTH, TT Xúc tiến đầu tư', 'Đc Hoan chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('17', '10', '', 'Tiến', 'Ban quarn lys', 'Theo thư mời', 'Chả có gì', null, null);
INSERT INTO `calendar_detail` VALUES ('20', '12', '', 'Đc Vinh, Đc Quốc', '', 'Theo kế hoạch', '', null, null);
INSERT INTO `calendar_detail` VALUES ('22', '14', '', 'Đc Trình, Đc Hoan', '', 'Theo kế hoạch', 'Đc Hoàn chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('23', '15', '', 'Đc Long', 'Sở KHDT', 'Thành viên Ban Tổ chức cuộc thi', 'Đc Đậu chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('24', '16', '', 'Dc Quốc', '', 'Theo tư mời của cty CP Zincoxit Corpozation Việt Nam', 'Đc Huỳnh chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('25', '17', '', 'Đc Quốc', '', 'Theo thư mời Tỉnh ủy', 'Đc Trân', null, null);
INSERT INTO `calendar_detail` VALUES ('26', '18', '', 'Đc Tịnh', '', 'Theo thư mời Tỉnh ủy', 'Đc Sáu chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('27', '19', '', 'Đc Tịnh', '', 'Theo thư mời của Tỉnh Ủy', 'Đc Sáu chuẩn bị', null, null);
INSERT INTO `calendar_detail` VALUES ('28', '20', '1. Thông qua dự thảo báo cáo tổng kết 5 năm thực hiện nghị quyết 6 về Chính sách pháp luật đất đai trong thời kỳ đẩy mạnh toàn diện công cuộc đổi mới;', 'Chủ tịch, các PCT', 'Sở TNMT', 'Sở TNMT, NNPTNT, XD, KHĐT, GTVT, DL, YT, GDĐT, VHTT, LDDTBXH, các Huyện, Thành phố', 'Đc Hương', null, null);
INSERT INTO `calendar_detail` VALUES ('29', '20', '2. Kế hoạch hành động thực hiện NQQ số 07 của TU về phát triển kinh tế trang trại', '', 'Sở NNPTNT', '- Sở NNPTNT, KHĐT, TNMT, XD, CgT, DL, các H, TP', 'Đc Chung', null, null);
INSERT INTO `calendar_detail` VALUES ('30', '21', '', 'TTrUB', '', 'Theo thư mời Tỉnh ủy', 'Đc Hoàn', null, null);

-- ----------------------------
-- Table structure for `calendar_resource`
-- ----------------------------
DROP TABLE IF EXISTS `calendar_resource`;
CREATE TABLE `calendar_resource` (
  `CalendarResourceId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(1000) COLLATE utf8_unicode_ci NOT NULL,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`CalendarResourceId`),
  KEY `IDX_User` (`UserId`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of calendar_resource
-- ----------------------------
INSERT INTO `calendar_resource` VALUES ('1', 'Phòng họp 1', '486');
INSERT INTO `calendar_resource` VALUES ('2', 'Phòng họp 2', '486');
INSERT INTO `calendar_resource` VALUES ('3', 'Phòng họp 3', '486');
INSERT INTO `calendar_resource` VALUES ('4', 'Phòng họp 4', '486');
INSERT INTO `calendar_resource` VALUES ('5', 'Phòng họp 5', '486');
INSERT INTO `calendar_resource` VALUES ('6', 'Phòng họp 6', '486');
INSERT INTO `calendar_resource` VALUES ('7', 'Phongf hop 7', '486');
INSERT INTO `calendar_resource` VALUES ('8', 'Phong hopj 8', '486');
