ALTER TABLE `documentcopy`
ADD COLUMN `UserThongBao`  text NULL AFTER `DocumentCopyParentPath`,
ADD COLUMN `UserNguoiThamGia`  text NULL AFTER `UserThongBao`,
ADD COLUMN `UserNguoiDaXem`  text NULL AFTER `UserNguoiThamGia`,
ADD COLUMN `UserGiamSat`  text NULL AFTER `UserNguoiDaXem`;

-- Update user thong bao
UPDATE documentcopy dc
JOIN (
		SELECT 
			ParentId, 
			CONCAT(";", GROUP_CONCAT(UserCurrentId SEPARATOR ";"),";") as users 
		FROM documentcopy 
		WHERE DocumentCopyType = 8 GROUP BY ParentId) as dc1 on dc.DocumentCopyId = dc1.ParentId
SET dc.UserThongBao = users;

UPDATE documentcopy dc
JOIN (
		SELECT 
			DocumentCopyId, 
			CONCAT(GROUP_CONCAT(UserId SEPARATOR ";"),";") as users 
		FROM docfinish 
		WHERE DocFinishType = 2 GROUP BY DocumentCopyId) as dc1 on dc.DocumentCopyId = dc1.DocumentCopyId
SET dc.UserThongBao = CONCAT(dc.UserThongBao, dc1.users);

-- Update user nguoi tham gia
UPDATE documentcopy dc
JOIN (
		SELECT 
			DocumentCopyId, 
			CONCAT(";", GROUP_CONCAT(UserId SEPARATOR ";"),";") as users 
		FROM docfinish 
		WHERE DocFinishType = 1 GROUP BY DocumentCopyId) as dc1 on dc.DocumentCopyId = dc1.DocumentCopyId
SET dc.UserNguoiThamGia = dc1.users;

-- Update User nguoi da xem
UPDATE documentcopy dc
JOIN (
		SELECT 
			DocumentCopyId, 
			CONCAT(";", GROUP_CONCAT(UserId SEPARATOR ";"),";") as users 
		FROM docfinish 
		WHERE IsViewed = 1 GROUP BY DocumentCopyId) as dc1 on dc.DocumentCopyId = dc1.DocumentCopyId
SET dc.UserNguoiDaXem = dc1.users;

-- Update user giam sat
UPDATE documentcopy dc
JOIN (
		SELECT 
			DocumentCopyId, 
			CONCAT(";", GROUP_CONCAT(UserId SEPARATOR ";"),";") as users 
		FROM docfinish 
		WHERE DocFinishType = 3 GROUP BY DocumentCopyId) as dc1 on dc.DocumentCopyId = dc1.DocumentCopyId
SET dc.UserNguoiDaXem = dc1.users;