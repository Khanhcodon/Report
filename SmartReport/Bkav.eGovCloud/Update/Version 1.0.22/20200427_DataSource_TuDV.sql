CREATE TABLE `datasource`  (
  `DataSourceId` int(11) NOT NULL AUTO_INCREMENT,
  `DomainId` int(11) NOT NULL,
  `Name` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `DateModified` datetime(0) NOT NULL,
  `DatabaseType` int(11) NOT NULL,
  `ConnectionString` varchar(4000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `UserCreatedId` int(11) NOT NULL,
  `Description` text CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL,
  `Customer` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `Server` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Port` int(11) NOT NULL DEFAULT 1,
  `DatabaseName` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Username` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Password` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `DepartmentId` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`DataSourceId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of datasource
-- ----------------------------
INSERT INTO `datasource` VALUES (3, 0, 'Yên Bái', '2020-04-24 17:17:12', 2, 'server=localhost;User Id=root;password=123456;database=wso2_yenbai;Convert Zero Datetime=True;Character Set=utf8;Persist Security Info=True;port=3306', 291, NULL, 'Quảng Ninh', 'localhost', 1, 'wso2_yenbai', 'root', '123456', 0);

SET FOREIGN_KEY_CHECKS = 1;
