ALTER TABLE `doc_publish`
ADD COLUMN `Traces`  longtext NULL AFTER `DocumentCopyResponsed`,
ADD COLUMN `AddressCode`  varchar(30) NULL AFTER `Traces`;
