
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
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId 
      INNER JOIN dbo.authorize  AS au 
      ON au.AuthorizeUserId = df.UserId
WHERE 
   dc.DocumentCopyType IN (1, 2, 4, 32, 64 ) AND 
   d.CategoryBusinessId <> 4 AND 
   au.AuthorizeUserId IN ({0}) AND 
   CAST(au.Active AS bigint) = 0x31 AND 
   getdate() >= au.DateBegin AND 
   getdate() <= au.DateEnd AND 
   d.DateCreated >= au.DateBegin AND 
   d.DateCreated <= au.DateEnd AND 
   (au.DocTypeId IS NULL OR au.DocTypeId = N'' OR au.DocTypeId LIKE (N'%') + (d.DocTypeId) + (N'%')) AND 
   (dc.UserCurrentId <> au.AuthorizeUserId OR (dc.UserCurrentId = au.AuthorizeUserId AND dc.Status = 4)) AND 
   dc.DateReceived >= @toDate AND 
   (@quicksearch = 0 OR d.Compendium LIKE @quicksearch)
   ORDER BY dc.DateReceived DESC


/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.authorize  AS au 
      ON dc.UserCurrentId = au.AuthorizeUserId
WHERE 
   au.AuthorizeUserId NOT IN ({0}) AND 
   CAST(au.Active AS bigint) = 0x31 AND 
   d.DateCreated <= au.DateBegin AND 
   d.DateCreated >= au.DateEnd AND 
   dc.DocumentCopyId IN ({1})
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
      ON dc.DocumentCopyId = df.DocumentCopyId 
      INNER JOIN dbo.authorize  AS au 
      ON au.AuthorizeUserId = df.UserId
WHERE 
   dc.DocumentCopyType IN (1, 2, 4, 32, 64) AND 
   au.AuthorizeUserId IN ({0}) AND 
   CAST(df.IsViewed AS bigint) = 0x30 AND 
   CAST(au.Active AS bigint) = 0x31 AND 
   getdate() >= au.DateBegin AND 
   getdate() <= au.DateEnd AND 
   d.DateCreated >= au.DateBegin AND 
   d.DateCreated <= au.DateEnd AND 
   (au.DocTypeId IS NULL OR au.DocTypeId = N'' OR au.DocTypeId LIKE N'%' + d.DocTypeId + N'%') AND 
   (dc.UserCurrentId <> au.AuthorizeUserId OR (dc.UserCurrentId = au.AuthorizeUserId AND dc.Status = 4))

/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT TOP (100) 
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
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId 
      INNER JOIN dbo.authorize  AS au 
      ON au.AuthorizeUserId = df.UserId
WHERE 
   dc.DocumentCopyType IN (1, 2, 4, 32, 64) AND 
   d.CategoryBusinessId <> 4 AND 
   au.AuthorizeUserId IN ({0}) AND 
   CAST(au.Active AS bigint) = 0x31 AND 
   getdate() >= au.DateBegin AND 
   getdate() <= au.DateEnd AND 
   d.DateCreated >= au.DateBegin AND 
   d.DateCreated <= au.DateEnd AND 
   (au.DocTypeId IS NULL OR au.DocTypeId = N'' OR au.DocTypeId LIKE (N'%') + (d.DocTypeId) + (N'%')) AND 
   (dc.UserCurrentId <> au.AuthorizeUserId OR (dc.UserCurrentId = au.AuthorizeUserId AND dc.Status = 4)) AND 
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
      ON dc.DocumentCopyId = df.DocumentCopyId 
      INNER JOIN dbo.authorize  AS au 
      ON au.AuthorizeUserId = df.UserId
WHERE 
   dc.DocumentCopyType IN (1, 2, 4, 32, 64) AND 
   au.AuthorizeUserId IN ({0}) AND 
   CAST(au.Active AS bigint) = 0x31 AND 
   getdate() >= au.DateBegin AND 
   getdate() <= au.DateEnd AND 
   d.DateCreated >= au.DateBegin AND 
   d.DateCreated <= au.DateEnd AND 
   (au.DocTypeId IS NULL OR au.DocTypeId = N'' OR au.DocTypeId LIKE N'%' + d.DocTypeId + N'%') AND 
   (dc.UserCurrentId <> au.AuthorizeUserId OR (dc.UserCurrentId = au.AuthorizeUserId AND dc.Status = 4))
