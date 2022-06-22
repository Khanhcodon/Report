/*
Navicat MySQL Data Transfer

Source Server         : Eform
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb_copy

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-08-24 15:53:47
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `QuickSearch`
-- ----------------------------
DROP PROCEDURE IF EXISTS `QuickSearch`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `QuickSearch`(IN `@SearchTerm` varchar(1000),IN `@UserId` int(11),IN `@FromDate` datetime,IN `@ToDate` datetime, IN `@StoreIds` varchar(1000), IN `@UserAuthorizes` varchar(1000), IN `@skip` int, IN `@take` int, IN `@isMainProcess` bit, in `@hasViewAll` bit)
BEGIN
-- call SearchProceduce("dang ky", NULL, NULL, NULL, 3907, NULL, NULL, NULL, NULL, NULL, null, null, null, null, null, null, null, null);

DECLARE keyword VARCHAR(1000);
SET keyword = ParseSearchTerm(`@SearchTerm`);

SELECT SQL_CALC_FOUND_ROWS dc.DocumentCopyId, 
		d.DocumentId, 
		-- dc.DocumentCopyId,
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
		d.StoreId
		FROM documentcopy dc
		INNER JOIN document d on d.DocumentId = dc.DocumentId
		WHERE 
			d.`Status` not in (8, 32) 
			and dc.DocumentCopyType != 8
			and (`@FromDate` IS NULL OR d.DateCreated >= `@FromDate`)
			AND (`@ToDate` IS NULL OR d.DateCreated <= `@ToDate`)
			AND (d.DocCode = `@SearchTerm` Or (MATCH(d.DocCode) AGAINST(`keyword` IN BOOLEAN MODE)) OR (MATCH(d.InOutCode) AGAINST(`keyword` IN BOOLEAN MODE)) 
							Or MATCH(d.Compendium, d.CitizenName) AGAINST(`keyword` IN BOOLEAN MODE))
			AND (	`@hasViewAll` = 1 Or
						(`@StoreIds` = '' OR d.StoreId in (`@StoreIds`)) OR -- Quyền Xem sổ
						(`@UserAuthorizes` = '' OR dc.UserCurrentId in (`@UserAuthorizes`)) Or -- Ủy quyền
						(dc.UserCurrentId = `@UserId` 
							OR d.UserCreatedId = `@UserId` 
							OR dc.DocumentUsers like CONCAT('%;',`@UserId`,';%')))	
			AND (`@isMainProcess` = 0 or dc.DocumentUsers like CONCAT('%;',`@UserId`,';%'))
GROUP BY dc.DocumentCopyId
ORDER BY dc.DateReceived DESC
LIMIT `@skip`, `@take`;

SELECT FOUND_ROWS() as Total;
END
;;
DELIMITER ;
