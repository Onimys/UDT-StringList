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
if EXISTS (select * from sys.assemblies where name='StringList') DROP ASSEMBLY StringList;

CREATE ASSEMBLY StringList
FROM '[path to StringList.dll]' 
WITH PERMISSION_SET = SAFE;
```
Create Type
```sh
CREATE TYPE dbo.StringList 
EXTERNAL NAME StringList.[StringList];
```

### Use It
As a separator used symbol **«|»**
- Declare Variables
```sh
Declare @List StringList
Declare @List StringList = 'Hello, world | !?'
```
- Convert to string
```sh
select @List.ToString() //return: 'NULL'
select @List.ToString() //return: 'Hello, world | !?'
```
- Convert to string with another separator
```sh
select @List.Concat('-') //return: 'NULL'
select @List.Concat('-') //return: 'Hello, world - !?'
```
- Items count
```sh
select @List.Length() //return: 0
select @List.Length() //return: 2
```
- Check empty
```sh
select @List.isEmpty() //return: 1 - true
select @List.isEmpty() //return: 0 - false
```


