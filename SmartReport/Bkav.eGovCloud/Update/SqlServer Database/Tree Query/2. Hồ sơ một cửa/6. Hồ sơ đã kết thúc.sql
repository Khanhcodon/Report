
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
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = @userId
WHERE 
   dc.Status = 4 AND 
   d.Status = 4 AND 
   d.CategoryBusinessId = 4 AND 
   dc.DateReceived >= @toDate AND 
   dc.DocumentCopyType IN ( 1, 2, 4 )
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM dbo.documentcopy  AS dc
WHERE (dc.Status <> 4 OR dc.DocumentCopyType NOT IN (1, 2, 4)) AND dc.DocumentCopyId IN ({0})
GO
/*
*   Câu truy vấn tính tổng số VB, HS chưa đọc
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = @userId
WHERE 
   dc.Status = 4 AND 
   d.Status = 4 AND 
   d.CategoryBusinessId = 4 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   CAST(df.IsViewed AS bigint) = 0x30

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
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = @userId
WHERE 
   dc.Status = 4 AND 
   d.Status = 4 AND 
   d.CategoryBusinessId = 4 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 )
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn tính tổng các VB, HS
*/

SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = @userId
WHERE 
   dc.Status = 4 AND 
   d.Status = 4 AND 
   d.CategoryBusinessId = 4 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 )
