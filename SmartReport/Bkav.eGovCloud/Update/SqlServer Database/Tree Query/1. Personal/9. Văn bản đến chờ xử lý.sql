
/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.*, 
   dc.DocumentCopyId, 
   dc.DateReceived, 
   df.IsViewed
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON df.DocumentCopyId = dc.DocumentCopyId
WHERE 
   dc.Status = 2 AND 
   d.CategoryBusinessId = 1 AND 
   d.DocTypeId IS NULL AND 
   dc.DateReceived >= @toDate AND 
   dc.UserCurrentId = @userId
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId
WHERE (
   dc.Status <> 2 OR 
   d.CategoryBusinessId <> 1 OR 
   d.DocTypeId IS NOT NULL OR 
   dc.UserCurrentId <> @userId) AND dc.DocumentCopyId IN ({0})
/*
*   Câu truy vấn tính tổng số VB, HS chưa đọc
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId
WHERE 
   dc.Status = 2 AND 
   d.CategoryBusinessId = 1 AND 
   dc.UserCurrentId = @userId

/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT 
   d.*, 
   dc.DocumentCopyId, 
   dc.DateReceived, 
   df.IsViewed
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.docfinish  AS df 
      ON df.DocumentCopyId = dc.DocumentCopyId
WHERE 
   dc.Status = 2 AND 
   d.CategoryBusinessId = 1 AND 
   d.DocTypeId IS NULL AND 
   dc.UserCurrentId = @userId
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn tính tổng các VB, HS
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId
WHERE 
   dc.Status = 2 AND 
   d.CategoryBusinessId = 1 AND 
   d.DocTypeId IS NULL AND 
   dc.UserCurrentId = @userId
