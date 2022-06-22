/*
Navicat MySQL Data Transfer

Source Server         : Eform
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb_copy

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-09-09 17:10:49
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `SearchProceduce`
-- ----------------------------
DROP PROCEDURE IF EXISTS `SearchProceduce`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SearchProceduce`(IN `@SearchTerm` varchar(1000),IN `@CategoryBusinessId` int(11),IN `@docCode` varchar(100),IN `@InoutCode` varchar(100),IN `@UserId` int(11),IN `@CategoryId` int(11),IN `@StoreId` int(11),IN `@StorePrivateId` int(11),IN `@UserCurrentId` int(11),IN `@UserSuccessId` int(11),IN `@UserCreatedId` int(11),IN `@UrgentId` int(11),IN `@UsrInDept` varchar(1000),IN `@OrganizationCreate` varchar(300),IN `@FromDate` datetime,IN `@ToDate` datetime,IN `@FromPubDate` datetime,IN `@ToPubDate` datetime, IN `@skip` int, IN `@take` int, IN `@isMainProcess` bit, in `@hasViewAll` bit, IN `@DocTypeCode` varchar(50), IN `@ReportModeId` int, IN `@DoctypeId` char(36),IN `@InOutPlace` varchar(150),IN `@UserCreatedName` varchar(150),IN `@Status` int,IN `@ActionLevel` int,IN `@TimeKey` varchar(50))
BEGIN

	DECLARE keyword VARCHAR(1000);
	DECLARE organ VARCHAR(300);
	DECLARE docCode VARCHAR(50);
	DECLARE inCode VARCHAR(50);
	DECLARE docTypeCode VARCHAR(50);

	SET keyword = ParseSearchTerm(`@SearchTerm`);
	set organ = ParseSearchTerm(`@OrganizationCreate`);
	set docCode = ParseSearchTerm(`@docCode`);
	set inCode = ParseSearchTerm(`@InoutCode`);
	SET docTypeCode = ParseSearchTerm(`@DocTypeCode`);

	Select 
			SQL_CALC_FOUND_ROWS dc.DocumentCopyId,
			d.DocumentId, 
			d.`Status`,
			if(d.CategoryBusinessId = 4, d.Compendium, d.Compendium) as Compendium, 
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
			dc.CurrentDepartmentName ,
			d.DocTypeCode,
			d.DocTypeId,
		getKyBaoCaoDocumentFull(dt.ActionLevel, d.DatePublished) as CategoryName
		FROM documentcopy dc
		INNER JOIN document d on d.DocumentId = dc.DocumentId
		INNER JOIN doctype dt on d.DocTypeId = dt.DocTypeId
		INNER JOIN reportmodes rm on rm.ReportModeId = dt.ReportModeId
		LEFT JOIN storeprivate_documentcopy spd on (`@StorePrivateId` IS NOT NULL AND spd.DocumentId = d.DocumentId and spd.StorePrivateId = `@StorePrivateId`)
		WHERE 
			 d.`Status` not in (8, 32)
			 AND dc.DocumentCopyType != 8
			 AND (`@CategoryBusinessId` IS NULL OR d.CategoryBusinessId = `@CategoryBusinessId`)
			 and (`@DocTypeCode` is NULL OR`@DocTypeCode` LIKE CONCAT('%',d.DocTypeCode,'%')COLLATE utf8_unicode_ci) 
			 -- and (`@SearchTerm` is NULL OR`@SearchTerm` LIKE CONCAT('%',d.Compendium,'%'))
			 -- AND (`@DoctypeId` is NULL OR `@DoctypeId`  LIKE CONCAT('%',d.DocTypeId,'%')COLLATE utf8_unicode_ci) 
			 AND (`@FromDate` IS NULL OR d.DateCreated >= `@FromDate`)
			 AND (`@ToDate` IS NULL OR d.DateCreated <= `@ToDate`)
			 AND (`@ReportModeId` is NULL or rm.ReportModeId = `@ReportModeId`)
			 AND (`@InOutPlace` is NULL or `@InOutPlace` LIKE CONCAT('%',dc.CurrentDepartmentName,'%'))
			 AND (`@UserCreatedName` is NULL or `@UserCreatedName` LIKE CONCAT('%',d.UserCreatedName,'%'))
			 AND (`@Status` is NULL or d.`Status` = `@Status` and dc.`Status` = `@Status`)
			 AND (`@ActionLevel` is NULL OR dt.ActionLevel = `@ActionLevel`)
			 AND (`@TimeKey` is NULL or `@TimeKey` LIKE CONCAT('%',d.TimeKey,'%'))
			 
			 and (`@SearchTerm` is NULL OR`@SearchTerm` LIKE CONCAT('%',d.Compendium,'%'))
			 -- AND (`@FromPubDate` IS NULL OR d.DatePublished >= `@FromPubDate`)
			 -- AND (`@ToPubDate` IS NULL OR d.DatePublished <= `@ToPubDate`)
			 -- AND (`@CategoryId` IS NULL OR d.CategoryId = `@CategoryId`)
			 -- AND (`@UrgentId` IS NULL OR d.UrgentId = `@UrgentId`)
			 -- AND (`@UserCurrentId` IS NULL OR dc.UserCurrentId = `@UserCurrentId`)
			 -- AND (`@UserSuccessId` IS NULL OR d.UserSuccessId = `@UserSuccessId`)
			 -- AND (`@UserCreatedId` is null OR d.UserCreatedId = `@UserCreatedId`)
			 -- AND (`@UsrInDept` IS NULL OR `@UsrInDept` LIKE CONCAT('%;',dc.UserCurrentId,';%'))
			 -- AND (`@StoreId` IS NULL OR d.StoreId = `@StoreId`)
			 -- AND (`@hasViewAll` = 1 Or d.UserCreatedId = `@UserId` OR (dc.DocumentUsers LIKE CONCAT('%;',`@UserId`,';%')))		
			 -- AND (`@isMainProcess` = 0 or dc.DocumentUsers like CONCAT('%;',`@UserId`,';%'))
			 -- AND (`@StorePrivateId` IS NULL OR spd.StorePrivateId is NOT NULL)
			 -- AND (`organ` = '' OR MATCH(d.Organization) AGAINST(`organ` in BOOLEAN MODE))
			 -- AND (`docCode` = '' OR MATCH(d.DocCode) AGAINST(`docCode` IN BOOLEAN MODE))
			 -- AND (`inCode` = '' OR MATCH(d.InOutCode) AGAINST(`inCode` IN BOOLEAN MODE))

			   -- AND (`keyword` = '' OR MATCH(d.Compendium, d.CitizenName) AGAINST(`keyword` IN BOOLEAN MODE))
		GROUP BY dc.DocumentCopyId
		ORDER BY dc.DateReceived DESC
		LIMIT `@skip`, `@take`;

	SELECT FOUND_ROWS() as Total;

	END
;;
DELIMITER ;
