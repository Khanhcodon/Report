-- Câu truy vấn dữ liệu
SELECT * FROM(
SELECT
	d.DocumentId,
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
	DATEDIFF(DAY, d.DateAppointed, GETDATE()) AS DiffDate,
	d.InOutCode,
	df2.DocFieldName,
	u2.FullName as UserSuccess,
	ROW_NUMBER() OVER (ORDER BY d.DateCreated DESC) AS idx 
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId

WHERE 
	d.[Status] != 8 
	AND d.[Status] != 1
	AND(@treeGroupValue = ''   or @treeGroupValue is null  or df2.DocFieldId = @treeGroupValue )
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0)
	AS tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take

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
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
WHERE 
	d.[Status] not in (1,8)
	AND(@treeGroupValue = '' or @treeGroupValue is null  or df2.DocFieldId = @treeGroupValue )
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
	AND (@treeGroupValue is null  or @treeGroupValue = '' or #treeGroup = @treeGroupValue)
    group by #groupValue

-- Câu truy vấn lấy tổng số hồ sơ trong báo cáo

SELECT  Count(1)  from (SELECT 
	dc.DocumentCopyId
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
WHERE 
	d.[Status] NOT IN (1,8)
	AND(@treeGroupValue = ''   or @treeGroupValue is null  or df2.DocFieldId = @treeGroupValue )
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
    AND (@groupValue = '' or #groupValue = @groupValue)) as sum

-- Câu truy vấn lấy số văn bản/hồ sơ quá hạn
SELECT  Count(1)  from (SELECT 
	dc.DocumentCopyId
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
WHERE 
	d.[Status] NOT IN(1,4,8)
    AND DATEDIFF(DAY, d.DateAppointed, GETDATE()) < 0 
	AND(@treeGroupValue = ''   or @treeGroupValue is null  or df2.DocFieldId = @treeGroupValue )
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
AND (@groupValue = '' or #groupValue = @groupValue)) as sum

-- Câu truy vấn lấy số văn bản/hồ sơ đã xử lý
SELECT  Count(1)  from (SELECT 
	dc.DocumentCopyId
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
WHERE 
	d.[Status]= 4
	AND(@treeGroupValue = ''   or @treeGroupValue is null  or df2.DocFieldId = @treeGroupValue )
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
	AND (@groupValue = '' or #groupValue = @groupValue)) as sum

-- Câu truy vấn dữ liệu
SELECT * FROM(
SELECT
	d.DocumentId,
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
	DATEDIFF(DAY, d.DateAppointed, GETDATE()) AS DiffDate,
	d.InOutCode,
	df2.DocFieldName,
	u2.FullName as UserSuccess,
	ROW_NUMBER() OVER (ORDER BY d.#sortBy #isDesc) AS idx 

FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId 
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
WHERE 
	d.[Status] != 8 
	AND d.[Status] != 1
	AND(@treeGroupValue = '' or @treeGroupValue is null  or df2.DocFieldId = @treeGroupValue )
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
	AND (@groupValue = '' or #groupValue = @groupValue)
    AND (@treeGroupValue = '' or @treeGroupValue is null  or #treeGroup = @treeGroupValue))
AS tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take