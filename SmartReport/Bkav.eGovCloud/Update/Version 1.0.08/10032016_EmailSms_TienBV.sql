ALTER TABLE `mail`
ADD COLUMN `DocumentId`  char(36) NULL AFTER `UserName`;

ALTER TABLE `sms`
ADD COLUMN `DocumentId`  char(36) NULL AFTER `UserSendId`;

