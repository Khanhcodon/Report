CREATE TABLE `sms` (
  `SmsId` int(11) NOT NULL AUTO_INCREMENT,
  `PhoneNumber` char(15) NOT NULL COMMENT 'Số điện thoại nhận sms',
  `Message` text NOT NULL COMMENT 'Nội dung sms',
  `IsSent` bit(1) DEFAULT b'0' COMMENT 'trạng thái đã gửi sms hay chưa',
  `DateCreated` datetime DEFAULT NULL COMMENT 'Ngày tạo sms',
  `DateSend` datetime DEFAULT NULL COMMENT 'Ngày gủi sms',
  `UserName` varchar(255) DEFAULT NULL,
  `UserSendId` int(11) DEFAULT NULL,
  PRIMARY KEY (`SmsId`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;