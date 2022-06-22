ALTER TABLE `mail`
MODIFY COLUMN `CountSentFail`  int(11) NULL AFTER `HasSentFail`,
ADD COLUMN `LinkApi`  varchar(255) NULL AFTER `CountSentFail`,
ADD COLUMN `TokenApi`  varchar(255) NULL AFTER `LinkApi`;

ALTER TABLE `sms`
ADD COLUMN `LinkApi`  varchar(255) NULL AFTER `CountSendFail`,
ADD COLUMN `TokenApi`  varchar(255) NULL AFTER `LinkApi`;
