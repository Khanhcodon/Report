DROP TABLE IF EXISTS `reportkey`;
CREATE TABLE `reportkey`  (
  `ReportKeyId` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(400) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `Name` varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `ParentId` int(11) NULL DEFAULT NULL,
  `Sql` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`ReportKeyId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;