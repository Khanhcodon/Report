insert into resource VALUES(NULL, "IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Label","Tên danh mục:");
insert into resource VALUES(NULL, "IndicatorCatalog.CreateOrEdit.Fields.ParentId.Label","Danh mục cha:");
insert into resource VALUES(NULL, "IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogCode.Label","Mã danh mục:");
insert into resource VALUES(NULL, "IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.Required","Bạn phải nhập tên danh mục!");
insert into resource VALUES(NULL, "IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogName.MaxLength","Tên danh mục không được vượt quá 500 ký tự!");
insert into resource VALUES(NULL, "IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogCode.Required","Bạn phải nhập mã danh mục!");
insert into resource VALUES(NULL, "IndicatorCatalog.CreateOrEdit.Fields.InCatalogId.Required","Bạn phải nhập danh mục!");
insert into resource VALUES(NULL, "IndicatorCatalog.CreateOrEdit.Fields.IndicatorCatalogCode.MaxLength","Mã danh mục không được vượt quá 300 ký tự!");
insert into resource VALUES(NULL, "Customer.IndicatorCatalog.NotPermission","Bạn không có quyển vào xem danh sách chỉ tiêu!");
insert into resource VALUES(NULL, "Customer.IndicatorCatalog.NotPermissionCreate","Bạn không có quyển thêm mới chỉ tiêu!");
insert into resource VALUES(NULL, "Customer.IndicatorCatalog.NotPermissionUpdate","Bạn không có quyển chỉnh sửa chỉ tiêu!");
insert into resource VALUES(NULL, "Customer.IndicatorCatalog.NotPermissionDelete","Bạn không có quyển xóa chỉ tiêu!");
insert into resource VALUES(NULL, "Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Updated","Cập nhật chỉ tiêu thành công!");
insert into resource VALUES(NULL, "Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Deleted","Xóa chỉ tiêu thành công!");
insert into resource VALUES(NULL, "Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.Created","Thêm mới chỉ tiêu thành công!");
insert into resource VALUES(NULL, "Bkav.eGovCloud.Areas.Admin.IndicatorCatalog.ConfirmDelete","Bạn chắc chắn muốn xóa chỉ tiêu này chứ?");
INSERT INTO `permission` VALUES ('0', 'IndicatorCatalogIndex',	'Danh sách chỉ tiêu','Quản lý chỉ tiêu',	'Cho phép người dùng xem danh sách chỉ tiêu');
INSERT INTO `permission` VALUES ('0', 'IndicatorCatalogCreate',	'Thêm mới chỉ tiêu','Quản lý chỉ tiêu',	'Cho phép người dùng thêm mới chỉ tiêu');
INSERT INTO `permission` VALUES ('0', 'IndicatorCatalogEdit',	'Chỉnh sửa chỉ tiêu','Quản lý chỉ tiêu',	'Cho phép người dùng chỉnh sửa chỉ tiêu');
INSERT INTO `permission` VALUES ('0', 'IndicatorCatalogDelete',	'Xóa chỉ tiêu','Quản lý chỉ tiêu',	'Cho phép người dùng xóa chỉ tiêu');

DROP TABLE IF EXISTS `indicatorcatalog`;
CREATE TABLE `indicatorcatalog`  (
  `IndicatorCatalogId` int(11) NOT NULL AUTO_INCREMENT,
  `IndicatorCatalogName` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `IndicatorCatalogCode` varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `ParentId` int(11) NULL DEFAULT NULL,
  `CategoryId` int(11) NOT NULL,
  PRIMARY KEY (`IndicatorCatalogId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;