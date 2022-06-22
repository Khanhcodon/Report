
/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.*, 
   dc.DateReceived, 
   df.IsViewed, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.UserCurrentId, 
   dc.DocumentCopyId, 
   df.IsDocumentImportant
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.authorize  AS a 
      ON dc.UserCurrentId = a.AuthorizeUserId AND (d.DocTypeId = a.DocTypeId OR a.DocTypeId IS NULL) 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 4 AND 
   d.CategoryBusinessId = 2 AND 
   CAST(a.Active AS bigint) = 0x31 AND 
   getdate() >= a.DateBegin AND 
   getdate() <= a.DateEnd AND 
   a.AuthorizedUserId = @userId AND 
   dc.DateReceived >= @toDate AND 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 ) AND  
   (@quicksearch = 0 OR d.Compendium LIKE @quicksearch)
   ORDER BY dc.DateReceived DESC
/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT documentcopy.DocumentCopyId
FROM dbo.documentcopy
WHERE documentcopy.UserCurrentId <> @userId AND documentcopy.DocumentCopyId IN ({0})

/*
*   Câu truy vấn tính tổng số VB, HS chưa đọc
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.authorize  AS a 
      ON dc.UserCurrentId = a.AuthorizeUserId AND (d.DocTypeId = a.DocTypeId OR a.DocTypeId IS NULL) 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 4 AND 
   d.CategoryBusinessId = 2 AND 
   CAST(a.Active AS bigint) = 0x31 AND 
   getdate() >= a.DateBegin AND 
   getdate() <= a.DateEnd AND 
   a.AuthorizedUserId = @userId AND 
   CAST(df.IsViewed AS bigint) = 0x30 AND 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 )
/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT TOP (@take) 
   d.*, 
   dc.DateReceived, 
   df.IsViewed, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.UserCurrentId, 
   dc.DocumentCopyId, 
   df.IsDocumentImportant
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.authorize  AS a 
      ON dc.UserCurrentId = a.AuthorizeUserId AND (d.DocTypeId = a.DocTypeId OR a.DocTypeId IS NULL) 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 4 AND 
   d.CategoryBusinessId = 2 AND 
   CAST(a.Active AS bigint) = 0x31 AND 
   getdate() >= a.DateBegin AND 
   getdate() <= a.DateEnd AND 
   a.AuthorizedUserId = @userId AND 
   dc.DateReceived <= @fromDate AND 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 ) AND  
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
      INNER JOIN dbo.authorize  AS a 
      ON dc.UserCurrentId = a.AuthorizeUserId AND (d.DocTypeId = a.DocTypeId OR a.DocTypeId IS NULL) 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 4 AND 
   d.CategoryBusinessId = 2 AND 
   CAST(a.Active AS bigint) = 0x31 AND 
   getdate() >= a.DateBegin AND 
   getdate() <= a.DateEnd AND 
   a.AuthorizedUserId = @userId AND 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 )
