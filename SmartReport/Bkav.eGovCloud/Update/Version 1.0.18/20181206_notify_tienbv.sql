ALTER TABLE `notifications`
ADD COLUMN `Token`  char(36) NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000' AFTER `IsDeleted`;

