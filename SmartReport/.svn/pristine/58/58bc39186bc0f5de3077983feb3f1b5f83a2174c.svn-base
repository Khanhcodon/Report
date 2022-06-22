-- Câu truy vấn dữ liệu
SELECT DISTINCT(dc.DocumentCopyId),
	d.Compendium, 					
	d.DocCode,		
	d.DateArrived,
    d.DatePublished,
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
	df.UserId,
	u3.FullName as ProcessUser
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId and df.UserId = dc.UserCurrentId
	JOIN category ON d.CategoryId = category.CategoryId
	JOIN store_doc AS sd ON sd.DocumentId = d.DocumentId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId
	INNER JOIN user_department_jobtitles_position udjp on df.UserId=udjp.UserId
	INNER JOIN department dept on dept.DepartmentId = udjp.DepartmentId
WHERE 
	d.[Status] NOT IN (1,8)
	AND d.CategoryBusinessId = 1
	and dc.DocumentCopyType = 1
	AND DATEDIFF(MINUTE, d.DateCreated, @from) >= 0 
	AND DATEDIFF(MINUTE, d.DateCreated, @to) <= 0
	AND (@treeGroupValue IS NULL OR  @treeGroupValue='' or #treeGroup = @treeGroupValue)
ORDER BY DateCreated

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
	JOIN category ON d.CategoryId = category.CategoryId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId
	INNER JOIN user_department_jobtitles_position udjp on df.UserId = udjp.UserId
	INNER JOIN department dept on dept.DepartmentId = udjp.DepartmentId

WHERE 
    d.[Status] NOT IN (1,8)
    AND d.CategoryBusinessId = 1
	AND df.UserId = @userId
	AND DATEDIFF(MINUTE, d.DateCreated, @from) >= 0 
	AND DATEDIFF(MINUTE, d.DateCreated, @to) <= 0
	AND (@treeGroupValue IS NULL OR @treeGroupValue = ''  or  #treeGroup = @treeGroupValue)
group by #groupValue

-- Câu truy vấn lấy tổng số hồ sơ trong báo cáo
select count(1) from (
	SELECT dc.DocumentCopyId
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId and df.UserId = dc.UserCurrentId
	JOIN category ON d.CategoryId = category.CategoryId
	JOIN store_doc AS sd ON sd.DocumentId = d.DocumentId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId
	INNER JOIN user_department_jobtitles_position udjp on df.UserId=udjp.UserId
	INNER JOIN department dept on dept.DepartmentId = udjp.DepartmentId
WHERE 
	dc.[Status] not in(1,8)
	AND d.CategoryBusinessId = 1
	and dc.DocumentCopyType = 1
	AND DATEDIFF(MINUTE, d.DateCreated, @from) >= 0 
	AND DATEDIFF(MINUTE, d.DateCreated, @to) <= 0
	AND (@treeGroupValue IS NULL OR @treeGroupValue = '' or #treeGroup = @treeGroupValue)) as sum

-- Câu truy vấn lấy số văn bản/hồ sơ quá hạn
select count(1) from (
	SELECT dc.DocumentCopyId
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId and df.UserId = dc.UserCurrentId

	JOIN category ON d.CategoryId = category.CategoryId
	JOIN store_doc AS sd ON sd.DocumentId = d.DocumentId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId
	INNER JOIN user_department_jobtitles_position udjp on df.UserId=udjp.UserId
	INNER JOIN department dept on dept.DepartmentId = udjp.DepartmentId
WHERE 
	dc.[Status] =2
	AND d.CategoryBusinessId = 1
	and dc.DocumentCopyType = 1
	AND DATEDIFF(MINUTE, d.DateCreated, @from) >= 0 
	AND DATEDIFF(MINUTE, d.DateCreated, @to) <= 0
	AND (@treeGroupValue IS NULL OR @treeGroupValue='' or #treeGroup = @treeGroupValue)) as sum

-- Câu truy vấn lấy số văn bản/hồ sơ đã xử lý
select count(1) from (
	SELECT dc.DocumentCopyId
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId and df.UserId = dc.UserCurrentId

	JOIN category ON d.CategoryId = category.CategoryId
	JOIN store_doc AS sd ON sd.DocumentId = d.DocumentId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId
	INNER JOIN user_department_jobtitles_position udjp on df.UserId=udjp.UserId
	INNER JOIN department dept on dept.DepartmentId = udjp.DepartmentId
WHERE 
	dc.[Status] =4
	AND d.CategoryBusinessId = 1
	and dc.DocumentCopyType = 1
	AND DATEDIFF(MINUTE, d.DateCreated, @from) >= 0 
	AND DATEDIFF(MINUTE, d.DateCreated, @to) <= 0
	AND (@treeGroupValue IS NULL OR @treeGroupValue='' or #treeGroup = @treeGroupValue)) as sum

-- Câu truy vấn dữ liệu
SELECT * FROM
(
	SELECT dc.DocumentCopyId,
	d.Compendium, 					
	d.DocCode,		
	d.DateArrived,
    d.DatePublished,
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
	df.UserId,
	u3.FullName as ProcessUser,
	ROW_NUMBER() OVER (ORDER BY d.#sortBy #isDesc) AS idx 
FROM document AS d
	JOIN documentcopy AS dc ON dc.DocumentId = d.DocumentId
	JOIN [user] AS u ON u.UserId = dc.UserCurrentId
	JOIN [user] AS u1 ON u1.UserId = d.UserCreatedId
	JOIN doctype AS dt ON d.DocTypeId = dt.DocTypeId
	JOIN docfinish AS df ON dc.DocumentCopyId = df.DocumentCopyId and df.UserId = dc.UserCurrentId
	JOIN category ON d.CategoryId = category.CategoryId
	JOIN store_doc AS sd ON sd.DocumentId = d.DocumentId
	LEFT OUTER JOIN [user] AS u2 ON u2.UserId = d.UserSuccessId
	LEFT OUTER JOIN docfield AS df2 ON dt.DocFieldId = df2.DocFieldId
	LEFT OUTER JOIN doc_publish AS dp ON dp.DocumentCopyId = dc.DocumentCopyId
	LEFT OUTER JOIN [user] AS u3 ON df.UserId = u3.UserId
	INNER JOIN user_department_jobtitles_position udjp on df.UserId=udjp.UserId
	INNER JOIN department dept on dept.DepartmentId = udjp.DepartmentId
WHERE 
	d.[Status] NOT IN (1,8)
	AND d.CategoryBusinessId = 1
	and dc.DocumentCopyType = 1
	AND DATEDIFF(MINUTE, d.DateCreated, @from) >= 0 
	AND DATEDIFF(MINUTE, d.DateCreated, @to) <= 0
	AND (@treeGroupValue IS NULL OR  @treeGroupValue='' or #treeGroup = @treeGroupValue)
	AND (@groupValue = '' or #groupValue = @groupValue)
)
AS tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take