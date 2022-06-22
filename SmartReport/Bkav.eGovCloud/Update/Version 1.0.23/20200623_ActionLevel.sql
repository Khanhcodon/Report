SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for `actionlevel`
-- ----------------------------
DROP TABLE IF EXISTS `actionlevel`;
CREATE TABLE `actionlevel` (
  `ActionLevelId` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id kỳ báo cáo',
  `ActionLevelName` varchar(1000) NOT NULL COMMENT 'Tên kỳ báo cáo',
  `ActionLevelCode` varchar(255) NOT NULL COMMENT 'Mã kỳ báo cáo',
  `StartTime` datetime DEFAULT NULL COMMENT 'Thời gian bắt đầu',
  `EndTime` datetime DEFAULT NULL COMMENT 'Thời gian kết thúc',
  `TemplateKey` varchar(255) DEFAULT NULL COMMENT 'Key lưu trữ',
  `CreatedByUserId` int(11) DEFAULT NULL COMMENT 'Id người tạo',
  `CreatedOnDate` datetime DEFAULT NULL COMMENT 'Ngày tạo',
  `LastModifiedByUserId` int(11) DEFAULT NULL COMMENT 'Id người thay đổi cuối cùng',
  `LastModifiedOnDate` datetime DEFAULT NULL COMMENT 'Ngày thay đổi cuối cùng',
  `Version` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ActionLevelId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

-- ----------------------------
-- Records of actionlevel
-- ----------------------------
INSERT INTO `actionlevel` VALUES ('1', 'Năm', '1', null, null, null, null, null, null, null, '0000-00-00 00:00:00');
INSERT INTO `actionlevel` VALUES ('2', '6 Tháng', '2', null, null, null, null, null, null, null, '0000-00-00 00:00:00');
INSERT INTO `actionlevel` VALUES ('3', 'Quý', '3', null, null, null, null, null, null, null, '0000-00-00 00:00:00');
INSERT INTO `actionlevel` VALUES ('4', 'Tháng', '4', null, null, null, null, null, null, null, '0000-00-00 00:00:00');
INSERT INTO `actionlevel` VALUES ('5', 'Tuần', '5', null, null, null, null, null, null, null, '0000-00-00 00:00:00');
INSERT INTO `actionlevel` VALUES ('6', 'Ngày', '6', null, null, null, null, null, null, null, '0000-00-00 00:00:00');
INSERT INTO `actionlevel` VALUES ('7', 'Khẩn cấp', '7', null, null, null, null, null, null, null, '0000-00-00 00:00:00');
INSERT INTO `actionlevel` VALUES ('8', '9 Tháng', '8', null, null, null, null, null, null, null, '0000-00-00 00:00:00');

-- ----------------------------
-- Records of permission
-- ----------------------------
INSERT INTO `permission` VALUES ('0', 'ActionLevelIndex', 'Danh sách kỳ báo cáo', 'Quản lý kỳ báo cáo', 'Cho phép người dùng xem danh sách kỳ báo cáo');
INSERT INTO `permission` VALUES ('0', 'ActionLevelCreate', 'Thêm mới kỳ báo cáo', 'Quản lý kỳ báo cáo', 'Cho phép người dùng tạo mới kỳ báo cáo');
INSERT INTO `permission` VALUES ('0', 'ActionLevelEdit', 'Chỉnh sửa kỳ báo cáo', 'Quản lý kỳ báo cáo', 'Cho phép người dùng chỉnh sửa kỳ báo cáo');
INSERT INTO `permission` VALUES ('0', 'ActionLevelDelete', 'Xóa kỳ báo cáo', 'Quản lý kỳ báo cáo', 'Cho phép người dùng xóa kỳ báo cáo');

-- ----------------------------
-- Records of resource
-- ----------------------------
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelName.Label', 'Tên kỳ báo cáo');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelName.Required', 'Bạn phải nhập tên kỳ báo cáo.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelCode.Label', 'Mã kỳ báo cáo');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.ActionLevelCode.Required', 'Bạn phải nhập mã kỳ báo cáo.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.StartTime.Label', 'Thời gian bắt đầu');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.StartTime.Required', 'Bạn phải nhập thời gian bắt đầu.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.EndTime.Label', 'Thời gian kết thúc');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.EndTime.Required', 'Bạn phải nhập thời gian kết thúc.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.TemplateKey.Label', 'Key lưu trữ');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.CreateOrEdit.Fields.TemplateKey.Required', 'Bạn phải nhập key lưu trữ');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Index.List.Column.ActionLevelName', 'Tên kỳ báo cáo');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Index.List.Column.TemplateKey', 'Key lưu trữ');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Index.List.Null', ' Không có danh mục kỳ báo cáo nào');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Label.Create', 'Thêm mới kỳ báo cáo');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Label.Update', 'Cập nhật kỳ báo cáo');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Created', 'Thêm mới kỳ báo cáo thành công!');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Updated', 'Cập nhật kỳ báo cáo thành công!');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.ConfirmDelete', 'Bạn có chắc chắn muốn xóa kỳ báo cáo này không?');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Deleted.Success', 'Xóa kỳ báo cáo thành công.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Customer.ActionLevel.NotPermission', 'Bạn không có quyền truy cập vào xem danh sách kỳ báo cáo!.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Customer.ActionLevel.NotPermissionCreate', 'Bạn không có quyền tạo mới kỳ báo cáo!.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Customer.ActionLevel.NotPermissionDelete', 'Bạn không có quyền xóa!.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Customer.ActionLevel.NotPermissionUpdate', 'Bạn không có quyền chỉnh sửa thông tin kỳ báo cáo!.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.List', 'Danh sách kỳ báo cáo');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Search.ActionLevelName.Label', 'Từ khóa');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.NotExist', 'Kỳ báo cáo không tồn tại.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ActionLevel.Create.Exist', 'Đã tồn tại kỳ báo cáo. Vui lòng xem lại.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ActionLevel.Search.ActionLevelName', 'Tên kỳ báo cáo:');
