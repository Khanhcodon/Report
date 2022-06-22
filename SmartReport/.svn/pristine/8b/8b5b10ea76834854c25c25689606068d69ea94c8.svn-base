/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : egovtn_tttt

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2017-12-02 08:58:17
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `baocaotonghop-nguoidung`
-- ----------------------------
DROP PROCEDURE IF EXISTS `baocaotonghop-nguoidung`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `baocaotonghop-nguoidung`(IN `@from` datetime,IN `@to` datetime)
BEGIN
	#Routine body goes here...

  DECLARE done INT DEFAULT FALSE;
	DECLARE userName varchar(250);
	DECLARE users CURSOR FOR SELECT CONCAT(u.FullName, "(", u.Username, ")") FROM `user` u WHERE u.IsActivated = 1 order by u.LastName;
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

  DROP TABLE IF EXISTS reportResult;
	CREATE TEMPORARY TABLE reportResult (
		DepartmentName VARCHAR(250) NOT NULL
		, Total int NOT NULL
		, TotalFinish int NOT NULL
		, TotalUnfinish int NOT NULL
		, Overduefinish int NOT NULL
		, OverdueUnfinish int NOT NULL
		, TotalOverdue int NOT NULL
	) ENGINE=MEMORY;

  DROP TABLE IF EXISTS documentTemp;
	CREATE TEMPORARY TABLE documentTemp (
		FullName VARCHAR(250) NOT NULL
		, `Status` int NOT NULL
		, DateFinished datetime null
		, DateAppointed datetime null
	) ENGINE=MEMORY;

	INSERT INTO documentTemp
		(FullName, `Status`, DateFinished, DateAppointed)
	SELECT
		CONCAT(u.FullName, "(", u.Username, ")"),
		dc.`Status`,
		dc.DateFinished,
		d.DateAppointed
	FROM document d
	JOIN documentcopy dc on dc.DocumentId = d.DocumentId
	JOIN `user` u on u.UserId = dc.UserCurrentId
	JOIN user_department_jobtitles_position ud on ud.UserId = dc.UserCurrentId
	join urgent ug on ug.UrgentId = d.UrgentId
	JOIN department dp on dp.DepartmentId = ud.DepartmentId
	Left JOIN category on category.CategoryId = d.CategoryId
	WHERE dc.DocumentCopyType in (1, 2)
		And dc.`Status` not in (1, 8) 
		AND d.CategoryBusinessId != 4
		AND Date(d.DateCreated) >= Date(`@from`)
		AND Date(d.DateCreated) <= Date(`@to`)
		AND d.CategoryId is NOT NULL
	GROUP BY dc.DocumentCopyId;
		
	OPEN users;
	read_loop: LOOP
		FETCH users INTO userName;
		IF done THEN
      LEAVE read_loop;
    END IF;
		SET @total = (SELECT count(1) FROM documentTemp WHERE FullName = userName);
		SET @totalFinish = (SELECT count(1) FROM documentTemp WHERE FullName = userName AND `Status` = 4);
		SET @totalUnfinish = (SELECT count(1) FROM documentTemp WHERE FullName = userName AND `Status` != 4);

		SET @overdueFinished = (SELECT count(1) FROM documentTemp WHERE FullName = userName 
																		AND `Status` = 4 AND DateAppointed is NOT null AND DateFinished is not null AND Date(DateFinished) > Date(DateAppointed));
		SET @overdueUnFinished = (SELECT count(1) FROM documentTemp WHERE FullName = userName 
																		AND `Status` != 4 AND DateAppointed is not null and Date(Now()) > Date(DateAppointed));

		SET @totalOverdue = @overdueFinished + @overdueUnFinished;
		
		INSERT INTO reportResult(DepartmentName, Total, TotalFinish, TotalUnfinish, Overduefinish, OverdueUnfinish, TotalOverdue)
		VALUES(userName, @total, @totalFinish, @totalUnfinish, @overdueFinished, @overdueUnFinished, @totalOverdue);
	END LOOP;

	SELECT * from reportResult;
	DROP TABLE documentTemp; 
	DROP TABLE reportResult; 
	CLOSE users;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `baocaotonghop-phongban`
