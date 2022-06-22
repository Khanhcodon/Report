-- 20191211 VUHQ REQ-5
ALTER TABLE form ADD COLUMN TableName MEDIUMTEXT COMMENT "Tên bảng";

insert into resource VALUES(NULL, "DocType.Form.CreateOrEdit.Fields.TableName.Label", "Tên bảng");

