
/*
*   Câu truy vấn VB, HS mới nhất
*/
SELECT 
   d.*, 
   dc.DateReceived, 
   dc.DocumentCopyId, 
   df.IsViewed, 
   u.FullName AS UserCurrentFullName, 
   u.FirstName AS UserCurrentFirstName, 
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
      INNER JOIN dbo.storeprivate_documentcopy  AS sdc 
      ON dc.DocumentCopyId = sdc.DocumentCopyId 
      INNER JOIN [user] AS u 
      ON dc.UserCurrentId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE 
   sdc.StorePrivateId = @storePrivateId AND 
   dc.DateReceived >= @toDate AND 
   (@quicksearch = 0 OR d.Compendium LIKE @quicksearch)
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
      INNER JOIN dbo.storeprivate_documentcopy  AS sdc 
      ON dc.DocumentCopyId = sdc.DocumentCopyId 
      INNER JOIN [user] AS u 
      ON dc.UserCurrentId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
WHERE df.IsViewed = 0

/*
*   Câu truy vấn VB, HS cũ hơn
*/
SELECT * FROM
	(SELECT d.*, 
	   dc.DateReceived, 
	   dc.DocumentCopyId, 
	   df.IsViewed, 
	   u.FullName AS UserCurrentFullName, 
	   u.FirstName AS UserCurrentFirstName, 
	   dbo.fnGetDocumentColor(
		  d.UrgentId, 
		  d.DateAppointed, 
		  d.DateResponsed, 
		  d.DateResponsedOverdue, 
		  dc.DateOverdue, 
		  dc.DocumentCopyType, 
		  dc.Status) AS Color,
		  ROW_NUMBER() OVER (ORDER BY dc.DateReceived DESC) AS idx 
	FROM 
	   dbo.document  AS d 
		  INNER JOIN dbo.documentcopy  AS dc 
		  ON d.DocumentId = dc.DocumentId 
		  INNER JOIN dbo.storeprivate_documentcopy  AS sdc 
		  ON dc.DocumentCopyId = sdc.DocumentCopyId 
		  INNER JOIN [user] AS u 
		  ON dc.UserCurrentId = u.UserId 
		  INNER JOIN dbo.docfinish  AS df 
		  ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId
	WHERE 
	   sdc.StorePrivateId = @storePrivateId AND 
	   dc.DateReceived <= @fromDate AND 
	   (@quicksearch = '' OR d.Compendium LIKE @quicksearch)
   ) AS tbl
WHERE tbl.idx BETWEEN @skip AND @skip + @take

/*
*   Câu truy vấn tính tổng các VB, HS
*/
SELECT count_big(1)
FROM 
   dbo.document  AS d 
      INNER JOIN dbo.documentcopy  AS dc 
      ON d.DocumentId = dc.DocumentId 
      INNER JOIN dbo.storeprivate_documentcopy  AS sdc 
      ON dc.DocumentCopyId = sdc.DocumentCopyId 
      INNER JOIN [user] AS u 
      ON dc.UserCurrentId = u.UserId 
      INNER JOIN dbo.docfinish  AS df 
      ON dc.DocumentCopyId = df.DocumentCopyId AND dc.UserCurrentId = df.UserId

