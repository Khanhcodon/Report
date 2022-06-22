-- ----------------------------
-- Function structure for fnGetDepartment
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetDepartment`;

CREATE DEFINER=`root`@`%` FUNCTION `fnGetDepartment`(`userId` int) RETURNS varchar(200) CHARSET utf8
BEGIN
	#Routine body goes here...
	DECLARE departmentName varchar(200);	

	SELECT d.DepartmentName INTO departmentName
	FROM `user` u
	JOIN user_department_jobtitles_position udjp on udjp.UserId = u.UserId
	JOIN department d on d.DepartmentId = udjp.DepartmentId
	WHERE u.UserId = userId
	ORDER BY udjp.IsPrimary DESC
	LIMIT 1;

	RETURN departmentName;
END
