/*
Navicat MySQL Data Transfer

Source Server         : .
Source Server Version : 50715
Source Host           : localhost:3306
Source Database       : egov_beta_57

Target Server Type    : MYSQL
Target Server Version : 50715
File Encoding         : 65001

Date: 2018-04-20 17:20:34
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Procedure structure for `Giamsat_phathanh_hoibao`
-- ----------------------------
DROP PROCEDURE IF EXISTS `Giamsat_phathanh_hoibao`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Giamsat_phathanh_hoibao`(IN `@from` datetime,IN `@to` datetime)
BEGIN
	#Routine body goes here...

	SELECT 
		DATE_FORMAT(dp.DatePublished, '%d/%m/%y') as DatePublished,
		dp.DocCode,
		d.Compendium,
		d.UserSuccessName as UserSuccess,
		GROUP_CONCAT(dp.AddressName SEPARATOR '\n') as ProcessInfo,
		d.InOutPlace,
		d.CategoryName,
		d.UserCreatedName,
		d.UserCreatedId
	FROM doc_publish dp
	JOIN document d on d.DocumentId = dp.DocumentId
	WHERE 		
		d.CategoryBusinessId = 2
		and (dp.HasRequireResponse is not null and dp.HasRequireResponse = FALSE and dp.IsResponsed = true)
		AND dp.DatePublished >= `@from`
		AND dp.DatePublished <= `@to`
	GROUP BY dp.DocumentCopyId
	ORDER BY dp.DatePublished;
END
;;
DELIMITER ;

-- ----------------------------
-- Procedure structure for `Giamsat_tw_den`
-- ----------------------------
DROP PROCEDURE IF EXISTS `Giamsat_tw_den`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `Giamsat_tw_den`(IN `@from` datetime,IN `@to` datetime)
BEGIN
	#Routine body goes here...

	SELECT 
	DATE_FORMAT(d.DateArrived, '%d/%m/%y') as DateArrived,
		d.InOutCode,
		d.Organization,
		d.DocCode,
		d.CategoryName,
		DATE_FORMAT(d.DatePublished, '%d/%m/%y') as DatePublished,
		d.Compendium,
		REPLACE(d.ProcessInfo, 'Nơi nhận: ', '') as ProcessInfo,
		DATE_FORMAT(d.DateAppointed, '%d/%m/%y') as DateAppointed,
		dr.DocCode as AnswerDocCode
	from document d
	LEFT JOIN docrelation dr on dr.DocumentId = d.DocumentId and dr.RelationType = 2
	WHERE 		
		d.CategoryBusinessId = 1
		AND d.DateArrived >= `@from`
		AND d.DateArrived <= `@to`
		AND d.Original = 2
    and d.Organization = "Văn Phòng Chính phủ"
	ORDER BY d.DateArrived, d.InOutCode;
END
;;
DELIMITER ;
