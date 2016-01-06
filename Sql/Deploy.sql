-- Set Database
USE [dbName];
GO
-- Enabling CLR Integration
sp_configure 'clr enabled', 1
GO
RECONFIGURE
GO
-- Deploy UDTs
if EXISTS (select * from sys.types where name='StringList') DROP TYPE dbo.StringList; 
if EXISTS(select * from sys.assemblies where name='StringList') DROP ASSEMBLY StringList;

CREATE ASSEMBLY StringList
FROM '[path to StringList.dll]' 
WITH PERMISSION_SET = SAFE;
GO
CREATE TYPE dbo.StringList 
EXTERNAL NAME StringList.[StringList];