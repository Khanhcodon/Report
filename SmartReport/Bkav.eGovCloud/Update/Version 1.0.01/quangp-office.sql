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
- Xóa foreign key office - level do chuy?n b?ng level sang enum
- Xóa b?ng level	
*/

ALTER TABLE `office` DROP FOREIGN KEY `office_ibfk_1`;
DROP TABLE `level`