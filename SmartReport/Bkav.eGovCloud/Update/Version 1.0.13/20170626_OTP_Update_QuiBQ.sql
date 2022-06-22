ALTER TABLE `otp`
MODIFY COLUMN `Content`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL AFTER `OtpId`,
MODIFY COLUMN `Email`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL AFTER `Content`,
MODIFY COLUMN `Sms`  varchar(250) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL AFTER `Email`,
MODIFY COLUMN `Status`  bit(1) NOT NULL DEFAULT b'0' AFTER `Sms`,
MODIFY COLUMN `ActivedCode`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL AFTER `Status`,
CHANGE COLUMN `TimeStart` `DateCreated`  datetime NULL DEFAULT NULL AFTER `ActivedUrl`,
ADD COLUMN `DateLimit`  datetime NULL AFTER `DateCreated`;
