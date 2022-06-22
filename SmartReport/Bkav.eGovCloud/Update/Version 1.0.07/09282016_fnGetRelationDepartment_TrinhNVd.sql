
-- ----------------------------
-- Function structure for fnGetRelationDeparment
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetRelationDeparment`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `fnGetRelationDeparment`(`documentId` char(36)  CHARACTER SET utf8 COLLATE utf8_unicode_ci) RETURNS varchar(500) CHARSET utf8
BEGIN
	#Routine body goes here...
	DECLARE departmentName varchar(500);	

	SELECT GROUP_CONCAT(DISTINCT fnGetDepartment(u2.UserId) SEPARATOR '; ') INTO departmentName
	FROM `document` d
	JOIN documentcopy dc on dc.DocumentId = d.DocumentId
	JOIN `user` u2 on dc.UserCurrentId = u2.UserId
	WHERE d.DocumentId = documentId
	AND dc.DocumentCopyType != 1;

	RETURN departmentName;

END
;;
DELIMITER ;
