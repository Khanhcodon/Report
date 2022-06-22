ALTER TABLE `doctimeline`
ADD COLUMN `NodeSendId`  int NULL AFTER `ConfirmedOn`,
ADD COLUMN `NodeSendName`  varchar(255) NULL AFTER `NodeSendId`,
ADD COLUMN `UserSendId`  int NOT NULL AFTER `NodeSendName`,
ADD COLUMN `DateOverdue`  datetime NULL AFTER `UserSendId`,
ADD COLUMN `WorkflowId`  int NULL AFTER `DateOverdue`;

ALTER TABLE `doctimeline`
CHANGE COLUMN `DateOverdue` `TimeInNode`  int NULL DEFAULT NULL AFTER `UserSendId`;
