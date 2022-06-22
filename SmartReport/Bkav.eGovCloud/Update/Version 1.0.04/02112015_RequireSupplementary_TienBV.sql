ALTER TABLE `requiredsupplementary`
ADD COLUMN `PaperIds`  varchar(100) NULL AFTER `UserId`,
ADD COLUMN `FeeIds`  varchar(100) NULL AFTER `PaperIds`;

