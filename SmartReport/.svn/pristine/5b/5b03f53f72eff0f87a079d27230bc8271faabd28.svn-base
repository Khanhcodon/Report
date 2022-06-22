DROP PROCEDURE IF EXISTS `SearchProceduce`;

CREATE DEFINER = `root`@`localhost` PROCEDURE `SearchProceduce`(IN `@SearchTerm` varchar(1000),IN `@CategoryBusinessId` int(11),IN `@docCode` varchar(100),IN `@InoutCode` varchar(100),IN `@UserId` int(11),IN `@CategoryId` int(11),IN `@StoreId` int(11),IN `@StorePrivateId` int(11),IN `@UserCurrentId` int(11),IN `@UserSuccessId` int(11),IN `@UserCreatedId` int(11),IN `@UrgentId` int(11),IN `@UsrInDept` varchar(1000),IN `@OrganizationCreate` varchar(300),IN `@FromDate` datetime,IN `@ToDate` datetime,IN `@FromPubDate` datetime,IN `@ToPubDate` datetime,IN `@HasOnlyGetRootDocument` bit(1))
BEGIN
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
			sd.StoreId,
			d.UserCreatedName,
			dc.CurrentDepartmentName
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
			AND (`@FromPubDate` IS NULL OR d.DatePublished >= `@FromPubDate`)
			AND (`@ToPubDate` IS NULL OR d.DatePublished <= `@ToPubDate`)
			AND (`@CategoryId` IS NULL OR d.CategoryId = `@CategoryId`)
			AND (`@UrgentId` IS NULL OR d.UrgentId = `@UrgentId`)
			AND (`@UserCurrentId` IS NULL OR dc.UserCurrentId = `@UserCurrentId`)
			AND (`@UserSuccessId` IS NULL OR d.UserSuccessId = `@UserSuccessId`)
			AND (`@UserCreatedId` is null OR d.UserCreatedId = `@UserCreatedId`)
			AND (`@UsrInDept` IS NULL OR `@UsrInDept` LIKE CONCAT('%;',dc.UserCurrentId,';%'))
			AND (`@OrganizationCreate` IS null OR d.Organization = `@OrganizationCreate`)
			AND (`@StoreId` IS NULL OR sd.StoreId is NOT NULL)
			AND (`@UserId` IS NULL OR (dc.DocumentUsers LIKE CONCAT('%;',`@UserId`,';%')))		
			AND (`@StorePrivateId` IS NULL OR spd.StorePrivateId is NOT NULL)
			AND (`@SearchTerm` IS NULL Or (CONCAT(COALESCE(`d`.`Compendium2`,''), ' ', COALESCE(`d`.`CitizenName`,'')) LIKE CONCAT('%', `@SearchTerm`, '%')))
			AND (`@docCode` is null or (d.DocCode LIKE CONCAT('%', `@docCode`, '%')))
			AND (`@InoutCode` is null or (d.InOutCode LIKE CONCAT('%', `@InoutCode`, '%')))
		GROUP BY d.UserCreatedId, d.DocCode
		ORDER BY dc.DateReceived DESC;
	END;;

