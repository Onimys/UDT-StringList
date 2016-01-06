-- Set Database
USE [dbName];
GO
/***************************
* Use it
****************************/
-- Declare Variables
Declare @List1 StringList
Declare @List2 StringList = 'Dog,Cat, People'
-- Convert to string
select @List1.ToString() -- return: 'NULL' 
select @List2.ToString() -- return: 'Dog,Cat,People'
-- Convert to string with another separator
select @List1.Concat('-') -- return: 'NULL' 
select @List2.Concat('-') -- return: 'Dog-Cat-People' 
-- Items count
select @List1.Length() -- return: 0 
select @List2.Length() -- return: 3 
-- Get item by index
select @List1.GetItem(0) -- return: 'NULL' 
select @List2.GetItem(3) -- return: 'NULL' 
select @List2.GetItem(1) -- return: 'Cat' 
-- Get index item
select @List2.GetIndex('People') -- return: 2 
select @List2.GetIndex('what') -- return: 'NULL'
-- Check empty
select @List1.isEmpty() -- return: 1 - true 
select @List2.isEmpty() -- return: 0 - false
-- Ñontains
select @List2.[Contains]('Dog') -- return: 1 - true 
select @List2.[Contains]('Dog-iii') -- return: 0 - false
-- Exists
select @List2.[Exists]('Dog') -- return: 1 - true
select @List2.[Exists]('Peop') -- return: 1 - true 
-- Format message. Pattern {\d} replace
select @List2.Formated('{0} - this {1} {2}') -- return: 'Dog - this Cat People'
select StringList::FormatMessage('{0} - this {1} {2}', 'Dog,Cat, People') -- return: 'Dog - this Cat People'


/***************************
* Manipulation
****************************/
-- Add item
SET @List1=@List1.[Add]('Mouse') -- return: 'Mouse' 
SET @List2=@List2.[Add]('Mouse') -- return: 'Dog,Cat,People,Mouse'
-- Add some separator items
SET @List1=@List1.AddRange('Mouse,Rabbit') -- return: 'Mouse,Rabbit'
SET @List2=@List2.AddRange('Mouse,Rabbit') -- return: 'Dog,Cat,People,Mouse,Rabbit'
-- Remove item
SET @List2=@List2.RemoveByName('Cat') -- return: 'Dog,People'
-- Remove item by index
SET @List2=@List2.RemoveByIndex(0) -- return: 'Cat,People'
-- Reverse
SET @List2.Reverse() -- return: 'People,Cat,Dog'
-- Sort
SET @List2.Sort() -- return: 'Cat,Dog,People'
-- Remove all items
SET @List2=@List2.RemoveAll() -- return: 'NULL'
GO