CREATE FUNCTION [dbo].[fnDateFormat] 
(
	-- Add the parameters for the function here
	@Datetime DATETIME,
	@FormatMask NVARCHAR(100)
)
RETURNS NVARCHAR(100)
AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result NVARCHAR(100)

	-- Add the T-SQL statements to compute the return value here
	DECLARE @StringDate NVARCHAR(100)

	SET @StringDate = @FormatMask
    
	IF (CHARINDEX ('YYYY',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'YYYY',
                        DATENAME(YY, @Datetime))

	IF (CHARINDEX ('yy',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'yy',
                        RIGHT(DATENAME(YY, @Datetime),2))

	IF (CHARINDEX ('MMMM',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'MMMM',
                        DATENAME(MM, @Datetime))

	IF (CHARINDEX ('MMM',@StringDate)>0)
	SET @StringDate = REPLACE(@StringDate, 'MMM',
                        LEFT(UPPER(DATENAME(MM, @Datetime)),3))

	IF (CHARINDEX ('MM',@StringDate COLLATE Latin1_General_CS_AS) > 0)
	SET @StringDate = REPLACE(@StringDate COLLATE Latin1_General_CS_AS, 'MM',
                RIGHT('0'+CONVERT(VARCHAR,DATEPART(MM, @Datetime)),2))

	IF (CHARINDEX ('M',@StringDate COLLATE Latin1_General_CS_AS) > 0)
	SET @StringDate = REPLACE(@StringDate COLLATE Latin1_General_CS_AS, 'M',
                        CONVERT(VARCHAR,DATEPART(MM, @Datetime)))

    IF (CHARINDEX ('DDDD',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'DDDD',
                                    DATENAME(DW, @Datetime))   
	IF (CHARINDEX ('DDD',@StringDate) > 0)
	SET @StringDate = LEFT(REPLACE(@StringDate, 'DDD',
                                    DATENAME(DW, @Datetime)),3)
	IF (CHARINDEX ('dd',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'dd',
                        RIGHT('0'+DATENAME(DD, @Datetime),2))

	IF (CHARINDEX ('d',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'd',
                        RIGHT(DATENAME(DD, @Datetime),2))
                                     
	IF (CHARINDEX ('ww',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'ww',
                                    DATENAME(WW, @Datetime))   
                                     
	IF (CHARINDEX ('HH',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'HH',
                                    DATENAME(HH, @Datetime))  
	--IF @StringDate > 12 SET @StringDate = @StringDate - 12

	IF (CHARINDEX ('mm',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'mm',
                        RIGHT('0'+DATENAME(MI, @Datetime),2)) 

	IF (CHARINDEX ('ss',@StringDate) > 0)
	SET @StringDate = REPLACE(@StringDate, 'ss',
                        RIGHT('0'+DATENAME(SS, @Datetime),2))

	SELECT @Result = @StringDate

	-- Return the result of the function
    RETURN @Result
END