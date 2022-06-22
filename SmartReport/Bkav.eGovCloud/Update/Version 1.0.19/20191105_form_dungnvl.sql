ALTER TABLE `form`
ADD COLUMN `ConfigFunction`  text NULL AFTER `VersionForm`,
ADD COLUMN `CompilationId`  char(36) NULL AFTER `ConfigFunction`,
ADD COLUMN `ChildCompilationId`  char(36) NULL AFTER `CompilationId`;