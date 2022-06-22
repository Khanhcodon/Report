ALTER TABLE `storeprivate_user`
MODIFY COLUMN `UserId`  int(11) NULL AFTER `StorePrivateId`,
ADD COLUMN `DepartmentId` int(11) NULL DEFAULT NULL AFTER `UserId`,
ADD COLUMN `DepartmentIdExt`  varchar(255) NULL AFTER `DepartmentId`;

ALTER TABLE `storeprivate_user` DROP FOREIGN KEY `storeprivate_user_ibfk_2`;
