UPDATE documentcopy dc
SET dc.DocumentUsers = CONCAT(IF(dc.UserNguoiThamGia is NULL, "", dc.UserNguoiThamGia), IF(dc.UserThongBao is NULL, "", dc.UserThongBao));