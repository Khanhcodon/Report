/*
Navicat MySQL Data Transfer

Source Server         : 10.2.65.100
Source Server Version : 50721
Source Host           : 10.2.65.100:3306
Source Database       : smartcity_qn

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2019-11-29 15:40:56
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `notifyWarning`
-- ----------------------------
DROP PROCEDURE IF EXISTS `notifyWarning`;
DELIMITER ;;
CREATE DEFINER=`smartcity`@`%` PROCEDURE `notifyWarning`(`userId` int)
BEGIN
	#Routine body goes here...
	(SELECT "Tổng số văn bản" as 'Name', COUNT(1) as 'Count'
	FROM documentcopy dc
	WHERE dc.UserCurrentId = `userId`
	AND `dc`.`Status` = 2)
	UNION
	(SELECT "Văn bản quá hạn" as 'Name', COUNT(1) as 'Count'
	FROM documentcopy dc
	JOIN document d on d.DocumentId = dc.DocumentId
	WHERE dc.UserCurrentId = `userId`
	AND `dc`.`Status` = 2 
	AND d.DateAppointed is not null
	AND d.DateAppointed < Now())
	UNION
	(SELECT "Văn bản khẩn" as 'Name', COUNT(1) as 'Count'
	FROM documentcopy dc
	JOIN document d on d.DocumentId = dc.DocumentId
	WHERE dc.UserCurrentId = `userId`
	AND `dc`.`Status` = 2 
	And d.UrgentId > 2);
END
;;
DELIMITER ;
