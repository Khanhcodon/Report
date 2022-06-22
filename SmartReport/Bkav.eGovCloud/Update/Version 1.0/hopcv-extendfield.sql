/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 200515
Username: HopCV
Description: 
    - Thêm trường "DefaultSort" cho bảng "processfunction" để cấu hình sắp xếp danh sách văn bản theo
*/

ALTER TABLE `processfunction`
ADD COLUMN `DefaultSort` varchar(1000) NULL;


