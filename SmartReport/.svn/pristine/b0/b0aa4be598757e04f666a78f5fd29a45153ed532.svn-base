-- Câu truy vấn dữ liệu
SELECT
    DISTINCT(CAST(dc.DocumentCopyId as NVARCHAR(1000))),
	d.Compendium, 					
	d.DocCode,							
	d.InOutPlace,
	d.DateCreated,
	u.FullName as CurrentUser,
	d.DateAppointed,
	u1.Fullname as UserCreated,
	d.[Status],
	dbo.fnGetDocumentStatus(d.[Status]) as StatusName,
	dp.AddressName as [Address],
	dp.DateSent,
	dt.DocTypeName,
	DATEDIFF(DAY, d.DateAppointed, GETDATE()) AS [DateDiff],
	d.InOutCode,
	df2.DocFieldName,
	u2.FullName as UserSuccess,
	u3.FullName as ProcessUser
	FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId  and df.UserId = dc.UserCurrentId
	JOIN category ON d.CategoryId = category.CategoryId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId

WHERE 
	d.[Status] != 8 
	AND d.[Status] != 1
    AND d.[Status] != 4
	AND d.CategoryBusinessId = 1
	AND dc.DocumentCopyType = 1
	AND DATEDIFF(DAY, d.DateAppointed, GETDATE()) <= 0 
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
	ORDER BY d.DateCreated DESC

-- Truy vấn lấy nhóm
SELECT 
	#groupValue as GroupValue,
	#groupName as GroupName,
	COUNT(#groupValue) as Total
	FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId  and df.UserId = dc.UserCurrentId
	JOIN category ON d.CategoryId = category.CategoryId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId
WHERE 
	d.[Status] != 8 
	AND d.[Status] != 1
    AND d.[Status] != 4
	AND d.CategoryBusinessId = 1
	AND dc.DocumentCopyType = 1
	AND DATEDIFF(DAY, d.DateAppointed, GETDATE()) <= 0 
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
	AND (@treeGroupValue = '' or @treeGroupValue is null or #treeGroup = @treeGroupValue)

-- Câu truy vấn lấy tổng số hồ sơ trong báo cáo
Select count(1) from(
	SELECT 
		dc.DocumentCopyId
	FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId  and df.UserId = dc.UserCurrentId
	JOIN category ON d.CategoryId = category.CategoryId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId

	WHERE 
		d.[Status] != 8 
		AND d.[Status] != 1
			AND d.[Status] != 4
		AND d.CategoryBusinessId = 1
	AND dc.DocumentCopyType = 1
		AND DATEDIFF(DAY, d.DateAppointed, GETDATE()) <= 0 
		AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
		AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
		AND (@treeGroupValue  is null or @treeGroupValue = '' or #treeGroup = @treeGroupValue)) as sum

-- Câu truy vấn dữ liệu
SELECT * FROM
	(SELECT
		dc.DocumentCopyId,
		d.Compendium, 					
		d.DocCode,							
		d.InOutPlace,
		d.DateCreated,
		u.FullName as CurrentUser,
		d.DateAppointed,
		u1.Fullname as UserCreated,
		d.[Status],
		dbo.fnGetDocumentStatus(d.[Status]) as StatusName,
		dp.AddressName as Address,
		dp.DateSent,
		dt.DocTypeName,
		DATEDIFF(DAY, d.DateAppointed, GETDATE()) AS [DateDiff],
		d.InOutCode,
		df2.DocFieldName,
		u2.FullName as UserSuccess,
		u3.FullName as ProcessUser,
		ROW_NUMBER() OVER (ORDER BY d.#sortBy #isDesc) AS idx 
	FROM document AS d
		JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
		JOIN [user] AS u ON u.UserId = dc.UserCurrentId
		JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
		JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
		JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId  and df.UserId = dc.UserCurrentId
		JOIN category ON d.CategoryId = category.CategoryId
		LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
		LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
		LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
		LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId
	WHERE 
		d.[Status] != 8 
		AND d.[Status] != 1
		AND d.[Status] != 4
		AND d.CategoryBusinessId = 1
		AND dc.DocumentCopyType = 1
		AND DATEDIFF(DAY, d.DateAppointed, GETDATE()) <= 0 
		AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
		AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
		AND (@treeGroupValue is null or @treeGroupValue = '' or #treeGroup = @treeGroupValue)
		AND (@groupValue = '' or #groupValue = @groupValue))
AS tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take
