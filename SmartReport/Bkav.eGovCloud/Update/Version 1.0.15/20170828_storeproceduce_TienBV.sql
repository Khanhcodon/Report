/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : egovls_tnmt

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2017-08-28 16:34:04
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `QuickSearch`
-- ----------------------------
DROP PROCEDURE IF EXISTS `QuickSearch`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `QuickSearch`(IN `@SearchTerm` varchar(1000),IN `@UserId` int(11))
BEGIN
-- call SearchProceduce("dang ky", NULL, NULL, NULL, 3907, NULL, NULL, NULL, NULL, NULL, null, null, null, null, null, null, null, null);

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
		d.Organization,
		dc.DocumentCopyType,
		null as IsViewed,
		`@UserId` as UserId,
		0 as StoreId
	FROM documentcopy dc
	INNER JOIN document d on d.DocumentId = dc.DocumentId
	LEFT JOIN store st on st.StoreId = d.StoreId
	WHERE 
		d.`Status` != 8 AND d.`Status` != 1
		AND dc.DocumentCopyType != 8
		AND (`@UserId` IS NULL OR dc.DocumentUsers LIKE CONCAT('%;',`@UserId`,';%'))		
		AND `@SearchTerm` IS NOT NULL 
		AND CONCAT(COALESCE(`d`.`Compendium`,''), ' ', COALESCE(`d`.`Compendium2`,''), ' ', COALESCE(`d`.`CitizenName`,''), ' ', COALESCE(`d`.`DocCode`,''), ' ', COALESCE(`d`.`InOutCode`,'')) LIKE CONCAT('%', `@SearchTerm`, '%')
	GROUP BY d.DocumentId
	ORDER BY dc.DateReceived DESC
	limit 300;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `SearchProceduce`
-- ----------------------------
DROP PROCEDURE IF EXISTS `SearchProceduce`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SearchProceduce`(IN `@SearchTerm` varchar(1000),IN `@CategoryBusinessId` int(11),IN `@docCode` varchar(100),IN `@InoutCode` varchar(100),IN `@UserId` int(11),IN `@CategoryId` int(11),IN `@StoreId` int(11),IN `@StorePrivateId` int(11),IN `@UserCurrentId` int(11),IN `@UserSuccessId` int(11),IN `@UserCreatedId` int(11),IN `@UrgentId` int(11),IN `@InOutPlace` varchar(300),IN `@OrganizationCreate` varchar(300),IN `@FromDate` datetime,IN `@ToDate` datetime,IN `@HasOnlyGetRootDocument` bit(1),IN `@Skip` int(11),IN `@Take` int(11))
BEGIN
-- call SearchProceduce("tra loi", NULL, NULL, NULL, 3907, NULL, NULL, NULL, NULL, NULL, null, null, null, null, null, null, null, null);

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
		d.Organization,
		dc.DocumentCopyType,
		null as IsViewed,
		`@UserId` as UserId,
		sd.StoreId
	FROM documentcopy dc
	INNER JOIN document d on d.DocumentId = dc.DocumentId
	LEFT JOIN store_doc sd on (`@StoreId` IS NOT NULL AND sd.DocumentId = d.DocumentId and sd.StoreId = `@StoreId`)
	LEFT JOIN storeprivate_documentcopy spd on (`@StorePrivateId` IS NOT NULL AND spd.DocumentId = d.DocumentId and spd.StorePrivateId = `@StorePrivateId`)
	WHERE 
		d.`Status` != 8 
		AND d.`Status` != 1
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
		AND (`@StoreId` IS NULL OR sd.StoreId is NOT NULL)
		AND (`@UserId` IS NULL OR (dc.DocumentUsers LIKE CONCAT('%;',`@UserId`,';%')))		
		AND (`@StorePrivateId` IS NULL OR spd.StorePrivateId is NOT NULL)
		AND (`@SearchTerm` IS NULL Or (CONCAT(COALESCE(`d`.`Compendium2`,''), ' ', COALESCE(`d`.`CitizenName`,'')) LIKE CONCAT('%', `@SearchTerm`, '%')))
		AND (`@docCode` is null or (d.DocCode LIKE CONCAT('%', `@docCode`, '%')))
		AND (`@InoutCode` is null or (d.InOutCode LIKE CONCAT('%', `@InoutCode`, '%')))
	GROUP BY d.DocumentId
	ORDER BY dc.DateReceived DESC
	limit 300;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `statistic`
-- ----------------------------
DROP PROCEDURE IF EXISTS `statistic`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `statistic`(IN `@year` int(11),IN `@userId` int(11))
BEGIN
	#Routine body goes here...
-- Call statisic(2017, 0)
	SELECT
	dc.DocumentCopyId,
	dc.DocumentId,
	d.DocTypeId, 
	dt.DocTypeName,
	d.CategoryId,
	c.CategoryName,
	dc.DocumentCopyType,
	d.DocCode,
	d.InOutCode,
	d.Compendium,
	d.CategoryBusinessId,
	d.Address,
	d.CitizenName,
	d.DateAppointed,
	d.DateCreated,
	dc.DateFinished,
	d.DateRequireSupplementary,
	d.DateReturned,
	d.DateSuccess,
	d.InOutPlace,
	d.Original,
	d.Phone,
	d.Status,
	d.UserCreatedId,
	dc.UserCurrentId,
	d.UserSuccessId,
	d.Organization,
	d.IsSuccess,
	dc.History,
	dc.DateReceived,
	d.ExpireProcess
FROM documentcopy dc
JOIN document d on d.DocumentId = dc.DocumentId
LEFT JOIN doctype dt on dt.DocTypeId = d.DocTypeId
LEFT JOIN category c on c.CategoryId = d.CategoryId
WHERE !d.IsConverted
	AND d.`Status` NOT IN (1, 8)
	AND dc.DocumentCopyType in (1, 2, 64)
	AND d.DocTypeId IS NOT NULL
	AND d.DocTypeId != '00000000-0000-0000-0000-000000000000'
	AND (`@userId`  = 0 || dc.UserCurrentId != `@userId` )
	AND YEAR(d.DateCreated) = `@year`;
END
;;
DELIMITER ;
