/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   dc.LastComment AS LastComment, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   u.FullName AS UserCurrentFullName, 
   u.FirstName AS UserCurrentFirstName, 
   dc.UserCurrentId, 
   dc.DateOverdue, 
   datediff(DAY, dc.DateOverdue, getdate()) AS NumberDayOverdue, 
   datediff(DAY, d.DateAppointed, getdate()) AS NumberDayAppointed, 
   dc.WorkflowId, 
   dc.NodeCurrentId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON d.UserCreatedId = u.UserId
WHERE 
   dc.Status = 2 AND 
   d.CategoryBusinessId <> 4 AND 
   dc.DocumentCopyType IN (1, 2, 4, 32, 64) AND 
   (@quicksearch = 0 OR d.Compendium LIKE @quicksearch)
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM 
   dbo.documentcopy  AS dc 
      INNER JOIN dbo.document  AS d 
      ON dc.DocumentId = d.DocumentId
WHERE 
   (dc.UserCurrentId <> @userId OR (dc.UserCurrentId = @userId AND dc.Status <> 2)) AND 
   dc.DocumentCopyId IN ({0}) AND 
   d.CategoryBusinessId <> 4

/*
*   Câu truy vấn tính tổng số VB, HS chưa đọc
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.CategoryBusinessId <> 4 AND 
   dc.UserCurrentId = @userId AND 
   CAST(df.IsViewed AS bigint) = 0x30 AND 
   dc.DocumentCopyType IN (1, 2, 4, 32, 64)
/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   dc.LastComment AS LastComment, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   df.DocFinishType, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   u.FullName AS UserCurrentFullName, 
   u.FirstName AS UserCurrentFirstName, 
   dc.UserCurrentId, 
   dc.DateOverdue, 
   datediff(DAY, dc.DateOverdue, getdate()) AS NumberDayOverdue, 
   datediff(DAY, d.DateAppointed, getdate()) AS NumberDayAppointed, 
   dc.WorkflowId, 
   dc.NodeCurrentId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON d.UserCreatedId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.CategoryBusinessId <> 4 AND 
   dc.UserCurrentId = @userId AND 
   dc.DocumentCopyType IN (1, 2, 4, 32, 64) AND 
   (@quicksearch = 0 OR d.Compendium LIKE @quicksearch)
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn tính tổng các VB, HS
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON dc.UserCurrentId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   dc.UserCurrentId = @userId AND 
   dc.DocumentCopyType IN (1, 2, 4, 32, 64) AND 
   d.CategoryBusinessId <> 4 AND 
   (@quicksearch = 0 OR d.Compendium LIKE @quicksearch)



SELECT 
   d.DocumentId, 
   d.Compendium, 
   dc.DateReceived, 
   u.FullName AS UserCurrentFullName, 
   datediff(DAY, dc.DateOverdue, getdate()) AS NumberDayOverdue, 
   datediff(DAY, d.DateAppointed, getdate()) AS NumberDayAppointed
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON d.UserCreatedId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.CategoryBusinessId <> 4 AND 
   dc.UserCurrentId = @userId AND 
   dc.DocumentCopyType IN (1, 2, 4, 32, 64) AND 
   (@quicksearch = 0 OR d.Compendium LIKE @quicksearch) AND 
   dc.DocumentCopyId IN ({0})
   ORDER BY dc.DateReceived DESC
