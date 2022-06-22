-- Câu truy vấn dữ liệu
Select 
	d.DocCode,
	d.CitizenName,
	d.CitizenInfo,
	CONVERT(VARCHAR(20), d.DateCreated, 113) as DateCreated,
	CONVERT(VARCHAR(20), d.DateAppointed, 113) as DateAppointed,
	#groupValue as GroupValue,
	#groupName as GroupName
From document AS d
	JOIN doctype dt on dt.DocTypeId = d.DocTypeId
	JOIN docfield df on dt.DocFieldId = df.DocFieldId
WHERE d.CategoryBusinessId = 4 -- La hsmc
	AND d.[Status] NOT IN (1, 8) -- Khong phai la loai bo va du theo
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
    AND (@treeGroupValue = '' or @treeGroupValue is null  or #treeGroup = @treeGroupValue)
	

-- Truy vấn lấy nhóm
SELECT 
	#groupValue as GroupValue,
	#groupName as GroupName,
	COUNT(#groupValue) as Total
From document AS d
	JOIN doctype dt on dt.DocTypeId = d.DocTypeId
	JOIN docfield df on dt.DocFieldId = df.DocFieldId
WHERE d.CategoryBusinessId = 4 -- La hsmc
	AND d.[Status] NOT IN (1, 8) -- Khong phai la loai bo va du theo
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
    AND (@treeGroupValue = ''   or @treeGroupValue is null  or #treeGroup = @treeGroupValue)
    group by #groupValue


-- Câu truy vấn lấy tổng số hồ sơ trong báo cáo
Select 
	Count(1)
From document AS d
	JOIN doctype dt on dt.DocTypeId = d.DocTypeId
	JOIN docfield df on dt.DocFieldId = df.DocFieldId
WHERE d.CategoryBusinessId = 4 -- La hsmc
	AND d.[Status] NOT IN (1, 8) -- Khong phai la loai bo va du theo
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
    AND (@treeGroupValue = ''   or @treeGroupValue is null  or #treeGroup = @treeGroupValue)


-- Câu truy vấn lấy số văn bản/hồ sơ quá hạn
SELECT 	COUNT(dc.DocumentCopyId)
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	LEFT JOIN docfinish AS dfi ON dc.DocumentCopyId = dfi.DocumentCopyId 
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df ON dt.DocFieldId = df.DocFieldId
	LEFT OUTER JOIN documentpublish AS dp ON dp.DocCopyId = dc.DocumentCopyId
	JOIN dailyprocess AS dap ON dap.DocumentId = d.DocumentId
WHERE 
    d.[Status] = 2
    AND  d.CategoryBusinessId = 4
    and dap.ProcessType=1
    AND DATEDIFF(DAY, d.DateAppointed, GETDATE()) < 0 
	AND dfi.UserId = @userId
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
	AND ( #treeGroup = @treeGroupValue  or @treeGroupValue = ''   or @treeGroupValue is null)


-- Câu truy vấn lấy số văn bản/hồ sơ đã xử lý
SELECT 	COUNT(dc.DocumentCopyId)
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	LEFT JOIN docfinish AS dfi ON dc.DocumentCopyId = dfi.DocumentCopyId 
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df ON dt.DocFieldId = df.DocFieldId
	LEFT OUTER JOIN documentpublish AS dp ON dp.DocCopyId = dc.DocumentCopyId
	JOIN dailyprocess AS dap ON dap.DocumentId = d.DocumentId

WHERE 
    d.[Status]= 4  
	AND  d.CategoryBusinessId = 4
    and dap.ProcessType=1
	AND dfi.UserId = @userId
	AND DATEDIFF(DAY, d.DateCreated, @from) > 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) < 0
	AND (#treeGroup = @treeGroupValue  or @treeGroupValue = ''   or @treeGroupValue is null)


-- Câu truy vấn dữ liệu
SELECT * FROM(
Select 
	d.DocCode,
	d.CitizenName,
	d.CitizenInfo,
	CONVERT(VARCHAR(20), d.DateCreated, 113) as DateCreated,
	CONVERT(VARCHAR(20), d.DateAppointed, 113) as DateAppointed,
	#groupValue as GroupValue,
	#groupName as GroupName,
   ROW_NUMBER() OVER (ORDER BY d.#sortBy #isDesc) AS idx 
From document AS d
	JOIN doctype dt on dt.DocTypeId = d.DocTypeId
	JOIN docfield df on dt.DocFieldId = df.DocFieldId
WHERE d.CategoryBusinessId = 4 -- La hsmc
	AND d.[Status] NOT IN (1, 8) -- Khong phai la loai bo va du theo
	AND DATEDIFF(DAY, d.DateCreated, @from) >= 0 
	AND DATEDIFF(DAY, d.DateCreated, @to) <= 0
	AND (@groupValue = '' or #groupValue = @groupValue)
    AND (@treeGroupValue = ''   or @treeGroupValue is null  or #treeGroup = @treeGroupValue))
AS tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take