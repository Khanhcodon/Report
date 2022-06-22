-- Phiếu hướng dẫn hoàn thiện hồ sơ
SELECT 
d.CitizenName, 
d.Address AS CitizenAddress, 
d.Email AS Email, 
d.Phone AS CitizenPhone, 
d.Compendium, 
dp.PaperName AS Paper, 
dp.Amount
FROM 
dbo.document  AS d 
INNER JOIN dbo.documentcopy  AS dc 
ON dc.DocumentId = d.DocumentId 
INNER JOIN dbo.paper  AS dp 
ON dp.DocTypeId = d.DocTypeId
WHERE 
dc.DocumentCopyId = @docCopyId AND 
dp.IsRequired = 1 AND 
dp.PaperTypeId = 1
   
--In biên nhận trả kết quả
SELECT 
d.DocCode, 
d.CitizenName, 
d.Address AS CitizenAddress, 
d.Email AS CitizenEmail, 
d.Phone AS CitizenPhone, 
d.Compendium, 
d.ReturnNote AS ReturnedNode, 
dbo.fnDateFormat(d.DateReturned, N'HH giờ mm phút, ngày dd tháng MM năm yyyy') AS DateReturned, 
ur.FullName AS UserReturnedName, 
dbo.fnDateFormat(d.DateCreated, N'HH giờ mm phút, ngày dd tháng MM năm yyyy') AS DateCreated, 
us.FullName AS UserSuccessName, 
dbo.fnDateFormat(d.DateSuccess, N'HH giờ mm phút, ngày dd tháng MM năm yyyy') AS DateSuccess, 
dp.PaperName AS Paper, 
dp.Amount
FROM 
dbo.document  AS d 
   INNER JOIN dbo.documentcopy  AS dc 
   ON dc.DocumentId = d.DocumentId 
   LEFT JOIN [user] AS ur 
   ON ur.UserId = d.UserReturnedId 
   LEFT JOIN [user] AS us 
   ON us.UserId = d.UserSuccessId 
   LEFT JOIN dbo.doc_paper  AS dp 
   ON dp.DocumentId = d.DocumentId AND dp.Type = 3
WHERE dc.DocumentCopyId = @docCopyId

--In biên nhận và hẹn trả kết quả
SELECT 
 d.DocCode, d.CitizenName, d.Address as CitizenAddress, d.Email as Email,
 d.Phone as CitizenPhone, d.Compendium, 
 dbo.fnDateFormat(d.DateCreated, N'HH giờ mm phút, ngày dd tháng MM năm yyyy') as DateCreated, 
 dbo.fnDateFormat(d.DateAppointed, N'HH giờ mm phút, ngày dd tháng MM năm yyyy') as DateAppointed, 
 dp.PaperName as Paper, dp.Amount,
 w.ExpireProcess as TotalTime
FROM document d
Inner JOIN documentcopy dc on dc.DocumentId = d.DocumentId
INNER JOIN workflow w on w.WorkflowId = dc.WorkflowId
LEFT JOIN doc_paper dp on dp.DocumentId = d.DocumentId
WHERE dc.DocumentCopyId = @docCopyId

--In biên nhận bàn giao
SELECT d.DocCode, d.CitizenName
FROM 
dbo.documentcopy  AS dc 
   INNER JOIN dbo.document  AS d 
   ON dc.DocumentId = d.DocumentId
WHERE dc.DocumentCopyId IN (#docCopyIds)

--In biên nhận bổ sung
SELECT doc.*, feepaper.PaperFeeName, feepaper.Amount, feepaper.PaperFeeType from
(SELECT 
		d.DocumentId, d.DocCode, d.CitizenName, d.Address,
		dt.DocTypeName as DoctypeName
	FROM documentcopy dc 
	INNER JOIN document d ON dc.DocumentId = d.DocumentId 
	INNER JOIN doctype dt on dt.DocTypeId = dc.DocTypeId
	Where dc.DocumentCopyId = @docCopyId) as doc
LEFT JOIN
(SELECT dp.DocumentId, dp.PaperName as PaperFeeName,  CAST(dp.Amount as VARCHAR(16)) + N' bản' as Amount, N'Giấy tờ đã nộp bổ sung' as PaperFeeType
from doc_paper dp where dp.Type = 2 AND (@suppId = 0 OR dp.SupplementaryId = @suppId)
UNION ALL
SELECT df.DocumentId, df.FeeName as PaperFeeName, CAST(df.Price as VARCHAR(16)) + N' Đồng' as Amount, N'Lệ phí nộp bổ sung' as PaperFeeType
from doc_fee df where df.Type = 2 AND (@suppId = 0 OR df.SupplementaryId = @suppId))
	as feepaper 
on doc.DocumentId = feepaper.DocumentId;

--Phiếu kiểm soát quá trình giải quyết hồ sơ
SELECT 
d.DocCode, 
d.InOutPlace, 
dtl.NodeSendName AS NodeSent, 
dtl.NodeName AS NodeReceived, 
usersend.FullName AS UserSent, 
userreceive.FullName AS UserReceived, 
dbo.fnDateFormat(
   CASE 
      WHEN (dtl.ConfirmedOn IS NOT NULL) THEN dtl.ConfirmedOn
      ELSE dtl.FromDate
   END, N'HH giờ mm phút, ngày dd tháng MM năm yyyy') AS DateReceived, 
CASE 
   WHEN ((dtl.TimeInNode * 60) > dtl.ProcessedMinutes) THEN N'Trước hạn'
ELSE CASE 
WHEN ((dtl.TimeInNode * 60) = dtl.ProcessedMinutes) THEN N'Đúng hạn'
ELSE N'Quá hạn'
END
END AS Overdue, 
CASE 
WHEN (CAST(dtl.IsSuccess AS bigint) = 1) THEN N'Đã xử lý'
ELSE N'Đang xử lý'
END AS Note
FROM 
dbo.document  AS d 
INNER JOIN dbo.documentcopy  AS dc 
ON dc.DocumentId = d.DocumentId 
INNER JOIN dbo.doctimeline  AS dtl 
ON dtl.DocumentCopyId = dc.DocumentCopyId 
INNER JOIN [user] AS usersend 
ON usersend.UserId = dtl.UserSendId 
INNER JOIN [user] AS userreceive 
ON userreceive.UserId = dtl.UserId
WHERE dc.DocumentCopyId = @docCopyId AND dtl.NodeSendId <> dtl.NodeId
