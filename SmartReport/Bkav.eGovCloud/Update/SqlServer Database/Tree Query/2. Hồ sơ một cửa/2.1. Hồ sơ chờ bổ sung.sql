
/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.DocumentId, 
   d.DocTypeId, 
   d.CategoryId, 
   d.DocCode, 
   d.CitizenName, 
   d.CitizenInfo, 
   d.IdentityCard, 
   d.Address, 
   d.Email, 
   d.Phone, 
   d.Compendium, 
   d.Compendium2, 
   d.DateCreated, 
   d.UserCreatedId, 
   d.DateAppointed, 
   d.DateModified, 
   d.IsSuccess, 
   d.DateSuccess, 
   d.UserSuccessId, 
   d.SuccessNote, 
   d.IsReturned, 
   d.DateReturned, 
   d.UserReturnedId, 
   d.ReturnNote, 
   d.Status, 
   d.IsAcknowledged, 
   d.IsGettingOut, 
   d.Original, 
   d.CategoryBusinessId, 
   d.ResultStatus, 
   d.DateResult, 
   d.IsConverted, 
   d.UrgentId, 
   d.InOutCode, 
   d.InOutPlace, 
   d.IsSupplemented, 
   d.DocFieldId, 
   d.DocFieldIds, 
   d.DocTypePermission, 
   d.DateResponsed, 
   d.DateResponsedOverdue, 
   d.ProcessedMinutes, 
   d.DateOfIssueCode, 
   d.DateResponse, 
   d.DateFinished, 
   d.Organization, 
   d.StoreId, 
   d.SecurityId, 
   d.TotalPage, 
   d.Keyword, 
   d.SendTypeId, 
   d.DateArrived, 
   d.DatePublished, 
   d.DelayReason, 
   d.WorkflowTypeId, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   dc.LastComment AS LastComment, 
   dc.DocumentCopyType, 
   dc.Status AS DocCopyStatus, 
   dc.WorkflowId, 
   dc.NodeCurrentId, 
   df.DocFinishType, 
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
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status IN ( 2, 16 ) AND 
   d.Status = 16 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   dc.DateReceived >= @toDate AND 
   CAST(dc.IsCreateNode AS bigint) <> 0x31 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSupplemented IS NOT NULL AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   (512 & dc.NodeCurrentPermission) = 512 AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048 AND 
   (d.IsReturned IS NULL OR CAST(d.IsReturned AS bigint) = 0x30)
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
   dc.UserCurrentId <> @userId OR 
   dc.Status NOT IN ( 2, 16 ) OR 
   d.Status <> 16 OR 
   dc.DocumentCopyType NOT IN ( 1, 2, 4 ) OR 
   CAST(d.IsGettingOut AS bigint) = 0x31 AND d.IsSupplemented IS NULL OR 
   (1024 & dc.NodeCurrentPermission) = 1024 OR 
   (512 & dc.NodeCurrentPermission) <> 512 OR 
   (2048 & dc.NodeCurrentPermission) = 2048 OR 
   CAST(d.IsReturned AS bigint) = 0x31) AND dc.DocumentCopyId IN ({0})

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
   dc.Status IN ( 2, 16 ) AND 
   d.Status = 16 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   CAST(dc.IsCreateNode AS bigint) <> 0x31 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSupplemented IS NOT NULL AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   (512 & dc.NodeCurrentPermission) = 512 AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048 AND 
   (d.IsReturned IS NULL OR CAST(d.IsReturned AS bigint) = 0x30) AND 
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
   dc.WorkflowId, 
   dc.NodeCurrentId, 
   df.DocFinishType, 
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
      ON dc.DocumentCopyId = df.DocumentCopyId
WHERE 
   dc.Status IN ( 2, 16 ) AND 
   d.Status = 16 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   CAST(dc.IsCreateNode AS bigint) <> 0x31 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSupplemented IS NOT NULL AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   (512 & dc.NodeCurrentPermission) = 512 AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048 AND 
   (d.IsReturned IS NULL OR CAST(d.IsReturned AS bigint) = 0x30)
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
   dc.Status IN ( 2, 16 ) AND 
   d.Status = 16 AND 
   d.CategoryBusinessId = 4 AND 
   dc.UserCurrentId = @userId AND 
   CAST(dc.IsCreateNode AS bigint) <> 0x31 AND 
   (1 & dc.NodeCurrentPermission) <> 1 AND 
   dc.DocumentCopyType IN ( 1, 2, 4 ) AND 
   d.IsSupplemented IS NOT NULL AND 
   (d.IsGettingOut IS NULL OR CAST(d.IsGettingOut AS bigint) = 0x30) AND 
   (512 & dc.NodeCurrentPermission) = 512 AND 
   (1024 & dc.NodeCurrentPermission) <> 1024 AND 
   (2048 & dc.NodeCurrentPermission) <> 2048 AND 
   (d.IsReturned IS NULL OR CAST(d.IsReturned AS bigint) = 0x30)

