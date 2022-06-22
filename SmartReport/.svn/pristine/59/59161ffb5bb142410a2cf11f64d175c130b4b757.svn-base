
ALTER TABLE `documentcopy`
ADD COLUMN `DocumentCopyParentPath`  varchar(150) NULL;

UPDATE documentcopy dc
LEFT JOIN documentcopy parent1 on parent1.DocumentCopyId = dc.ParentId
LEFT JOIN documentcopy parent2 on parent2.DocumentCopyId = parent1.ParentId
LEFT JOIN documentcopy parent3 on parent3.DocumentCopyId = parent2.ParentId
SET
	dc.DocumentCopyParentPath = CASE
					WHEN parent3.DocumentCopyId is not null then CONCAT("\\",parent3.DocumentCopyId, "\\", parent2.DocumentCopyId, "\\", parent1.DocumentCopyId, "\\")
					WHEN parent2.DocumentCopyId is not null then CONCAT("\\",parent2.DocumentCopyId, "\\", parent1.DocumentCopyId, "\\")
					ELSE CONCAT("\\", parent1.DocumentCopyId, "\\")
			END
WHERE dc.ParentId is NOT null;