CREATE TABLE `tree_group` (
  `TreeGroupId` int(11) NOT NULL AUTO_INCREMENT,
  `TreeGroupName` varchar(255) NOT NULL,
  `IsShowUserFullName` bit(1) NOT NULL DEFAULT b'0',
  `IsActived` bit(1) NOT NULL DEFAULT b'1',
  `DateCreated` datetime DEFAULT NULL,
  `UserNameCreated` varchar(125) DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  `UserNameModified` varchar(125) DEFAULT NULL,
  `Order` int(11) DEFAULT NULL,
  `IsShowTreeName` bit(1) NOT NULL,
  `HasChildrenContextMenuAdmin` bit(1) NOT NULL DEFAULT b'1',
  `IsDocumentTree` bit(1) NOT NULL DEFAULT b'1',
  `IsOtherSystems` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`TreeGroupId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
