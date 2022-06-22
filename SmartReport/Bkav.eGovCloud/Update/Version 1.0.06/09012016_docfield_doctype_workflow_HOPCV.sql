CREATE TABLE `docfield_doctype_workflow` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `IsActivated` bit(1) NOT NULL,
  `WorkflowId` int(11) NOT NULL,
  `DocFieldId` int(11) DEFAULT NULL,
  `DocTypeId` char(36) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
