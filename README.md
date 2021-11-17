# ZPersianDate
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
Install-Package ZPersianDateTime -Version 0.0.3
```
or 

Go to [Nuget Page](https://www.nuget.org/packages/ZPersianDateTime/).


# Example Usage 


## Assign System.Date to PersianDate and in reverse
  ~~~
  PersianDate persianDate = new DateTime(2019, 01, 13);
  PersianDate persianDate = DateTime.Today;


  DateTime date = new PersianDate(1397,10,23);
  DateTime date = PersianDate.Today;
  
~~~

## Defined most usage method that persian developer need it
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
