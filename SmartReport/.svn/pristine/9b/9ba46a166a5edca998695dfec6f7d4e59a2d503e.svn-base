/*
Navicat MySQL Data Transfer

Source Server         : 10.2.78.125
Source Server Version : 50521
Source Host           : 10.2.78.125:3306
Source Database       : egovcloud_lastest

Target Server Type    : MYSQL
Target Server Version : 50521
File Encoding         : 65001

Date: 2016-02-26 14:47:33
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for baocaotonghop
-- ----------------------------
DROP PROCEDURE IF EXISTS `baocaotonghop`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `baocaotonghop`(IN `@from` datetime,
  IN `@to` datetime)
BEGIN
	DECLARE done INT DEFAULT FALSE;
	DECLARE groupName VARCHAR(300);
	DECLARE groupValue CHAR(36);

	DECLARE doctypeCur CURSOR FOR SELECT doctype.DocTypeId, doctype.DocTypeName from doctype where doctype.IsActivated = 1;
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	

	DROP TEMPORARY TABLE IF EXISTS `DocumentTemp`;
	DROP TEMPORARY TABLE IF EXISTS `ResultTemp`;
	CREATE TEMPORARY TABLE `DocumentTemp` (
		`Id` int(11) NOT NULL AUTO_INCREMENT,
		`DocumentId` char(36) NOT NULL,
		`DocTypeId` char(36) NULL,
		`DateCreated` datetime NOT NULL,
		`DateAppointed` datetime,
		`DateFinished` datetime,
		`Status` varchar(50),
		 PRIMARY KEY (`Id`)
	);
	CREATE TEMPORARY TABLE `ResultTemp` (
		`Stt` int(11) NOT NULL AUTO_INCREMENT,
		`LoaiHoSo` varchar(300) NULL,
		`TonKyTruoc` INT(11) NULL,
		`NhanTrongKy` INT(11) NULL,
		`Tong` INT(11) NULL,
		`DaGiaiQuyet` INT(11) NULL,
		`DungHen` INT(11) NULL,
		`TiLeDungHen` INT(11) NULL ,
		`TreHen` INT(11) NULL,
		`TiLeTreHen` INT(11) NULL,
		`ChuaGiaiQuyet` INT(11) NULL,
		`TrongHan` INT(11) NULL,
		`TiLeTrongHan` INT(11) NULL,
		`QuaHan` INT(11) NULL,
		`TiLeQuaHan` INT(11) NULL,
		`GhiChu` VARCHAR(300) NULL DEFAULT '',
		PRIMARY KEY (`Stt`)
	);
	
	INSERT INTO `DocumentTemp`(`DocumentId`,`DocTypeId`,`DateCreated`,`DateAppointed`,`DateFinished`,`Status`)
	Select 
		d.DocumentId, 
		dt.DocTypeId,
		d.DateCreated, 
		d.DateAppointed,
		d.DateFinished,
		fnGetDocumentStatus(d.`Status`) AS `Status`
	FROM document AS d
  INNER JOIN documentcopy as dc ON dc.DocumentId = d.DocumentId and dc.`Status` NOT IN (1, 8)
	INNER JOIN doctype AS dt ON dt.DocTypeId = d.DocTypeId
	WHERE d.`Status` NOT IN (1, 8)
	AND ((dc.DateCreated >= `@from` AND dc.DateCreated <= `@to`)
				|| (d.DateCreated < `@from` AND d.`Status` != 4));
	
	OPEN doctypeCur;
	read_loop: LOOP
    FETCH doctypeCur INTO groupValue, groupName;
		IF done THEN
			LEAVE read_loop;
		END IF;
		
		SET @doctype = groupName;

		# Tồn kỳ trước
		SET @TonKyTruoc = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated < `@from`) AND d.`Status` != 4 AND d.DocTypeId = groupValue);

		# Nhận Trong kỳ
		SET @NhanTrongKy = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated >= `@from`) AND d.DateCreated <= `@to` AND d.DocTypeId = groupValue);

		# Tổng văn bản xử lý
		SET @Tong = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.DocTypeId = groupValue);
		# Đã giải quyết
		SET @DaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` = 4 AND d.DocTypeId = groupValue);
		# Đúng hẹn
		SET @DungHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) AND (d.DateFinished <= d.DateAppointed) AND d.DocTypeId = groupValue);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeDungHen = 0; ELSE SET @TiLeDungHen = (@DungHen*100/@DaGiaiQuyet); END IF;
		# Trễ hẹn
		SET @TreHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) AND (d.DateFinished > d.DateAppointed) AND d.DocTypeId = groupValue);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeTreHen = 0; ELSE SET @TiLeTreHen = 100 - @TiLeDungHen; END IF;

# Chưa giải quyết
		SET @ChuaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` != 4 AND d.DocTypeId = groupValue);
		# Trong hạn
		SET @TrongHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) AND (d.DateAppointed IS NULL || d.DateAppointed <= NOW()) AND d.DocTypeId = groupValue);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeTrongHan = 0; ELSE SET @TiLeTrongHan = (@TrongHan*100/@ChuaGiaiQuyet); END IF;		
		# Quá hạn
		SET @QuaHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) AND (d.DateAppointed > NOW()) AND d.DocTypeId = groupValue);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeQuaHan = 0; ELSE SET @TiLeQuaHan = 100 - @TiLeTrongHan; END IF;	

		IF @ChuaGiaiQuyet > 0 THEN SET @GhiChu = 'Chuyển kỳ sau'; ELSE SET @GhiChu = ''; END IF;

		INSERT INTO ResultTemp(
				`TonKyTruoc`, `LoaiHoSo`, `NhanTrongKy`, `Tong`, 
				`DaGiaiQuyet`, `DungHen`, `TreHen`, `TiLeDungHen`, `TiLeTreHen`,
				`ChuaGiaiQuyet`, `TrongHan`, `QuaHan`, `TiLeTrongHan`, `TiLeQuaHan`, `GhiChu`)
		VALUES(
				@TonKyTruoc, @doctype, @NhanTrongKy, @Tong, 
				@DaGiaiQuyet, @DungHen, @TreHen, @TiLeDungHen, @TiLeTreHen,
				@ChuaGiaiQuyet, @TrongHan, @QuaHan, @TiLeTrongHan, @TiLeQuaHan, @GhiChu);

	END LOOP read_loop;
	CLOSE doctypeCur;

	SELECT * FROM ResultTemp;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for baocaotonghop_dept
-- ----------------------------
DROP PROCEDURE IF EXISTS `baocaotonghop_dept`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `baocaotonghop_dept`(IN `@from` datetime,
  IN `@to` datetime, IN `@treeGroupValue` int(4))
BEGIN
	DECLARE done INT DEFAULT FALSE;
	DECLARE deptId int(3);
	DECLARE deptExt VARCHAR(200);
	DECLARE deptName varchar(300);
	DECLARE departmentExt varchar(100);
	DECLARE deptCur CURSOR FOR 
			SELECT d.DepartmentId, d.DepartmentIdExt, d.DepartmentPath
			FROM department d INNER JOIN user_department_jobtitles_position udjp on udjp.DepartmentId=d.DepartmentId
			where d.IsActivated = 1  AND  udjp.IsPrimary = 1
				AND ((`@treeGroupValue` IS NULL AND d.ParentId = 1) 
				OR (d.ParentId = `@treeGroupValue`) or (d.DepartmentId = `@treeGroupValue`));
	
  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DROP TEMPORARY TABLE IF EXISTS `DocumentTemp`;
	DROP TEMPORARY TABLE IF EXISTS `ResultTemp`;
	CREATE TEMPORARY TABLE `DocumentTemp` (
		`Id` INT(11) NOT NULL AUTO_INCREMENT,
		`DocumentId` char(36) NOT NULL,
		`DepartmentId` INT(11) NULL,
		`DepartmentExt` VARCHAR(300) NULL,
		`DateCreated` datetime NOT NULL,
		`DateAppointed` datetime,
		`DateFinished` datetime,
		`Status` varchar(50),
		-- `Level` INT(4),
		 PRIMARY KEY (`Id`)
	);

	CREATE TEMPORARY TABLE `ResultTemp` (
		`Stt` int(11) NOT NULL AUTO_INCREMENT,
		`LoaiHoSo` varchar(300) NULL,
		`TonKyTruoc` INT(11) NULL,
		`NhanTrongKy` INT(11) NULL,
		`Tong` INT(11) NULL,
		`DaGiaiQuyet` INT(11) NULL,
		`DungHen` INT(11) NULL,
		`TiLeDungHen` INT(11) NULL ,
		`TreHen` INT(11) NULL,
		`TiLeTreHen` INT(11) NULL,
		`ChuaGiaiQuyet` INT(11) NULL,
		`TrongHan` INT(11) NULL,
		`TiLeTrongHan` INT(11) NULL,
		`QuaHan` INT(11) NULL,
		`TiLeQuaHan` INT(11) NULL,
		`GhiChu` VARCHAR(300) NULL DEFAULT '',
		PRIMARY KEY (`Stt`)
	);
	
	SET departmentExt = (SELECT d.DepartmentIdExt from department d 
											where ((`@treeGroupValue` IS NULL AND d.DepartmentId = 1)
											OR d.DepartmentId = `@treeGroupValue`));
	
  INSERT INTO `DocumentTemp`(DocumentId, DepartmentId, DepartmentExt, DateCreated, DateAppointed, DateFinished, `Status`)-- , `Level`)
	Select 
		d.DocumentId, 
		udf.DepartmentId,
		udf.DepartmentIdExt,
		d.DateCreated, 
		d.DateAppointed, 
		d.DateFinished,
		fnGetDocumentStatus(d.`Status`) AS `Status`
	-- , MIN(dept.`Level`)
	FROM document AS d
	INNER JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId AND dc.`Status` NOT IN (1, 8)
	INNER JOIN user_department_jobtitles_position AS udf ON udf.UserId = dc.UserCurrentId
	-- INNER JOIN department AS dept ON dept.DepartmentId = udf.DepartmentId
	WHERE d.`Status` NOT IN (1, 8)
	AND ((dc.DateCreated >= `@from` AND dc.DateCreated <= `@to`)
				|| (d.DateCreated < `@from` AND d.`Status` != 4))
	-- AND dc.DocumentCopyType IN (1, 2, 64)
	AND (udf.DepartmentIdExt collate utf8_general_ci = departmentExt 
			or udf.DepartmentIdExt collate utf8_general_ci LIKE CONCAT(departmentExt,'.%'));
  -- GROUP BY d.DocumentId;

	OPEN deptCur;
	read_loop: LOOP
    FETCH deptCur INTO deptId, deptExt, deptName;
		IF done THEN
			LEAVE read_loop;
		END IF;
		
		SET @doctype = deptName;

		# Tồn kỳ trước
		SET @TonKyTruoc = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated < `@from`) AND d.`Status` != 4
					AND ( d.DepartmentExt = deptExt or d.DepartmentExt LIKE CONCAT(deptExt, '.%')));

		# Nhận Trong kỳ
		SET @NhanTrongKy = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated >= `@from`) AND d.DateCreated <= `@to` 
					AND (d.DepartmentExt = deptExt or d.DepartmentExt LIKE CONCAT(deptExt, '.%')));

		# Tổng văn bản xử lý
		SET @Tong = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.DepartmentExt = deptExt or d.DepartmentExt LIKE CONCAT(deptExt, '.%'));
		# Đã giải quyết
		SET @DaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` = 4
												AND (d.DepartmentExt = deptExt or d.DepartmentExt LIKE CONCAT(deptExt, '.%')));
		# Đúng hẹn
		SET @DungHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) 
										AND (d.DateFinished <= d.DateAppointed) 
										AND(d.DepartmentExt = deptExt or  d.DepartmentExt LIKE CONCAT(deptExt, '.%')));

		IF @DaGiaiQuyet = 0 
		THEN 
				SET @TiLeDungHen = 0;
		 ELSE 
				SET @TiLeDungHen = (@DungHen*100/@DaGiaiQuyet); 
		END IF;

		# Trễ hẹn
		SET @TreHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) 
										AND (d.DateFinished > d.DateAppointed) 
										AND (d.DepartmentExt = deptExt or d.DepartmentExt LIKE CONCAT(deptExt, '.%')));

		IF @DaGiaiQuyet = 0 
			THEN 
				SET @TiLeTreHen = 0; 
			ELSE 
				SET @TiLeTreHen = 100 - @TiLeDungHen;
    END IF;

   # Chưa giải quyết
		SET @ChuaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` != 4
													AND (d.DepartmentExt = deptExt or d.DepartmentExt LIKE CONCAT(deptExt, '.%')));
		# Trong hạn
		SET @TrongHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4)
										 AND (d.DateAppointed IS NULL || d.DateAppointed > NOW()) 
									  AND (d.DepartmentExt = deptExt or d.DepartmentExt LIKE CONCAT(deptExt, '.%')));
		IF @ChuaGiaiQuyet = 0
			 THEN 
			   SET @TiLeTrongHan = 0;
			 ELSE 
				SET @TiLeTrongHan = (@TrongHan*100/@ChuaGiaiQuyet);
		END IF;		

		# Quá hạn
		SET @QuaHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) 
										AND (d.DateAppointed <= NOW())
										AND (d.DepartmentExt = deptExt or d.DepartmentExt LIKE CONCAT(deptExt, '.%')));
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeQuaHan = 0; ELSE SET @TiLeQuaHan = 100 - @TiLeTrongHan; END IF;	

		IF @ChuaGiaiQuyet > 0 THEN SET @GhiChu = 'Chuyển kỳ sau'; ELSE SET @GhiChu = ''; END IF;

		INSERT INTO ResultTemp(
				`TonKyTruoc`, `LoaiHoSo`, `NhanTrongKy`, `Tong`, 
				`DaGiaiQuyet`, `DungHen`, `TreHen`, `TiLeDungHen`, `TiLeTreHen`,
				`ChuaGiaiQuyet`, `TrongHan`, `QuaHan`, `TiLeTrongHan`, `TiLeQuaHan`, `GhiChu`)
		VALUES(
				@TonKyTruoc, @doctype, @NhanTrongKy, @Tong, 
				@DaGiaiQuyet, @DungHen, @TreHen, @TiLeDungHen, @TiLeTreHen,
				@ChuaGiaiQuyet, @TrongHan, @QuaHan, @TiLeTrongHan, @TiLeQuaHan, @GhiChu);

	END LOOP read_loop;
	CLOSE deptCur;

	SELECT * FROM ResultTemp;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for baocaotonghop_dept_user
-- ----------------------------
DROP PROCEDURE IF EXISTS `baocaotonghop_dept_user`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `baocaotonghop_dept_user`(IN `@from` datetime,
  IN `@to` datetime, IN `@treeGroupValue` int(4),IN `@userId` int(4))
BEGIN
	DECLARE done INT DEFAULT FALSE;
	DECLARE UserId int(3);
	DECLARE UserName varchar(50);
	DECLARE DeptExt varchar(250);

	DECLARE DeptUserCursor CURSOR FOR
					(select d.DepartmentIdExt
						from department d 
						INNER JOIN user_department_jobtitles_position udjp 
						on udjp.DepartmentId = d.DepartmentId
						where d.DepartmentId = `@treeGroupValue` 
						AND udjp.UserId = `@userId`
						and udjp.IsPrimary = 1);

	DECLARE userCur CURSOR FOR 
			SELECT DISTINCT u.UserId,u.FullName
			FROM `user` u
			INNER JOIN `user_department_jobtitles_position` AS udf ON u.UserId = udf.UserId
			where u.IsActivated = 1 
			and udf.DepartmentIdExt collate utf8_general_ci like CONCAT( '%',`@treeGroupValue`,'%') ;

  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
 
	DROP TEMPORARY TABLE IF EXISTS `DocumentTemp`;
	DROP TEMPORARY TABLE IF EXISTS `ResultTemp`;
	CREATE TEMPORARY TABLE `DocumentTemp` (
		`Id` INT(11) NOT NULL AUTO_INCREMENT,
		`DocumentId` char(36) NOT NULL,
		`UserId` INT(11) NULL,
		`DateCreated` datetime NOT NULL,
		`DateAppointed` datetime,
		`DateFinished` datetime,
		`Status` int,
		 PRIMARY KEY (`Id`)
	);

	CREATE TEMPORARY TABLE `ResultTemp` (
		`Stt` int(11) NOT NULL AUTO_INCREMENT,
		`LoaiHoSo` varchar(300) NULL,
		`TonKyTruoc` INT(11) NULL,
		`NhanTrongKy` INT(11) NULL,
		`Tong` INT(11) NULL,
		`DaGiaiQuyet` INT(11) NULL,
		`DungHen` INT(11) NULL,
		`TiLeDungHen` INT(11) NULL ,
		`TreHen` INT(11) NULL,
		`TiLeTreHen` INT(11) NULL,
		`ChuaGiaiQuyet` INT(11) NULL,
		`TrongHan` INT(11) NULL,
		`TiLeTrongHan` INT(11) NULL,
		`QuaHan` INT(11) NULL,
		`TiLeQuaHan` INT(11) NULL,
		`GhiChu` VARCHAR(300) NULL DEFAULT '',
		PRIMARY KEY (`Stt`)
	);
	
	INSERT INTO `DocumentTemp`(DocumentId, UserId, DateCreated, DateAppointed, DateFinished, `Status`)
	Select 
		d.DocumentId, 
		u.UserId,
		d.DateCreated, 
		d.DateAppointed, 
		d.DateFinished,
		d.`Status`
	FROM document AS d
	INNER JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId AND dc.`Status` NOT IN (1, 8)
	INNER JOIN `user` AS u ON u.UserId = dc.UserCurrentId
	WHERE d.`Status` NOT IN (1, 8)
	AND ((dc.DateCreated >= `@from` AND dc.DateCreated <= `@to`)
				|| (d.DateCreated < `@from` AND d.`Status` != 4));
	-- AND dc.DocumentCopyType IN (1, 2, 64);

Open DeptUserCursor;
 deptuser_loop:LOOP
 FETCH DeptUserCursor INTO DeptExt;
		IF done THEN
			LEAVE deptuser_loop;
		END IF;		

	OPEN userCur;
	read_loop: LOOP
    FETCH userCur INTO userId, userName;
		IF done THEN
			LEAVE read_loop;
		END IF;

		if NOT EXISTS (select udjp.UserId FROM user_department_jobtitles_position udjp
									WHERE udjp.UserId = userId 
									AND(udjp.DepartmentIdExt collate utf8_general_ci = DeptExt 
									or udjp.DepartmentIdExt collate utf8_general_ci like CONCAT(DeptExt,'.%')))
    THEN			
				LEAVE read_loop;
		END if;
		
		SET @doctype = userName;

		# Tồn kỳ trước
		SET @TonKyTruoc = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated < `@from`) AND d.`Status` != 4 AND d.UserId = userId);

		# Nhận Trong kỳ
		SET @NhanTrongKy = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated >= `@from`) AND d.DateCreated <= `@to` AND d.UserId = userId);

		# Tổng văn bản xử lý
		SET @Tong = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.UserId = userId);
		# Đã giải quyết
		SET @DaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` = 4 AND d.UserId = userId);
		# Đúng hẹn
		SET @DungHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4)
										AND (d.DateFinished <= d.DateAppointed) AND d.UserId = userId);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeDungHen = 0; ELSE SET @TiLeDungHen = (@DungHen*100/@DaGiaiQuyet); END IF;
		# Trễ hẹn
		SET @TreHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4)
									AND (d.DateFinished > d.DateAppointed) AND d.UserId = userId);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeTreHen = 0; ELSE SET @TiLeTreHen = 100 - @TiLeDungHen; END IF;

# Chưa giải quyết
		SET @ChuaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` != 4 AND d.UserId = userId);
		# Trong hạn
		SET @TrongHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4)
										 AND (d.DateAppointed IS NULL || d.DateAppointed > NOW()) 
										AND d.UserId = userId);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeTrongHan = 0; ELSE SET @TiLeTrongHan = (@TrongHan*100/@ChuaGiaiQuyet); END IF;		
		# Quá hạn
		SET @QuaHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) 
									AND (d.DateAppointed <= NOW()) 
									AND d.UserId = userId);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeQuaHan = 0; ELSE SET @TiLeQuaHan = 100 - @TiLeTrongHan; END IF;	

		IF @ChuaGiaiQuyet > 0 THEN SET @GhiChu = 'Chuyển kỳ sau'; ELSE SET @GhiChu = ''; END IF;

		INSERT INTO ResultTemp(
				`TonKyTruoc`, `LoaiHoSo`, `NhanTrongKy`, `Tong`, 
				`DaGiaiQuyet`, `DungHen`, `TreHen`, `TiLeDungHen`, `TiLeTreHen`,
				`ChuaGiaiQuyet`, `TrongHan`, `QuaHan`, `TiLeTrongHan`, `TiLeQuaHan`, `GhiChu`)
		VALUES(
				@TonKyTruoc, @doctype, @NhanTrongKy, @Tong, 
				@DaGiaiQuyet, @DungHen, @TreHen, @TiLeDungHen, @TiLeTreHen,
				@ChuaGiaiQuyet, @TrongHan, @QuaHan, @TiLeTrongHan, @TiLeQuaHan, @GhiChu);

	END LOOP read_loop;
	CLOSE userCur;

END LOOP deptuser_loop;
CLOSE DeptUserCursor;

	SELECT DISTINCT `Stt`,`LoaiHoSo`,`TonKyTruoc`,`NhanTrongKy`,`Tong`,`DaGiaiQuyet`,
		`DungHen`,`TiLeDungHen`,`TreHen`,`TiLeTreHen`,`ChuaGiaiQuyet`,`TrongHan`,
		`TiLeTrongHan`,`QuaHan`,`TiLeQuaHan`,	`GhiChu` FROM ResultTemp;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for Baocaotonghop_sotheodoihoso
-- ----------------------------
DROP PROCEDURE IF EXISTS `Baocaotonghop_sotheodoihoso`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `Baocaotonghop_sotheodoihoso`(IN `@fromDate` datetime, In `@toDate` datetime)
BEGIN
	SELECT * FROM document;
	-- WHERE DateCreated >= @formDate
	-- 	AND DateCreated <= @toDate;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for baocaotonghop_user
-- ----------------------------
DROP PROCEDURE IF EXISTS `baocaotonghop_user`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `baocaotonghop_user`(IN `@from` datetime,
  IN `@to` datetime, IN `@treeGroupValue` int(4))
BEGIN
	DECLARE done INT DEFAULT FALSE;
	DECLARE UserId int(3);
	DECLARE UserName varchar(50);

	DECLARE userCur CURSOR FOR 
			SELECT DISTINCT `user`.UserId, `user`.FullName
			FROM `user`
			INNER JOIN `user_department_jobtitles_position` AS udf
			ON `user`.UserId = udf.UserId
			where `user`.UserId=`@treeGroupValue`;
	
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DROP TEMPORARY TABLE IF EXISTS `DocumentTemp`;
	DROP TEMPORARY TABLE IF EXISTS `ResultTemp`;
	CREATE TEMPORARY TABLE `DocumentTemp` (
		`Id` INT(11) NOT NULL AUTO_INCREMENT,
		`DocumentId` char(36) NOT NULL,
		`UserId` INT(11) NULL,
		`DateCreated` datetime NOT NULL,
		`DateAppointed` datetime,
		`DateFinished` datetime,
		`Status` int,
		 PRIMARY KEY (`Id`)
	);

	CREATE TEMPORARY TABLE `ResultTemp` (
		`Stt` int(11) NOT NULL AUTO_INCREMENT,
		`LoaiHoSo` varchar(300) NULL,
		`TonKyTruoc` INT(11) NULL,
		`NhanTrongKy` INT(11) NULL,
		`Tong` INT(11) NULL,
		`DaGiaiQuyet` INT(11) NULL,
		`DungHen` INT(11) NULL,
		`TiLeDungHen` INT(11) NULL ,
		`TreHen` INT(11) NULL,
		`TiLeTreHen` INT(11) NULL,
		`ChuaGiaiQuyet` INT(11) NULL,
		`TrongHan` INT(11) NULL,
		`TiLeTrongHan` INT(11) NULL,
		`QuaHan` INT(11) NULL,
		`TiLeQuaHan` INT(11) NULL,
		`GhiChu` VARCHAR(300) NULL DEFAULT '',
		PRIMARY KEY (`Stt`)
	);
	
	INSERT INTO `DocumentTemp`(DocumentId, UserId, DateCreated, DateAppointed, DateFinished, `Status`)
	Select d.DocumentId, u.UserId, d.DateCreated, d.DateAppointed, d.DateFinished, d.`Status`
	FROM document AS d
	INNER JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId AND dc.`Status` NOT IN (1, 8)
	INNER JOIN `user` AS u ON u.UserId = dc.UserCurrentId and u.IsActivated = 1
	WHERE d.`Status` NOT IN (1, 8)
	AND ((dc.DateCreated >= `@from` AND dc.DateCreated <= `@to`)
				|| (d.DateCreated < `@from` AND d.`Status` != 4));
	 -- AND dc.DocumentCopyType IN (1, 2, 64);

	OPEN userCur;
	read_loop: LOOP
    FETCH userCur INTO  UserId, userName;
		IF done THEN
			LEAVE read_loop;
		END IF;
		
		SET @doctype = userName;

		# Tồn kỳ trước
		SET @TonKyTruoc = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated < `@from`) AND d.`Status` != 4 AND d.UserId = `@treeGroupValue`);

		# Nhận Trong kỳ
		SET @NhanTrongKy = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated >= `@from`) AND d.DateCreated <= `@to` AND d.UserId = `@treeGroupValue`);

		# Tổng văn bản xử lý
		SET @Tong = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.UserId = `@treeGroupValue`);
		# Đã giải quyết
		SET @DaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` = 4 AND d.UserId = `@treeGroupValue`);
		# Đúng hẹn
		SET @DungHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) 
										AND (d.DateFinished <= d.DateAppointed) AND d.UserId = `@treeGroupValue`);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeDungHen = 0; ELSE SET @TiLeDungHen = (@DungHen*100/@DaGiaiQuyet); END IF;
		# Trễ hẹn
		SET @TreHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4)
									AND (d.DateFinished > d.DateAppointed) AND d.UserId = `@treeGroupValue`);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeTreHen = 0; ELSE SET @TiLeTreHen = 100 - @TiLeDungHen; END IF;

# Chưa giải quyết
		SET @ChuaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` != 4 AND d.UserId = `@treeGroupValue`);
		# Trong hạn
		SET @TrongHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) 
											AND (d.DateAppointed IS NULL || d.DateAppointed > NOW()) AND d.UserId = `@treeGroupValue`);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeTrongHan = 0; ELSE SET @TiLeTrongHan = (@TrongHan*100/@ChuaGiaiQuyet); END IF;		
		# Quá hạn
		SET @QuaHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) 
										AND (d.DateAppointed <= NOW()) AND d.UserId = `@treeGroupValue`);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeQuaHan = 0; ELSE SET @TiLeQuaHan = 100 - @TiLeTrongHan; END IF;	

		IF @ChuaGiaiQuyet > 0 THEN SET @GhiChu = 'Chuyển kỳ sau'; ELSE SET @GhiChu = ''; END IF;

		INSERT INTO ResultTemp(
				`TonKyTruoc`, `LoaiHoSo`, `NhanTrongKy`, `Tong`, 
				`DaGiaiQuyet`, `DungHen`, `TreHen`, `TiLeDungHen`, `TiLeTreHen`,
				`ChuaGiaiQuyet`, `TrongHan`, `QuaHan`, `TiLeTrongHan`, `TiLeQuaHan`, `GhiChu`)
		VALUES(
				@TonKyTruoc, @doctype, @NhanTrongKy, @Tong, 
				@DaGiaiQuyet, @DungHen, @TreHen, @TiLeDungHen, @TiLeTreHen,
				@ChuaGiaiQuyet, @TrongHan, @QuaHan, @TiLeTrongHan, @TiLeQuaHan, @GhiChu);

	END LOOP read_loop;
	CLOSE userCur;

	SELECT * FROM ResultTemp;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for baocaotonghop_user_dept_copy
-- ----------------------------
DROP PROCEDURE IF EXISTS `baocaotonghop_user_dept_copy`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `baocaotonghop_user_dept_copy`(IN `@from` datetime,
  IN `@to` datetime, IN `@treeGroupValue` int(4),IN `@userId` int(4))
BEGIN
	DECLARE done INT DEFAULT FALSE;
	DECLARE UserId int(3);
	DECLARE UserName varchar(50);
	DECLARE Dept varchar(100) DEFAULT 
					(select d.DepartmentIdExt from department d INNER JOIN user_department_jobtitles_position udjp on udjp.DepartmentId = d.DepartmentId
					where d.DepartmentId = `@treeGroupValue` AND udjp.UserId = `@userId` and udjp.IsPrimary = 1);

	DECLARE userCur CURSOR FOR 
			SELECT DISTINCT `user`.UserId, `user`.FullName
			FROM `user`
			INNER JOIN `user_department_jobtitles_position` AS udf ON `user`.UserId = udf.UserId
			where `user`.IsActivated = 1  
        and udf.DepartmentIdExt collate utf8_general_ci LIKE (select CONCAT(d.DepartmentIdExt,'%') from department d 
				INNER JOIN user_department_jobtitles_position udjp2 on udjp2.DepartmentId = d.DepartmentId
        where udjp2.UserId = `@userId` and udjp2.IsPrimary = 1);
	
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DROP TEMPORARY TABLE IF EXISTS `DocumentTemp`;
	DROP TEMPORARY TABLE IF EXISTS `ResultTemp`;
	CREATE TEMPORARY TABLE `DocumentTemp` (
		`Id` INT(11) NOT NULL AUTO_INCREMENT,
		`DocumentId` char(36) NOT NULL,
		`UserId` INT(11) NULL,
		`DateCreated` datetime NOT NULL,
		`DateAppointed` datetime,
		`DateFinished` datetime,
		`Status` int,
		 PRIMARY KEY (`Id`)
	);
	CREATE TEMPORARY TABLE `ResultTemp` (
		`Stt` int(11) NOT NULL AUTO_INCREMENT,
		`LoaiHoSo` varchar(300) NULL,
		`TonKyTruoc` INT(11) NULL,
		`NhanTrongKy` INT(11) NULL,
		`Tong` INT(11) NULL,
		`DaGiaiQuyet` INT(11) NULL,
		`DungHen` INT(11) NULL,
		`TiLeDungHen` INT(11) NULL ,
		`TreHen` INT(11) NULL,
		`TiLeTreHen` INT(11) NULL,
		`ChuaGiaiQuyet` INT(11) NULL,
		`TrongHan` INT(11) NULL,
		`TiLeTrongHan` INT(11) NULL,
		`QuaHan` INT(11) NULL,
		`TiLeQuaHan` INT(11) NULL,
		`GhiChu` VARCHAR(300) NULL DEFAULT '',
		PRIMARY KEY (`Stt`)
	);
	
	INSERT INTO `DocumentTemp`(DocumentId, UserId, DateCreated, DateAppointed, DateFinished, `Status`)
	Select 
		d.DocumentId, 
		u.UserId,
		d.DateCreated, 
		d.DateAppointed, 
		d.DateFinished,
		d.`Status`
	FROM document AS d
	INNER JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId AND dc.`Status` NOT IN (1, 8)
	INNER JOIN `user` AS u ON u.UserId = dc.UserCurrentId
	WHERE d.`Status` NOT IN (1, 8)
	AND ((d.DateCreated >= `@from` AND d.DateCreated <= `@to`)
				|| (d.DateCreated < `@from` AND d.`Status` != 4))
	AND dc.DocumentCopyType IN (1, 2, 64);

	OPEN userCur;
	read_loop: LOOP
    FETCH userCur INTO userId, userName;
		IF done THEN
			LEAVE read_loop;
		END IF;
		
		SET @doctype = userName;

		# Tồn kỳ trước
		SET @TonKyTruoc = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated < `@from`) AND d.`Status` != 4 AND d.UserId = userId);

		# Nhận Trong kỳ
		SET @NhanTrongKy = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated >= `@from`) AND d.DateCreated <= `@to` AND d.UserId = userId);

		# Tổng văn bản xử lý
		SET @Tong = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.UserId = userId);
		# Đã giải quyết
		SET @DaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` = 4 AND d.UserId = userId);
		# Đúng hẹn
		SET @DungHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) AND (d.DateFinished <= d.DateAppointed) AND d.UserId = userId);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeDungHen = 0; ELSE SET @TiLeDungHen = (@DungHen*100/@DaGiaiQuyet); END IF;
		# Trễ hẹn
		SET @TreHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) AND (d.DateFinished > d.DateAppointed) AND d.UserId = userId);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeTreHen = 0; ELSE SET @TiLeTreHen = 100 - @TiLeDungHen; END IF;

# Chưa giải quyết
		SET @ChuaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` != 4 AND d.UserId = userId);
		# Trong hạn
		SET @TrongHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) AND (d.DateAppointed IS NULL || d.DateAppointed > NOW()) AND d.UserId = userId);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeTrongHan = 0; ELSE SET @TiLeTrongHan = (@TrongHan*100/@ChuaGiaiQuyet); END IF;		
		# Quá hạn
		SET @QuaHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) AND (d.DateAppointed <= NOW()) AND d.UserId = userId);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeQuaHan = 0; ELSE SET @TiLeQuaHan = 100 - @TiLeTrongHan; END IF;	

		IF @ChuaGiaiQuyet > 0 THEN SET @GhiChu = 'Chuyển kỳ sau'; ELSE SET @GhiChu = ''; END IF;

		INSERT INTO ResultTemp(
				`TonKyTruoc`, `LoaiHoSo`, `NhanTrongKy`, `Tong`, 
				`DaGiaiQuyet`, `DungHen`, `TreHen`, `TiLeDungHen`, `TiLeTreHen`,
				`ChuaGiaiQuyet`, `TrongHan`, `QuaHan`, `TiLeTrongHan`, `TiLeQuaHan`, `GhiChu`)
		VALUES(
				@TonKyTruoc, @doctype, @NhanTrongKy, @Tong, 
				@DaGiaiQuyet, @DungHen, @TreHen, @TiLeDungHen, @TiLeTreHen,
				@ChuaGiaiQuyet, @TrongHan, @QuaHan, @TiLeTrongHan, @TiLeQuaHan, @GhiChu);

	END LOOP read_loop;
	CLOSE userCur;

	SELECT * FROM ResultTemp;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for baocaotonghop_user_dept_copy1
-- ----------------------------
DROP PROCEDURE IF EXISTS `baocaotonghop_user_dept_copy1`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `baocaotonghop_user_dept_copy1`(IN `@from` datetime,
  IN `@to` datetime, IN `@treeGroupValue` int(4),IN `@userId` int(4))
BEGIN
	DECLARE done INT DEFAULT FALSE;
	DECLARE UserId int(3);
	DECLARE UserName varchar(50);
 
	DECLARE Dept varchar(100) DEFAULT
 (select d.DepartmentIdExt from department d INNER JOIN user_department_jobtitles_position udjp on udjp.DepartmentId = d.DepartmentId
    where d.DepartmentId = `@treeGroupValue` AND udjp.UserId = `@userId` and udjp.IsPrimary = 1);

	DECLARE userCur CURSOR FOR 
			SELECT DISTINCT `user`.UserId, `user`.FullName
			FROM `user`
			INNER JOIN `user_department_jobtitles_position` AS udf ON `user`.UserId = udf.UserId
			where `user`.IsActivated = 1  
			AND ((`@treeGroupValue` IS NULL AND udf.DepartmentId = 1)
			OR udf.DepartmentIdExt collate utf8_general_ci  LIKE (select CONCAT(d.DepartmentIdExt,'%') from department d 
				INNER JOIN user_department_jobtitles_position udjp2 on udjp2.DepartmentId = d.DepartmentId
        where d.DepartmentId = `@treeGroupValue` AND udjp2.UserId = `@userId` and udjp2.IsPrimary = 1));

  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DROP TEMPORARY TABLE IF EXISTS `DocumentTemp`;
	DROP TEMPORARY TABLE IF EXISTS `ResultTemp`;
	CREATE TEMPORARY TABLE `DocumentTemp` (
		`Id` INT(11) NOT NULL AUTO_INCREMENT,
		`DocumentId` char(36) NOT NULL,
		`UserId` INT(11) NULL,
		`DateCreated` datetime NOT NULL,
		`DateAppointed` datetime,
		`DateFinished` datetime,
		`Status` int,
		 PRIMARY KEY (`Id`)
	);

	CREATE TEMPORARY TABLE `ResultTemp` (
		`Stt` int(11) NOT NULL AUTO_INCREMENT,
		`LoaiHoSo` varchar(300) NULL,
		`TonKyTruoc` INT(11) NULL,
		`NhanTrongKy` INT(11) NULL,
		`Tong` INT(11) NULL,
		`DaGiaiQuyet` INT(11) NULL,
		`DungHen` INT(11) NULL,
		`TiLeDungHen` INT(11) NULL ,
		`TreHen` INT(11) NULL,
		`TiLeTreHen` INT(11) NULL,
		`ChuaGiaiQuyet` INT(11) NULL,
		`TrongHan` INT(11) NULL,
		`TiLeTrongHan` INT(11) NULL,
		`QuaHan` INT(11) NULL,
		`TiLeQuaHan` INT(11) NULL,
		`GhiChu` VARCHAR(300) NULL DEFAULT '',
		PRIMARY KEY (`Stt`)
	);
	
	INSERT INTO `DocumentTemp`(DocumentId, UserId, DateCreated, DateAppointed, DateFinished, `Status`)
	Select 
		d.DocumentId, 
		u.UserId,
		d.DateCreated, 
		d.DateAppointed, 
		d.DateFinished,
		d.`Status`
	FROM document AS d
	INNER JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId AND dc.`Status` NOT IN (1, 8)
	INNER JOIN `user` AS u ON u.UserId = dc.UserCurrentId
	WHERE d.`Status` NOT IN (1, 8)
	AND ((d.DateCreated >= `@from` AND d.DateCreated <= `@to`)
				|| (d.DateCreated < `@from` AND d.`Status` != 4))
	AND dc.DocumentCopyType IN (1, 2, 64);

	OPEN userCur;
	read_loop: LOOP
    FETCH userCur INTO userId, userName;
		IF done THEN
			LEAVE read_loop;
		END IF;
		
		SET @doctype = userName;

		# Tồn kỳ trước
		SET @TonKyTruoc = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated < `@from`) AND d.`Status` != 4 AND d.UserId = userId);

		# Nhận Trong kỳ
		SET @NhanTrongKy = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated >= `@from`) AND d.DateCreated <= `@to` AND d.UserId = userId);

		# Tổng văn bản xử lý
		SET @Tong = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.UserId = userId);
		# Đã giải quyết
		SET @DaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` = 4 AND d.UserId = userId);
		# Đúng hẹn
		SET @DungHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) AND (d.DateFinished <= d.DateAppointed) AND d.UserId = userId);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeDungHen = 0; ELSE SET @TiLeDungHen = (@DungHen*100/@DaGiaiQuyet); END IF;
		# Trễ hẹn
		SET @TreHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) AND (d.DateFinished > d.DateAppointed) AND d.UserId = userId);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeTreHen = 0; ELSE SET @TiLeTreHen = 100 - @TiLeDungHen; END IF;

# Chưa giải quyết
		SET @ChuaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` != 4 AND d.UserId = userId);
		# Trong hạn
		SET @TrongHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) AND (d.DateAppointed IS NULL || d.DateAppointed > NOW()) AND d.UserId = userId);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeTrongHan = 0; ELSE SET @TiLeTrongHan = (@TrongHan*100/@ChuaGiaiQuyet); END IF;		
		# Quá hạn
		SET @QuaHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) AND (d.DateAppointed <= NOW()) AND d.UserId = userId);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeQuaHan = 0; ELSE SET @TiLeQuaHan = 100 - @TiLeTrongHan; END IF;	

		IF @ChuaGiaiQuyet > 0 THEN SET @GhiChu = 'Chuyển kỳ sau'; ELSE SET @GhiChu = ''; END IF;

		INSERT INTO ResultTemp(
				`TonKyTruoc`, `LoaiHoSo`, `NhanTrongKy`, `Tong`, 
				`DaGiaiQuyet`, `DungHen`, `TreHen`, `TiLeDungHen`, `TiLeTreHen`,
				`ChuaGiaiQuyet`, `TrongHan`, `QuaHan`, `TiLeTrongHan`, `TiLeQuaHan`, `GhiChu`)
		VALUES(
				@TonKyTruoc, @doctype, @NhanTrongKy, @Tong, 
				@DaGiaiQuyet, @DungHen, @TreHen, @TiLeDungHen, @TiLeTreHen,
				@ChuaGiaiQuyet, @TrongHan, @QuaHan, @TiLeTrongHan, @TiLeQuaHan, @GhiChu);

	END LOOP read_loop;
	CLOSE userCur;

	SELECT * FROM ResultTemp;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for baocaotonghop_user_test
-- ----------------------------
DROP PROCEDURE IF EXISTS `baocaotonghop_user_test`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `baocaotonghop_user_test`(IN `@from` datetime,
  IN `@to` datetime, IN `@treeGroupValue` int(4))
BEGIN
	DECLARE done INT DEFAULT FALSE;
	DECLARE UserId int(3);
	DECLARE UserName varchar(50);
	-- DECLARE Dept varchar(100) DEFAULT (select department.DepartmentIdExt from department where department.DepartmentId = `@treeGroupValue`);
DECLARE userCur CURSOR FOR 
			SELECT DISTINCT `user`.UserId, `user`.FullName
			FROM `user`
			INNER JOIN `user_department_jobtitles_position` AS udf ON `user`.UserId = udf.UserId
			where `user`.UserId=`@treeGroupValue`;
	
	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

	DROP TEMPORARY TABLE IF EXISTS `DocumentTemp`;
	DROP TEMPORARY TABLE IF EXISTS `ResultTemp`;
	CREATE TEMPORARY TABLE `DocumentTemp` (
		`Id` INT(11) NOT NULL AUTO_INCREMENT,
		`DocumentId` char(36) NOT NULL,
		`UserId` INT(11) NULL,
		`DateCreated` datetime NOT NULL,
		`DateAppointed` datetime,
		`DateFinished` datetime,
		`Status` int,
		 PRIMARY KEY (`Id`)
	);

	CREATE TEMPORARY TABLE `ResultTemp` (
		`Stt` int(11) NOT NULL AUTO_INCREMENT,
		`LoaiHoSo` varchar(300) NULL,
		`TonKyTruoc` INT(11) NULL,
		`NhanTrongKy` INT(11) NULL,
		`Tong` INT(11) NULL,
		`DaGiaiQuyet` INT(11) NULL,
		`DungHen` INT(11) NULL,
		`TiLeDungHen` INT(11) NULL ,
		`TreHen` INT(11) NULL,
		`TiLeTreHen` INT(11) NULL,
		`ChuaGiaiQuyet` INT(11) NULL,
		`TrongHan` INT(11) NULL,
		`TiLeTrongHan` INT(11) NULL,
		`QuaHan` INT(11) NULL,
		`TiLeQuaHan` INT(11) NULL,
		`GhiChu` VARCHAR(300) NULL DEFAULT '',
		PRIMARY KEY (`Stt`)
	);
	
	INSERT INTO `DocumentTemp`(DocumentId, UserId, DateCreated, DateAppointed, DateFinished, `Status`)
	Select 
		d.DocumentId, 
		u.UserId,
		d.DateCreated, 
		d.DateAppointed, 
		d.DateFinished,
		d.`Status`
	FROM document AS d
	INNER JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId AND dc.`Status` NOT IN (1, 8)
	INNER JOIN `user` AS u ON u.UserId = dc.UserCurrentId
	WHERE d.`Status` NOT IN (1, 8)
	AND ((d.DateCreated >= `@from` AND d.DateCreated <= `@to`)
				|| (d.DateCreated < `@from` AND d.`Status` != 4))
	  AND dc.DocumentCopyType IN (1, 2, 64);

	OPEN userCur;
	read_loop: LOOP
    FETCH userCur INTO  UserId, userName;
		IF done THEN
			LEAVE read_loop;
		END IF;
		
		SET @doctype = userName;

		# Tồn kỳ trước
		SET @TonKyTruoc = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated < `@from`) AND d.`Status` != 4 AND d.UserId =  `@treeGroupValue`);

		# Nhận Trong kỳ
		SET @NhanTrongKy = ( SELECT COUNT(DocumentId) 
					FROM DocumentTemp AS d 
					WHERE (d.DateCreated >= `@from`) AND d.DateCreated <= `@to` AND d.UserId =  `@treeGroupValue`);

		# Tổng văn bản xử lý
		SET @Tong = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.UserId = `@treeGroupValue`);
		# Đã giải quyết
		SET @DaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` = 4 AND d.UserId = `@treeGroupValue`);
		# Đúng hẹn
		SET @DungHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) AND (d.DateFinished <= d.DateAppointed) AND d.UserId = `@treeGroupValue`);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeDungHen = 0; ELSE SET @TiLeDungHen = (@DungHen*100/@DaGiaiQuyet); END IF;
		# Trễ hẹn
		SET @TreHen = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` = 4) AND (d.DateFinished > d.DateAppointed) AND d.UserId = `@treeGroupValue`);
		IF @DaGiaiQuyet = 0 THEN SET @TiLeTreHen = 0; ELSE SET @TiLeTreHen = 100 - @TiLeDungHen; END IF;

# Chưa giải quyết
		SET @ChuaGiaiQuyet = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE d.`Status` != 4 AND d.UserId = `@treeGroupValue`);
		# Trong hạn
		SET @TrongHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) AND (d.DateAppointed IS NULL || d.DateAppointed > NOW()) AND d.UserId = `@treeGroupValue`);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeTrongHan = 0; ELSE SET @TiLeTrongHan = (@TrongHan*100/@ChuaGiaiQuyet); END IF;		
		# Quá hạn
		SET @QuaHan = (SELECT COUNT(DocumentId) FROM DocumentTemp AS d WHERE (d.`Status` != 4) AND (d.DateAppointed <= NOW()) AND d.UserId = `@treeGroupValue`);
		IF @ChuaGiaiQuyet = 0 THEN SET @TiLeQuaHan = 0; ELSE SET @TiLeQuaHan = 100 - @TiLeTrongHan; END IF;	

		IF @ChuaGiaiQuyet > 0 THEN SET @GhiChu = 'Chuyển kỳ sau'; ELSE SET @GhiChu = ''; END IF;

		INSERT INTO ResultTemp(
				`TonKyTruoc`, `LoaiHoSo`, `NhanTrongKy`, `Tong`, 
				`DaGiaiQuyet`, `DungHen`, `TreHen`, `TiLeDungHen`, `TiLeTreHen`,
				`ChuaGiaiQuyet`, `TrongHan`, `QuaHan`, `TiLeTrongHan`, `TiLeQuaHan`, `GhiChu`)
		VALUES(
				@TonKyTruoc, @doctype, @NhanTrongKy, @Tong, 
				@DaGiaiQuyet, @DungHen, @TreHen, @TiLeDungHen, @TiLeTreHen,
				@ChuaGiaiQuyet, @TrongHan, @QuaHan, @TiLeTrongHan, @TiLeQuaHan, @GhiChu);

	END LOOP read_loop;
	CLOSE userCur;

	SELECT * FROM ResultTemp;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for CategoryAdd
-- ----------------------------
DROP PROCEDURE IF EXISTS `CategoryAdd`;
DELIMITER ;;
CREATE DEFINER=`trungvh`@`` PROCEDURE `CategoryAdd`(IN categoryName varchar(128))
BEGIN
		INSERT into category (CategoryName) 
		values (codeName);
		SELECT LAST_INSERT_ID();
	END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for CodeAdd
-- ----------------------------
DROP PROCEDURE IF EXISTS `CodeAdd`;
DELIMITER ;;
CREATE DEFINER=`trungvh`@`` PROCEDURE `CodeAdd`(IN codeName varchar(255), IN template varchar(255), IN increaseid int)
BEGIN
	INSERT into code (CodeName, Template, YearLastest, MonthLastest, DayLastest, NumberLastest, IncreaseId, Version) 
	values (codeName, template, 0, 0, 0, 0, increaseid, now() );
	SELECT LAST_INSERT_ID();
end
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for departmentadd
-- ----------------------------
DROP PROCEDURE IF EXISTS `departmentadd`;
DELIMITER ;;
CREATE DEFINER=`trungvh`@`` PROCEDURE `departmentadd`(IN mparentId int, IN mdepartmentName varchar(128),IN mdepartmentIdExt varchar(64), IN mdepartmentPath varchar(128), IN mdepartmentOrder int, IN mdepartmentLevel int)
BEGIN
	INSERT into department (ParentId, DepartmentName, IsActivated, DepartmentIdExt,DepartmentPath,DepartmentOrder, DepartmentLevel, Version ) 
	values (mparentId , mdepartmentName , 1, mdepartmentIdExt, mdepartmentPath, mdepartmentOrder, mdepartmentLevel,  now());
	SELECT LAST_INSERT_ID();
end
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for departmentaddroot
-- ----------------------------
DROP PROCEDURE IF EXISTS `departmentaddroot`;
DELIMITER ;;
CREATE DEFINER=`trungvh`@`` PROCEDURE `departmentaddroot`( IN mdepartmentName varchar(128),IN mdepartmentIdExt varchar(64), IN mdepartmentPath varchar(128), IN mdepartmentOrder int, IN mdepartmentLevel int)
BEGIN
	INSERT into department (DepartmentName, IsActivated, DepartmentIdExt,DepartmentPath,DepartmentOrder, DepartmentLevel, Version ) 
	values (mdepartmentName , 1, mdepartmentIdExt, mdepartmentPath, mdepartmentOrder, mdepartmentLevel,  now());
	SELECT LAST_INSERT_ID();
end
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for departmentget
-- ----------------------------
DROP PROCEDURE IF EXISTS `departmentget`;
DELIMITER ;;
CREATE DEFINER=`trungvh`@`` PROCEDURE `departmentget`(IN mdepartmentPath varchar(128))
BEGIN
	Select * from `department` where `DepartmentPath` = mdepartmentPath;
end
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for departmentupdate
-- ----------------------------
DROP PROCEDURE IF EXISTS `departmentupdate`;
DELIMITER ;;
CREATE DEFINER=`trungvh`@`` PROCEDURE `departmentupdate`(IN mdepartmentId  int)
update department set DepartmentIdExt = DepartmentId WHERE DepartmentId = mdepartmentId
;
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for GetStoreByUser
-- ----------------------------
DROP PROCEDURE IF EXISTS `GetStoreByUser`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `GetStoreByUser`(IN `@userId` int(11))
BEGIN
    DECLARE id INT DEFAULT 0;
    DECLARE val varchar(255);
    DECLARE leng INT DEFAULT 0;
    DECLARE idex INT DEFAULT 0;
    DECLARE splitted_value varchar(11);
    DECLARE char_split char(1) DEFAULT ',';
    DECLARE done INT DEFAULT FALSE;

    DECLARE cur CURSOR FOR SELECT StoreId, REPLACE(REPLACE(UserViewIds,'[',''),']','') as 'listStr' FROM store;

    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

    DROP TEMPORARY TABLE IF EXISTS StoreTmp;
    CREATE TEMPORARY TABLE StoreTmp(
			`Id` INT NOT NULL,
			`UserId` VARCHAR(255) NOT NULL
    );

		OPEN cur;
      read_loop: LOOP
        FETCH cur INTO id, val;
        IF done THEN
          LEAVE read_loop;
        END IF;

        SET leng = (SELECT LENGTH(val) - LENGTH(REPLACE(val, char_split, '')) + 1);
        SET idex = 1;

        WHILE idex <= leng DO
          SET splitted_value =(SELECT REPLACE(SUBSTRING(SUBSTRING_INDEX(val, char_split, idex),	LENGTH(SUBSTRING_INDEX(val, char_split, idex - 1)) + 1), ',', ''));

          if(LENGTH(REPLACE(splitted_value, ' ', '')) > 0)THEN
             INSERT INTO StoreTmp VALUES (id, splitted_value);
          end if;

          SET idex = idex + 1;

        END WHILE;
      END LOOP;      
    CLOSE cur;

SELECT s.StoreName as GroupName, s.StoreId as GroupValue FROM `store` s
		  where s.UserId = @userId
UNION
SELECT s.StoreName as GroupName, s.StoreId as GroupValue FROM `store` s
			 where s.DepartmentId in (select udjp.DepartmentId from user_department_jobtitles_position udjp where udjp.UserId = `@userId`)
UNION 
SELECT s.StoreName as GroupName, s.StoreId as GroupValue FROM `store` s
			WHERE s.StoreId in (SELECT StoreTmp.id from StoreTmp WHERE StoreTmp.UserId = `@userId`);

END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for getUserUyQuyen
-- ----------------------------
DROP PROCEDURE IF EXISTS `getUserUyQuyen`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `getUserUyQuyen`(IN `@inUserId` int,IN `@inStartDate` datetime,IN `@inEndDate` datetime)
BEGIN
	#Routine body goes here...
  DECLARE leng INT DEFAULT 0;
  DECLARE idex INT DEFAULT 0;
  DECLARE userTmpId INT(11);
  DECLARE done INT DEFAULT FALSE;

  DECLARE cur CURSOR FOR
				SELECT `au`.AuthorizedUserId from `authorize` as `au`
		    WHERE `au`.Active = 1 and `au`.DateBegin >= `@inStartDate` 
				and `au`.DateEnd <= `@inEndDate` 
				and au.AuthorizeUserId <> `@inUserId`
				or  au.AuthorizeUserId not in ( select au.AuthorizedUserId from `authorize` as `au`
											WHERE au.AuthorizeUserId = `@inUserId` 
											and au.Active = 1 	
											and au.DateBegin >= `@inStartDate`
											and au.DateEnd <= `@inEndDate`
									) ;

  DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

  DROP TEMPORARY TABLE IF EXISTS `UyQuyenTmp`;
 -- Bảng chứa danh sách người dùng được ủy quyền
	CREATE TEMPORARY TABLE `UyQuyenTmp` (
		`Id` INT(11) NOT NULL AUTO_INCREMENT,
		`uyQuyenUserId` INT(11) NULL,
		 PRIMARY KEY (`Id`)
	);

  INSERT INTO `UyQuyenTmp`(uyQuyenUserId) VALUES(`@inUserId`);
  INSERT INTO `UyQuyenTmp`(uyQuyenUserId) 
				select au.AuthorizedUserId from `authorize` as `au`
				WHERE au.AuthorizeUserId = `@inUserId` 
				and au.Active = 1 	
				and au.DateBegin >= `@inStartDate`
				and au.DateEnd <= `@inEndDate`;

	OPEN cur;
		 read_loop: LOOP
				FETCH cur INTO userTmpId;
						IF done THEN
							LEAVE read_loop;
						END IF;

				SET leng = (SELECT COUNT(*) from `UyQuyenTmp`);
				SET idex = 1;

				WHILE idex <= leng DO
						

					SET idex = idex + 1;
				END WHILE;

		 END LOOP;      
  CLOSE cur;
SELECT DISTINCT uq.uyQuyenUserId from UyQuyenTmp;

END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for LayCacNamKhac
-- ----------------------------
DROP PROCEDURE IF EXISTS `LayCacNamKhac`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `LayCacNamKhac`()
BEGIN

	DECLARE yearTmp int;
	DECLARE val INT DEFAULT 0;
	DECLARE leng INT DEFAULT 0;
	DECLARE idex INT DEFAULT 0;
	DECLARE maxYear int DEFAULT(select YEAR(Max(DateCreated)) from document);
	DECLARE minYear int DEFAULT(select YEAR(Min(DateCreated)) from document);

	DROP TEMPORARY TABLE IF EXISTS `LayCacNamTmp`;
	CREATE TEMPORARY TABLE `LayCacNamTmp`(
		`Id` INT(11) NOT NULL AUTO_INCREMENT,
		`GroupValue` int(11) not null,
		`GroupName` varchar(250) NOT NULL,
		 PRIMARY KEY (`Id`)
	);

  INSERT INTO `LayCacNamTmp`(`GroupValue`,`GroupName`) VALUES (maxYear , CONCAT('Năm ',maxYear));

	SET idex = 1;
  set leng = maxYear - minYear + 1;

	If (leng > 1)
	 THEN
		WHILE idex <= leng DO
			SET val = maxYear - idex;

			IF(val >= minYear)THEN
				INSERT INTO `LayCacNamTmp`(`GroupValue`,`GroupName`) VALUES (val, CONCAT('Năm ',val));
			end IF;

			SET idex = idex + 1;
		END WHILE;
	END IF;

select `GroupValue`, `GroupName` from `LayCacNamTmp`;

END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for LayCanBoCapDuoi
-- ----------------------------
DROP PROCEDURE IF EXISTS `LayCanBoCapDuoi`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `LayCanBoCapDuoi`(IN `@userId` int(11))
BEGIN
	#Routine body goes here...
  DECLARE done INT DEFAULT FALSE;
	DECLARE ext varchar(250);

	DECLARE DeptUserCursor CURSOR FOR(
					SELECT DepartmentIdExt
					from user_department_jobtitles_position where UserId = `@userId` AND IsPrimary = 1
);

	DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
	DROP TEMPORARY TABLE IF EXISTS `CanBoCapDuoiTemp`;
	CREATE TEMPORARY TABLE `CanBoCapDuoiTemp` (
		`Id` INT(11) NOT NULL AUTO_INCREMENT,
		`GroupValue` INT(11) NOT NULL,
		`GroupName` varchar(250) NOT NULL,
		 PRIMARY KEY (`Id`)
	);

	OPEN DeptUserCursor;
		read_loop:LOOP
			FETCH DeptUserCursor INTO ext;
			if done THEN
				LEAVE read_loop;
			END IF; 

			insert INTO `CanBoCapDuoiTemp`(`GroupName`,`GroupValue`) 
								select u.FullName, u.UserId from `user` u 
								INNER JOIN user_department_jobtitles_position udjp on udjp.UserId = u.UserId
								where udjp.DepartmentIdExt collate utf8_general_ci = ext
								      or udjp.DepartmentIdExt collate utf8_general_ci LIKE CONCAT(ext,'.%');

	  END LOOP read_loop;
	CLOSE DeptUserCursor;

  select DISTINCT `GroupValue`,`GroupName` FROM  CanBoCapDuoiTemp WHERE GroupValue <> `@userId`;

END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for LayTrangThaiVanBan
-- ----------------------------
DROP PROCEDURE IF EXISTS `LayTrangThaiVanBan`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `LayTrangThaiVanBan`()
BEGIN
	Select 1 as 'GroupValue', N'Văn bản dự thảo' as 'GroupName'
	union 
	Select 2 as 'GroupValue', N'Văn bản đang xử lý' as 'GroupName'
	union 
	Select 4 as 'GroupValue', N'Văn bản kết thúc' as 'GroupName'
	union
	Select 8 as 'GroupValue', N'Văn bản đã loại bỏ' as 'GroupName'
	union 
	Select 16 as 'GroupValue', N'Văn bản dừng xử lý' as 'GroupName';
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for spGetAllDocumentInStorePrivate
-- ----------------------------
DROP PROCEDURE IF EXISTS `spGetAllDocumentInStorePrivate`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `spGetAllDocumentInStorePrivate`(IN `storePrivateId` int)
BEGIN
	SELECT
	*
FROM
	(
		SELECT
			'' AS `DocCode`,
			`sa`.`AttachmentName` AS `Compendium`,
			`sa`.`CreatedByUserName` AS `Email`,
			`sa`.`CreatedOnDate` AS `DateCreated`,
			NULL AS `DateAppointed`,
			NULL AS `LastComment`,
			`sa`.`CreatedOnDate` AS `DateReceived`,
			`sa`.`StorePrivateAttachmentId` AS `DocumentCopyId`,
			1 AS `IsViewed`,
			NULL AS `UserCurrentFullName`,
			NULL AS `UserCurrentFirstName`,
			1 AS `Color`,
			1 AS `IsFile`
		FROM
			`storeprivate_attachment` AS `sa`
		WHERE
			`sa`.`StorePrivateId` = storePrivateId
		UNION
			SELECT
				`d`.`DocCode`,
				`d`.`Compendium`,
				`d`.`Email`,
				`d`.`DateCreated`,
				`d`.`DateAppointed`,
				`dc`.`LastComment`,
				`dc`.`DateReceived`,
				`dc`.`DocumentCopyId`,
				`df`.`IsViewed`,
				`u`.`FullName` AS `UserCurrentFullName`,
				`u`.`FirstName` AS `UserCurrentFirstName`,
				fnGetDocumentColor (
					`d`.`UrgentId`,
					`d`.`DateAppointed`,
					`d`.`DateResponsed`,
					`d`.`DateResponsedOverdue`,
					`dc`.`DateOverdue`,
					`dc`.`DocumentCopyType`,
					`dc`.`Status`
				) AS `Color`,
				0 AS `IsFile`
			FROM
				`document` AS `d`
			INNER JOIN `documentcopy` AS `dc` ON `d`.`DocumentId` = `dc`.`DocumentId`
			INNER JOIN `storeprivate_documentcopy` AS `sdc` ON `dc`.`DocumentCopyId` = `sdc`.`DocumentCopyId`
			INNER JOIN `user` AS `u` ON `dc`.`UserCurrentId` = `u`.`UserId`
			INNER JOIN `docfinish` AS `df` ON `dc`.`DocumentCopyId` = `df`.`DocumentCopyId`
			AND `dc`.UserCurrentId = `df`.UserId
			WHERE
				`sdc`.`StorePrivateId` = storePrivateId
	) AS `a`
ORDER BY
	`a`.`DateReceived` DESC;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for StoreAdd
-- ----------------------------
DROP PROCEDURE IF EXISTS `StoreAdd`;
DELIMITER ;;
CREATE DEFINER=`trungvh`@`` PROCEDURE `StoreAdd`(IN storeName varchar(255), IN userId int, IN departmentId int,IN categoryBusinessCode varchar(128))
BEGIN
	INSERT into store (StoreName, UserId, DepartmentId, CategoryBusinessCode) 
	values (storeName, userId, departmentId, categoryBusinessCode );
	SELECT LAST_INSERT_ID();
end
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for USERADD
-- ----------------------------
DROP PROCEDURE IF EXISTS `USERADD`;
DELIMITER ;;
CREATE DEFINER=`trungvh`@`` PROCEDURE `USERADD`(IN musername VARCHAR(64),IN musernameemaildomain varchar(255),IN mdomainname varchar(64),IN mfullname varchar(128),IN mgender INT,IN memail varchar(255))
BEGIN
	INSERT into user (Username, UsernameEmailDomain, DomainName, PasswordLastModifiedOnDate, FullName, Gender, Email,IsActivated, IsLockedOut ) 
	values (musername, musernameemaildomain, mdomainname,  now(), mfullname, mgender, memail, 1, 0);
	SELECT LAST_INSERT_ID();
end
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for userdepjobposadd
-- ----------------------------
DROP PROCEDURE IF EXISTS `userdepjobposadd`;
DELIMITER ;;
CREATE DEFINER=`trungvh`@`` PROCEDURE `userdepjobposadd`(IN userid int, IN departmentid int,IN jobtitleid int,IN positionid int)
BEGIN
	INSERT into user_department_jobtitles_position (UserId, DepartmentId, JobTitlesId, PositionId ) 
	values (userid  , departmentid  , jobtitleid ,  positionid );

END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for CountIncompletedDocByLevel
-- ----------------------------
DROP FUNCTION IF EXISTS `CountIncompletedDocByLevel`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `CountIncompletedDocByLevel`(EndDate datetime, LevelId int) RETURNS int(11)
BEGIN
	DECLARE Count int(11);
	SELECT COUNT(*) INTO `Count` 
	FROM `documentonline` AS `d` 
	INNER JOIN `doctype` as `dt` on `d`.`DoctypeId`=`dt`.`DoctypeId`
	INNER JOIN `level` AS `l` on `dt`.`LevelId`=`l`.`LevelId` 
	WHERE `Status`=1 AND DATEDIFF(`DateReceived`,`EndDate`) < 0 AND `dt`.`LevelId` = LevelId; 
	RETURN Count;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for DateCompare
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
-- Function structure for fnConvertDateToInt
-- ----------------------------
DROP FUNCTION IF EXISTS `fnConvertDateToInt`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `fnConvertDateToInt`(`date_create` date,`k` int,`date_now` datetime,`han_giai_quyet` datetime,`trang_thai` int) RETURNS int(11)
BEGIN
	DECLARE d INT;
	IF trang_thai = 2 -- Độ khẩn: 1 - Thường. 2 - Khẩn. 3 - Hỏa tốc.
	THEN
		IF YEAR(han_giai_quyet) = 9000 OR han_giai_quyet = NULL
		THEN
			SET date_create = ADDDATE(date_create,INTERVAL k DAY);
			SET d = DATEDIFF(date_create,date_now) + 2 * fnGetDateForAday(date_create, date_now, 'Sunday');
		ELSE 
			SET d=DATEDIFF(han_giai_quyet, date_now) + 2 * fnGetDateForAday(han_giai_quyet, date_now, 'Sunday');
		END IF;
	ELSE  
		SET d=0;
	END IF;
	RETURN d;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for fnGetCatalogValue
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetCatalogValue`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `fnGetCatalogValue`(`formId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`controlId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`documentId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci) RETURNS varchar(255) CHARSET utf8
BEGIN
	DECLARE result varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci;
	SELECT `catalogvalue` INTO result 
	FROM `doc_catalog` 
	WHERE `FormId` = formId 
				AND `CatalogValueId` = controlId 
				AND `DocumentId` = documentId;
	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for fnGetDateForAday
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetDateForAday`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `fnGetDateForAday`(`DtFrom` datetime,`DtTo` datetime,`DayName` varchar(12)) RETURNS int(11)
BEGIN
	DECLARE c INT;
	DECLARE TotDays INT;
  DECLARE CNT INT;
	SET c=0;
	IF NOT (DayName = 'Monday' OR DayName = 'Sunday' OR DayName = 'Tuesday' OR DayName = 'Wednesday' OR DayName = 'Thursday' OR DayName = 'Friday' OR DayName = 'Saturday')
	THEN
		RETURN c;
	END IF;
	
	SET TotDays = DATEDIFF(DtTo,DtFrom);
	SET CNT = 0;
	WHILE TotDays >= CNT DO
		IF DAYNAME(SUBDATE(DtTo, INTERVAL CNT DAY)) = DayName
		THEN
				 SET c = c + 1;
		END IF;
    SET CNT = CNT + 1;
	END WHILE;

  RETURN c;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for fnGetDocOnlineForm
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetDocOnlineForm`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `fnGetDocOnlineForm`(`formId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`controlId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci) RETURNS varchar(255) CHARSET utf8
BEGIN
	DECLARE result varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci;
	SELECT `ExtendFieldValue` INTO result 
	FROM `doc_extendfield` 
	WHERE `FormId` = formId  
				AND `ExtendFieldId` = controlId ;
	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for fnGetDocumentColor
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetDocumentColor`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `fnGetDocumentColor`(`UrgentId` int, `DateAppointed` datetime, `DateResponsed` datetime, `DateResponsedOverdue` datetime, `DateOverdue` datetime, `DocumentCopyType` int, `Status` int) RETURNS int(11)
BEGIN
	-- CuongNT@bkav.com - 180513
	-- Danh sách mã màu:
	-- 1: Đen, 2: Xanh, 3: Cam, 4: Đỏ, 5: Hồng, 6: Nền đỏ.
	-- Trong đó:
	-- 1: Đen - Văn bản bình thường
	-- 2: Xanh - Văn bản Đồng xử lý, Văn bản Thông báo (documentcopy.DocumentCopyType)
	-- 3: Cam - Văn bản còn 1 ngày nữa thì hết hạn (documentcopy.DateOverdue = NOW() + 1)
	-- 4: Đỏ - Văn bản Khẩn, Văn bản quá hạn (Quá hạn xử lý, Quá hạn bổ sung, Quá hạn dừng xử lý...) (documentcopy.DateOverdue < NOW() OR DateAppointed < NOW() OR UrgentId=2)
	-- 5: Hồng - Văn bản quá hạn hồi báo (có thể để màu đỏ luôn) (document.DateResponsed = null && document.DateResponsedOverdue < NOW())
	-- 6: Nền đỏ - Văn bản Hỏa tốc (document.UrgentId)
	DECLARE result INT;
	SET result = 6;
	-- Các trường hợp văn bản dự thảo, kết thúc, hủy bỏ ko set màu sắc
	IF `Status` = 4 OR `Status` = 1 OR `Status` = 8
	THEN
		SET result = 1;
	ELSE
		IF UrgentId = 3 -- Độ khẩn: Hỏa tốc     
		THEN
			SET result = 6; -- Màu đỏ sẫm
		ELSE
			IF DateResponsed = null && DATE(DateResponsedOverdue) < DATE(NOW())
			THEN
				SET result = 5; -- Màu hồng
			ELSE
				IF UrgentId = 2 OR DATE(DateOverdue) < DATE(NOW()) OR DATE(DateAppointed) < DATE(NOW())
				THEN
					SET result = 4;-- Màu đỏ
				ELSE
					IF DATE(DateOverdue) = DATE(DATE_ADD(NOW(),INTERVAL 1 DAY)) OR DATE(DateOverdue) = DATE(NOW())
					THEN
						SET result = 3;-- Màu cam
					ELSE
						IF DocumentCopyType = 2 OR DocumentCopyType = 4
						THEN
							SET result = 2;-- Màu xanh
						ELSE
							SET result = 1;-- Màu đen
						END IF;
					END IF;
				END IF;
			END IF;
		END IF;
	END IF;	
RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for GetDayDocumentProcess_new
-- ----------------------------
DROP FUNCTION IF EXISTS `GetDayDocumentProcess_new`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `GetDayDocumentProcess_new`(`dateCompare` datetime, `Status` int, `IsSuccess` bit, `IsReturned` bit) RETURNS text CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	#Routine body goes here...
	-- Trả ra số ngày còn lại mà người dùng có thể giữ
	DECLARE result varchar(100);
	DECLARE totalDay int;
	set totalDay = DATEDIFF(Now(),dateCompare);

	IF (`Status` = 4)
	THEN
		RETURN 'Đã kết thúc';
	END IF;

	IF(`IsReturned` = 1)
	THEN
		RETURN 'Đã trả kết quả';
	END IF;
			
	IF(`IsSuccess` IS NOT NULL)
	THEN 
		RETURN 'Đã duyệt';
	END IF;

	IF(totalDay > 0)
		then set result = CONCAT('QH ',totalDay,' ngày');		
	ELSE
		SET totalDay= totalDay*-1;
		IF(totalDay > 2)
		THEN 
			SET result = CONCAT('Còn ',totalDay,' ngày');	
		ELSE 
			IF(totalDay = 2)
				THEN set result = 'Ngày kia';
			ELSE 
				IF (HOUR(NOW())>=12)
				THEN 
					SET result='Buổi chiều';
				ELSE 
					SET result='Buổi sáng';
				END IF;
			END IF;
		END IF;
  END IF;

	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for fnGetDocumentStatus
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetDocumentStatus`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `fnGetDocumentStatus`(`Status` int) RETURNS varchar(30) CHARSET utf8
BEGIN
	DECLARE result VARCHAR(30);
	SET result = "";
	IF `Status` = 1 
THEN
SET result = 'Dự thảo'; 
	ELSE

		IF `Status` = 2
		THEN
			SET result = 'Đang xử lý';
		ELSE

			if `Status` = 4
			THEN
				set result = 'Kết thúc';
			ELSE
				if `Status` = 8
				then 
					set result = 'Loại bỏ';
				ELSE
					if `Status` = 16
					THEN
						set result = 'Dừng xử lý';
					end if;
				end if;
			end if;

		END IF;

	END IF;
	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for fnGetExtendFieldValue
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetExtendFieldValue`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `fnGetExtendFieldValue`(`formId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`controlId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`documentId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci) RETURNS varchar(255) CHARSET utf8
BEGIN
	DECLARE result varchar(255) CHARACTER SET utf8 COLLATE utf8_unicode_ci; -- '148ce4f0-d727-46cb-ae30-2a98f3745967','e85d433c-221d-4bc9-a5e9-fb71070cf008','196e4386-537c-4a95-90e0-6c8c2feef1a4'
	SELECT `ExtendFieldValue` INTO result 
	FROM `doc_extendfield` 
	WHERE `FormId` = formId 
				AND `ExtendFieldId` = controlId 
				AND `DocumentId` = documentId;
	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for fnTrim
-- ----------------------------
DROP FUNCTION IF EXISTS `fnTrim`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `fnTrim`(`str` varchar(255)) RETURNS varchar(255) CHARSET utf8
BEGIN
	#Routine body goes here...
DECLARE result varchar(255);
set result = REPLACE(REPLACE(REPLACE(`str`,'=',''),'>',''),'<','');
   RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for GetDayDocumentProcess
-- ----------------------------
DROP FUNCTION IF EXISTS `GetDayDocumentProcess`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `GetDayDocumentProcess`(`dateCompare` datetime) RETURNS text CHARSET utf8
BEGIN
	#Routine body goes here...
	-- Trả ra số ngày còn lại mà người dùng có thể giữ
	DECLARE result varchar(100);
	DECLARE totalDay int;
	set totalDay = DATEDIFF(Now(),dateCompare);

	IF(totalDay > 0)
		then set result = CONCAT('QH ',totalDay,' ngày');		
	ELSE
			SET totalDay= totalDay*-1;
		IF(totalDay > 2)
				THEN set result = CONCAT('Còn ',totalDay,' ngày');	
			ELSE 
					if(totalDay = 2)
						THEN set result = 'Ngày kia';
					ELSE 
							if (HOUR(NOW())>=12)
								then set result='Buổi chiều';
							ELSE 
								set result='Buổi sáng';
							END IF;
				END IF;
			END IF;
  END IF;

	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for GetDayDocumentProcess_new
-- ----------------------------
DROP FUNCTION IF EXISTS `GetDayDocumentProcess_new`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `GetDayDocumentProcess_new`(`dateCompare` datetime, `Status` int, `IsSuccess` bit, `IsReturned` bit) RETURNS text CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	#Routine body goes here...
	-- Trả ra số ngày còn lại mà người dùng có thể giữ
	DECLARE result varchar(100);
	DECLARE totalDay int;
	set totalDay = DATEDIFF(Now(),dateCompare);

	IF (`Status` = 4)
	THEN
		RETURN 'Đã kết thúc';
	END IF;

	IF(`IsReturned` = 1)
	THEN
		RETURN 'Đã trả kết quả';
	END IF;
			
	IF(`IsSuccess` IS NOT NULL)
	THEN 
		RETURN 'Đã duyệt';
	END IF;

	IF(totalDay > 0)
		then set result = CONCAT('QH ',totalDay,' ngày');		
	ELSE
		SET totalDay= totalDay*-1;
		IF(totalDay > 2)
		THEN 
			SET result = CONCAT('Còn ',totalDay,' ngày');	
		ELSE 
			IF(totalDay = 2)
				THEN set result = 'Ngày kia';
			ELSE 
				IF (HOUR(NOW())>=12)
				THEN 
					SET result='Buổi chiều';
				ELSE 
					SET result='Buổi sáng';
				END IF;
			END IF;
		END IF;
  END IF;

	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for GetStatus
-- ----------------------------
DROP FUNCTION IF EXISTS `GetStatus`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `GetStatus`(`Status` int) RETURNS varchar(50) CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	
	DECLARE `result` varchar(50);	
	CASE `Status`
    WHEN 1 THEN 
			SET result = "Hồ sơ dự thảo";
    WHEN 2 THEN
			SET result = "Hs đang xử lý";
		WHEN 4 THEN
			SET result = "Hs đã kết thúc";
		WHEN 8 THEN
			SET result = "Hs đã hủy";
		WHEN 16 THEN
			SET result = "Hs đang dừng xử lý";
		ELSE
			SET result = "";
	END CASE;

	RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for OverdueStatus
-- ----------------------------
DROP FUNCTION IF EXISTS `OverdueStatus`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `OverdueStatus`(`Status` int, `DateAppointed` datetime, `DateSuccess` datetime, `DateReturned` datetime, `DateFinished` datetime, `DateRequireSupplementary` datetime) RETURNS int(11)
BEGIN
	-- Trả về trạng thái Đúng hạn, chưa đến hạn, tới hạn, quá hạn, trễ hẹn
	-- 1 - Đúng hạn: đã xử lý đúng hạn
  -- 2 - Chưa đến hạn: chưa xử lý + chưa đến hạn.
  -- 3 - Quá hạn: Chưa xử lý + quá hạn.
  -- 4 - Trễ hẹn: Đã xử lý + quá hạn
	DECLARE `result` int;	

	IF (`Status` = 2 OR `Status` = 4) THEN
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
		ELSE
			IF DateCompare(NOW(), `DateAppointed`, NULL) = 0 THEN
				SET result = 2; -- Chưa đến hạn
			ELSE
				SET result = 3; -- Quá hạn
			END IF;
		END IF;
	ELSEIF `Status` = 16 THEN -- Đang bổ sung - Liên thông
		IF DateCompare(`DateRequireSupplementary`, `DateAppointed`, null) = 0 THEN
			SET result = 2; -- Chưa đến hạn
		ELSE
			SET result = 3; -- Trễ hẹn
		END IF;
	END IF;

	RETURN result;
END
;;
DELIMITER ;
