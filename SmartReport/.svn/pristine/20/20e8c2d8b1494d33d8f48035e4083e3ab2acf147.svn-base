ALTER TABLE `docrelation`
ADD COLUMN `Compendium`  longtext NULL AFTER `RelationType`,
ADD COLUMN `DocCode`  varchar(150) NULL AFTER `Compendium`,
ADD COLUMN `DateArrived`  datetime NULL AFTER `DocCode`,
ADD COLUMN `InOutCode`  varchar(250) NULL AFTER `DateArrived`,
ADD COLUMN `CurrentUser`  varchar(150) NULL AFTER `InOutCode`,
ADD COLUMN `CategoryName`  varchar(250) NULL AFTER `InOutCode`,
ADD COLUMN `CitizenName`  varchar(250) NULL AFTER `CategoryName`;

Update `docrelation` dr
JOIN document d on d.DocumentId = dr.RelationId
JOIN DocumentCopy dc on dc.DocumentCopyId = dr.RelationCopyId
JOIN `user` u on u.UserId = dc.UserCurrentId
set dr.`Compendium` = d.Compendium,
 dr.`DocCode` = d.DocCode,
 dr.`DateArrived` = d.DateCreated,
 dr.`InOutCode` = d.InOutCode,
 dr.`CurrentUser` = u.FullName,
 dr.`CitizenName` = d.`CitizenName`;
 
ALTER TABLE `docrelation`
MODIFY COLUMN `Compendium`  longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL AFTER `RelationType`,
MODIFY COLUMN `DateArrived`  datetime NOT NULL AFTER `DocCode`;
