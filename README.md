# CountriesInfo
Console application created with .NET Core 3.1 for obtaining information about countries through a third-party [API](https://restcountries.eu/rest/v2) and for saving this information to database (MSSQL Server).   
Country information includes following attributes:  
- Name  
- Code  
- Capital  
- Area  
- Population  
- Region  
## Database  
There you can find database backup file - Countries.bak.  
Database consists of 3 tables: Regions, Cities, Countries.
## Use
App will suggest you to search info about country which name you will enter or to display all countries from database.  
If you chose the first, app will output requested information and suggest you to save it to database.
