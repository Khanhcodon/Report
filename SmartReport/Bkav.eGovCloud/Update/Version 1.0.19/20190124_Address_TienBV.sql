ALTER TABLE `address`
ADD COLUMN `HasRecalled`  bit NOT NULL DEFAULT b'0' AFTER `LevelEdocId`;

