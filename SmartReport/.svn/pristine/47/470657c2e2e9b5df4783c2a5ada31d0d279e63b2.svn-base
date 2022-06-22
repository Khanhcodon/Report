/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125 - MySQL
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2016-10-28 13:59:58
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for notification
-- ----------------------------
DROP TABLE IF EXISTS notifications;
DROP TABLE IF EXISTS `notification`;
CREATE TABLE `notification` (
  `NotificationId` int(11) NOT NULL AUTO_INCREMENT,
  `NotificationType` int(11) DEFAULT NULL,
  `UserId` int(11) NOT NULL,
  `MailId` int(11) DEFAULT NULL,
  `ChatId` int(11) DEFAULT NULL,
  `DocumentCopyId` int(11) DEFAULT NULL,
  `Title` varchar(150) NOT NULL,
  `Content` varchar(500) NOT NULL,
  `SenderAvatar` varchar(500) DEFAULT NULL,
  `SenderUserName` varchar(50) DEFAULT NULL,
  `SenderFullName` varchar(255) DEFAULT NULL,
  `Date` datetime NOT NULL,
  `ReceiveDate` datetime NOT NULL,
  `ViewdDate` datetime DEFAULT NULL,
  `Hidden` bit(1) DEFAULT NULL,
  PRIMARY KEY (`NotificationId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
