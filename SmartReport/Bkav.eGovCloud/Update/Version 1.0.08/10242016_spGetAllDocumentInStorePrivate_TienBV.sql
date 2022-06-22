
-- ----------------------------
-- Procedure structure for spGetAllDocumentInStorePrivate
-- ----------------------------
DROP PROCEDURE IF EXISTS `spGetAllDocumentInStorePrivate`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` PROCEDURE `spGetAllDocumentInStorePrivate`(IN `storePrivateId` int)
BEGIN
	SELECT
	*
FROM
	(
		SELECT
			'' AS `DocCode`,
			`sa`.`AttachmentName` AS `Compendium`,
			`sa`.`CreatedByUserName` AS `Email`,
			`sa`.`CreatedOnDate` AS `DateCreated`,
			NULL AS `DateAppointed`,
			NULL AS `DateFinished`,
			NULL AS `DateRequireSupplementary`,
			NULL AS `DateSuccess`,
			NULL AS `DateReturned`,
			2 AS `Status`,
			NULL AS `LastComment`,
			`sa`.`CreatedOnDate` AS `DateReceived`,
			`sa`.`StorePrivateAttachmentId` AS `DocumentCopyId`,
			1 AS `IsViewed`,
			NULL AS `UserCurrentFullName`,
			NULL AS `UserCurrentFirstName`,
			1 AS `Color`,
			1 AS `IsFile`
		FROM
			`storeprivate_attachment` AS `sa`
		WHERE
			`sa`.`StorePrivateId` = storePrivateId
		UNION
			SELECT
				`d`.`DocCode`,
				`d`.`Compendium`,
				`d`.`Email`,
				`d`.`DateCreated`,
				`d`.`DateAppointed`,
				`d`.`DateFinished`,
				`d`.`DateRequireSupplementary`,
				`d`.`DateSuccess`,
				`d`.`DateReturned`,				
				`d`.`Status`,
				`dc`.`LastComment`,
				`dc`.`DateReceived`,
				`dc`.`DocumentCopyId`,
				`df`.`IsViewed`,
				`u`.`FullName` AS `UserCurrentFullName`,
				`u`.`FirstName` AS `UserCurrentFirstName`,
				1 AS `Color`,
				0 AS `IsFile`
			FROM
				`document` AS `d`
			INNER JOIN `documentcopy` AS `dc` ON `d`.`DocumentId` = `dc`.`DocumentId`
			INNER JOIN `storeprivate_documentcopy` AS `sdc` ON `dc`.`DocumentCopyId` = `sdc`.`DocumentCopyId`
			INNER JOIN `user` AS `u` ON `dc`.`UserCurrentId` = `u`.`UserId`
			INNER JOIN `docfinish` AS `df` ON `dc`.`DocumentCopyId` = `df`.`DocumentCopyId`
			AND `dc`.UserCurrentId = `df`.UserId
			WHERE
				`sdc`.`StorePrivateId` = storePrivateId
	) AS `a`
ORDER BY
	`a`.`DateReceived` DESC;
END
;;
DELIMITER ;
