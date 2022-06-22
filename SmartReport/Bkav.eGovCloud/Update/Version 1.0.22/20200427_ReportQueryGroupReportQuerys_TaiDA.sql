CREATE TABLE `reportquerygroup_reportquerys`  (
  `ReportQueryGroupReportQueryId` int(11) NOT NULL AUTO_INCREMENT,
  `ReportQueryGroupId` int(11) NOT NULL,
  `ReportQueryId` int(11) NOT NULL,
  PRIMARY KEY (`ReportQueryGroupReportQueryId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 98 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
