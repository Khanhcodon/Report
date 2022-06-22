ALTER TABLE `attachment`
ADD COLUMN `UserDeleted`  varchar(200) NULL AFTER `Size`,
ADD COLUMN `DeletedDate`  datetime NULL AFTER `UserDeleted`;

