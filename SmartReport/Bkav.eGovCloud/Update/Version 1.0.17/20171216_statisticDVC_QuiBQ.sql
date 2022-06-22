BEGIN
	#Routine body goes here...
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
	d.DocFieldIds,
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

