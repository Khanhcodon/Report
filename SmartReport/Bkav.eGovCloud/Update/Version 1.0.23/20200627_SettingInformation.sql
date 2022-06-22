ALTER TABLE `infomation`
ADD COLUMN `SystemName`  varchar(250) NULL AFTER `Phone`;
ALTER TABLE `infomation`
ADD COLUMN `ImagePath`  varchar(250) NULL AFTER `SystemName`;


INSERT INTO `resource`
VALUES ("Bkav.eGovCloud.Areas.Admin.Infomation.CreateOrEdit.Fields.SystemName.Label", "Tên hệ thống:");