-- ----------------------------
DROP PROCEDURE IF EXISTS `baocaotonghop-phongban`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `baocaotonghop-phongban`(IN `@from` datetime,IN `@to` datetime)
BEGIN
	#Routine body goes here...

  DECLARE done INT DEFAULT FALSE;
	DECLARE deptName VARCHAR(250);
	DECLARE deptPaths CURSOR FOR SELECT department.DepartmentPath FROM department;
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

  DROP TABLE IF EXISTS reportResult;
	CREATE TEMPORARY TABLE reportResult (
		DepartmentName VARCHAR(250) NOT NULL
		, Total int NOT NULL
		, TotalFinish int NOT NULL
		, TotalUnfinish int NOT NULL
		, Overduefinish int NOT NULL
		, OverdueUnfinish int NOT NULL
		, TotalOverdue int NOT NULL
	) ENGINE=MEMORY;

  DROP TABLE IF EXISTS documentTemp;
	CREATE TEMPORARY TABLE documentTemp (
		DepartmentName VARCHAR(250) NOT NULL
		, `Status` int NOT NULL
		, DateFinished datetime null
		, DateAppointed datetime null
	) ENGINE=MEMORY;

	INSERT INTO documentTemp
		(DepartmentName, `Status`, DateFinished, DateAppointed)
	SELECT
		dp.DepartmentPath,
		dc.`Status`,
		dc.DateFinished,
		d.DateAppointed
	FROM document d
	JOIN documentcopy dc on dc.DocumentId = d.DocumentId
	JOIN `user` u on u.UserId = dc.UserCurrentId
	JOIN user_department_jobtitles_position ud on ud.UserId = dc.UserCurrentId
	join urgent ug on ug.UrgentId = d.UrgentId
	JOIN department dp on dp.DepartmentId = ud.DepartmentId
	Left JOIN category on category.CategoryId = d.CategoryId
	WHERE dc.DocumentCopyType in (1, 2)
		And dc.`Status` not in (1, 8) 
		AND d.CategoryBusinessId != 4
		AND Date(d.DateCreated) >= Date(`@from`)
		AND Date(d.DateCreated) <= Date(`@to`)
		AND d.CategoryId is NOT NULL
	GROUP BY dc.DocumentCopyId;
		
	OPEN deptPaths;
	read_loop: LOOP
		FETCH deptPaths INTO deptName;
		IF done THEN
      LEAVE read_loop;
    END IF;
		SET @total = (SELECT count(1) FROM documentTemp WHERE DepartmentName = deptName);
		SET @totalFinish = (SELECT count(1) FROM documentTemp WHERE DepartmentName = deptName AND `Status` = 4);
		SET @totalUnfinish = (SELECT count(1) FROM documentTemp WHERE DepartmentName = deptName AND `Status` != 4);

		SET @overdueFinished = (SELECT count(1) FROM documentTemp WHERE DepartmentName = deptName 
																		AND `Status` = 4 AND DateAppointed is NOT null AND DateFinished is not null AND Date(DateFinished) > Date(DateAppointed));
		SET @overdueUnFinished = (SELECT count(1) FROM documentTemp WHERE DepartmentName = deptName 
																		AND `Status` != 4 AND DateAppointed is not null and Date(Now()) > Date(DateAppointed));

		SET @totalOverdue = @overdueFinished + @overdueUnFinished;
		
		INSERT INTO reportResult(DepartmentName, Total, TotalFinish, TotalUnfinish, Overduefinish, OverdueUnfinish, TotalOverdue)
		VALUES(deptName, @total, @totalFinish, @totalUnfinish, @overdueFinished, @overdueUnFinished, @totalOverdue);
	END LOOP;

	SELECT * from reportResult;
	DROP TABLE documentTemp; 
	DROP TABLE reportResult; 
	CLOSE deptPaths;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for `DateCompare`
-- ----------------------------
DROP FUNCTION IF EXISTS `DateCompare`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `DateCompare`(`date1` datetime, `date2` datetime, `format` varchar(100)) RETURNS bit(1)
BEGIN
	#Routine body goes here...
	DECLARE `fromStr` varchar(100);
	DECLARE `toStr` varchar(100);	

  IF(`format` is NULL)
	THEN
		SET `format` = '%Y%m%d';
	END IF;

	SET fromStr = DATE_FORMAT(date1, `format`);
	SET toStr = DATE_FORMAT(date2, `format`);
	if `fromStr` <= `toStr`
	THEN
		RETURN 0;
	ELSE
		RETURN 1;
	end IF;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for `GetCategoryBusiness`
