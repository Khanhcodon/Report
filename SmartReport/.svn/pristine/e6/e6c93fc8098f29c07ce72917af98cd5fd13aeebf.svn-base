
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
   df.DocFinishType, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   u.FullName AS UserCurrentFullName, 
   u.FirstName AS UserCurrentFirstName, 
   dc.UserCurrentId, 
   dc.DateOverdue, 
   datediff(DAY, dc.DateOverdue, getdate()) AS NumberDayOverdue, 
   datediff(DAY, d.DateAppointed, getdate()) AS NumberDayAppointed, 
   dc.WorkflowId, 
   dc.NodeCurrentId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON d.UserCreatedId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   dc.DateReceived >= @toDate AND 
   dc.DocumentCopyType = 1 AND 
   (2048 & dc.NodeCurrentPermission) = 2048 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   d.DocFieldId = @docfieldid
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
   dc.Status <> 2 OR 
   d.Status <> 2 OR 
   d.CategoryBusinessId <> 4 OR 
   dc.UserCurrentId <> @userId OR 
   dc.DocumentCopyType <> 1 OR 
   (1 & dc.NodeCurrentPermission) = 1 OR 
   (1024 & dc.NodeCurrentPermission) <> 1024 OR 
   CAST(d.IsGettingOut AS bigint) = 0x31) AND dc.DocumentCopyId IN ({0})

/*
*   Câu truy vấn tính tổng số VB, HS chưa đọc
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON d.UserCreatedId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   dc.DocumentCopyType = 1 AND 
   (2048 & dc.NodeCurrentPermission) = 2048 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   d.DocFieldId = @docfieldid AND 
   CAST(df.IsViewed AS bigint) = 0x30

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
   df.DocFinishType, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   u.FullName AS UserCurrentFullName, 
   u.FirstName AS UserCurrentFirstName, 
   dc.UserCurrentId, 
   dc.DateOverdue, 
   datediff(DAY, dc.DateOverdue, getdate()) AS NumberDayOverdue, 
   datediff(DAY, d.DateAppointed, getdate()) AS NumberDayAppointed, 
   dc.WorkflowId, 
   dc.NodeCurrentId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON d.UserCreatedId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   dc.DocumentCopyType = 1 AND 
   (2048 & dc.NodeCurrentPermission) = 2048 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   d.DocFieldId = @docfieldid
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn tính tổng các VB, HS
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN [user] AS u 
      ON d.UserCreatedId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   dc.DocumentCopyType = 1 AND 
   (2048 & dc.NodeCurrentPermission) = 2048 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   d.DocFieldId = @docfieldid
