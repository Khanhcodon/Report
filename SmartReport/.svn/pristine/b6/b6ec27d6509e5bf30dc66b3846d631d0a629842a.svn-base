ALTER TABLE `doctype`
ADD COLUMN `Order`  int NULL AFTER `IconFileDisplayName`;

UPDATE doctype
SET `Order` = IF(LENGTH(SUBSTRING_INDEX(DocTypeName,'.',1)) > 3, 999, CONVERT(SUBSTRING_INDEX(DocTypeName,'.',1), SIGNED INTEGER)) ;
