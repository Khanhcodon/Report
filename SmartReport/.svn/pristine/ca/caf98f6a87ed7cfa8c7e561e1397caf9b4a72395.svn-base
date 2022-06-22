
/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   dc.UserCurrentId, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status <> 4 AND 
   d.CategoryBusinessId <> 4 AND 
   dc.DocumentCopyType = 8 AND 
   dc.UserCurrentId = @userId AND 
   DateReceived >= [@toDate] AND 
   (@quicksearch = 0 OR d.Compendium LIKE @quicksearch)
   ORDER BY dc.DateReceived DESC
/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM dbo.documentcopy  AS dc
WHERE dc.UserCurrentId <> @userId 
	AND dc.Status <> 4 OR 
	dc.DocumentCopyType = 8
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
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status <> 4 AND 
   dc.UserCurrentId = @userId AND 
   CAST(df.IsViewed AS bigint) = 0x30 AND 
   dc.DocumentCopyType = 8
/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT TOP (100) 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.UserCurrentId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status <> 4 AND 
   d.CategoryBusinessId <> 4 AND 
   dc.DocumentCopyType = 8 AND 
   dc.UserCurrentId = @userId AND 
   dc.DateReceived <= @fromDate AND 
   (@quicksearch = '' OR d.Compendium LIKE @quicksearch)
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
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status <> 4 AND 
   dc.DocumentCopyType = 8 AND 
   dc.UserCurrentId = @userId
