ALTER TABLE `notifications`
CHANGE COLUMN `UserSendName` `GroupId`  varchar(50) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL AFTER `NotificationId`;

ALTER TABLE `notifications`
CHANGE COLUMN `IsViewed` `IsNew`  bit(1) NOT NULL AFTER `IsSent`;

ALTER TABLE `notifications`
ADD COLUMN `IsDeleted`  bit NULL AFTER `IsReaded`;

update	 notifications
SET IsDeleted = 1;

ALTER TABLE `notifications`
MODIFY COLUMN `IsDeleted`  bit(1) NOT NULL AFTER `IsReaded`;

