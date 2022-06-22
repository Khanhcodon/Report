ALTER TABLE docrelation
ADD COLUMN `Compendium` LONGTEXT NULL,
ADD COLUMN `DocCode` varchar(150) NULL,
ADD COLUMN `DateArrived` datetime NULL;