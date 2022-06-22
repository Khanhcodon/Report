/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : egovbrvt_soyt

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2016-10-29 09:22:21
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `notify_config`
-- ----------------------------
DROP TABLE IF EXISTS `notify_config`;
CREATE TABLE `notify_config` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Key` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Description` varchar(1000) COLLATE utf8_unicode_ci NOT NULL,
  `HasAutoSendMail` bit(1) DEFAULT NULL,
  `HasAutoSendSms` bit(1) DEFAULT NULL,
  `MailTemplateId` int(11) NOT NULL,
  `SmsTemplateId` int(11) NOT NULL,
  `MailTemplateName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  `SmsTemplateName` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Records of notify_config
-- ----------------------------
INSERT INTO `notify_config` VALUES ('14', 'Document_WhenReceived', 'Gửi thông báo cho người nhận khi gửi văn bản, hồ sơ', '', '', '0', '24', null, 'Sms _ Thông báo trễ hẹn giải quyết hồ sơ');
INSERT INTO `notify_config` VALUES ('15', 'Document_WhenCreated', 'Gửi thông báo cho Công dân khi tiếp nhận hồ sơ', '', '', '30', '48', 'eMail_Thông báo tiếp nhận hồ sơ', 'Sms_Thông báo tiếp nhận hồ sơ');
INSERT INTO `notify_config` VALUES ('16', 'Document_WhenHasResult', 'Gửi thông báo cho Công dân khi hồ sơ có kết quả xử lý và hẹn trả.', '', '', '43', '46', 'eMail_Thông báo trả kết quả', 'Sms_Thông báo trả kết quả hồ sơ');
INSERT INTO `notify_config` VALUES ('17', 'Document_WhenOverdue', 'Gửi thông báo cho Công dân khi hồ sơ quá hạn xử lý.', '', '', '51', '24', 'eMail_Thông báo hồ sơ trễ hẹn', 'Sms _ Thông báo trễ hẹn giải quyết hồ sơ');
INSERT INTO `notify_config` VALUES ('18', 'Document_WhenRequireSupplementary', 'Gửi thông báo cho công dân khi có yêu cầu bổ sung.', '', '', '42', '45', 'eMail_Thông báo yêu cầu bổ sung', 'Sms_Thông báo yêu cầu bổ sung hồ sơ');
INSERT INTO `notify_config` VALUES ('19', 'Document_WhenCompleteSupplementary', 'Gửi thông báo cho Công dân khi tiếp nhận yêu cầu bổ sung.', '', '', '50', '49', 'eMail_Thông báo hoàn thành bổ sung', 'Sms_Thông báo hoàn thành bổ sung hồ sơ');
INSERT INTO `notify_config` VALUES ('20', 'DocumentOnline_WhenAccepted', 'Gửi thông báo cho Công dân khi tiếp nhận hồ sơ đăng ký qua mạng.', '', '', '30', '29', 'eMail_Thông báo tiếp nhận hồ sơ', 'Sms_Thông báo tiếp nhận hồ sơ đăng ký qua mạng');
INSERT INTO `notify_config` VALUES ('21', 'DocumentOnline_WhenRejected', 'Gửi thông báo cho Công dân khi từ chối hồ sơ đăng ký qua mạng.', '', '', '31', '28', 'eMail_Thông báo từ chối hồ sơ đăng ký qua mạng', 'Sms_Thông báo từ chối đăng ký qua mạng');
