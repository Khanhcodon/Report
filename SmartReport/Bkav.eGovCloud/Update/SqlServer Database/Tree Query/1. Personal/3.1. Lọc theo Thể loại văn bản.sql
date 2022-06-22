
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
   dc.Status AS DocCopyStatus
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 1 AND 
   d.CategoryBusinessId <> 4 AND 
   d.CategoryId = @categoryid AND 
   dc.UserCurrentId = @userId AND 
   dc.DateReceived >= @toDate AND 
   (@quicksearch = '' OR d.Compendium LIKE @quicksearch)
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
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 1 AND 
   d.CategoryId = @categoryid AND 
   dc.UserCurrentId = @userId AND 
   CAST(df.IsViewed AS bigint) = 0x30

/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT * FROM (SELECT 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus,
   ROW_NUMBER()	OVER (ORDER BY dc.DateReceived DESC) AS idx 	
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 1 AND 
   d.CategoryBusinessId <> 4 AND 
   d.CategoryId = @categoryid AND 
   dc.UserCurrentId = @userId AND 
   dc.DateReceived <= @fromDate AND 
   (@quicksearch = '' OR d.Compendium LIKE @quicksearch)
) as tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take
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
WHERE 
   dc.Status = 1 AND 
   d.CategoryId = @categoryid AND 
   dc.UserCurrentId = @userId

