/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : egovbrvt_soyt

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2017-03-15 10:00:47
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `baocaotonghop`
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
-- Procedure structure for `LayCacNamKhac`
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
-- Procedure structure for `LayCanBoCapDuoi`
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
-- Procedure structure for `LayTrangThaiVanBan`
-- ----------------------------
DROP PROCEDURE IF EXISTS `LayTrangThaiVanBan`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `LayTrangThaiVanBan`()
BEGIN
	Select 1 as 'GroupValue', 'Văn bản dự thảo' as 'GroupName'
	union 
	Select 2 as 'GroupValue', 'Văn bản đang xử lý' as 'GroupName'
	union 
	Select 4 as 'GroupValue', 'Văn bản kết thúc' as 'GroupName'
	union
	Select 8 as 'GroupValue', 'Văn bản đã loại bỏ' as 'GroupName'
	union 
	Select 16 as 'GroupValue', 'Văn bản dừng xử lý' as 'GroupName';
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `SearchProceduce`
-- ----------------------------
DROP PROCEDURE IF EXISTS `SearchProceduce`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SearchProceduce`(IN `@SearchTerm` varchar(1000),IN `@CategoryBusinessId` int(11),IN `@docCode` varchar(100),IN `@InoutCode` varchar(100),IN `@UserId` int(11),IN `@CategoryId` int(11),IN `@StoreId` int(11),IN `@UserCurrentId` int(11),IN `@UserSuccessId` int(11),IN `@UserCreatedId` int(11),IN `@UrgentId` int(11),IN `@InOutPlace` varchar(300),IN `@OrganizationCreate` varchar(300),IN `@FromDate` datetime,IN `@ToDate` datetime,IN `@HasOnlyGetRootDocument` bit(1),IN `@Skip` int(11),IN `@Take` int(11))
BEGIN
-- call SearchProceduce_copy("dang ky", NULL, NULL, NULL, 3907, NULL, NULL, NULL, NULL, NULL, null, null, null, null, null, null, null, null);

Select 
		d.DocumentId, 
		dc.DocumentCopyId, 
		d.`Status`,
		if(d.CategoryBusinessId = 4, d.CitizenName, d.Compendium) as Compendium, 
		d.DocCode, 
		d.InOutCode,
		dc.UserCurrentId,
		d.UserCreatedId,
		d.DateAppointed, 
		d.DateCreated,
		dc.DocumentCopyType,
		null as IsViewed,
		`@UserId` as UserId
	FROM documentcopy dc
	INNER JOIN document d on d.DocumentId = dc.DocumentId
	LEFT JOIN store_doc sd on (`@StoreId` IS NOT NULL AND sd.DocumentId = d.DocumentId and sd.StoreId = `@StoreId`)
	WHERE 
		d.`Status` != 8 AND d.`Status` != 1
		AND dc.DocumentCopyType != 8
		AND (`@CategoryBusinessId` IS NULL OR d.CategoryBusinessId = `@CategoryBusinessId`)
		AND (`@FromDate` IS NULL OR d.DateCreated >= `@FromDate`)
		AND (`@ToDate` IS NULL OR d.DateCreated <= `@ToDate`)
		AND (`@CategoryId` IS NULL OR d.CategoryId = `@CategoryId`)
		AND (`@UrgentId` IS NULL OR d.UrgentId = `@UrgentId`)
		AND (`@UserCurrentId` IS NULL OR dc.UserCurrentId = `@UserCurrentId`)
		AND (`@UserSuccessId` IS NULL OR d.UserSuccessId = `@UserSuccessId`)
		AND (`@UserCreatedId` is null OR d.UserCreatedId = `@UserCreatedId`)
		AND (`@InOutPlace` IS NULL OR d.InOutPlace = `@InOutPlace`)
		AND (`@OrganizationCreate` IS null OR d.Organization = `@OrganizationCreate`)
		AND (`@UserId` IS NULL OR dc.UserNguoiThamGia LIKE CONCAT('%;',`@UserId`,';%') OR dc.UserThongBao LIKE CONCAT('%;',`@UserId`,';%'))
		AND (`@SearchTerm` IS NOT NULL 
			AND (d.Compendium2 LIKE CONCAT('%', `@SearchTerm`, '%')
					Or d.CitizenName LIKE CONCAT('%', `@SearchTerm`, '%')
					Or (`@docCode` is not null and d.DocCode LIKE CONCAT('%', `@docCode`, '%'))
					Or (`@InoutCode` is not null and d.InOutCode LIKE CONCAT('%', `@InoutCode`, '%'))))
	GROUP BY d.DocumentId
	ORDER BY dc.DateReceived DESC
	limit 300;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `spGetAllDocumentInStorePrivate`
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
			NULL AS `DateFinished`,
			NULL AS `DateRequireSupplementary`,
			NULL AS `DateSuccess`,
			2 AS `Status`,
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
				`d`.`DateFinished`,
				`d`.`DateRequireSupplementary`,
				`d`.`DateSuccess`,
				`d`.`Status`,
				`dc`.`LastComment`,
				`dc`.`DateReceived`,
				`dc`.`DocumentCopyId`,
				`df`.`IsViewed`,
				`u`.`FullName` AS `UserCurrentFullName`,
				`u`.`FirstName` AS `UserCurrentFirstName`,
				1 AS `Color`,
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
-- Function structure for `CountIncompletedDocByLevel`
-- ----------------------------
DROP FUNCTION IF EXISTS `CountIncompletedDocByLevel`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `CountIncompletedDocByLevel`(EndDate datetime, LevelId int) RETURNS int(11)
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
-- Function structure for `fnConvertDateToInt`
-- ----------------------------
DROP FUNCTION IF EXISTS `fnConvertDateToInt`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `fnConvertDateToInt`(`date_create` date,`k` int,`date_now` datetime,`han_giai_quyet` datetime,`trang_thai` int) RETURNS int(11)
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
-- Function structure for `fnGetCatalogValue`
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetCatalogValue`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `fnGetCatalogValue`(`formId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`controlId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`documentId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci) RETURNS varchar(255) CHARSET utf8
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
-- Function structure for `fnGetDateForAday`
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetDateForAday`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `fnGetDateForAday`(`DtFrom` datetime,`DtTo` datetime,`DayName` varchar(12)) RETURNS int(11)
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
-- Function structure for `fnGetDocOnlineForm`
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetDocOnlineForm`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `fnGetDocOnlineForm`(`formId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`controlId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci) RETURNS varchar(255) CHARSET utf8
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
-- Function structure for `fnGetDocumentColor`
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetDocumentColor`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `fnGetDocumentColor`(`UrgentId` int, `DateAppointed` datetime, `DateResponsed` datetime, `DateResponsedOverdue` datetime, `DateOverdue` datetime, `DocumentCopyType` int, `Status` int) RETURNS int(11)
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
-- Function structure for `fnGetDocumentStatus`
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetDocumentStatus`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `fnGetDocumentStatus`(`Status` int) RETURNS varchar(30) CHARSET utf8
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
-- Function structure for `fnGetExtendFieldValue`
-- ----------------------------
DROP FUNCTION IF EXISTS `fnGetExtendFieldValue`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `fnGetExtendFieldValue`(`formId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`controlId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci,`documentId` char(36) CHARACTER SET utf8 COLLATE utf8_unicode_ci) RETURNS varchar(255) CHARSET utf8
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
-- Function structure for `fnTrim`
-- ----------------------------
DROP FUNCTION IF EXISTS `fnTrim`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `fnTrim`(`str` varchar(255)) RETURNS varchar(255) CHARSET utf8
BEGIN
	#Routine body goes here...
DECLARE result varchar(255);
set result = REPLACE(REPLACE(REPLACE(`str`,'=',''),'>',''),'<','');
   RETURN result;
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for `GetDayDocumentProcess`
-- ----------------------------
DROP FUNCTION IF EXISTS `GetDayDocumentProcess`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `GetDayDocumentProcess`(`dateCompare` datetime) RETURNS text CHARSET utf8
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
-- Function structure for `GetDayDocumentProcess_new`
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
-- Function structure for `GetStatus`
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
		IF DateCompare(DateCompare, `DateAppointed`, NULL) = 0 THEN
			SET result = 2; -- Chưa đến hạn
		ELSE
			SET result = 3; -- Quá hạn
		END IF;
	END IF;

	RETURN result;
END
;;
DELIMITER ;
