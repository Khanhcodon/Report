ALTER TABLE `document`
ADD COLUMN `UserCreatedName`  varchar(150) NULL AFTER `HasCA`,
ADD COLUMN `UserSuccessName`  varchar(150) NULL AFTER `UserCreatedName`;


ALTER TABLE `documentcopy`
ADD COLUMN `UserCurrentName`  varchar(150) NULL AFTER `ProcessInfo`,
ADD COLUMN `UserSendId`  int NULL AFTER `UserCurrentName`,
ADD COLUMN `CurrentDepartmentName`  varchar(350) NULL AFTER `UserSendId`;

