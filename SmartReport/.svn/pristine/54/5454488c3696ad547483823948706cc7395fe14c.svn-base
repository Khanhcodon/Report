ALTER TABLE `calendar_detail`
MODIFY COLUMN `UserPrimary`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Cán bộ chủ trì' AFTER `Content`,
MODIFY COLUMN `Department`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Đơn vị chủ trì' AFTER `UserPrimary`,
MODIFY COLUMN `Joined`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Thành phần tham gia' AFTER `Department`,
MODIFY COLUMN `Note`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Ghi chú' AFTER `Joined`,
MODIFY COLUMN `Prepare`  varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL COMMENT 'Cán bộ chuẩn bị' AFTER `UserVieweds`,
ADD COLUMN `UserSecondary`  varchar(300) NULL AFTER `Prepare`;

ALTER TABLE `calendar`
ADD COLUMN `HasPublish`  bit NOT NULL DEFAULT b'0' AFTER `DepartmentIdExt`;

ALTER TABLE `calendar`
ADD COLUMN `TitlePublish`  varchar(1000) NULL AFTER `HasPublish`;

