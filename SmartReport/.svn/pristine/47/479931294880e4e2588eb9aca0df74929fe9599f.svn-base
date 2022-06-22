
/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.Status, 
   d.Compendium, 
   d.DateCreated, 
   d.DocTypeId, 
   d.CategoryBusinessId, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.WorkflowId, 
   dc.NodeCurrentId, 
   df.DocFinishType, 
   u.FullName AS UserCurrentFullName, 
   u.FirstName AS UserCurrentFirstName, 
   dc.UserCurrentId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON dc.UserCurrentId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   (dc.Status = 2 OR dc.Status = 16) AND 
   df.UserId = @userId AND 
   d.CategoryBusinessId <> 4 AND 
   dc.UserCurrentId <> @userId AND 
   dc.DateReceived >= @toDate AND 
   (@quicksearch = 0 OR d.Compendium LIKE @quicksearch)
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM 
   dbo.documentcopy  AS dc 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   ((dc.Status IN ( 2, 16 ) AND dc.UserCurrentId = @userId) OR dc.Status NOT IN ( 2, 16 )) AND 
   df.UserId = @userId AND 
   dc.DocumentCopyId IN ({0})
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
   (dc.Status = 2 OR dc.Status = 16) AND 
   df.UserId = @userId AND 
   d.CategoryBusinessId <> 4 AND 
   dc.UserCurrentId <> @userId AND 
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
   dc.WorkflowId, 
   dc.NodeCurrentId, 
   df.DocFinishType, 
   u.FullName AS UserCurrentFullName, 
   u.FirstName AS UserCurrentFirstName, 
   dc.UserCurrentId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON dc.UserCurrentId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   (dc.Status = 2 OR dc.Status = 16) AND 
   d.CategoryBusinessId <> 4 AND 
   df.UserId = @userId AND 
   dc.UserCurrentId <> @userId AND 
   dc.DateCreated <= @fromDate AND 
   (@quickSearch = '' OR d.Compendium LIKE @quickSearch)
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn tính tổng các VB, HS
*/

