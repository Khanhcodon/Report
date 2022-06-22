
/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.UserCurrentId, 
   dc.WorkflowId, 
   dc.NodeCurrentId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 1 AND 
   dc.UserCurrentId = @userId AND 
   dc.DateReceived >= @toDate AND 
   d.CategoryBusinessId <> 4
   ORDER BY dc.DateReceived DESC
/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM dbo.documentcopy  AS dc
WHERE (dc.Status <> 1 
	OR dc.UserCurrentId <> @userId) 
	AND dc.DocumentCopyId IN ({0})
/*
*   Câu truy vấn tính tổng số VB, HS chưa đọc
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 1 AND 
   dc.UserCurrentId = @userId AND 
   CAST(df.IsViewed AS bigint) = 0x30
/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.UserCurrentId, 
   dc.WorkflowId, 
   dc.NodeCurrentId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 1 AND 
   dc.UserCurrentId = @userId AND 
   d.CategoryBusinessId <> 4
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
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE dc.Status = 1 AND dc.UserCurrentId = @userId
