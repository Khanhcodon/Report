ALTER TABLE `docrelation`
MODIFY COLUMN `Compendium`  varchar(2000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL AFTER `RelationType`,
MODIFY COLUMN `CitizenName`  varchar(2000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `CategoryName`;

