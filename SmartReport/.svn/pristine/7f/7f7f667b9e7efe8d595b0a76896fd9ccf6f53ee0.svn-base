/*
Navicat MySQL Data Transfer

Source Server         : .
Source Server Version : 50622
Source Host           : localhost:3306
Source Database       : qng_stttt

Target Server Type    : MYSQL
Target Server Version : 50622
File Encoding         : 65001

Date: 2017-08-02 17:20:00
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `vote`
-- ----------------------------
DROP TABLE IF EXISTS `vote`;
CREATE TABLE `vote` (
  `VoteId` int(11) NOT NULL AUTO_INCREMENT,
  `TimeBegin` datetime DEFAULT NULL,
  `TimeEnd` datetime DEFAULT NULL,
  `IsMultiSelect` bit(1) DEFAULT NULL,
  `VoteDetailId` text COLLATE utf8_unicode_ci,
  `Title` varchar(1000) COLLATE utf8_unicode_ci DEFAULT NULL,
  `IsPublic` bit(1) DEFAULT NULL,
  `IsCommentDiff` bit(1) DEFAULT NULL,
  `IsViewResultImmediately` bit(1) DEFAULT NULL,
  `DepartmentsView` text COLLATE utf8_unicode_ci,
  `UsersView` text COLLATE utf8_unicode_ci,
  `DepartmentsVote` text COLLATE utf8_unicode_ci,
  `UsersVote` text COLLATE utf8_unicode_ci,
  `UserIdCreate` int(11) DEFAULT NULL,
  `IsNotify` bit(1) DEFAULT NULL,
  PRIMARY KEY (`VoteId`)
) ENGINE=InnoDB AUTO_INCREMENT=66 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

-- ----------------------------
-- Table structure for `votedetail`
-- ----------------------------
DROP TABLE IF EXISTS `votedetail`;
CREATE TABLE `votedetail` (
  `VoteDetailId` int(11) NOT NULL AUTO_INCREMENT,
  `UserIdsVote` text COLLATE utf8_unicode_ci,
  `UserIdCreate` int(11) DEFAULT NULL,
  `VoteId` int(11) DEFAULT NULL,
  `TitleDetail` varchar(255) COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`VoteDetailId`)
) ENGINE=InnoDB AUTO_INCREMENT=179 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;


