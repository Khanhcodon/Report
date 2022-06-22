
INSERT INTO `report` VALUES ('205', 'Báo cáo thu chi lệ phí tiếp nhận hồ sơ', 'Báo cáo thu chi lệ phí', '0', '[6]', 'select UPPER(df.DocTypeName) as DocTypeName, df.DocCode, \r\n		CONVERT(IFNULL(dtf.dtPrice * 1000, 0),UNSIGNED INTEGER) as DtPrice, \r\n		CONVERT(IFNULL(df.dPrice * 1000, 0),UNSIGNED INTEGER) as DPrice, \r\n		IFNULL(dtf.dtPrice * 1000 - df.dPrice * 1000, 0) as Remain\r\nfrom\r\n	( select d.DocTypeId, dt.DocTypeName, df.DocumentId, d.DocCode, SUM(df.Price) as dPrice\r\n	FROM doc_fee df\r\n	JOIN document d on d.DocumentId = df.DocumentId\r\n	JOIN doctype dt on dt.DocTypeId = d.DocTypeId\r\n	WHERE df.Price != 0\r\n		AND d.`Status` not in (1,8)\r\n		AND d.CategoryBusinessId = 4\r\n		AND d.DocCode != \'\' and d.DocCode is NOT NULL\r\n		AND d.DocTypeId is not NULL		\r\n		-- AND TIMEDIFF(d.DateCreated, @from) >= 0 \r\n		-- AND TIMEDIFF(d.DateCreated, @to) <= 0\r\n    -- AND (@treeGroupValue IS NULL OR  @treeGroupValue=\'\' or #treeGroup = @treeGroupValue)\r\n	GROUP BY df.DocumentId) as df\r\nLeft JOIN\r\n (select df.DoctypeId, SUM(f.Price) as dtPrice\r\n	from doctype_fee df\r\n	JOIN fee f on f.FeeId = df.FeeId\r\n	GROUP BY df.DoctypeId) as dtf on df.DocTypeId = dtf.DoctypeId;', null, 'select \r\n	IF(df.DocCode is null, \"\", df.DocTypeName) as DocTypeName,\r\n	IFNULL(df.DocCode, \"Tổng\") as DocCode, \r\n	FORMAT(SUM(dtf.dtPrice), 0) as dtPrice, \r\n	FORMAT(SUM(df.dPrice), 0) as dPrice, \r\n	FORMAT(SUM(dtf.dtPrice - df.dPrice), 0) as remain\r\nfrom\r\n		(select d.DocTypeId, dt.DocTypeName, d.DocumentId, d.DocCode, d.DateCreated, SUM(df.Price * 1000) as dPrice\r\n		FROM doc_fee df\r\n		JOIN document d on d.DocumentId = df.DocumentId\r\n		JOIN doctype dt on d.DocTypeId = dt.DocTypeId\r\n		WHERE df.Price != 0\r\n			AND d.`Status` not in (1,8)\r\n			AND d.CategoryBusinessId = 4\r\n			AND d.DocCode != \'\' and d.DocCode is NOT NULL\r\n			AND d.DocTypeId is not NULL		\r\n			AND TIMEDIFF(d.DateCreated, @from) >= 0 \r\n			AND TIMEDIFF(d.DateCreated, @to) <= 0\r\n			AND (@groupValue = \'\' or #groupValue = @groupValue)\r\n			AND (@treeGroupValue IS NULL OR  @treeGroupValue=\'\' or #treeGroup = @treeGroupValue)\r\n		GROUP BY df.DocumentId)\r\nas df\r\nLeft JOIN\r\n	 (select df.DoctypeId, SUM(f.Price) as dtPrice\r\n		from doctype_fee df\r\n		JOIN fee f on f.FeeId = df.FeeId\r\n		GROUP BY df.DoctypeId)\r\nas dtf on df.DocTypeId = dtf.DoctypeId\r\nGROUP BY df.DocCode WITH ROLLUP;', '\r\nselect \r\n	#groupValue as GroupValue,\r\n	#groupName as GroupName,\r\n	COUNT(#groupValue) as Total\r\nfrom\r\n	( select doc.DocTypeId, dt.DocTypeName, df.DocumentId, doc.DocCode, SUM(df.Price) as dPrice\r\n	FROM doc_fee df\r\n	JOIN document doc on doc.DocumentId = df.DocumentId\r\n	JOIN doctype dt on dt.DocTypeId = doc.DocTypeId\r\n	WHERE df.Price != 0\r\n		AND doc.`Status` not in (1,8)\r\n		AND doc.CategoryBusinessId = 4\r\n		AND doc.DocCode != \'\' and doc.DocCode is NOT NULL\r\n		AND doc.DocTypeId is not NULL		\r\n		AND TIMEDIFF(doc.DateCreated, @from) >= 0 \r\n		AND TIMEDIFF(doc.DateCreated, @to) <= 0\r\n	GROUP BY df.DocumentId) as d\r\nLeft JOIN\r\n (select dt.DocTypeName, df.DoctypeId, SUM(f.Price) as dtPrice\r\n	from doctype_fee df\r\n	JOIN doctype dt on dt.DocTypeId = df.DoctypeId\r\n	JOIN fee f on f.FeeId = df.FeeId\r\n	GROUP BY df.DoctypeId) as dt on d.DocTypeId = dt.DoctypeId;', 'select count(1)\r\nfrom\r\n	( select d.DocTypeId, dt.DocTypeName, df.DocumentId, d.DocCode, SUM(df.Price) as dPrice\r\n	FROM doc_fee df\r\n	JOIN document d on d.DocumentId = df.DocumentId\r\n	JOIN doctype dt on dt.DocTypeId = d.DocTypeId\r\n	WHERE df.Price != 0\r\n		AND d.`Status` not in (1,8)\r\n		AND d.CategoryBusinessId = 4\r\n		AND d.DocCode != \'\' and d.DocCode is NOT NULL\r\n		AND d.DocTypeId is not NULL		\r\n		AND TIMEDIFF(d.DateCreated, @from) >= 0 \r\n		AND TIMEDIFF(d.DateCreated, @to) <= 0\r\n		AND (@groupValue = \'\' or #groupValue = @groupValue)\r\n    AND (@treeGroupValue IS NULL OR  @treeGroupValue=\'\' or #treeGroup = @treeGroupValue)\r\n	GROUP BY df.DocumentId) as df\r\nLeft JOIN\r\n (select df.DoctypeId, SUM(f.Price) as dtPrice\r\n	from doctype_fee df\r\n	JOIN fee f on f.FeeId = df.FeeId\r\n	GROUP BY df.DoctypeId) as dtf on df.DocTypeId = dtf.DoctypeId;', null, '{\"Fields\":[{\"Name\":\"Tên công dân\",\"Key\":\"CitizenName\",\"Type\":1,\"DataType\":\"string\"},{\"Name\":\"Mã hồ sơ\",\"Key\":\"DocCode\",\"Type\":1,\"DataType\":\"string\"},{\"Name\":\"Trích yếu\",\"Key\":\"Compendium\",\"Type\":1,\"DataType\":\"string\"},{\"Name\":\"Ngày tiếp nhận\",\"Key\":\"DateCreated\",\"Type\":1,\"DataType\":\"datetime\"},{\"Name\":\"Cán bộ tiếp nhận\",\"Key\":\"UserCreated\",\"Type\":1,\"DataType\":\"string\"},{\"Name\":\"Ngày hẹn trả\",\"Key\":\"DateAppointed\",\"Type\":1,\"DataType\":\"datetime\"},{\"Name\":\"Phải thu\",\"Key\":\"dtPrice\",\"Type\":1,\"DataType\":\"string\"},{\"Name\":\"Đã thu\",\"Key\":\"dPrice\",\"Type\":1,\"DataType\":\"string\"},{\"Name\":\"Còn lại\",\"Key\":\"remain\",\"Type\":1,\"DataType\":\"string\"},{\"Name\":\"Ngày tháng hiện tại\",\"Key\":\"CurrentDate\",\"Type\":2,\"DataType\":\"datetime\"},{\"Name\":\"Từ thời gian\",\"Key\":\"FromDate\",\"Type\":2,\"DataType\":\"datetime\"},{\"Name\":\"Đến thời gian\",\"Key\":\"ToDate\",\"Type\":2,\"DataType\":\"datetime\"},{\"Name\":\"Số thứ tự\",\"Key\":\"Stt\",\"Type\":2,\"DataType\":\"number\"},{\"Name\":\"Tên nhóm\",\"Key\":\"GroupName\",\"Type\":3,\"DataType\":\"string\"},{\"Name\":\"Số lượng\",\"Key\":\"GroupCount\",\"Type\":3,\"DataType\":\"number\"},{\"Name\":\"Số thứ tự\",\"Key\":\"gStt\",\"Type\":3,\"DataType\":\"number\"}],\"UsedFields\":[{\"Name\":\"Tên nhóm\",\"Key\":\"[GroupName]\",\"Type\":3,\"Datatype\":\"string\",\"Style\":{\"Top\":0,\"Left\":0,\"Width\":757.5,\"Height\":15,\"FullStyle\":\"width: 1015px;\"}},{\"Name\":\"Số thứ tự\",\"Key\":\"[Stt]\",\"Type\":2,\"Datatype\":\"number\",\"Style\":{\"Top\":0,\"Left\":0,\"Width\":50.25,\"Height\":15,\"FullStyle\":\"width: 70px; top: 0px; left: 0px; height: 13px;\"}},{\"Name\":\"Mã hồ sơ\",\"Key\":\"[DocCode]\",\"Type\":1,\"Datatype\":\"string\",\"Style\":{\"Top\":0,\"Left\":54.75,\"Width\":190.5,\"Height\":15,\"FullStyle\":\"width: 259px; top: 0px; left: 0px; height: 20px;\"}},{\"Name\":\"Phải thu\",\"Key\":\"[dtPrice]\",\"Type\":1,\"Datatype\":\"string\",\"Style\":{\"Top\":0,\"Left\":249.75,\"Width\":189,\"Height\":15,\"FullStyle\":\"width: 258px; text-align: right;\"}},{\"Name\":\"Đã thu\",\"Key\":\"[dPrice]\",\"Type\":1,\"Datatype\":\"string\",\"Style\":{\"Top\":0,\"Left\":443.25,\"Width\":153,\"Height\":15,\"FullStyle\":\"width: 209px; text-align: right;\"}},{\"Name\":\"Còn lại\",\"Key\":\"[remain]\",\"Type\":1,\"Datatype\":\"string\",\"Style\":{\"Top\":0,\"Left\":600.75,\"Width\":156.75,\"Height\":15,\"FullStyle\":\"width: 214px; text-align: right;\"}}],\"Summaries\":[],\"pageSettup\":{},\"Content\":{\"header\":\"<div style=\\\"height: 49px;\\\" class=\\\"report-content\\\"><table class=\\\"table-header\\\" cellpadding=\\\"0\\\" cellspacing=\\\"0\\\" style=\\\"border-collapse: collapse; position: absolute; width: 100%; top: 20px;; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:0pt; mso-table-top:15pt; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:0pt; mso-table-top:15pt; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:0pt; mso-table-top:15pt; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:0pt; mso-table-top:15pt; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:0pt; mso-table-top:15pt; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:0pt; mso-table-top:15pt; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:0pt; mso-table-top:15pt; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:0pt; mso-table-top:15pt; mso-table-anchor-vertical:paragraph; mso-table-anchor-horizontal:margin; mso-table-left:0pt; mso-table-top:15pt\\\">                     <thead>                         <tr class=\\\"header\\\">                                                     <th col=\\\"0\\\" class=\\\"\\\" style=\\\"width: 75px; top: 0px; left: 0px; height: 17px;\\\"><div class=\\\"report-toolbox-field\\\" data-value=\\\"Stt\\\" type=\\\"header\\\">Stt</div></th>                                                     <th col=\\\"1\\\" class=\\\"\\\" style=\\\"width: 244px; top: 0px; left: 0px; height: 22px;\\\"><div class=\\\"report-toolbox-field\\\" data-value=\\\"Mã hồ sơ\\\" type=\\\"header\\\">Mã hồ sơ</div></th>                                                     <th col=\\\"2\\\" class=\\\"\\\" style=\\\"width: 245px; top: 0px; left: 0px; height: 22px;\\\"><div class=\\\"report-toolbox-field\\\" data-value=\\\"Lệ phí phải thu\\\" type=\\\"header\\\">Lệ phí phải thu</div></th>                                                     <th col=\\\"3\\\" class=\\\"\\\" style=\\\"width: 223px; top: 0px; left: 0px; height: 23px;\\\"><div class=\\\"report-toolbox-field\\\" data-value=\\\"Lệ phí đã thu\\\" type=\\\"header\\\">Lệ phí đã thu</div></th>                                                     <th col=\\\"4\\\" class=\\\"\\\" style=\\\"width: 223px;\\\"><div class=\\\"report-toolbox-field\\\" data-value=\\\"Còn lại\\\" type=\\\"header\\\">Còn lại</div></th>                                                 </tr>                     </thead>                 </table></div>\",\"footer\":\"<div style=\\\"height: 25px;\\\" class=\\\"report-content\\\"></div>\",\"detail\":\"<div style=\\\"height: 25px;\\\" class=\\\"report-content\\\"><table class=\\\"table-detail\\\" cellpadding=\\\"0\\\" cellspacing=\\\"0\\\" style=\\\"border-collapse: collapse; position: relative; width: 100%; left: 0px;\\\">                 <tbody>                     <tr class=\\\"detail\\\">                                             <td col=\\\"0\\\" class=\\\"used-field word-wrap\\\" style=\\\"width: 70px; top: 0px; left: 0px; height: 13px;\\\" datatype=\\\"number\\\" type=\\\"data\\\">[Stt]</td>                                             <td col=\\\"1\\\" class=\\\"used-field word-wrap\\\" style=\\\"width: 259px; top: 0px; left: 0px; height: 20px;\\\" datatype=\\\"string\\\" type=\\\"data\\\">[DocCode]</td>                                             <td col=\\\"2\\\" class=\\\"used-field word-wrap\\\" style=\\\"width: 258px; text-align: right;\\\" datatype=\\\"string\\\" type=\\\"data\\\">[dtPrice]</td>                                             <td col=\\\"3\\\" class=\\\"used-field word-wrap\\\" style=\\\"width: 209px; text-align: right;\\\" datatype=\\\"string\\\" type=\\\"data\\\">[dPrice]</td>                                             <td col=\\\"4\\\" class=\\\"used-field word-wrap\\\" style=\\\"width: 214px; text-align: right;\\\" datatype=\\\"string\\\" type=\\\"data\\\">[remain]</td>                                         </tr>                 </tbody>             </table></div>\",\"gHeader\":\"<div style=\\\"height: 25px;\\\" class=\\\"report-content\\\"><table class=\\\"table-detail\\\" cellpadding=\\\"0\\\" cellspacing=\\\"0\\\" style=\\\"border-collapse: collapse; position: relative; width: 100%;\\\">                 <tbody>                     <tr class=\\\"group-header\\\">                                             <td col=\\\"0\\\" class=\\\"used-field word-wrap\\\" style=\\\"width: 1015px;\\\" colspan=\\\"5\\\" datatype=\\\"string\\\" type=\\\"data\\\">[GroupName]</td>                                                                                                                                                                                                                             </tr>                 </tbody>             </table></div>\",\"gFooter\":\"<div style=\\\"height: 25px; display: none;\\\" class=\\\"report-content\\\"></div>\"}}', 'undefined', 'BaoCao_ThuChiLePhi.rpt', '8d79399f4ff0493eac9769fefda6bb65.rpt', null, null, null, null, null, null, '', '', '', '0001-01-01', 'select 0;', 'select 0;');