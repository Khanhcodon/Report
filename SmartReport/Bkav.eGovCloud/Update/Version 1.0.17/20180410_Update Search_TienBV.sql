/*
Navicat MySQL Data Transfer

Source Server         : Localhost
Source Server Version : 50715
Source Host           : localhost:3306
Source Database       : egovbrvt_vpub

Target Server Type    : MYSQL
Target Server Version : 50715
File Encoding         : 65001

Date: 2018-04-10 18:01:28
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `QuickSearch`
-- ----------------------------
DROP PROCEDURE IF EXISTS `QuickSearch`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `QuickSearch`(IN `@SearchTerm` varchar(1000),IN `@UserId` int(11),IN `@FromDate` datetime,IN `@ToDate` datetime, IN `@StoreIds` varchar(1000), IN `@UserAuthorizes` varchar(1000), IN `@skip` int, IN `@take` int)
BEGIN
-- call SearchProceduce("dang ky", NULL, NULL, NULL, 3907, NULL, NULL, NULL, NULL, NULL, null, null, null, null, null, null, null, null);

DECLARE keyword VARCHAR(1000);
SET keyword = ParseSearchTerm(`@SearchTerm`);

SELECT * FROM
	((Select 
		d.DocumentId, 
		dc.DocumentCopyId,
		d.`Status`,
		if(d.CategoryBusinessId = 4, d.CitizenName, d.Compendium) as Compendium, 
		d.DocCode, 
		d.InOutCode,
		dc.UserCurrentName,
		d.UserCreatedId,
		d.DatePublished, 
		d.DateArrived,
		d.DateCreated,
		dc.DateReceived,
		dc.UserCurrentId,
		d.Organization,
		dc.DocumentCopyType,
		dc.DocumentUsers, 
		d.UserCreatedName,
		d.UserSuccessName,
		dc.CurrentDepartmentName,
		d.CategoryName,
		d.StoreId,
		1000 as srank
	FROM documentcopy dc
	INNER JOIN document d on d.DocumentId = dc.DocumentId
	WHERE 
		dc.DocumentCopyType != 8
		AND (d.DocCode = `@SearchTerm` Or (MATCH(d.DocCode) AGAINST(`keyword` IN BOOLEAN MODE)) OR (MATCH(d.InOutCode) AGAINST(`keyword` IN BOOLEAN MODE)))
		AND (	`@UserId` IS NULL Or
					(`@StoreIds` = '' OR d.StoreId in (`@StoreIds`)) OR -- Quyền Xem sổ
					(`@UserAuthorizes` = '' OR dc.UserCurrentId in (`@UserAuthorizes`)) Or -- Ủy quyền
					(	dc.UserCurrentId = `@UserId` 
							OR d.UserCreatedId = `@UserId` 
							OR dc.DocumentUsers like CONCAT('%;',`@UserId`,';%')))
	)

	UNION

	(Select 
		d.DocumentId, 
		dc.DocumentCopyId,
		d.`Status`,
		if(d.CategoryBusinessId = 4, d.CitizenName, d.Compendium) as Compendium, 
		d.DocCode, 
		d.InOutCode,
		dc.UserCurrentName,
		d.UserCreatedId,
		d.DatePublished, 
		d.DateArrived,
		d.DateCreated,
		dc.DateReceived,
		dc.UserCurrentId,
		d.Organization,
		dc.DocumentCopyType,
		dc.DocumentUsers,
		d.UserCreatedName,
		d.UserSuccessName,
		dc.CurrentDepartmentName,
		d.CategoryName,
		d.StoreId,
		MATCH(d.Compendium) AGAINST(`keyword` IN BOOLEAN MODE) as srank
		FROM documentcopy dc
		INNER JOIN document d on d.DocumentId = dc.DocumentId
		WHERE 
			dc.DocumentCopyType != 8
			AND MATCH(d.Compendium) AGAINST(`keyword` IN BOOLEAN MODE)	
			AND (	`@UserId` IS NULL Or
						(`@StoreIds` = '' OR d.StoreId in (`@StoreIds`)) OR -- Quyền Xem sổ
						(`@UserAuthorizes` = '' OR dc.UserCurrentId in (`@UserAuthorizes`)) Or -- Ủy quyền
						(dc.UserCurrentId = `@UserId` 
							OR d.UserCreatedId = `@UserId` 
							OR dc.DocumentUsers like CONCAT('%;',`@UserId`,';%')))
		)) as doc
WHERE
	doc.`Status` not in (8, 32) 
	and (`@FromDate` IS NULL OR doc.DateCreated >= `@FromDate`)
	AND (`@ToDate` IS NULL OR doc.DateCreated <= `@ToDate`)
GROUP BY `doc`.DocumentCopyId
ORDER BY `doc`.DateReceived DESC, `doc`.srank desc;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `SearchProceduce`
-- ----------------------------
DROP PROCEDURE IF EXISTS `SearchProceduce`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SearchProceduce`(IN `@SearchTerm` varchar(1000),IN `@CategoryBusinessId` int(11),IN `@docCode` varchar(100),IN `@InoutCode` varchar(100),IN `@UserId` int(11),IN `@CategoryId` int(11),IN `@StoreId` int(11),IN `@StorePrivateId` int(11),IN `@UserCurrentId` int(11),IN `@UserSuccessId` int(11),IN `@UserCreatedId` int(11),IN `@UrgentId` int(11),IN `@UsrInDept` varchar(1000),IN `@OrganizationCreate` varchar(300),IN `@FromDate` datetime,IN `@ToDate` datetime,IN `@FromPubDate` datetime,IN `@ToPubDate` datetime)
BEGIN

	DECLARE keyword VARCHAR(1000);
	DECLARE organ VARCHAR(300);
	DECLARE docCode VARCHAR(50);
	DECLARE inCode VARCHAR(50);

	SET keyword = ParseSearchTerm(`@SearchTerm`);
	set organ = ParseSearchTerm(`@OrganizationCreate`);
	set docCode = ParseSearchTerm(`@docCode`);
	set inCode = ParseSearchTerm(`@InoutCode`);

	Select 
			d.DocumentId, 
			dc.DocumentCopyId,
			d.`Status`,
			if(d.CategoryBusinessId = 4, d.CitizenName, d.Compendium) as Compendium, 
			d.DocCode, 
			d.InOutCode,
			dc.UserCurrentName,
			d.UserCreatedId,
			d.DateAppointed, 
			d.DateArrived,
			d.DatePublished, 
			dc.DateReceived,
			d.Organization,
			dc.DocumentCopyType,
			dc.DocumentUsers,
			d.UserCreatedName,
			d.UserSuccessName,
			dc.CurrentDepartmentName,
			d.CategoryName
		FROM documentcopy dc
		INNER JOIN document d on d.DocumentId = dc.DocumentId
		LEFT JOIN storeprivate_documentcopy spd on (`@StorePrivateId` IS NOT NULL AND spd.DocumentId = d.DocumentId and spd.StorePrivateId = `@StorePrivateId`)
		WHERE 
			d.`Status` not in (8, 32)
			AND dc.DocumentCopyType != 8
			AND (`@CategoryBusinessId` IS NULL OR d.CategoryBusinessId = `@CategoryBusinessId`)
			AND (`@FromDate` IS NULL OR d.DateCreated >= `@FromDate`)
			AND (`@ToDate` IS NULL OR d.DateCreated <= `@ToDate`)
			AND (`@FromPubDate` IS NULL OR d.DatePublished >= `@FromPubDate`)
			AND (`@ToPubDate` IS NULL OR d.DatePublished <= `@ToPubDate`)
			AND (`@CategoryId` IS NULL OR d.CategoryId = `@CategoryId`)
			AND (`@UrgentId` IS NULL OR d.UrgentId = `@UrgentId`)
			AND (`@UserCurrentId` IS NULL OR dc.UserCurrentId = `@UserCurrentId`)
			AND (`@UserSuccessId` IS NULL OR d.UserSuccessId = `@UserSuccessId`)
			AND (`@UserCreatedId` is null OR d.UserCreatedId = `@UserCreatedId`)
			AND (`@UsrInDept` IS NULL OR `@UsrInDept` LIKE CONCAT('%;',dc.UserCurrentId,';%'))
			AND (`@StoreId` IS NULL OR d.StoreId = `@StoreId`)
			AND (`@UserId` IS NULL Or d.UserCreatedId = `@UserId` OR (dc.DocumentUsers LIKE CONCAT('%;',`@UserId`,';%')))		
			AND (`@StorePrivateId` IS NULL OR spd.StorePrivateId is NOT NULL)
			AND (`organ` = '' OR MATCH(d.Organization) AGAINST(`organ` in BOOLEAN MODE))
			AND (`docCode` = '' OR MATCH(d.DocCode) AGAINST(`docCode` IN BOOLEAN MODE))
			AND (`inCode` = '' OR MATCH(d.InOutCode) AGAINST(`inCode` IN BOOLEAN MODE))
			AND (`keyword` = '' OR MATCH(d.Compendium) AGAINST(`keyword` IN BOOLEAN MODE))
		ORDER BY dc.DateReceived DESC;
	END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for `ParseSearchTerm`
-- ----------------------------
DROP FUNCTION IF EXISTS `ParseSearchTerm`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `ParseSearchTerm`(`SearchTerm` varchar(1000)) RETURNS varchar(1000) CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	#Routine body goes here...
	
	DECLARE `result` varchar(1111);	
	DECLARE `searchWord` VARCHAR(100);
	DECLARE `tempParse` varchar(1111);	
	DECLARE spaceIdx int;
	DECLARE hasColon bit;

	IF(`SearchTerm` is null or `SearchTerm` = '') THEN
		RETURN '';
	end IF;

	SET `SearchTerm` = REPLACE(`SearchTerm`, '-', ' ');
	SET `SearchTerm` = REPLACE(`SearchTerm`, '/', ' ');	
	SET `SearchTerm` = REPLACE(`SearchTerm`, ',', '');	
	SET `SearchTerm` = TRIM(`SearchTerm`);

	SET spaceIdx = 1;
	SET result = '';
	set hasColon = 0;
		
	REPEAT
		SET tempParse = SUBSTRING_INDEX(`SearchTerm`,' ', spaceIdx);

		SET searchWord = SUBSTRING_INDEX(tempParse, ' ', -1);
		SET spaceIdx = spaceIdx + 1;

		IF(searchWord != '')
		THEN
					
				if hasColon = 1 THEN
					SET `result` = CONCAT(`result`, ' ', searchWord);
				else
					SET `result` = CONCAT(`result`, ' +', searchWord);
				end if;

				if INSTR(searchWord, '"') = 1 THEN
					SET hasColon = 1;
				END if;

				if INSTR(searchWord, '"') = CHAR_LENGTH(searchWord) THEN
					SET hasColon = 0;
				END if;
		END IF;

	UNTIL tempParse = `SearchTerm`
	END REPEAT;

	RETURN TRIM(result);
END
;;
DELIMITER ;
