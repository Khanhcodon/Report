CREATE TABLE `permission_setting` (
  `PermissionSettingId` int(11) NOT NULL AUTO_INCREMENT,
  `PermissionSettingName` varchar(255) COLLATE utf8_unicode_ci NOT NULL COMMENT 'Tên chức năng',
  `DepartmentPositionHasPermission` text COLLATE utf8_unicode_ci,
  `PositionHasPermission` text COLLATE utf8_unicode_ci,
  `UserHasPermission` text COLLATE utf8_unicode_ci,
  PRIMARY KEY (`PermissionSettingId`)
) ENGINE=InnoDB AUTO_INCREMENT=192 DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

