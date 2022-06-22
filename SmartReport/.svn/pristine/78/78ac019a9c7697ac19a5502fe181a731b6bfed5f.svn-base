/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50540
Source Host           : localhost:3306
Source Database       : egovbrvt_soyt

Target Server Type    : MYSQL
Target Server Version : 50540
File Encoding         : 65001

Date: 2016-11-04 13:40:01
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `SearchProceduce`
-- ----------------------------
DROP PROCEDURE IF EXISTS `SearchProceduce`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `SearchProceduce`(IN `@SearchTerm` varchar(1000),IN `@CategoryBusinessId` int(11),IN `@docCode` varchar(100),IN `@InoutCode` varchar(100),IN `@UserId` int(11),IN `@CategoryId` int(11),IN `@StoreId` int(11),IN `@UserCurrentId` int(11),IN `@UserSuccessId` int(11),IN `@UserCreatedId` int(11),IN `@UrgentId` int(11),IN `@InOutPlace` varchar(300),IN `@OrganizationCreate` varchar(300),IN `@FromDate` datetime,IN `@ToDate` datetime,IN `@HasOnlyGetRootDocument` bit(1),IN `@Skip` int(11),IN `@Take` int(11))
BEGIN
-- call SearchProceduce("dang ky", NULL, NULL, NULL, 3907, NULL, NULL, NULL, NULL, NULL, null, null, null, null, null, null, null, null);
Select 
		d.DocumentId, 
		dc.DocumentCopyId, 
		d.`Status`,
		if(d.CategoryBusinessId = 4, d.CitizenName, d.Compendium) as Compendium, 
		d.DocCode, 
		d.InOutCode,
		CONCAT(uc.FullName," (", uc.Username, ")") as CurrentUsername,
		CONCAT(ucreate.FullName," (", ucreate.Username, ")") as UserCreated,
		d.DateAppointed, 
		d.DateCreated,
		dc.DocumentCopyType
	FROM documentcopy dc
	INNER JOIN document d on d.DocumentId = dc.DocumentId
	INNER JOIN `user` uc on uc.UserId = dc.UserCurrentId
	INNER JOIN `user` ucreate on ucreate.UserId = d.UserCreatedId
	LEFT JOIN docfinish df on (@UserId IS NOT NULL AND df.DocumentCopyId = dc.DocumentCopyId AND df.UserId = `@UserId`)
	LEFT JOIN store_doc sd on (@StoreId IS NOT NULL AND sd.DocumentId = d.DocumentId and sd.StoreId = `@StoreId`)
	WHERE 
		d.`Status` != 8 AND d.`Status` != 1
		AND (`@CategoryBusinessId` IS NULL OR d.CategoryBusinessId = `@CategoryBusinessId`)
		AND (`@FromDate` IS NULL OR d.DateCreated >= `@FromDate`)
		AND (`@ToDate` IS NULL OR d.DateCreated <= `@ToDate`)
		AND (`@CategoryId` IS NULL OR d.CategoryId = `@CategoryId`)
		AND (`@UrgentId` IS NULL OR d.UrgentId = `@UrgentId`)
		AND (`@UserCurrentId` IS NULL OR dc.UserCurrentId = `@UserCurrentId`)
		AND (`@UserSuccessId` IS NULL OR d.UserSuccessId = `@UserSuccessId`)
		AND (`@UserCreatedId` is null OR d.UserCreatedId = `@UserCreatedId`)
		AND (`@InOutPlace` IS NULL OR d.InOutPlace = `@InOutPlace`)
		AND (`@OrganizationCreate` IS null OR d.Organization = @OrganizationCreate)		
		AND (`@SearchTerm` IS NULL OR d.Compendium2 LIKE CONCAT('%', `@SearchTerm`, '%'))
		AND (`@docCode` IS NULL or d.DocCode LIKE CONCAT('%', `@docCode`, '%'))
		AND (`@InoutCode` IS NULL or d.InOutCode LIKE CONCAT('%', `@InoutCode`, '%'))
	GROUP BY d.DocumentId
	ORDER BY d.DocCode;

select `@docCode`;
END
;;
DELIMITER ;
