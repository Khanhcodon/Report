ALTER TABLE `mobiledevice`
MODIFY COLUMN `UserId`  int(11) NOT NULL AFTER `MobileDeviceId`,
MODIFY COLUMN `Serial`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL COMMENT 'DeviceId neu la thiet bi di dong; MAC neu la desktop' AFTER `OS`,
MODIFY COLUMN `DeviceName`  varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `Serial`,
MODIFY COLUMN `OSVersion`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `DeviceName`,
MODIFY COLUMN `Token`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `OSVersion`,
MODIFY COLUMN `IP`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL AFTER `IsActive`,
MODIFY COLUMN `LoginToken`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `IP`,
MODIFY COLUMN `Browser`  varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `LoginToken`,
ADD COLUMN `HasBlock`  bit NULL AFTER `Browser`;
ADD COLUMN `History`  varchar(3000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `HasBlock`;