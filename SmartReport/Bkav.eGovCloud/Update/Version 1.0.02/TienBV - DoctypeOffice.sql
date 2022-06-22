/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovclouddemo

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-07-15 15:20:17
Username: Tienbv
Description: 
- Them ban quan he giua doctype va office
*/

CREATE TABLE `doctype_office` (
`DoctypeOfficeId`  int NOT NULL AUTO_INCREMENT,
`DoctypeId`  char(36) NOT NULL ,
`OfficeId`  int NOT NULL ,
PRIMARY KEY (`DoctypeOfficeId`)
)
;


ALTER TABLE `office`
MODIFY COLUMN `FileService`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'Đường dẫn file service' AFTER `Address`,
MODIFY COLUMN `DataService`  text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL COMMENT 'Đường dẫn data service' AFTER `FileService`;

