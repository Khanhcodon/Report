/*Filename: update_version_date*/
DELIMITER //
DROP PROCEDURE IF EXISTS pro_updateDB //
CREATE PROCEDURE pro_updateDB() 
BEGIN   
    IF NOT EXISTS( SELECT NULL
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE table_name = 'documentcopy' 
            AND column_name = 'ReportKey')  THEN
        alter table `documentcopy` add COLUMN `ReportKey` longtext DEFAULT NULL;
		
    END IF;
	
	 IF NOT EXISTS( SELECT NULL
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE table_name = 'form' 
            AND column_name = 'MappingMaDinhDanhCP')  THEN
        alter table `form` add COLUMN `MappingMaDinhDanhCP` text DEFAULT null;
    END IF;
	
END//