/*
Navicat MySQL Data Transfer

Source Server         : .
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_yenbai

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2019-10-23 17:16:40
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Function structure for `getKyBaoCaoDocument`
-- ----------------------------
DROP FUNCTION IF EXISTS `getKyBaoCaoDocument`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `getKyBaoCaoDocument`(`ActionLevel` int,`DatePublished` datetime) RETURNS varchar(100) CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	#Routine body goes here...
	DECLARE `result` varchar(50);	
	IF (`DatePublished` is null) then 
		return ''; 
	end if;

	CASE `ActionLevel`
    WHEN 1 THEN 
			SET result = CONCAT("Năm ", YEAR(`DatePublished`));
		WHEN 1 THEN 
			SET result = CONCAT("6T ", IF(MONTH(`DatePublished`) < 6, "đầu năm", "cuối năm"));
    WHEN 3 THEN
			SET result = CONCAT("Quý ", QUARTER(`DatePublished`));
		WHEN 4 THEN
			SET result = CONCAT("Tháng ", MONTH(`DatePublished`));
		WHEN 5 THEN
			SET result = CONCAT("Tuần ", WEEK(`DatePublished`));
		WHEN 6 THEN
			SET result = CONCAT("Ngày ", DATE_FORMAT(`DatePublished`, '%d/%m'));
		ELSE
			SET result = "";
	END CASE;

	RETURN result;
END
;;
DELIMITER ;
