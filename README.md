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
- Get item by index
```sh
select @List1.GetItem(0) or select @List2.GetItem(3) //return: 'NULL'
select @List2.GetItem(1) //return: ' !?'
```
- Check empty
```sh
select @List1.isEmpty() //return: 1 - true
select @List2.isEmpty() //return: 0 - false
```
- Format message. 
Pattern **{\d}** replace
```sh
select @List2.Formated('{0} - this {1} {2}') //return: 'Hello, world  - this  !? '
```
or use static method
```sh
select StringList::FormatMessage('{0} big {1} - this big {1}!', 'Hello|world') //return: 'Hello big world - this big world!'
```

### Manipulation
- Add item
```sh
SET @List1=@List1.AddItem('cat') //return: 'cat'
SET @List2=@List2.AddItem('cat') //return: 'Hello, world | !?|cat'
```
- Add some separator items
```sh
SET @List1=@List1.AddRange('cat|dog') //return: 'cat|dog'
SET @List2=@List2.AddRange('cat|dog') //return: 'Hello, world | !?|cat|dog'
```
- Remove item by index
```sh
SET @List2=@List2.RemoveItem(1) //return: 'Hello, world '
```
- Remove all items
```sh
SET @List2=@List2.RemoveAll()
```
