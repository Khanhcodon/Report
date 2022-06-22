ALTER TABLE `documentcopy`
CHANGE COLUMN `IsCreateNode` `HasJustCreated`  bit(1) NULL DEFAULT NULL AFTER `DateModified`;

