﻿ALTER TABLE `doc_extendfield`
MODIFY COLUMN `ExtendFieldValue`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'Giá trị' AFTER `ExtendFieldName`;

