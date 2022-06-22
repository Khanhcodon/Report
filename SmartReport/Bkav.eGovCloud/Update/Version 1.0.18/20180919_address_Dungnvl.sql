ALTER TABLE `address`
ADD COLUMN `LevelEdocId`  int NULL DEFAULT 2 AFTER `IsPublishEmail`;

update `address` set LevelEdocId = 2