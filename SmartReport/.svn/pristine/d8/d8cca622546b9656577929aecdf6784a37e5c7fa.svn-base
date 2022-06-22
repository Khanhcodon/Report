
/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.UserCurrentId, 
   df.IsViewed, 
   df.IsDocumentImportant
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId 
      INNER JOIN dbo.authorize  AS au 
      ON au.AuthorizeUserId = df.UserId AND CAST(au.Active AS bigint) = 0x31
WHERE 
   dc.DocumentCopyType IN ( 1, 2, 4, 32,64 ) AND 
   d.CategoryBusinessId <> 4 AND 
   (au.DocTypeId IS NULL OR au.DocTypeId = N'' OR au.DocTypeId LIKE (N'%') + (d.DocTypeId) + (N'%')) AND 
   dc.UserCurrentId = au.AuthorizeUserId AND 
   dc.Status IN ( 2, 16 ) AND 
   getdate() >= au.DateBegin AND 
   getdate() <= au.DateEnd AND 
   au.AuthorizeUserId IN ( (0) ) AND 
   dc.DateReceived >= @toDate AND 
   (@quicksearch = '' OR d.Compendium LIKE @quicksearch) AND 
   d.CategoryId = @categoryid
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
WHERE (au.AuthorizeUserId NOT IN ({0}) OR (au.AuthorizeUserId IN ({0}) AND dc.Status <> 2)) OR d.CategoryId <> @categoryid AND dc.DocumentCopyId IN ({1})
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
      INNER JOIN dbo.authorize  AS au 
      ON au.AuthorizeUserId = df.UserId AND CAST(au.Active AS bigint) = 0x31
WHERE 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 ) AND 
   (au.DocTypeId IS NULL OR au.DocTypeId = N'' OR au.DocTypeId LIKE (N'%') + (d.DocTypeId) + (N'%')) AND 
   dc.UserCurrentId = au.AuthorizeUserId AND 
   dc.Status IN ( 2, 16 ) AND 
   getdate() >= au.DateBegin AND 
   getdate() <= au.DateEnd AND 
   CAST(df.IsViewed AS bigint) = 0x30 AND 
   au.AuthorizeUserId IN ( (0) ) AND 
   d.CategoryId = @categoryid

/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT TOP (100) 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.UserCurrentId, 
   df.IsViewed, 
   df.IsDocumentImportant
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId 
      INNER JOIN dbo.authorize  AS au 
      ON au.AuthorizeUserId = df.UserId AND CAST(au.Active AS bigint) = 0x31
WHERE 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 ) AND 
   d.CategoryBusinessId <> 4 AND 
   (au.DocTypeId IS NULL OR au.DocTypeId = N'' OR au.DocTypeId LIKE (N'%') + (d.DocTypeId) + (N'%')) AND 
   dc.UserCurrentId = au.AuthorizeUserId AND 
   dc.Status IN ( 2, 16 ) AND 
   getdate() >= au.DateBegin AND 
   getdate() <= au.DateEnd AND 
   au.AuthorizeUserId IN ( (0) ) AND 
   dc.DateReceived <= @fromDate AND 
   (@quicksearch = '' OR d.Compendium LIKE @quicksearch) AND 
   d.CategoryId = @categoryid
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
      INNER JOIN dbo.authorize  AS au 
      ON au.AuthorizeUserId = df.UserId AND CAST(au.Active AS bigint) = 0x31
WHERE 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 ) AND 
   (au.DocTypeId IS NULL OR au.DocTypeId = N'' OR au.DocTypeId LIKE (N'%') + (d.DocTypeId) + (N'%')) AND 
   dc.UserCurrentId = au.AuthorizeUserId AND 
   dc.Status IN ( 2, 16 ) AND 
   getdate() >= au.DateBegin AND 
   getdate() <= au.DateEnd AND 
   au.AuthorizeUserId IN ( (0) ) AND 
   d.CategoryId = @categoryid

