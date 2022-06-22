DROP PROCEDURE IF EXISTS `QuickSearch`;

CREATE DEFINER = `root`@`localhost` PROCEDURE `QuickSearch`(IN `@SearchTerm` varchar(1000),IN `@UserId` int(11),IN `@FromDate` datetime,IN `@ToDate` datetime)
BEGIN
-- call SearchProceduce("dang ky", NULL, NULL, NULL, 3907, NULL, NULL, NULL, NULL, NULL, null, null, null, null, null, null, null, null);

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
			d.DateCreated,
			d.Organization,
			dc.DocumentCopyType,
			null as IsViewed,
		`@UserId` as UserId,
		0 as StoreId,
			d.UserCreatedName,
			dc.CurrentDepartmentName
	FROM documentcopy dc
	INNER JOIN document d on d.DocumentId = dc.DocumentId
	LEFT JOIN store st on st.StoreId = d.StoreId
	WHERE 
		d.`Status` != 8 
		AND d.`Status` != 1
		AND (`@FromDate` IS NULL OR d.DateCreated >= `@FromDate`)
		AND (`@ToDate` IS NULL OR d.DateCreated <= `@ToDate`)
		AND dc.DocumentCopyType != 8
		AND (`@UserId` IS NULL OR dc.DocumentUsers LIKE CONCAT('%;',`@UserId`,';%'))		
		AND `@SearchTerm` IS NOT NULL 
		AND CONCAT(COALESCE(`d`.`Compendium`,''), ' ', COALESCE(`d`.`Compendium2`,''), ' ', COALESCE(`d`.`CitizenName`,''), ' ', COALESCE(`d`.`DocCode`,''), ' ', COALESCE(`d`.`InOutCode`,'')) LIKE CONCAT('%', `@SearchTerm`, '%')
		GROUP BY d.UserCreatedId, d.DocCode
	ORDER BY dc.DateReceived DESC;
END;

