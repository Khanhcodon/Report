CREATE TABLE `mail` (
  `MailId` int(11) NOT NULL AUTO_INCREMENT,
  `Subject` varchar(500) NOT NULL,
  `Body` longtext NOT NULL,
  `SendTo` varchar(500) NOT NULL,
  `Signature` longtext,
  `Header` longtext,
  `Sender` varchar(500) DEFAULT NULL,
  `SenderDisplayName` varchar(500) DEFAULT NULL,
  `IsBodyHtml` bit(1) DEFAULT b'1',
  `CarbonCopysStr` varchar(1000) DEFAULT NULL,
  `AttachmentIdStr` varchar(1000) DEFAULT NULL,
  `IsSent` bit(1) DEFAULT b'0',
  `DateCreated` datetime DEFAULT NULL,
  `DateSend` datetime DEFAULT NULL,
  `UserSendId` int(11) DEFAULT NULL,
  `UserName` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`MailId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;