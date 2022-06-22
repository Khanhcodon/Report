CREATE TABLE `otp` (
	`OtpId`  int(11) NOT NULL AUTO_INCREMENT ,
	`Content`  varchar(250) NOT NULL ,
	`TimeStart`  datetime NOT NULL ,
	`Email`  varchar(250) NOT NULL ,
	`Sms`  varchar(250) NOT NULL ,
	`Status`  bit NOT NULL ,
	`ActivedCode`  varchar(50) NOT NULL ,
	`ActivedUrl`  varchar(250) NULL ,
	`UserId`  int(11) NOT NULL,
PRIMARY KEY (`OtpId`)
)
;