ALTER TABLE `doctype_timejob`
ADD COLUMN `isActiveAlert`  bit(1) NOT NULL AFTER `IsActive`,
ADD COLUMN `ScheduleConfigDueDate`  text NULL AFTER `isActiveAlert`,
ADD COLUMN `ScheduleConfigOutOfDate`  text NULL AFTER `ScheduleConfigDueDate`,
ADD COLUMN `ScheduleTypeDueDate`  tinyint(4) NOT NULL AFTER `ScheduleConfigOutOfDate`,
ADD COLUMN `ScheduleTypeOutOfDate`  tinyint(4) NOT NULL AFTER `ScheduleTypeDueDate`,
ADD COLUMN `IsActiveAlertOut`  bit(1) NOT NULL AFTER `ScheduleTypeOutOfDate`;