DROP TABLE IF EXISTS `templateKey_category`;
CREATE TABLE `templateKey_category`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(300) CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `IsActive` bit(1) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8 COLLATE = utf8_unicode_ci ROW_FORMAT = Dynamic;

INSERT INTO `templateKey_category` VALUES ('0', 'category 1', 1);
INSERT INTO `templateKey_category` VALUES ('0', 'category 2', 1);

ALTER TABLE `templatekey`
ADD COLUMN  `CategoryId` int(11) NOT NULL DEFAULT '1';

insert into resource VALUES(NULL, "Common.TemplateKey.CreateOrEdit.Fields.Category.Label", "Danh mục");