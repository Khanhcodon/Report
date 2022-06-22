CREATE TABLE `check_infringe` (
  `CheckInfringeId` int(11) NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL,
  `Detail` longtext CHARACTER SET utf8 COLLATE utf8_unicode_ci NOT NULL,
  `InfringeUserId` int(11) NOT NULL,
  `CreateUserId` int(11) NOT NULL,
  `RateEmployeeId` int(11) NOT NULL,
  `IsActived` bit(1) DEFAULT NULL,
  `Cause` varchar(1000) CHARACTER SET utf8 COLLATE utf8_unicode_ci DEFAULT NULL,
  PRIMARY KEY (`CheckInfringeId`),
  KEY `RateEmployeeId` (`RateEmployeeId`),
  CONSTRAINT `check_infringe_ibfk_1` FOREIGN KEY (`RateEmployeeId`) REFERENCES `rateemployee` (`RateEmployeeId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
