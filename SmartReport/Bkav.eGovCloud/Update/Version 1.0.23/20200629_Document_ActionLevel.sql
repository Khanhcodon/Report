ALTER TABLE `documentcopy`
ADD COLUMN `ActionLevelId`  int(11) NULL COMMENT 'Id kỳ báo cáo' AFTER `OrganizationCode`,
ADD COLUMN `ActionLevelStartDate`  datetime NULL COMMENT 'Thời gian bắt đầu kỳ báo cáo' AFTER `ActionLevelId`,
ADD COLUMN `ActionLevelEndDate`  datetime NULL COMMENT 'Thời gian kết thúc kỳ báo cáo' AFTER `ActionLevelStartDate`;
