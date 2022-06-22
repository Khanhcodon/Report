ALTER TABLE `mobiledevice`
CHANGE COLUMN `OSVersion` `Location`  varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `DeviceName`;

