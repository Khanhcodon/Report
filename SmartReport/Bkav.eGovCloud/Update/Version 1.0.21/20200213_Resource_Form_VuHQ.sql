ALTER TABLE form ADD COLUMN FormCodeCompilation MEDIUMTEXT COMMENT "FormCode của mẫu báo cáo Compilation";
insert into resource VALUES(NULL, "DocType.Form.CreateOrEdit.Fields.CompilationId.Label", "Nhận chỉ tiêu của");
insert into resource VALUES(NULL, "DocType.Form.CreateOrEdit.Fields.FormCodeCompilation.Label", "Json cấu hình chỉ tiêu");