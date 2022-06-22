DROP TABLE IF EXISTS notifications;
DROP TABLE IF EXISTS `notification`;
CREATE TABLE `notification` (
  `NotificationId` int(11) NOT NULL AUTO_INCREMENT,
  `NotificationType` int(11) DEFAULT NULL,
  `UserId` int(11) NOT NULL,
  `MailId` VARCHAR(50) DEFAULT NULL,
  `FolderId` VARCHAR(50) NULL,
  `FolderLocation` VARCHAR(500) NULL,
  `ChatId` int(11) DEFAULT NULL,
  `ChatterJid` VARCHAR(500),
  `DocumentCopyId` int(11) DEFAULT NULL,
  `Title` varchar(1500) NOT NULL,
  `Content` varchar(500) NOT NULL,
  `SenderAvatar` varchar(500) DEFAULT NULL,
  `SenderUserName` varchar(50) DEFAULT NULL,
  `SenderFullName` varchar(255) DEFAULT NULL,
  `Date` datetime NOT NULL,
  `ReceiveDate` datetime NOT NULL,
  `ViewdDate` datetime DEFAULT NULL,
  `Hidden` bit(1) DEFAULT NULL,
  PRIMARY KEY (`NotificationId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
