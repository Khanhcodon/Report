/*
Navicat MySQL Data Transfer

Source Server         : Localhost
Source Server Version : 50715
Source Host           : localhost:3306
Source Database       : egovbrvt_vpub

Target Server Type    : MYSQL
Target Server Version : 50715
File Encoding         : 65001

Date: 2018-06-04 10:51:14
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Function structure for `ParseSearchTerm`
-- ----------------------------
DROP FUNCTION IF EXISTS `ParseSearchTerm`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `ParseSearchTerm`(`SearchTerm` varchar(1000)) RETURNS varchar(1000) CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	#Routine body goes here...
	
	DECLARE `result` varchar(1111);	
	DECLARE `searchWord` VARCHAR(100);
	DECLARE `tempParse` varchar(1111);	
	DECLARE spaceIdx int;
	DECLARE hasColon bit;

	IF(`SearchTerm` is null or `SearchTerm` = '') THEN
		RETURN '';
	end IF;

	SET `SearchTerm` = REPLACE(`SearchTerm`, '-', ' ');
	SET `SearchTerm` = REPLACE(`SearchTerm`, '/', ' ');	
	SET `SearchTerm` = REPLACE(`SearchTerm`, ',', '');	
	SET `SearchTerm` = stripVietnameseChars(TRIM(`SearchTerm`));

	SET spaceIdx = 1;
	SET result = '';
	set hasColon = 0;
		
	REPEAT
		SET tempParse = SUBSTRING_INDEX(`SearchTerm`,' ', spaceIdx);

		SET searchWord = SUBSTRING_INDEX(tempParse, ' ', -1);
		SET spaceIdx = spaceIdx + 1;

		IF(searchWord != '')
		THEN
					
				if hasColon = 1 THEN
					SET `result` = CONCAT(`result`, ' ', searchWord);
				else
					SET `result` = CONCAT(`result`, ' +', searchWord);
				end if;

				if INSTR(searchWord, '"') = 1 THEN
					SET hasColon = 1;
				END if;

				if INSTR(searchWord, '"') = CHAR_LENGTH(searchWord) THEN
					SET hasColon = 0;
				END if;
		END IF;

	UNTIL tempParse = `SearchTerm`
	END REPEAT;

	RETURN TRIM(result);
END
;;
DELIMITER ;

-- ----------------------------
-- Function structure for `stripVietnameseChars`
-- ----------------------------
DROP FUNCTION IF EXISTS `stripVietnameseChars`;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `stripVietnameseChars`(`@inputVar` varchar(1000)) RETURNS varchar(1000) CHARSET utf8 COLLATE utf8_unicode_ci
BEGIN
	#Routine body goes here... 
    SET @SIGN_CHARS = ' ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' COLLATE utf8_unicode_ci;
    SET @UNSIGN_CHARS = ' aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD' COLLATE utf8_unicode_ci;
		SET @result = "";
    SET @COUNTER = 1 ;

		SET @charSearch = '';
    WHILE @COUNTER <= CHAR_LENGTH(`@inputVar`) DO -- Duyệt tất cả các ký tự của chuỗi đầu vào
        SET @COUNTER1 = 1;
				SET @charSearch = SUBSTR(`@inputVar`,@COUNTER ,1);
				SET @charReplace= @charSearch;
myloop: WHILE @COUNTER1 <= CHAR_LENGTH(@SIGN_CHARS) + 1 DO -- duyệt tất cả các ký tự của sign-char
            IF SUBSTR(@SIGN_CHARS, @COUNTER1, 1)= @charSearch THEN 
							SET @charReplace = SUBSTR(@UNSIGN_CHARS, @COUNTER1, 1);                
							LEAVE myloop;
						END IF;

						SET @COUNTER1 = @COUNTER1 +1;						
        END WHILE;
				SET @result = CONCAT(@result, @charReplace);
        SET @COUNTER = @COUNTER +1;
    END WHILE;

    RETURN @result;
END
;;
DELIMITER ;
