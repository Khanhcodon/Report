ALTER TABLE `mobiledevice`
ADD COLUMN `IP`  varchar(255) NULL AFTER `IsActive`,
ADD COLUMN `LoginToken`  varchar(255) NULL AFTER `IP`,
ADD COLUMN `Browser`  varchar(255) NULL AFTER `LoginToken`;