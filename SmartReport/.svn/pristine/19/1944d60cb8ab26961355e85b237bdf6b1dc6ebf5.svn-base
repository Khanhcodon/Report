CREATE TABLE `reportqueryfilters`  (
  `ReportQueryFilterId` int(255) NOT NULL AUTO_INCREMENT,
  `ReportQueryId` int(11) NOT NULL,
  `DataFieldId` int(11) NULL DEFAULT NULL,
  `FilterValue` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FilterToValue` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `FilterValues` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Condition` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsFilter` bit(1) NULL DEFAULT NULL,
  `IsSummary` bit(1) NULL DEFAULT NULL,
  PRIMARY KEY (`ReportQueryFilterId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 99 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