-- ----------------------------
DROP FUNCTION IF EXISTS `GetCategoryBusiness`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `GetCategoryBusiness`(`CategoryBusinessId` int) RETURNS varchar(50) CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	
	DECLARE `result` varchar(50);	
	CASE `CategoryBusinessId`
    WHEN 1 THEN 
			SET result = "Văn bản đến";
    WHEN 2 THEN
			SET result = "Văn bản đi";
		WHEN 4 THEN
			SET result = "Hồ sơ một cửa";
		ELSE
			SET result = "";
	END CASE;

	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for `GetStatus`
-- ----------------------------
DROP FUNCTION IF EXISTS `GetStatus`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `GetStatus`(`Status` int) RETURNS varchar(50) CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	
	DECLARE `result` varchar(50);	
	CASE `Status`
    WHEN 1 THEN 
			SET result = "Dự thảo";
    WHEN 2 THEN
			SET result = "Đang xử lý";
		WHEN 4 THEN
			SET result = "Đã kết thúc";
		WHEN 8 THEN
			SET result = "Đã hủy";
		WHEN 16 THEN
			SET result = "Đang dừng xử lý";
		ELSE
			SET result = "";
	END CASE;

	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for `OverdueStatus`
-- ----------------------------
DROP FUNCTION IF EXISTS `OverdueStatus`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `OverdueStatus`(`Status` int, `DateAppointed` datetime, `DateSuccess` datetime, `DateReturned` datetime, `DateFinished` datetime, `DateRequireSupplementary` datetime, `DateCompare` datetime) RETURNS int(11)
BEGIN
	-- Trả về trạng thái Đúng hạn, chưa đến hạn, tới hạn, quá hạn, trễ hẹn
	-- 1 - Đúng hạn: đã xử lý đúng hạn
  -- 2 - Chưa đến hạn: chưa xử lý + chưa đến hạn.
  -- 3 - Quá hạn: Chưa xử lý + quá hạn.
  -- 4 - Trễ hẹn: Đã xử lý + quá hạn
	DECLARE `result` int;	

	if(`Status` = 16 AND `DateRequireSupplementary`  is NULL AND DateSuccess is NOT NULL) THEN
		SET `DateRequireSupplementary` = DateSuccess;
	END IF;

	if (DateCompare is NULL OR DateCompare >= NOW()) THEN
		SET DateCompare = NOW();
	end IF;

	if (DateSuccess is NOT NULL AND DateSuccess > DateCompare) THEN
		SET DateSuccess = NULL;
	END IF;

	if (DateReturned is NOT NULL AND DateReturned > DateCompare) THEN
		SET DateReturned = NULL;
	END IF;
	
	if (DateFinished is NOT NULL AND DateFinished > DateCompare) THEN
		SET DateFinished = NULL;
		SET `Status` = 2;
	END IF;

	if (`DateRequireSupplementary`  is NOT NULL AND `DateRequireSupplementary`  > DateCompare) THEN
		SET `DateRequireSupplementary`  = NULL;
		SET `Status` = 2;
	END IF;

	IF `DateSuccess` IS NOT NULL THEN -- Đã duyệt  
		IF DateCompare(DateSuccess, `DateAppointed`, null) = 0 THEN
			SET result = 1; -- Đúng hạn
		ELSE
			SET Result = 4; -- Trễ hẹn
		END IF;
	ELSEIF `DateReturned` is NOT NULL THEN -- Đã trả kết quả
		IF DateCompare(DateReturned, `DateAppointed`, null) = 0 THEN
			SET result = 1; -- Đúng hạn
		ELSE
			SET result = 4; -- Trễ hẹn
		END IF;
	ELSEIF `Status` = 4 THEN -- Đã kết thúc
		IF DateCompare(`DateFinished`, `DateAppointed`, null) = 0 THEN
			SET result = 1; -- Đúng hạn
		ELSE
			SET result = 4; -- Trễ hẹn
		END IF;
	ELSEIF `Status` = 16 THEN
		IF DateCompare(`DateRequireSupplementary`, `DateAppointed`, null) = 0 THEN
			SET result = 2; -- Chưa đến hạn
		ELSE
			SET result = 3; -- Trễ hẹn
		END IF;
	ELSE
		IF (DateAppointed is NULL OR DateCompare(DateCompare, `DateAppointed`, NULL) = 0) THEN
			SET result = 2; -- Chưa đến hạn
		ELSE
			SET result = 3; -- Quá hạn
		END IF;
	END IF;

	RETURN result;
END
;;
DELIMITER ;
