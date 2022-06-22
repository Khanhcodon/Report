ALTER TABLE `documentcopy`
ADD COLUMN `Note`  mediumtext NULL AFTER `CurrentDepartmentName`,
ADD COLUMN `FormId`  varchar(255) NULL AFTER `Note`,
ADD COLUMN `TimeKey`  varchar(255) NULL AFTER `FormId`,
ADD COLUMN `OrganizationCode`  varchar(30) NULL AFTER `TimeKey`;
