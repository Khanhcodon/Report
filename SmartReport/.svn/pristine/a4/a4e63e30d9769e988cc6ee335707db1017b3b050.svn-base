ALTER TABLE `supplementary`
ADD COLUMN `IsReceived`  bit NULL,
ADD COLUMN `Papers`  text NULL AFTER `IsReceived`,
ADD COLUMN `Fees`  text NULL AFTER `Papers`;

UPDATE supplementary
SET IsReceived = CASE
			WHEN DateReceived IS NOT NULL THEN 1
			WHEN DateReceived IS NULL THEN 0
		END;

ALTER TABLE `supplementary`
MODIFY COLUMN `IsReceived`  bit(1) NOT NULL;
