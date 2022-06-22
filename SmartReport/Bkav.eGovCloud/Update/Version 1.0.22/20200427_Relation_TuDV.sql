CREATE TABLE `relation`  (
  `RelationId` int(11) NOT NULL AUTO_INCREMENT,
  `SourceTableId` int(11) NOT NULL,
  `TargetTableId` int(11) NOT NULL,
  `SourceColumn` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `TargetColumn` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `JoinType` int(11) NOT NULL,
  `JoinExpression` varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `TargetName` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `JoinOperators` varchar(500) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL DEFAULT NULL,
  `JoinId` int(11) NULL DEFAULT NULL,
  PRIMARY KEY (`RelationId`) USING BTREE,
  INDEX `SourceTableId`(`SourceTableId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

SET FOREIGN_KEY_CHECKS = 1;

ALTER TABLE `relation`
ADD COLUMN  `JoinOperators` varchar(200) CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL
ADD COLUMN  `JoinId` int(11) NOT NULL DEFAULT 0;