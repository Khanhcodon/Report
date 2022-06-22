ALTER TABLE `doctype`
ADD COLUMN `HasOverdueInNode`  bit NOT NULL AFTER `Order`;

UPDATE `doctype`
SET `HasOverdueInNode` = 1;

UPDATE documentcopy dc
JOIN doctype dt on dt.DoctypeId = dc.DoctypeId
set dc.DateOverdue = NULL
where dt.IsActivated and dt.HasOverdueInNode = 0;