/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : egovbrvt_soyt

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2016-10-29 09:25:24
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `time_job`
-- ----------------------------
DROP TABLE IF EXISTS `time_job`;
CREATE TABLE `time_job` (
  `TimeJobId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(500) COLLATE utf8_unicode_ci NOT NULL,
  `TimerJobType` tinyint(4) NOT NULL,
  `DateLastJobRun` datetime DEFAULT NULL,
  `JobInterval` datetime NOT NULL,
  `DateNextJobStartAfter` datetime NOT NULL,
  `DateNextJobStartBefore` datetime NOT NULL,
  `ScheduleType` tinyint(4) NOT NULL,
  `ScheduleConfig` text COLLATE utf8_unicode_ci,
  `IsActive` bit(1) NOT NULL,
  `IsRunning` bit(1) NOT NULL,
  PRIMARY KEY (`TimeJobId`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of time_job
-- ----------------------------
INSERT INTO `time_job` VALUES ('2', 'IndexTimerElapsed', '1', null, '2015-05-28 15:00:17', '2015-05-29 10:00:00', '2015-05-29 09:00:00', '3', '{\"Type\":\"HangNgay\",\"FromHour\":9,\"FromMinute\":0,\"ToHour\":10,\"ToMinute\":0}', '', '');
INSERT INTO `time_job` VALUES ('3', 'Kiểm tra những service không hoạt động', '2', null, '2015-05-28 15:00:50', '2015-05-29 10:00:00', '2015-05-29 09:00:00', '3', '{\"Type\":\"HangNgay\",\"FromHour\":9,\"FromMinute\":0,\"ToHour\":10,\"ToMinute\":0}', '', '');
INSERT INTO `time_job` VALUES ('4', 'Kiểm tra xem có văn bản mới tới không', '3', '2016-10-29 09:24:54', '2016-10-28 16:32:35', '2016-10-29 09:25:54', '2016-10-29 09:25:54', '1', '{\"Type\":\"HangPhut\",\"Minutes\":1}', '', '');
INSERT INTO `time_job` VALUES ('5', 'Notify những văn bản chưa notify', '4', '2016-10-29 09:01:18', '2016-06-11 14:02:22', '2016-10-29 10:10:00', '2016-10-29 10:01:00', '2', '{\"Type\":\"HangGio\",\"FromMinute\":1,\"ToMinute\":10}', '', '');
INSERT INTO `time_job` VALUES ('6', 'Notify những văn bản chờ xử lý', '5', '2016-10-29 09:01:26', '2016-06-11 14:02:56', '2016-10-29 10:10:00', '2016-10-29 10:01:00', '2', '{\"Type\":\"HangGio\",\"FromMinute\":1,\"ToMinute\":10}', '', '');
INSERT INTO `time_job` VALUES ('7', 'Kiểm tra file bị thay đổi', '6', null, '2015-05-28 15:02:33', '2015-05-29 09:59:00', '2015-05-29 08:00:00', '3', '{\"Type\":\"HangNgay\",\"FromHour\":8,\"FromMinute\":0,\"ToHour\":9,\"ToMinute\":59}', '', '');
INSERT INTO `time_job` VALUES ('8', 'Đánh index tìm kiếm', '7', null, '2015-05-28 15:02:47', '2015-05-28 23:59:00', '2015-05-28 08:00:00', '3', '{\"Type\":\"HangNgay\",\"FromHour\":8,\"FromMinute\":0,\"ToHour\":23,\"ToMinute\":59}', '', '');
INSERT INTO `time_job` VALUES ('9', 'Sao lưu têp tin', '9', null, '2016-06-08 11:36:54', '2016-06-11 01:00:00', '2016-06-11 00:01:00', '4', '{\"Type\":\"HangTuan\",\"FromDayOfWeek\":\"Saturday\",\"ToDayOfWeek\":\"Saturday\",\"FromHour\":0,\"FromMinute\":1,\"ToHour\":1,\"ToMinute\":0}', '', '');
INSERT INTO `time_job` VALUES ('10', 'Sao lưu cơ sở dữ liệu', '8', null, '2016-06-08 11:37:01', '2016-06-08 20:00:00', '2016-06-08 14:15:00', '3', '{\"Type\":\"HangNgay\",\"FromHour\":14,\"FromMinute\":15,\"ToHour\":20,\"ToMinute\":0}', '', '');
INSERT INTO `time_job` VALUES ('11', 'Tiến trình gửi sms', '11', '2016-10-29 09:24:00', '2016-10-27 11:44:37', '2016-10-29 09:29:00', '2016-10-29 09:29:00', '1', '{\"Type\":\"HangPhut\",\"Minutes\":5}', '', '');
INSERT INTO `time_job` VALUES ('12', 'Tiến trình gửi mail', '10', '2016-10-29 09:25:04', '2016-10-03 14:16:08', '2016-10-29 09:26:04', '2016-10-29 09:26:04', '1', '{\"Type\":\"HangPhut\",\"Minutes\":1}', '', '');
