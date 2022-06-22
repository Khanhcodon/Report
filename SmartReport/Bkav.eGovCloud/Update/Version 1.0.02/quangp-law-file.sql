/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovclouddemo

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-06-18 15:20:17
Username: QuangP
Description: 
- Xóa column FileId trong bảng Law, xóa foreign key law-file, xóa index bảng law
- Thêm column LawId trong bảng file	
*/

ALTER TABLE `law` DROP FOREIGN KEY `law_ibfk_1`;
ALTER TABLE `law`
DROP COLUMN `FileId`,
DROP INDEX `FK_Law_File`;


ALTER TABLE `file`
ADD COLUMN `LawId`  int(11) NULL DEFAULT NULL AFTER `DocOnlineId`;

