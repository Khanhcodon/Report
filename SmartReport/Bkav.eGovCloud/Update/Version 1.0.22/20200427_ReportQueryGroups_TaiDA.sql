CREATE TABLE `reportquerygroups`  (
  `ReportQueryGroupId` int(11) NOT NULL AUTO_INCREMENT,
  `ReportQueryGroupName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CreatedAt` datetime(0) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `UpdatedAt` datetime(0) NULL DEFAULT NULL,
  `UpdatedBy` int(11) NULL DEFAULT NULL,
  `Description` varchar(500) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`ReportQueryGroupId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 15 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;