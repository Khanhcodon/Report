ALTER TABLE `user_department_jobtitles_position`
ADD COLUMN `HasReceiveDocument`  bit(1) NULL AFTER `IsAdmin`;

UPDATE `user_department_jobtitles_position`
SET HasReceiveDocument = 0;

ALTER TABLE `user_department_jobtitles_position`
MODIFY COLUMN `HasReceiveDocument`  bit(1) NOT NULL AFTER `IsAdmin`;

