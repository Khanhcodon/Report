ALTER TABLE `authorize`
add COLUMN `AuthorizeUserName`   varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ;

ALTER TABLE `authorize`
add COLUMN `AuthorizedUserName` varchar(128) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL ;