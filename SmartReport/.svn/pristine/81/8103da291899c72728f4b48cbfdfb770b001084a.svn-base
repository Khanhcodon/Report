
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
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   dc.DateReceived >= @toDate AND 
   1 & dc.NodeCurrentPermission = 1 AND 
   dc.DocumentCopyType IN (1, 2, 4)
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM dbo.documentcopy  AS dc
WHERE (
   dc.UserCurrentId <> @userId OR 
   dc.Status <> 2 OR 
   1 & dc.NodeCurrentPermission <> 1 OR 
   dc.DocumentCopyType NOT IN (1, 2, 4)) AND dc.DocumentCopyId IN ({0})
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
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   1 & dc.NodeCurrentPermission = 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
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
      INNER JOIN [user] u 
      ON d.UserCreatedId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   1 & dc.NodeCurrentPermission = 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 )
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
      ON dc.UserCurrentId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   dc.Status = 2 AND 
   d.Status = 2 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   1 & dc.NodeCurrentPermission = 1 AND 
   dc.DocumentCopyType IN (1, 2, 4)
   ORDER BY dc.DateReceived DESC

/*
*   Câu truy vấn sinh ra tệp
*/
SELECT 
   d.DocCode, 
   d.CitizenName, 
   d.DateCreated, 
   d.DateAppointed, 
   datediff(DAY, d.DateCreated, d.DateAppointed) AS RemainTime
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON dc.DocumentId = d.DocumentId
WHERE dc.DocumentCopyId IN (#docCopyIds)