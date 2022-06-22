/*
Navicat MySQL Data Transfer

Source Server         : 127.0.0.1
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : egov_stttt

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2018-12-12 09:05:35
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `mobile_count_document`
-- ----------------------------
DROP PROCEDURE IF EXISTS `mobile_count_document`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `mobile_count_document`(IN `@userId` int,IN `@authorizeIds` varchar(200))
BEGIN
	#Routine body goes here...

  DROP TABLE IF EXISTS docCountTemp;
	CREATE TEMPORARY TABLE docCountTemp (
		DocumentCopyId int not null
		, CategoryBusinessId int not null
		, UserNguoiThamGia VARCHAR(4000) NOT NULL
		, UserCurrentId int NOT NULL
		, UserNguoiDaXem VARCHAR(4000) NOT NULL
	) ENGINE=MEMORY;
		
	INSERT INTO docCountTemp (DocumentCopyId, CategoryBusinessId, UserNguoiThamGia, UserCurrentId, UserNguoiDaXem)
	SELECT 
				dc.DocumentCopyId, d.CategoryBusinessId, dc.UserNguoiThamGia, dc.UserCurrentId, dc.UserNguoiDaXem
		FROM `document` as `d` 
		INNER JOIN `documentcopy` as `dc` ON `d`.`DocumentId` = `dc`.`DocumentId` 
		WHERE `dc`.`Status` = 2 
			and `d`.`status` not in (1, 8, 32)
			AND `d`.CategoryBusinessId != 4
			AND (dc.UserCurrentId = `@userId` OR dc.UserNguoiThamGia LIKE CONCAT('%;',`@userId`,';%'))
			AND `dc`.`DocumentCopyType` in (1, 2, 4, 32, 64);

	SET @denChoXL = (SELECT COUNT(1) FROM docCountTemp where CategoryBusinessId = 1 AND UserCurrentId = `@userId`);
	SET @denChoXL_seen = (SELECT COUNT(1) FROM docCountTemp where CategoryBusinessId = 1 AND UserCurrentId = `@userId` and UserNguoiDaXem Like CONCAT('%;',`@userId`,';%'));
	SET @denChoXL_new = (SELECT COUNT(1) FROM docCountTemp where CategoryBusinessId = 1 AND UserCurrentId = `@userId` and UserNguoiDaXem not Like CONCAT('%;',`@userId`,';%'));
	SET @diChoXL = (SELECT COUNT(1) FROM docCountTemp where CategoryBusinessId = 2 AND UserCurrentId = `@userId`);
	SET @diChoXL_seen = (SELECT COUNT(1) FROM docCountTemp where CategoryBusinessId = 2 AND UserCurrentId = `@userId` and UserNguoiDaXem Like CONCAT('%;',`@userId`,';%'));
	SET @diChoXL_new = (SELECT COUNT(1) FROM docCountTemp where CategoryBusinessId = 2 AND UserCurrentId = `@userId` and UserNguoiDaXem not Like CONCAT('%;',`@userId`,';%'));
	SET @theoDoi = (SELECT COUNT(1) FROM docCountTemp where UserCurrentId != `@userId`);
	SET @thongbao = (SELECT COUNT(1) FROM documentcopy where UserThongBao like CONCAT('%;',`@userId`,';%') LIMIT 1000);
	SET @uyQuyen = (SELECT COUNT(1) FROM `document` as `d` 
									INNER JOIN `documentcopy` as `dc` ON `d`.`DocumentId` = `dc`.`DocumentId`
									INNER JOIN `authorize` as `au` on `au`.AuthorizeUserId = `dc`.UserCurrentId AND `au`.`Active` = 1
									WHERE  `dc`.`DocumentCopyType` in (1, 2, 4, 32, 64)
										AND (`au`.`DocTypeId` IS NULL OR `au`.`DocTypeId` ='' or  `au`.DocTypeId  like CONCAT('%"', `d`.`DocTypeId`,'"%'))
										AND `dc`.UserCurrentId = `au`.AuthorizeUserId 
										AND `dc`.`Status` IN (2, 16) AND d.CategoryBusinessId != 4 
										AND NOW() >= `au`.`DateBegin` 
										AND NOW() <= `au`.`DateEnd` 
										AND  `au`.`AuthorizeUserId` in (`@authorizeIds`) and `au`.`AuthorizedUserId` = `@userId`);

	SET @result = CONCAT(@denChoXL,',', @denChoXL_seen,',', @denChoXL_new,',', @diChoXL,',', @diChoXL_seen,',', @diChoXL_new,',', @theoDoi,',', @uyQuyen,',', @thongbao);

	SELECT @result;

END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `mobile_documentannoucements`
-- ----------------------------
DROP PROCEDURE IF EXISTS `mobile_documentannoucements`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `mobile_documentannoucements`(IN `@userId` int)
BEGIN
	#Routine body goes here...
	SELECT 
			`d`.`DocumentId`, 
			`dc`.`DocumentCopyId` as DocumentCopyId, 
			`d`.`Compendium`, 
			`d`.`DocCode`, 
			`d`.`Organization`,
			`d`.`InOutCode`,
			`d`.`CategoryName`,
			`d`.`UrgentId`,
			`d`.`CategoryBusinessId`,
			if(INSTR(dc.UserNguoiDaXem, CONCAT(';',`@userId`,';')) > 0, 1, 0) as `IsViewed`,
			dc.UserCurrentName as UserName,
			dc.UserCurrentId as UserId,
			dc.DateReceived,
			"" as `DateOverdue`, 
			"" as  `DateAppointed`,
			1 as hasUpdateViewed
	FROM `document` as `d` 
	INNER JOIN `documentcopy` as `dc` ON `d`.`DocumentId` = `dc`.`DocumentId` 
	WHERE `d`.`status` not in (1, 8, 32)
		AND `d`.CategoryBusinessId != 4
		AND dc.UserThongBao LIKE CONCAT('%;',`@userId`,';%')
	ORDER BY `dc`.`DateReceived` DESC LIMIT 150;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `mobile_documentauthorizeds`
-- ----------------------------
DROP PROCEDURE IF EXISTS `mobile_documentauthorizeds`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `mobile_documentauthorizeds`(IN `@userId` int,IN `@authorizeIds` varchar(200))
BEGIN
	#Routine body goes here...
	SELECT 
			`d`.`DocumentId`, 
			`dc`.`DocumentCopyId` as DocumentCopyId, 
			`d`.`Compendium`, 
			`d`.`DocCode`, 
			`d`.`Organization`,
			`d`.`InOutCode`,
			`d`.`CategoryName`,
			`d`.`UrgentId`,
			`d`.`CategoryBusinessId`,
			if(INSTR(dc.UserNguoiDaXem, CONCAT(';',`@userId`,';')) > 0, 1, 0) as `IsViewed`,
			dc.UserCurrentName as UserName,
			dc.UserCurrentId as UserId,
			GetDayDocumentProcess_new(dc.DateOverdue, d.Status, d.IsSuccess, d.IsReturned) as `DateOverdue`, 
			GetDayDocumentProcess_new(d.DateAppointed, d.Status, d.IsSuccess, d.IsReturned) as  `DateAppointed`
	FROM `document` as `d` 
	INNER JOIN `documentcopy` as `dc` ON `d`.`DocumentId` = `dc`.`DocumentId`
	INNER JOIN `authorize` as `au` on `au`.AuthorizeUserId = `dc`.UserCurrentId AND `au`.`Active` = 1
	WHERE  `dc`.`DocumentCopyType` in (1, 2, 4, 32, 64)
		AND (`au`.`DocTypeId` IS NULL OR `au`.`DocTypeId` ='' or  `au`.DocTypeId  like CONCAT('%"', `d`.`DocTypeId`,'"%'))
		AND `dc`.UserCurrentId = `au`.AuthorizeUserId 
		AND `dc`.`Status` IN (2, 16) AND d.CategoryBusinessId != 4 
		AND NOW() >= `au`.`DateBegin` 
		AND NOW() <= `au`.`DateEnd` 
		AND  `au`.`AuthorizeUserId` in (`@authorizeIds`) and `au`.`AuthorizedUserId` = `@userId`
	ORDER BY `dc`.`DateReceived` DESC;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `mobile_documentfinisheds`
-- ----------------------------
DROP PROCEDURE IF EXISTS `mobile_documentfinisheds`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `mobile_documentfinisheds`(IN `@userId` int)
BEGIN
	#Routine body goes here...
	SELECT 
			`d`.`DocumentId`, 
			`dc`.`DocumentCopyId` as DocumentCopyId, 
			`d`.`Compendium`, 
			`d`.`DocCode`, 
			`d`.`Organization`,
			`d`.`InOutCode`,
			`d`.`CategoryName`,
			`d`.`UrgentId`,
			`d`.`CategoryBusinessId`,
			1 as `IsViewed`,
			dc.UserCurrentName as UserName,
			dc.UserCurrentId as UserId,
			"" as `DateOverdue`, 
			"" as  `DateAppointed`
	FROM `document` as `d` 
	INNER JOIN `documentcopy` as `dc` ON `d`.`DocumentId` = `dc`.`DocumentId` 
	WHERE `dc`.`Status` = 4
		AND `d`.CategoryBusinessId != 4
		AND dc.UserNguoiThamGia LIKE CONCAT('%;',`@userId`,';%')
	ORDER BY `dc`.`DateReceived` DESC LIMIT 150;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `mobile_documentprocessings`
-- ----------------------------
DROP PROCEDURE IF EXISTS `mobile_documentprocessings`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `mobile_documentprocessings`(IN `@userId` int,IN `@categoryBusinessId` int, IN `@viewed` int)
BEGIN
	#Routine body goes here...
	SELECT 
			`d`.`DocumentId`, 
			`dc`.`DocumentCopyId` as DocumentCopyId, 
			`d`.`Compendium`, 
			`d`.`DocCode`, 
			`d`.`Organization`,
			`d`.`InOutCode`,
			`d`.`CategoryName`,
			`d`.`UrgentId`,
			`dc`.`Status`,
			`dc`.`DateReceived`,
			`@categoryBusinessId` as CategoryBusinessId,
			if(INSTR(dc.UserNguoiDaXem, CONCAT(';',`@userId`,';')) > 0, 1, 0) as `IsViewed`,
			`@viewed` as view1,
			dc.LastUserComment as UserName,
			dc.LastUserIdComment as UserId,
			dc.UserCurrentId,
			1 as hasUpdateViewed,
			GetDayDocumentProcess_new(dc.DateOverdue, d.Status, d.IsSuccess, d.IsReturned) as `DateOverdue`, 
			GetDayDocumentProcess_new(d.DateAppointed, d.Status, d.IsSuccess, d.IsReturned) as  `DateAppointed`
	FROM `document` as `d` 
	INNER JOIN `documentcopy` as `dc` ON `d`.`DocumentId` = `dc`.`DocumentId` 
	WHERE `dc`.`Status` = 2 
		and `d`.`status` not in (1, 4, 8, 32)
		AND `d`.CategoryBusinessId = `@categoryBusinessId`
		AND `dc`.`UserCurrentId` = `@userId` 
		AND `dc`.`DocumentCopyType` in (1, 2, 4, 32, 64)
		AND (`@viewed` is null OR (if(INSTR(dc.UserNguoiDaXem, CONCAT(';',`@userId`,';')) > 0, 1, 0) = `@viewed`))
	ORDER BY `dc`.`DateReceived` DESC;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `mobile_documentsents`
-- ----------------------------
DROP PROCEDURE IF EXISTS `mobile_documentsents`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `mobile_documentsents`(IN `@userId` int)
BEGIN
	#Routine body goes here...
	SELECT 
			`d`.`DocumentId`, 
			`dc`.`DocumentCopyId` as DocumentCopyId, 
			`d`.`Compendium`, 
			`d`.`DocCode`, 
			`d`.`Organization`,
			`d`.`InOutCode`,
			`d`.`CategoryName`,
			`d`.`UrgentId`,
			`dc`.`Status`,
			`dc`.`DateReceived`,
			`d`.`CategoryBusinessId`,
			if(INSTR(dc.UserNguoiDaXem, CONCAT(';',`@userId`,';')) > 0, 1, 0) as `IsViewed`,
			dc.UserCurrentName as UserName,
			dc.UserCurrentId as UserId,
			dc.UserCurrentId,
			"" as `DateOverdue`, 
			"" as  `DateAppointed`
	FROM `document` as `d` 
	INNER JOIN `documentcopy` as `dc` ON `d`.`DocumentId` = `dc`.`DocumentId` 
	WHERE `dc`.`Status` = 2 
		and `d`.`status` not in (1, 8, 4, 32)
		AND `d`.CategoryBusinessId != 4
		AND dc.UserNguoiThamGia LIKE CONCAT('%;',`@userId`,';%')
		AND `dc`.`UserCurrentId` != `@userId` 
		AND `dc`.`DocumentCopyType` in (1, 2, 4, 32, 64)
	ORDER BY `dc`.`DateReceived` DESC;
END
;;
DELIMITER ;
