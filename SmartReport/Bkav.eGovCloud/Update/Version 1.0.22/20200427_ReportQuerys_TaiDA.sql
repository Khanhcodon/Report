CREATE TABLE `reportquerys`  (
  `ReportQueryId` int(11) NOT NULL AUTO_INCREMENT,
  `ReportQueryName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `ActionLevelId` int(11) NULL DEFAULT NULL,
  `DataTableId` int(255) NULL DEFAULT NULL,
  `FormulaDataColumnId` int(255) NULL DEFAULT NULL,
  `FormCode` mediumtext CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Description` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreatedAt` datetime(0) NOT NULL,
  `CreatedBy` int(11) NOT NULL,
  `UpdatedAt` datetime(0) NULL DEFAULT NULL,
  `UpdatedBy` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`ReportQueryId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 33 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
