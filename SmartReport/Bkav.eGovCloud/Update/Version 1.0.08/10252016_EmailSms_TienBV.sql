ALTER TABLE `mail`
ADD COLUMN `DocumentCopyId`  int NULL AFTER `DocumentId`,
ADD COLUMN `NotifyConfigType`  varchar(100) NULL AFTER `DocumentCopyId`;


ALTER TABLE `sms`
ADD COLUMN `DocumentCopyId`  int NULL AFTER `DocumentId`,
ADD COLUMN `NotifyConfigType`  varchar(100) NULL AFTER `DocumentCopyId`;
