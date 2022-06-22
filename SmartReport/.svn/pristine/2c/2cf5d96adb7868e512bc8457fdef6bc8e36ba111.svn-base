CREATE TABLE `time_job` (
  `TimerJobId` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(500) NOT NULL,
  `TimerJobType` int(11) NOT NULL,
  `DateLastJobRun` datetime DEFAULT NULL,
  `JobInterval` datetime NOT NULL,
  `DateNextJobStartAfter` datetime NOT NULL,
  `DateNextJobStartBefore` datetime NOT NULL,
  `ScheduleType` int(11) NOT NULL,
  `ScheduleConfig` text,
  `IsActive` bit(1) NOT NULL,
  `IsRunning` bit(1) NOT NULL,
  PRIMARY KEY (`TimerJobId`)
) 

