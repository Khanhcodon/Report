-- 20191030 VUHQ REQ-2
ALTER TABLE form ADD COLUMN DefineFieldJson MEDIUMTEXT COMMENT "Json cấu hình của header (name, comment) của handsontable";
-- 20191030 VUHQ REQ-2
ALTER TABLE form ADD COLUMN DefineConfigJson MEDIUMTEXT COMMENT "Json cấu hình của header (type, required, default value, readonly) của handsontable";
-- 20191030 VUHQ REQ-2
ALTER TABLE form ADD COLUMN DefineValueJson MEDIUMTEXT COMMENT "Json cấu hình giá trị mặc định của handsontable";