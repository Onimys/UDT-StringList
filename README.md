# MS SQL StringList
User-Defined Data Type List<string> for MS SQL.

### Installation
Set DB
```sql
USE [dbName];
```
Enabling CLR Integration
```sql
sp_configure 'clr enabled', 1
GO
RECONFIGURE
```
Deploy
```sql
if EXISTS (select * from sys.types where name='StringList') DROP TYPE dbo.StringList; 
if EXISTS (select * from sys.assemblies where name='StringList') DROP ASSEMBLY StringList;

CREATE ASSEMBLY StringList
FROM '[path to StringList.dll]' 
WITH PERMISSION_SET = SAFE;
```
Create Type
```sql
CREATE TYPE dbo.StringList 
EXTERNAL NAME StringList.[StringList];
```

### Use It
As a separator used symbol **«|»**
- Declare Variables
```sql
Declare @List1 StringList
Declare @List2 StringList = 'Dog,Cat, People'
```
- Convert to string
```sql
select @List1.ToString() -- return: 'NULL' 
select @List2.ToString() -- return: 'Dog,Cat,People'
```
- Convert to string with another separator
```sql
select @List1.Concat('-') -- return: 'NULL' 
select @List2.Concat('-') -- return: 'Dog-Cat-People' 
```
- Items count
```sql
select @List1.Length() -- return: 0 
select @List2.Length() -- return: 3 
```
- Get item by index
```sql
select @List1.get_Item(0) -- return: 'NULL' 
select @List2.get_Item(3) -- return: 'NULL' 
select @List2.get_Item(1) -- return: 'Cat' '
```
- Get item index
```sql
select @List2.get_Index('People') -- return: 2 
select @List2.get_Index('what') -- return: 'NULL'
```
- Set item
```sql
SET @List2.set_Item(1, 'Snake') -- return: 'Dog,Snake,People'
SET @List2.set_Item(5, 'Snake') -- return: 'Dog,Cat,People'
```
- Check empty
```sql
select @List1.isEmpty() -- return: 1 - true 
select @List2.isEmpty() -- return: 0 - false
```
- Сontains
```sql
select @List2.[Contains]('Dog') -- return: 1 - true 
select @List2.[Contains]('Dog-iii') -- return: 0 - false
```
- Exists
```sql
select @List2.[Exists]('Dog') -- return: 1 - true
select @List2.[Exists]('Peop') -- return: 1 - true 
```
- Format message. 
Pattern **{\d}** replace
```sql
select @List2.Formated('{0} - this {1} {2}') -- return: 'Dog - this Cat People'
```
or use static method
```sql
select StringList::FormatMessage('{0} - this {1} {2}', 'Dog,Cat, People') -- return: 'Dog - this Cat People'
```

### Manipulation
- Add item
```sql
SET @List1=@List1.[Add]('Mouse') -- return: 'Mouse' 
SET @List2=@List2.[Add]('Mouse') -- return: 'Dog,Cat,People,Mouse'
```
- Add some separator items
```sql
SET @List1=@List1.AddRange('Mouse,Rabbit') -- return: 'Mouse,Rabbit'
SET @List2=@List2.AddRange('Mouse,Rabbit') -- return: 'Dog,Cat,People,Mouse,Rabbit'
```
- Remove item
```sql
SET @List2=@List2.RemoveByName('Cat') -- return: 'Dog,People'
```
- Remove item by index
```sql
SET @List2=@List2.RemoveByIndex(0) -- return: 'Cat,People'
```
- Remove all items
```sql
SET @List2=@List2.RemoveAll() -- return: 'NULL'
```
- Reverse
```sql
SET @List2.Reverse() -- return: 'People,Cat,Dog'
```
- Sort
```sql
SET @List2.Sort() -- return: 'Cat,Dog,People'
```
