ALTER TABLE `document`
ADD COLUMN `DocTypeName`  varchar(1000) NULL AFTER `UserSuccessName`,
ADD COLUMN `CategoryName`  varchar(255) NULL AFTER `DocTypeName`;

