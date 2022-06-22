UPDATE `workflow` 
set `workflow`.`WorkflowName`
= CONCAT((SELECT d.DocTypeName  from 
doctype d WHERE d.DocTypeId = `workflow`.DocTypeId),' - ',`workflow`.WorkflowName) 

ALTER TABLE `workflow` DROP FOREIGN KEY `workflow_ibfk_1`;

ALTER TABLE `workflow` DROP COLUMN `DocTypeId`;
