ALTER TABLE `calendar`
MODIFY COLUMN `Title`  varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL AFTER `CalendarId`,
MODIFY COLUMN `Place`  varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `IsAccepted`,
ADD COLUMN `PlacePublish`  varchar(300) NULL AFTER `TitlePublish`,
ADD COLUMN `UserPrimaryPublish`  varchar(300) NULL AFTER `PlacePublish`,
ADD INDEX `IDX_IsPrivate` (`IsPrivate`) USING BTREE ,
ADD INDEX `IDX_HasPublish` (`HasPublish`) USING BTREE ,
ADD INDEX `IDX_IsAccepted` (`IsAccepted`) USING BTREE ,
ADD INDEX `IDX_BeginTime` (`BeginTime`) USING BTREE ,
ADD INDEX `IDX_UserCreate` (`UserCreatedId`) USING BTREE ;

