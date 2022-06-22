-- Câu truy vấn dữ liệu
SELECT
   dc.DocumentCopyId, 
   d.Compendium, 
   d.DocCode, 
   d.InOutPlace, 
   d.DateCreated, 
   u.FullName AS CurrentUser, 
   d.DateAppointed, 
   u1.Fullname AS UserCreated, 
   d.Status, 
   dbo.fnGetDocumentStatus(d.Status) AS StatusName, 
   dp.Address, 
   dp.SendTime, 
   dt.DocTypeName, 
   datediff(DAY, getdate(), d.DateAppointed) AS DiffDate, 
   d.InOutCode, 
   df2.DocFieldName, 
   u2.FullName AS UserSuccess, 
   u3.FullName AS ProcessUser,
   ROW_NUMBER() OVER (ORDER BY dc.DateReceived DESC) AS idx 
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON dc.DocumentId = d.DocumentId 
      INNER JOIN [user] AS u 
      ON u.UserId = dc.UserCurrentId 
      INNER JOIN [user] AS u1 
      ON u1.UserId = d.UserCreatedId 
      INNER JOIN dbo.doctype  AS dt 
      ON d.DocTypeId = dt.DocTypeId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = dc.UserCurrentId 
      INNER JOIN dbo.category 
      ON d.CategoryId = category.CategoryId 
      LEFT OUTER JOIN [user] AS u2 
      ON u2.UserId = d.UserSuccessId 
      LEFT OUTER JOIN dbo.docfield  AS df2 
      ON dt.DocFieldId = df2.DocFieldId 
      LEFT OUTER JOIN dbo.documentpublish  AS dp 
      ON dp.DocCopyId = dc.DocumentCopyId 
      LEFT OUTER JOIN [user] AS u3 
      ON df.UserId = u3.UserId
WHERE 
   d.Status = 4 AND 
   d.CategoryBusinessId = 1 AND 
   dc.DocumentCopyType = 1 AND 
   datediff(DAY, @from, d.DateCreated) > 0 AND 
   datediff(DAY, @to, d.DateCreated) < 0 AND 
   (@treeGroupValue = '' OR @treeGroupValue IS NULL OR #treeGroup = @treeGroupValue)

-- Truy vấn lấy nhóm
SELECT 
	#groupValue as GroupValue,
	#groupName as GroupName,
	count(#groupValue) AS Total
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON dc.DocumentId = d.DocumentId 
      INNER JOIN [user] AS u 
      ON u.UserId = dc.UserCurrentId 
      INNER JOIN [user] AS u1 
      ON u1.UserId = d.UserCreatedId 
      INNER JOIN dbo.doctype  AS dt 
      ON d.DocTypeId = dt.DocTypeId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = dc.UserCurrentId 
      INNER JOIN dbo.category 
      ON d.CategoryId = category.CategoryId 
      LEFT OUTER JOIN [user] AS u2 
      ON u2.UserId = d.UserSuccessId 
      LEFT OUTER JOIN dbo.docfield  AS df2 
      ON dt.DocFieldId = df2.DocFieldId 
      LEFT OUTER JOIN dbo.documentpublish  AS dp 
      ON dp.DocCopyId = dc.DocumentCopyId 
      LEFT OUTER JOIN [user] AS u3 
      ON df.UserId = u3.UserId
WHERE 
   d.Status = 4 AND 
   d.CategoryBusinessId = 1 AND 
   dc.DocumentCopyType = 1 AND 
   datediff(DAY, @from, d.DateCreated) > 0 AND 
   datediff(DAY, @to, d.DateCreated) < 0 AND 
   (@treeGroupValue = '' OR @treeGroupValue IS NULL OR #treeGroup = @treeGroupValue)

-- Câu truy vấn lấy tổng số hồ sơ trong báo cáo
SELECT count(1)
FROM 
   (
      SELECT dc.DocumentCopyId
      FROM 
         dbo.document  AS d 
            INNER JOIN dbo.documentcopy  AS dc 
            ON dc.DocumentId = d.DocumentId 
            INNER JOIN [user] AS u 
            ON u.UserId = dc.UserCurrentId 
            INNER JOIN [user] AS u1 
            ON u1.UserId = d.UserCreatedId 
            INNER JOIN dbo.doctype  AS dt 
            ON d.DocTypeId = dt.DocTypeId 
            INNER JOIN dbo.docfinish  AS df 
            ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = dc.UserCurrentId 
            INNER JOIN dbo.category 
            ON d.CategoryId = category.CategoryId 
            LEFT OUTER JOIN [user] AS u2 
            ON u2.UserId = d.UserSuccessId 
            LEFT OUTER JOIN dbo.docfield  AS df2 
            ON dt.DocFieldId = df2.DocFieldId 
            LEFT OUTER JOIN dbo.documentpublish  AS dp 
            ON dp.DocCopyId = dc.DocumentCopyId 
            LEFT OUTER JOIN [user] AS u3 
            ON df.UserId = u3.UserId
      WHERE 
         d.Status = 4 AND 
         d.CategoryBusinessId = 1 AND 
         dc.DocumentCopyType = 1 AND 
         datediff(DAY, @from, d.DateCreated) > 0 AND 
         datediff(DAY, @to, d.DateCreated) < 0 AND 
         (@treeGroupValue = '' OR @treeGroupValue IS NULL OR #treeGroup = @treeGroupValue)
   )  AS sum

-- Câu truy vấn dữ liệu
SELECT * FROM
(SELECT 
   dc.DocumentCopyId, 
   d.Compendium, 
   d.DocCode, 
   d.InOutPlace, 
   d.DateCreated, 
   u.FullName AS CurrentUser, 
   d.DateAppointed, 
   u1.Fullname AS UserCreated, 
   d.Status, 
   dbo.fnGetDocumentStatus(d.Status) AS StatusName, 
   dp.Address, 
   dp.SendTime, 
   dt.DocTypeName, 
   datediff(DAY, getdate(), d.DateAppointed) AS DiffDate, 
   d.InOutCode, 
   df2.DocFieldName, 
   u2.FullName AS UserSuccess, 
   u3.FullName AS ProcessUser,
   ROW_NUMBER() OVER (ORDER BY d.#sortBy #isDesc) AS idx 
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON dc.DocumentId = d.DocumentId 
      INNER JOIN [user] AS u 
      ON u.UserId = dc.UserCurrentId 
      INNER JOIN [user] AS u1 
      ON u1.UserId = d.UserCreatedId 
      INNER JOIN dbo.doctype  AS dt 
      ON d.DocTypeId = dt.DocTypeId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = dc.UserCurrentId 
      INNER JOIN dbo.category 
      ON d.CategoryId = category.CategoryId 
      LEFT OUTER JOIN [user] AS u2 
      ON u2.UserId = d.UserSuccessId 
      LEFT OUTER JOIN dbo.docfield  AS df2 
      ON dt.DocFieldId = df2.DocFieldId 
      LEFT OUTER JOIN dbo.documentpublish  AS dp 
      ON dp.DocCopyId = dc.DocumentCopyId 
      LEFT OUTER JOIN [user] AS u3 
      ON df.UserId = u3.UserId
WHERE 
   d.Status = 4 AND 
   d.CategoryBusinessId = 1 AND 
   dc.DocumentCopyType = 1 AND 
   datediff(DAY, @from, d.DateCreated) > 0 AND 
   datediff(DAY, @to, d.DateCreated) < 0 AND 
   (@treeGroupValue = '' OR @treeGroupValue IS NULL OR #treeGroup = @treeGroupValue) AND 
   (@groupValue = '' OR #groupValue = @groupValue)) 
AS tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take