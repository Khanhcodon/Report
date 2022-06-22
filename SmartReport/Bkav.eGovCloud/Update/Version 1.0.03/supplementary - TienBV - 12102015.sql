
ALTER TABLE `supplementary`
ADD COLUMN `PaperIds`  varchar(100) NULL AFTER `FeeIds`
ADD COLUMN `FeeIds`  varchar(100) NULL AFTER `FeeIds`;

ALTER TABLE `supplementary`
ADD COLUMN `NewDateAppointed`  datetime NULL AFTER `FeeIds`;