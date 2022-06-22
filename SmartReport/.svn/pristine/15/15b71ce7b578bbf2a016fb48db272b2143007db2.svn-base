/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : egovbrvt_soyt

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2016-11-04 13:34:07
*/

INSERT INTO `template` VALUES ('52', 'Sms_Tra cứu_Hồ sơ đang xử lý', 'Ho so {doc_code} dang duoc Ong/ba {doc_usercurrent}-{doc_currentPosition} xu ly. Chung toi se gui thong tin den Ong/ba trong thoi gian som nhat. Tran trong!', null, null, null, '3', '16383', '', null, null, null, '0', null);
INSERT INTO `template` VALUES ('53', 'Sms_Tra cứu_Hồ sơ đã xử lý xong', 'HS {doc_code} cua Ong/ba da duoc giai quyet, KQ duoc tra vao buoi lam viec ke tiep tai BPTN&amp;TKQ tinh BRVT, hoac tra tai nha neu da yeu cau. Tran trong!', null, null, null, '3', '16383', '', null, null, null, '0', null);

INSERT INTO `templatekey` VALUES ('68', null, '00000000-0000-0000-0000-000000000000', 'Người đang giữ', '{doc_usercurrent}', 'select u.FullName as CurrentUserName\r\nfrom document d\r\njoin documentcopy dc on dc.DocumentId = d.DocumentId and dc.DocumentCopyType = 1\r\nJOIN `user` u on u.UserId = dc.UserCurrentId\r\nwhere d.DocumentId = @DocId', '@Model[0].CurrentUserName', '2', '', null, '');
INSERT INTO `templatekey` VALUES ('69', null, '00000000-0000-0000-0000-000000000000', 'Chức vụ đang giữ', '{doc_currentPosition}', 'select p.PositionName as CurrentPositionName\r\nfrom document d\r\njoin documentcopy dc on dc.DocumentId = d.DocumentId and dc.DocumentCopyType = 1\r\nJOIN user_department_jobtitles_position udp on udp.UserId = dc.UserCurrentId\r\nJOIN position p on p.PositionId = udp.PositionId\r\nwhere d.DocumentId = @DocId\r\nORDER BY udp.IsPrimary\r\nLIMIT 1;', '@Model[0].CurrentPositionName', '2', '', null, '');

INSERT INTO `notify_config` VALUES ('22', 'Lookup_DocumentHasResult', 'egovcloud.enum.notifyconfigtype.Lookup_DocumentHasResult', '', '', '0', '53', null, 'Sms_Tra cứu_Hồ sơ đã xử lý xong');
INSERT INTO `notify_config` VALUES ('23', 'Lookup_DocumentProcessing', 'egovcloud.enum.notifyconfigtype.Lookup_DocumentProcessing', '', '', '0', '52', null, 'Sms_Tra cứu_Hồ sơ đang xử lý');
