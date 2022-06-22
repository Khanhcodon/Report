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

ALTER TABLE `vote`
ADD COLUMN `UsersVoted`  text NULL AFTER `IsNotify`;

