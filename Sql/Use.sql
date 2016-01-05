/***************************
* Use it
****************************/
-- Declare Variables
Declare @List1 StringList
Declare @List2 StringList = 'Hello, world | !?'
-- Convert to string
select @List1.ToString() 
select @List2.ToString() 
-- Convert to string with another separator
select @List1.Concat('-') 
select @List2.Concat('-')
-- Items count
select @List1.Length()
select @List2.Length()
-- Get item by index
select @List1.GetItem(0) 
select @List2.GetItem(3)
select @List2.GetItem(1)
-- Get index item
select @List2.GetIndex(' !?')
select @List2.GetIndex('1 !?')
-- Check empty
select @List1.isEmpty() 
select @List2.isEmpty() 
-- Ñontains
select @List2.[Contains]('Hello')
select @List2.[Contains]('Hello, world ')
-- Exists
select @List2.[Exists]('Hello')
select @List2.[Exists]('Hello, world ')
-- Format message. Pattern {\d} replace
select @List2.Formated('{0} - this {1} {2}') 
select StringList::FormatMessage('{0} big {1} - this big {1}!', 'Hello|world')


/***************************
* Manipulation
****************************/
-- Add item
SET @List1=@List1.[Add]('cat') -- return: 'cat|dog' 
SET @List2=@List2.[Add]('cat') -- return: 'Hello, world | !?|cat|dog'
-- Add some separator items
SET @List1=@List1.AddRange('cat|dog') -- return: 'cat|dog'
SET @List2=@List2.AddRange('cat|dog') -- return: 'Hello, world | !?|cat|dog'
-- Remove item
SET @List2=@List2.RemoveByName(' !?') -- return: 'Hello, world '
-- Remove item by index
SET @List2=@List2.RemoveByIndex(1) -- return: 'Hello, world '
-- Remove all items
SET @List2=@List2.RemoveAll() -- return: 'NULL'
-- Reverse
SET @List2=@List2.AddRange('cat|dog') -- return: 'Hello, world | !?|cat|dog'
SET @List2.Reverse() -- return: ' !?|Hello, world '
-- Sort
SET @List2.Sort() -- return: ' !?|Hello, world '
