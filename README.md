# ZPersianDate

## Nuget Packages


| Package Name                                                                                  | Target Framework          | Version                                                       | Downloads                                                             |
| --------------------------------------------------------------------------------------------- | ------------------------- | ------------------------------------------------------------- | --------------------------------------------------------------------- |
| [ZPersianDateTime]((https://www.nuget.org/packages/ZPersianDateTime/ "https://www.nuget.org") | .NET 5.0;.NET .0;.NET 8.0 | ![NuGet](https://img.shields.io/nuget/v/ZPersianDateTime.svg) | ![NuGet](https://img.shields.io/nuget/dt/ZpersianDateTime?style=flat) |

## Persian Date Time For .Net

###### This is library for working with Date and it's instead of System.DateTime when need Jalali date in .net applications.

# Implemented with

```
  .net 5 and later
```

# Prerequisites

```
  Nothing
```

# Install

```
Install-Package ZPersianDateTime -Version 0.0.4
```

or

Go to [Nuget Page](https://www.nuget.org/packages/ZPersianDateTime/).

# Example Usage

## Cast System.Date to PersianDate and vice versa

~~~
PersianDate persianDate = new DateTime(2019, 01, 13);
PersianDate persianDate = DateTime.Today;


DateTime date = new PersianDate(1397,10,23);
DateTime date = PersianDate.Today;

~~~

## Built-in most usage function

~~~
 var date = new PersianDate(1397, 10, 23);
 
 var bom = date.BeginOfMonth();
 var bow = date.BeginOfWeek();
 var boy = date.BeginOfYear();

 var eom = date.EndOfMonth();
 var eow = date.EndOfWeek();
 var eoy = date.EndOfYear();

~~~

## Easy change or navigation

~~~
  var newDay = new PersianDate(1397, 1, 1).GotoDays(10);
  var newMonth  = new PersianDate(1397, 1, 1).GotoMonths(1);
  var newYear = new PersianDate(1397, 1, 1).GotoYears(1);
  var newWeek = new PersianDate(1397, 1, 1).GotoWeeks(2);


~~~
