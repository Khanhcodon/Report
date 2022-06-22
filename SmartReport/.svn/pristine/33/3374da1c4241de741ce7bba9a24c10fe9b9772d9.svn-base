ALTER TABLE `fee`
CHANGE COLUMN `IsRequierd` `IsRequired`  bit(1) NOT NULL COMMENT 'Là bắt buộc (true) Không bắt buộc (false)' AFTER `Price`;

ALTER TABLE `doc_paper`
ADD COLUMN `SupplementaryId`  int(11) NULL AFTER `Type`;

ALTER TABLE `doc_fee`
ADD COLUMN `SupplementaryId`  int(11) NULL AFTER `Type`;
