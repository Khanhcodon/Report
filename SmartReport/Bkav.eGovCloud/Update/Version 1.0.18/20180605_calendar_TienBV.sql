ALTER TABLE `calendar`
MODIFY COLUMN `BeginTime`  datetime NULL AFTER `Title`;

ALTER TABLE `calendar`
ADD COLUMN `Order`  int NOT NULL AFTER `UserPrimaryPublish`;


