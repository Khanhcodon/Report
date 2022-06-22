/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2015-05-08 18:20:17
Username: TienBV
Description: 
    - Thêm trường "Test4" cho bảng "extendfield" để test
	- Xóa 2 trường "Test", "test2" để test
*/

ALTER TABLE `extendfield`
DROP COLUMN `Test`,
DROP COLUMN `test2`,
ADD COLUMN `Test4`  varchar(50) NOT NULL AFTER `Mask`;
