--Truy vấn lấy nhóm
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
      INNER JOIN dbo.store_doc  AS sd 
      ON sd.DocumentId = d.DocumentId 
      LEFT OUTER JOIN [user] AS u2 
      ON u2.UserId = d.UserSuccessId 
      LEFT OUTER JOIN dbo.docfield  AS df2 
      ON dt.DocFieldId = df2.DocFieldId 
      LEFT OUTER JOIN dbo.documentpublish  AS dp 
      ON dp.DocCopyId = dc.DocumentCopyId 
      LEFT OUTER JOIN [user] AS u3 
      ON df.UserId = u3.UserId
WHERE 
   d.Status NOT IN ( 1, 8 ) AND 
   sd.StoreId = @treeGroupValue AND 
   dc.DocumentCopyType = 1 AND 
   datediff(DAY, @from, d.DateCreated) > 0 AND 
   datediff(DAY, @to, d.DateCreated) < 0 AND 
   (@treeGroupValue = '' or #treeGroup = @treeGroupValue)
GROUP BY #groupValue

