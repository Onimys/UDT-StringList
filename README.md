# MS SQL StringList
User-Defined Data Type List<string> for MS SQL.

### Installation
Set DB
```sh
USE [dbName];
```
Enabling CLR Integration
```sh
sp_configure 'clr enabled', 1
GO
RECONFIGURE
```
Deploy
```sh
if EXISTS (select * from sys.types where name='StringList') DROP TYPE dbo.StringList; 
if EXISTS(select * from sys.assemblies where name='StringList') DROP ASSEMBLY StringList;

CREATE ASSEMBLY StringList
FROM '[path to StringList.dll]' 
WITH PERMISSION_SET = SAFE;
```
Create Type
```sh
CREATE TYPE dbo.StringList 
EXTERNAL NAME StringList.[StringList];
```
