ALTER TABLE `docfield`
ADD COLUMN `IconFileName`  varchar(255) NULL AFTER `Order`
ADD COLUMN `IconFileDisplayName`  varchar(255) NULL AFTER `IconFileName`;

ALTER TABLE `doctype`
ADD COLUMN `IconFileName`  varchar(255) NULL AFTER `Content`,
ADD COLUMN `IconFileDisplayName`  varchar(255) NULL AFTER `IconFileName`;

ALTER TABLE `template`
ADD COLUMN `Sql`  text NULL AFTER `ContentFileLocalName`;

ALTER TABLE `store`
ADD COLUMN `DocFieldIds`  text NULL AFTER `CategoryBusinessId`;

