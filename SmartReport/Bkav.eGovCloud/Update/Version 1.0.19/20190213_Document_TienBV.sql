ALTER TABLE `document`
ADD COLUMN `LienThongStatus`  varchar(20) NOT NULL DEFAULT '' AFTER `CategoryName`;