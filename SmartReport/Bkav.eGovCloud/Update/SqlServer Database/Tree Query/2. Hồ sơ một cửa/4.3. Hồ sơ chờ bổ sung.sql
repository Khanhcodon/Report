
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
   df.IsViewed, 
   df.IsDocumentImportant, 
   u.FullName AS UserCurrentFullName, 
   u.FirstName AS UserCurrentFirstName, 
   dc.UserCurrentId, 
   dbo.fnGetDocumentColor(
      d.UrgentId, 
      d.DateAppointed, 
      d.DateResponsed, 
      d.DateResponsedOverdue, 
      dc.DateOverdue, 
      dc.DocumentCopyType, 
      dc.Status) AS Color
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON d.UserCreatedId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = @userId
WHERE 
   dc.Status IN ( 2, 16 ) AND 
   d.Status = 16 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId <> @userId AND 
   dc.DateReceived >= @toDate AND 
   dc.IsCreateNode <> '1' AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSupplemented IS NOT NULL AND 
   (d.IsGettingOut IS NULL OR d.IsGettingOut = '0') AND 
   (512 & dc.NodeCurrentPermission) = 512 AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048 AND 
   (d.IsReturned IS NULL OR d.IsReturned = '0')
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM 
   dbo.documentcopy  AS dc 
      INNER JOIN dbo.document  AS d 
      ON d.DocumentId = dc.DocumentId
WHERE (
   dc.UserCurrentId = @userId OR 
   dc.Status NOT IN ( 2, 16 ) OR 
   d.Status <> 16 OR 
   dc.DocumentCopyType NOT IN ( 1, 2, 4 ) OR 
   d.IsGettingOut = 1 AND d.IsSupplemented IS NULL OR 
   (1024 & dc.NodeCurrentPermission) = 1024 OR 
   (512 & dc.NodeCurrentPermission) <> 512 OR 
   (2048 & dc.NodeCurrentPermission) = 2048 OR 
   d.IsReturned = 1) AND dc.DocumentCopyId IN ({0})
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
   dc.Status IN ( 2, 16 ) AND 
   d.Status = 16 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId <> @userId AND 
   dc.IsCreateNode <> 1 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSupplemented IS NOT NULL AND 
   (d.IsGettingOut IS NULL OR d.IsGettingOut = 0) AND 
   (512 & dc.NodeCurrentPermission) = 512 AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048 AND 
   (d.IsReturned IS NULL OR d.IsReturned = 0) AND 
   df.IsViewed = 0

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
   df.IsViewed, 
   df.IsDocumentImportant, 
   u.FullName AS UserCurrentFullName, 
   u.FirstName AS UserCurrentFirstName, 
   dc.UserCurrentId, 
   dbo.fnGetDocumentColor(
      d.UrgentId, 
      d.DateAppointed, 
      d.DateResponsed, 
      d.DateResponsedOverdue, 
      dc.DateOverdue, 
      dc.DocumentCopyType, 
      dc.Status) AS Color
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON d.UserCreatedId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = @userId
WHERE 
   dc.Status IN ( 2, 16 ) AND 
   d.Status = 16 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId <> @userId AND 
   dc.IsCreateNode <> 1 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSupplemented IS NOT NULL AND 
   (d.IsGettingOut IS NULL OR d.IsGettingOut = 0) AND 
   (512 & dc.NodeCurrentPermission) = 512 AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048 AND 
   (d.IsReturned IS NULL OR d.IsReturned = 0)
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
   dc.Status IN ( 2, 16 ) AND 
   d.Status = 16 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId <> @userId AND 
   dc.IsCreateNode <> 1 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSupplemented IS NOT NULL AND 
   (d.IsGettingOut IS NULL OR d.IsGettingOut = 0) AND 
   (512 & dc.NodeCurrentPermission) = 512 AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048 AND 
   (d.IsReturned IS NULL OR d.IsReturned = 0) AND 

