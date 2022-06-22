ALTER TABLE `sms`
ADD COLUMN `HasSendFail`  bit NOT NULL AFTER `NotifyConfigType`,
ADD COLUMN `CountSendFail`  int NOT NULL AFTER `HasSendFail`;

