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
Declare @List1 StringList
Declare @List2 StringList = 'Hello, world | !?'
```
- Convert to string
```sh
select @List1.ToString() //return: 'NULL'
select @List2.ToString() //return: 'Hello, world | !?'
```
- Convert to string with another separator
```sh
select @List1.Concat('-') //return: 'NULL'
select @List2.Concat('-') //return: 'Hello, world - !?'
```
- Items count
```sh
select @List1.Length() //return: 0
select @List2.Length() //return: 2
```
- Check empty
```sh
select @List1.isEmpty() //return: 1 - true
select @List2.isEmpty() //return: 0 - false
```
- Format message
Pattern **{\d}** replace
```sh
select @List.Formated('{0} - this {1} {2}') //return: 'Hello, world  - this  !? '
```
or use static method
```sh
select StringList::FormatMessage('{0} big {1} - this big {1}!', 'Hello|world') //return: 'Hello big world - this big world!'
```



