ALTER TABLE `mail`
DROP COLUMN `DocumentId`,
DROP COLUMN `DocumentCopyId`,
DROP COLUMN `NotifyConfigType`,
ADD COLUMN `HasSentFail`  bit NOT NULL AFTER `UserName`,
ADD COLUMN `CountSentFail`  int NOT NULL AFTER `HasSentFail`;

