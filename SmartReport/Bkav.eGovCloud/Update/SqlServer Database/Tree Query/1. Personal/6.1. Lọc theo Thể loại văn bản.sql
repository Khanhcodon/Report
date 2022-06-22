
/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.Compendium, 
   d.DocCode, 
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
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 4 AND 
   d.CategoryId = @categoryid AND 
   d.CategoryBusinessId <> 4 AND 
   df.UserId = @userId AND 
   dc.DateReceived >= @toDate AND 
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
WHERE dc.UserCurrentId <> @userId AND dc.Status <> 4 OR d.CategoryId <> @categoryid AND dc.DocumentCopyId IN ({0})


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
   dc.Status = 4 AND 
   d.CategoryBusinessId <> 4 AND 
   df.UserId = @userId AND 
   d.CategoryId = @categoryid AND 
   CAST(df.IsViewed AS bigint) = 0x30

/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT TOP (100) 
   d.Compendium, 
   d.DateCreated, 
   d.DocTypeId, 
   d.CategoryBusinessId, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   d.DocCode, 
   df.IsViewed, 
   df.IsDocumentImportant, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.WorkflowId, 
   dc.NodeCurrentId, 
   df.DocFinishType, 
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
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status = 4 AND 
   d.CategoryId = @categoryid AND 
   d.CategoryBusinessId <> 4 AND 
   df.UserId = @userId AND 
   dc.DateReceived <= [@fromDate] AND 
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
WHERE 
   dc.Status = 4 AND 
   d.CategoryBusinessId <> 4 AND 
   df.UserId = @userId AND 
   d.CategoryId = @categoryid

