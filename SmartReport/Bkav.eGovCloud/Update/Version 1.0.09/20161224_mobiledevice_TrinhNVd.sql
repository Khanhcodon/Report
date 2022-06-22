
CREATE TABLE `mobiledevice` (
  `MobileDeviceId` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NULL,
  `OS` int(11) NOT NULL,
  `Serial` VARCHAR(100) NOT NULL,
  `DeviceName` VARCHAR(100) NULL,
  `OSVersion` VARCHAR(50) NULL,
  `Token` VARCHAR(1000) NULL,
  `CreatedDate` datetime NOT NULL,
  `LastUpdate` datetime NULL,
  `IsActive` bit(1) NULL,
  PRIMARY KEY (`MobileDeviceId`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;
