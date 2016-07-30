# AzureStorageFilterBuilder
The library for creation filters for Azure Storage Tables requests. Contains standard opportunities (And\Or\Not, Equal\LessThan\etc) and some other features (In, Between, StartsWith). If you are not fan of code like
```cs
string date1 = TableQuery.GenerateFilterConditionForDate(
                   "Date", QueryComparisons.GreaterThanOrEqual,
                   DateTimeOffsetVal);
string date2 = TableQuery.GenerateFilterConditionForDate(
                   "Date", QueryComparisons.LessThanOrEqual,
                   DateTimeOffsetVal);
string finalFilter = TableQuery.CombineFilters(
                        TableQuery.CombineFilters(
                            partitionFilter,
                            TableOperators.And,
                            date1),
                        TableOperators.And, date2);
```
this lib is created for you.

## Content

* [Getting started](#getting-started)
* [Standard operations](#standard-operations)
* [Additional operations](#additional-operations)
* [Unusual cases](#unusual-cases)
* [Team](#team)

## Getting started
**Step 1.**
Install AzureStorageFilterBuilder via the NuGet package: [AzureStorageFilterBuilder](https://www.nuget.org/packages/AzureStorageFilterBuilder)

```
PM> Install-Package AzureStorageFilterBuilder
```

**Step 2.**
Add using statement
```cs
using AzureStorageFilterBuilder;
```
and write a code like

```cs
string filter = AzureStrorageFilter.Empty
                  .PartitionKey().Equal("AnyPartitionKey")
                  .And()
                  .RowKey().Equal("AnyRowKey");
```
for the "PartitionKey eq 'AnyPartitionKey' and RowKey eq 'AnyRowKey'" filter.

## Basic methods
Empty, PartitionKey(), RowKey(), Column(columnName), Const(@const) and Bracket methods. Empty return new filter converted to empty string. PartitionKey and RowKey add "PartitionKey" and "RowKey" statements to filter. Column("Age") add "Age" statement to the filter, and Const("StringValue") add formatted value to the filter. 

As the example:
```cs
string filter = AzureStrorageFilter.Empty
                  .Column("Name").Equal().Const("Adam"); // Name eq 'Adam'
                  
filter = AzureStrorageFilter.Empty
                  .Column("CustomerId").Equal().Const(Guid.Parse("a455c695-df98-5678-aaaa-81d3367e5a34")); 
                  // CustomerId eq guid'a455c695-df98-5678-aaaa-81d3367e5a34'
```

OpenBracket() and CloseBracket() methods add "(" and ")" to filters.

## Standard operations
It's standard possibilities of Azure SDK: logical operators (And, Or, Not), comparison operators (Equal, NotEqual, GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual, NotEqual). 

**Logical operator examples.**

```cs
string filter = AzureStrorageFilter.Empty
                .Column("Name").Equal().Const("Andrew")
                .And()
                .Column("Age").Equal().Const(27); // Name eq 'Andrew' and Age eq 27
                
filter = AzureStrorageFilter.Empty
                .Column("Age").Equal().Const(17)
                .Or()
                .Column("Age").Equal().Const(18); // Age eq 17 or Age eq 18
                
filter = AzureStrorageFilter.Empty
                .Not().OpenBracket()
                  .Column("flatNumber").Equal().Const(666)
                .CloseBracket(); // not (flatNumber eq 666)
```

Comparison methods allows work in two styles.
First is Compare().Const() above.
The second: Compare(value). For example:
```cs
string filter = AzureStrorageFilter.Empty
            .Column("Weight").GreaterThan(5); // Weight gt 5
            
filter = AzureStrorageFilter.Empty
            .PartitionKey().Equal("user@mail.com"); // PartitionKey eq 'user@mail.com'
```
Be careful: Equals method does not check what column it filters. Do not use Equal(int value) overload for the PartitionKey or RowKey columns, they are strings.

## Additional operations
This is operations absent in standard SDK. Right now they are In, Between, StartsWith.

**In** method checks that column contains one value from the specified collection.
```cs
string filter = AzureStrorageFilter.Empty
                .In("Age", 20, 27); // (Age eq 20 or Age eq 27)
                
int[] warYears = new int[] { 1917, 1941, 1978 };
filter = AzureStrorageFilter.Empty
                .In("Year", warYears); // (Year eq 1917 or Year eq 1941 or Year eq 1978)
```
Does not support bool collections. In() method works with a single value array, but throws an exception on empty or null collection.

**Between method** checks that column value belongs to the specified interval.
```cs
DateTime min = new DateTime(2015, 7, 16, 0, 0, 0, DateTimeKind.Utc);
DateTime max = new DateTime(2016, 7, 16, 0, 0, 0, DateTimeKind.Utc); 
            
string filter = AzureStrorageFilter.Empty
            .Between("Time", min, max); 
          // (Time ge datetime'2015-07-16T00:00:00Z' and Time le datetime'2016-07-16T00:00:00Z')
```
Both min and max argument are inclusive, Guid and bool type are not supported. Between method does not check that min is less than max.

**StartsWith** method is applied for strings only and checks that the column value starts from the specified substring.
```cs
string filter = AzureStrorageFilter.Empty
            .StartsWith("Name", "Alex");
            // (Name ge 'Alex' and Name lt 'Aley')
```

## Unusual cases
There are some details you should remember:
* Double columns can not be filtered by Double.NaN, Double.PositiveInfinity, Double.NegativeInfinity
* All methods will throw a FilterBuilderException on null string argument (or string[] with null string)
* The library does not check logical operators. If you try create request "IsValid eq true and IsValid eq false": it will be created. 

## Team
Authors: [Alexander Laptev](https://github.com/Oxoron) (maintainer), [Andrew Isayev](https://github.com/t89611100101)

2016
