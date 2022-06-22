INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Customer.ReportQuery.NotPermission', 'Bạn không có quyền xem danh sách [Truy vấn báo cáo]!.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Customer.ReportQuery.NotPermissionEdit', 'Bạn không có quyền chỉnh sửa [Truy vấn báo cáo]!.');

INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.ReportQuery.Label', 'Tên truy vấn báo cáo');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.ReportQuery.MaxLength', 'Tên truy vấn báo cáo không được vượt quá 255 ký tự!');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.ReportQuery.Required', 'Bạn phải nhập Tên truy vấn báo cáo!');

INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('ReportQueryCreate', 'Thêm mới truy vấn báo cáo', 'Quản lý truy vấn báo cáo', 'Cho phép người dùng tạo mới truy vấn báo cáo');
INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('ReportQueryDelete', 'Xóa truy vấn báo cáo', 'Quản lý truy vấn báo cáo', 'Cho phép người dùng xóa truy vấn báo cáo');
INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('ReportQueryEdit', 'Chỉnh sửa truy vấn báo cáo', 'Quản lý truy vấn báo cáo', 'Cho phép người dùng chỉnh sửa truy vấn báo cáo');
INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('ReportQueryIndex', 'Danh sách truy vấn báo cáo', 'Quản lý truy vấn báo cáo', 'Cho phép người dùng xem danh sách truy vấn báo cáo');

INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQuery.ConfirmDelete', 'Bạn có chắc chắn muốn xóa thông tin truy vấn báo cáo này không?');

INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQuery.Deleted.Error', 'Có lỗi trong quá trình xóa truy vấn báo cáo');

INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.ReportQueryName.Label', 'Tên truy vấn');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.TemplateKeyCategoryId.Label', 'Loại danh mục');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.FormulaColumnName.Label', 'Tên công thức');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.TableName.Label', 'Tên bảng');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.TableDescription.Label', 'Tên bảng');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.Query.Label', 'Câu truy vấn');

INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.CreatedAt.Label', 'Tạo lúc');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.CreatedBy.Label', 'Tạo bởi');

INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQuery.Created', 'Thêm mới truy vấn báo cáo thành công!');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQuery.Deleted', 'Xóa truy vấn báo cáo thành công!.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQuery.Updated', 'Cập nhật truy vấn báo cáo thành công!');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQuery.NotExist', 'Truy vấn báo cáo không tồn tại!');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQueryName.Exist', 'Tên truy vấn báo cáo đã tồn tại!');

INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('ReportQueryGroupCreate', 'Thêm mới nhóm truy vấn báo cáo', 'Quản lý nhóm truy vấn báo cáo', 'Cho phép người dùng tạo mới nhóm truy vấn báo cáo');
INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('ReportQueryGroupDelete', 'Xóa nhóm truy vấn báo cáo', 'Quản lý truy nhóm truy vấn báo cáo', 'Cho phép người dùng xóa nhóm truy vấn báo cáo');
INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('ReportQueryGroupEdit', 'Chỉnh sửa nhóm truy vấn báo cáo', 'Quản lý nhóm truy vấn báo cáo', 'Cho phép người dùng chỉnh sửa nhóm truy vấn báo cáo');
INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('ReportQueryGroupIndex', 'Danh sách nhóm truy vấn báo cáo', 'Quản lý nhóm truy vấn báo cáo', 'Cho phép người dùng xem danh sách nhóm truy vấn báo cáo');

INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQueryGroup.ConfirmDelete', 'Bạn có chắc chắn muốn xóa nhóm thông tin truy vấn báo cáo này không?');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQueryGroup.CreateOrEdit.Fields.ReportQueryGroupName.Label', 'Tên nhóm truy vấn');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQueryGroup.CreateOrEdit.Fields.Description.Label', 'Mô tả');

INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Customer.ReportQueryGroup.NotPermission', 'Bạn không có quyền xem danh sách [Nhóm truy vấn báo cáo]!.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Customer.ReportQueryGroup.NotPermissionEdit', 'Bạn không có quyền chỉnh sửa [Nhóm truy vấn báo cáo]!.');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQueryGroup.NotExist', 'Nhóm truy vấn báo cáo không tồn tại!');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('Bkav.eGovCloud.Areas.Admin.ReportQueryGroupName.Exist', 'Nhóm truy vấn báo cáo đã tồn tại!');
INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('ReportQueryGroupCreate', 'Thêm mới nhóm truy vấn báo cáo', 'Quản lý nhóm truy vấn báo cáo', 'Cho phép người dùng tạo mới nhóm truy vấn báo cáo');

INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.ReportQueryGroup.MaxLength', 'Tên nhóm truy vấn báo cáo không được vượt quá 255 ký tự!');
INSERT INTO `resource`(`ResourceKey`, `ResourceValue`) VALUES ('ReportQuery.CreateOrEdit.Fields.ReportQueryGroup.Required', 'Bạn phải nhập Tên nhóm truy vấn báo cáo!');

INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('DataSourceIndex', 'Danh sách DataSource', 'Quản lý DataSource', 'Cho phép người dùng xem danh sách DataSource');
INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('DataSourceCreate', 'Thêm mới DataSource', 'Quản lý DataSource', 'Cho phép người dùng thêm mới DataSource');
INSERT INTO `permission`(`PermissionKey`, `PermissionName`, `ModuleName`, `Description`) VALUES ('DataSourceEdit', 'Chỉnh sửa DataSource', 'Quản lý DataSource', 'Cho phép người dùng chỉnh sửa DataSource');
