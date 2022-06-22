ALTER TABLE `address`
ADD COLUMN `IsPublishEmail`  bit NOT NULL DEFAULT b'0' AFTER `GroupName`;

