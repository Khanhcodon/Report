CREATE TABLE `rateemployee` (
  `RateEmployeeId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Point` int(11) DEFAULT NULL,
  `Description` text,
  `IsActive` bit(1) DEFAULT NULL,
  `DepartmentId` int(11) DEFAULT NULL,
  `ParentId` int(11) DEFAULT NULL,
  PRIMARY KEY (`RateEmployeeId`),
  KEY `FK_ParentId` (`ParentId`),
  KEY `FK_department` (`DepartmentId`),
  CONSTRAINT `FK_department` FOREIGN KEY (`DepartmentId`) REFERENCES `department` (`DepartmentId`),
  CONSTRAINT `FK_ParentId` FOREIGN KEY (`ParentId`) REFERENCES `rateemployee` (`RateEmployeeId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
