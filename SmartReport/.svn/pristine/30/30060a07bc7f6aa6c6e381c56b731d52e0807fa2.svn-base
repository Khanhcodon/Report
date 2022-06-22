DROP TABLE IF EXISTS doctype_form_catalog;
DROP TABLE IF EXISTS doctype_form_extendfield;
DROP TABLE IF EXISTS doctype_embryonicform;
DROP TABLE IF EXISTS doc_office;
DROP TABLE IF EXISTS docdestroy;
DROP TABLE IF EXISTS docreason;
DROP TABLE IF EXISTS doctypeattachment;
DROP TABLE IF EXISTS documentpublish;
DROP TABLE IF EXISTS embryonicform;
ALTER TABLE `form`
DROP COLUMN `DocTypeId`,
CHANGE COLUMN `Embryonic` `EmbryonicPath`  varchar(350) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Tên mẫu phôi cho form động' AFTER `Template`,
ADD COLUMN `EmbryonicLocationName`  varchar(45) NULL AFTER `FormUrl`;
