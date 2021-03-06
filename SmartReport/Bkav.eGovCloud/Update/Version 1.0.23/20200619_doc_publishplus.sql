DROP TABLE IF EXISTS `doc_publishplus`;
CREATE TABLE `doc_publishplus` (
  `DocumentPublishPlusId` int(11) NOT NULL AUTO_INCREMENT,
  `DocumentId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `DocumentCopyId` int(11) NOT NULL,
  `DoctypeId` char(36) COLLATE utf8_unicode_ci NOT NULL,
  `DocCode` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `DatePublished` datetime NOT NULL,
  `AddressName` varchar(400) COLLATE utf8_unicode_ci NOT NULL,
  `IsHsmc` bit(1) NOT NULL,
  `UserPublishId` int(11) NOT NULL,
  `UserPublishName` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `HasLienThong` bit(1) NOT NULL,
  `AddressId` int(11) DEFAULT NULL,
  `DateSent` datetime DEFAULT NULL,
  `IsPending` bit(1) NOT NULL,
  `HasRequireResponse` bit(1) NOT NULL,
  `DateAppointed` datetime DEFAULT NULL,
  `IsResponsed` bit(1) NOT NULL,
  `DateResponsed` datetime DEFAULT NULL,
  `DocCodeResponsed` varchar(100) COLLATE utf8_unicode_ci DEFAULT NULL,
  `DocumentCopyResponsed` int(11) DEFAULT NULL,
  `Traces` longtext COLLATE utf8_unicode_ci,
  `AddressCode` varchar(30) COLLATE utf8_unicode_ci DEFAULT NULL,
  `Note` varchar(500) COLLATE utf8_unicode_ci DEFAULT NULL,
  `HasSendFail` bit(1) NOT NULL DEFAULT b'0',
  `IsSentCDDH` bit(1) DEFAULT NULL,
  `Status` tinyint(4) NOT NULL DEFAULT '0',
  `DataSend` MEDIUMTEXT CHARACTER SET utf8 COLLATE utf8_unicode_ci NULL,
  PRIMARY KEY (`DocumentPublishPlusId`),
  KEY `FK_DocumentId` (`DocumentId`) USING BTREE,
  KEY `FK_DocumentCopy` (`DocumentCopyId`) USING BTREE,
  KEY `HasLienThong` (`HasLienThong`) USING BTREE,
  KEY `Address` (`AddressId`) USING BTREE
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;