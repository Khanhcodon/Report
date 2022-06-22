-- Báo cáo động: Câu truy vấn dữ liệu
SELECT * FROM(
SELECT
	d.Compendium, 					
	d.DocCode,							
	d.InOutPlace,
	d.DateCreated,
	u.FullName as CurrentUser,
	d.DateAppointed,
	u1.Fullname as UserCreated,
	d.[Status],
	dbo.fnGetDocumentStatus(d.[Status]) as StatusName,
	dp.[Address],
	dp.SendTime,
	dt.DocTypeName,
	DATEDIFF(DAY, d.DateAppointed, GETDATE()) AS DiffDate,
	d.InOutCode,
	df2.DocFieldName,
	u2.FullName as UserSuccess,
	df.UserId,
	u3.FullName as ProcessUser,
	ROW_NUMBER() OVER (ORDER BY d.DateCreated DESC) as idx
FROM document AS d
JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
JOIN [user] AS u ON u.UserId = dc.UserCurrentId
JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 
JOIN category ON d.CategoryId = category.CategoryId
LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
LEFT OUTER JOIN documentpublish AS dp ON dp.DocCopyId = dc.DocumentCopyId
LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId

WHERE 
	d.[Status] != 8 
	AND d.[Status] != 1
	AND df.UserId = @treeGroupValue
	AND DATEDIFF(DAY, d.DateCreated, @from) > 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) < 0
) as tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take

-- Mẫu xem thống kê: Truy vấn nhóm
SELECT 
	COUNT(dc.DocumentCopyId)
FROM document AS d
JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
JOIN [user] AS u ON u.UserId = dc.UserCurrentId
JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 

JOIN category ON d.CategoryId = category.CategoryId
LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
LEFT OUTER JOIN documentpublish AS dp ON dp.DocCopyId = dc.DocumentCopyId

-- Mẫu xem thống kê: Câu truy vấn lấy tổng số hồ sơ trong báo cáo
SELECT 
	COUNT(dc.DocumentCopyId)
FROM document AS d
JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
JOIN [user] AS u ON u.UserId = dc.UserCurrentId
JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 

JOIN category ON d.CategoryId = category.CategoryId
LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
LEFT OUTER JOIN documentpublish AS dp ON dp.DocCopyId = dc.DocumentCopyId

WHERE 
	d.Status NOT IN (1,8)
	AND df.UserId = @treeGroupValue
	AND DATEDIFF(DAY, d.DateCreated, @from) > 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) < 0
AND (@groupValue = '' or #groupValue = @groupValue)
-- Mẫu xem thống kê: Câu truy vấn lấy số văn bản/hồ sơ quá hạn
SELECT 	COUNT(dc.DocumentCopyId)
FROM document AS d
JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
JOIN [user] AS u ON u.UserId = dc.UserCurrentId
JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 

JOIN category ON d.CategoryId = category.CategoryId
LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
LEFT OUTER JOIN documentpublish AS dp ON dp.DocCopyId = dc.DocumentCopyId

WHERE 
	d.Status NOT IN(1,4,8)
        AND DATEDIFF(DAY, d.DateAppointed, GETDATE()) < 0 
	AND df.UserId = @treeGroupValue
	AND DATEDIFF(DAY, d.DateCreated, @from) > 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) < 0
AND (@groupValue = '' or #groupValue = @groupValue)
-- Mẫu xem thống kê: Câu truy vấn lấy số văn bản/hồ sơ đã xử lý
SELECT 	COUNT(dc.DocumentCopyId)
FROM document AS d
JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
JOIN [user] AS u ON u.UserId = dc.UserCurrentId
JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 

JOIN category ON d.CategoryId = category.CategoryId
LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
LEFT OUTER JOIN documentpublish AS dp ON dp.DocCopyId = dc.DocumentCopyId

WHERE 
	d.Status= 4
	AND df.UserId = @treeGroupValue
	AND DATEDIFF(DAY, d.DateCreated, @from) > 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) < 0
AND (@groupValue = '' or #groupValue = @groupValue)
-- Mẫu xem thống kê: Câu truy vấn lấy dữ liệu
SELECT * FROM(
	SELECT
		d.Compendium, 					
		d.DocCode,							
		d.InOutPlace,
		d.DateCreated,
		u.FullName as CurrentUser,
		d.DateAppointed,
		u1.Fullname as UserCreated,
		d.[Status],
		dbo.fnGetDocumentStatus(d.[Status]) as StatusName,
		dp.[Address],
		dp.SendTime,
		dt.DocTypeName,
		DATEDIFF(DAY, d.DateAppointed, GETDATE()) AS DiffDate,
		d.InOutCode,
		df2.DocFieldName,
		u2.FullName as UserSuccess,
		df.UserId,
		u3.FullName as ProcessUser,
		ROW_NUMBER() OVER (ORDER BY #sortBy #isDesc) as idx
	FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 
	JOIN category ON d.CategoryId = category.CategoryId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN documentpublish AS dp ON dp.DocCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId

	WHERE 
		d.[Status] != 8 
		AND d.[Status] != 1
		AND df.UserId = @treeGroupValue
		AND DATEDIFF(DAY, d.DateCreated, @from) > 0 
		AND DATEDIFF(DAY, d.DateCreated, @to) < 0
		AND (@groupValue = '' or #groupValue = @groupValue)
		AND (@treeGroupValue = '' or #treeGroup = @treeGroupValue)
	) as tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take