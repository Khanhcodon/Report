CREATE TABLE `datafield`  (
  `DataFieldId` int(11) NOT NULL AUTO_INCREMENT,
  `FieldName` varchar(100) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `Description` varchar(2000) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `Datatype` varchar(22) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `DataTableId` int(11) NOT NULL,
  `FieldType` int(11) NOT NULL,
  `IsActivated` bit(1) NOT NULL,
  PRIMARY KEY (`DataFieldId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11572 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;
