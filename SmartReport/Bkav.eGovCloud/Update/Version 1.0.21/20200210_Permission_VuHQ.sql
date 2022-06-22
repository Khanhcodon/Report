INSERT INTO `permission` VALUES ('0', 'DocTypeCreateExplicit', 'Thêm mới loại văn bản tường minh', 'Quản lý loại văn bản', 'Cho phép người dùng tạo mới loại văn bản tường minh');
INSERT INTO `permission` VALUES ('0', 'DocTypeEditExplicit', 'Chỉnh sửa loại văn bản tường minh', 'Quản lý loại văn bản', 'Cho phép người dùng chỉnh sửa loại văn bản tường minh');
-- INSERT INTO `permission` VALUES ('0', 'DocTypeFormPlus', 'Danh sách mẫu loại văn bản tường minh', 'Quản lý loại văn bản', 'Cho phép người dùng xem danh sách mẫu văn bản tường minh');

ALTER TABLE form ADD COLUMN ExplicitTemplate MEDIUMTEXT COMMENT "Mẫu báo cáo tường minh";

insert into resource VALUES(NULL, "DocType.Form.CreateOrEdit.Fields.ExplicitTemplate.Label", "Nội dung");