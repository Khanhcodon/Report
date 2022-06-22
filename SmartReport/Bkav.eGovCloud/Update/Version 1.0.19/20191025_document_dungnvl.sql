ALTER TABLE `document`
ADD COLUMN `TimeKey`  varchar(255) NULL AFTER `LienThongStatus`,
ADD COLUMN `CompilationData`  text NULL AFTER `TimeKey`;
ADD COLUMN `FormId`  varchar(255) NULL AFTER `CompilationData`;