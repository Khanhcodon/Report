ALTER TABLE `backup_restore_file_config`
CHANGE COLUMN `IsNetwork` `IsNetwork`  bit(1) NULL DEFAULT b'0' AFTER `HasAutoRun`;