/*
Navicat MySQL Data Transfer

Source Server         : 10.206.19.69
Source Server Version : 50521
Source Host           : 10.206.19.69:3306
Source Database       : egovbrvt-nv

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2016-11-08 15:42:23
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Records of templatekey
-- ----------------------------
delete from `templatekey`
where TemplateKeyId in (68, 69);

INSERT INTO `templatekey` VALUES ('68', null, '00000000-0000-0000-0000-000000000000', 'Người đang giữ', '{doc_usercurrent}', 'select u.FullName as CurrentUserName\r\nfrom document d\r\njoin documentcopy dc on dc.DocumentId = d.DocumentId and dc.DocumentCopyType in (1, 64)\r\nJOIN `user` u on u.UserId = dc.UserCurrentId\r\nwhere d.DocumentId = @DocId', '@Model[0].CurrentUserName', '2', '', null, '');
INSERT INTO `templatekey` VALUES ('69', null, '00000000-0000-0000-0000-000000000000', 'Chức vụ đang giữ', '{doc_currentPosition}', 'select p.PositionName as CurrentPositionName\r\nfrom document d\r\njoin documentcopy dc on dc.DocumentId = d.DocumentId and dc.DocumentCopyType in (1, 64)\r\nJOIN user_department_jobtitles_position udp on udp.UserId = dc.UserCurrentId\r\nJOIN position p on p.PositionId = udp.PositionId\r\nwhere d.DocumentId = @DocId\r\nORDER BY udp.IsPrimary\r\nLIMIT 1;', '@Model[0].CurrentPositionName', '2', '', null, '');
