
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
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId <> @userId AND 
   dc.DateReceived >= @toDate AND 
   CAST(dc.IsCreateNode AS bigint) <> 0x31 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSuccess IS NULL AND 
   d.IsSupplemented IS NULL AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048

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
   dc.Status NOT IN ( (2) ) OR 
   dc.DocumentCopyType NOT IN ( 1, 2, 4 ) OR 
   d.IsSuccess IS NOT NULL OR 
   d.IsSupplemented IS NOT NULL OR 
   CAST(d.IsGettingOut AS bigint) = 0x31 OR 
   (1024 & dc.NodeCurrentPermission) = 1024 OR 
   (2048 & dc.NodeCurrentPermission) = 2048) AND dc.DocumentCopyId IN ({0})

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
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId <> @userId AND 
   CAST(dc.IsCreateNode AS bigint) <> 0x31 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSuccess IS NULL AND 
   d.IsSupplemented IS NULL AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048 AND
   CAST(df.IsViewed AS bigint) = 0x30)

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
      ON dc.DocumentCopyId = df.DocumentCopyId AND df.UserId = [@userId]
WHERE 
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId <> @userId AND 
   CAST(dc.IsCreateNode AS bigint) <> 0x31 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSuccess IS NULL AND 
   d.IsSupplemented IS NULL AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048

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
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId <> @userId AND 
   CAST(dc.IsCreateNode AS bigint) <> 0x31 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSuccess IS NULL AND 
   d.IsSupplemented IS NULL AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048

