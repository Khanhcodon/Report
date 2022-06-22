/*
Navicat MySQL Data Transfer

Source Server         : .
Source Server Version : 50721
Source Host           : localhost:3306
Source Database       : eform_new_yb

Target Server Type    : MYSQL
Target Server Version : 50721
File Encoding         : 65001

Date: 2020-11-13 13:16:23
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Function structure for `Get_StatisticIndicator`
-- ----------------------------
DROP FUNCTION IF EXISTS `Get_StatisticIndicator`;
DELIMITER ;;
CREATE DEFINER=`root`@`%` FUNCTION `Get_StatisticIndicator`(`LocalityKey` varchar(255),`OrganizationKey` varchar(255),`TimeKey` int,`TimeType` varchar(255)) RETURNS varchar(255) CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	#Routine body goes here...
  DECLARE total DOUBLE;
	DECLARE Giatri DOUBLE;

set `total` = (select Count(ivd.IndicatorValueId)
from department d 
join indicatorvaluedepartment ivd on d.DepartmentId = ivd.DepartmentId
WHERE d.Emails = `OrganizationKey`);
set `Giatri` = (select Count(DISTINCT(i.InCatalogValueCode))
from department d 
join indicatorvaluedepartment ivd on d.DepartmentId = ivd.DepartmentId
join dim_incatalogvalue i on i.InCatalogValueId = ivd.IndicatorValueId
join fact_eform_model fact on i.InCatalogValueCode = fact.IndicatorKey
WHERE
fact.TimeKey = `TimeKey` and
fact.TimeType = `TimeType` 
and d.Emails = `OrganizationKey`); 

RETURN CONCAT(`Giatri`, ' / ',`total`);

END
;;
DELIMITER ;
