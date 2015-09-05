/*drop table (select table_name from INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE 'Profesor_%');*/
DECLARE @id varchar(255) -- used to store the table name to drop
DECLARE @dropCommand varchar(255) -- used to store the t-sql command to drop the table
DECLARE @namingPattern varchar(255) -- user to defie the naming pattern of the tables to drop
DECLARE tableCursor CURSOR FOR 
    SELECT table_name FROM INFORMATION_SCHEMA.TABLES WHERE table_name like 'Profesor_%' 

OPEN tableCursor 
FETCH next FROM tableCursor INTO @id 

WHILE @@fetch_status=0 
BEGIN 
	-- Prepare the sql statement
    SET @dropcommand = N'drop table ' + @id 
    -- print @dropCommand -- just a debug check
    -- Execute the drop
    EXECUTE(@dropcommand) 
    
    -- move to next record
    FETCH next FROM tableCursor INTO @id 
END 