ALTER TABLE `doc_publish`
ADD COLUMN `Status`  tinyint NOT NULL DEFAULT 0 AFTER `IsSentCDDH`;

