ALTER TABLE `department`
ADD COLUMN `HasReceiveWarning`  bit NULL AFTER `Version`,
ADD COLUMN `Emails`  varchar(200) NULL AFTER `HasReceiveWarning`;

UPDATE department
SET HasReceiveWarning = 0;

ALTER TABLE `department`
MODIFY COLUMN `HasReceiveWarning`  bit(1) NOT NULL AFTER `Version`;

