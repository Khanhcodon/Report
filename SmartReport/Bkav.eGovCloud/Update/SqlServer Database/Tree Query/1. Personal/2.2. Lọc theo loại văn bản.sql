﻿
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
   dc.UserCurrentId = @userId AND 
   CAST(dc.DocTypeId AS float(53)) = @docTypeId AND 
   d.CategoryBusinessId <> 4 AND 
   dc.DateReceived >= @toDate AND 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 ) AND 
   (@quicksearch = '' OR d.Compendium LIKE @quicksearch)
   ORDER BY dc.DateReceived DESC


/*
*   Câu truy vấn VB, HS vừa xóa khỏi danh sách trước đó
*/
SELECT dc.DocumentCopyId
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId
WHERE ((dc.UserCurrentId <> @userId OR (dc.UserCurrentId = @userId AND dc.Status <> 2)) OR CAST(dc.DocTypeId AS float(53)) <> @docTypeId) AND dc.DocumentCopyId IN ({0})
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
   CAST(dc.DocTypeId AS float(53)) = @docTypeId AND 
   d.CategoryBusinessId <> 4 AND 
   dc.UserCurrentId = @userId AND 
   CAST(df.IsViewed AS bigint) = 0x30 AND 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 ) 
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
   dc.UserCurrentId = @userId AND 
   dc.DocTypeId = @docTypeId AND 
   dc.DocumentCopyType IN ( 1, 2, 4, 32, 64 ) AND 
   (@quicksearch = '' OR d.Compendium LIKE @quicksearch)
   ORDER BY dc.DateReceived DESC


/*
*   Câu truy vấn tính tổng các VB, HS
*/

