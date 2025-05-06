This projects is based on Employee and thier Salary Management system 
Here based on thier roles I have implemented the authorizations 


-------------------------------------------------------------------
Adding Connection String Based on the Environment 
-------------------------------------------------------------------

For this Initially on Appsettings itself Declare the String with our Environment as 

"ConnectionStrings": {
  "Development": "Data Source=trucslenovo;Initial Catalog=ConcertDiary;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;",
  "UAT": "Data Source=trucslenovo;Initial Catalog=EmployeeDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True;"
}

Config this with program.cs as

var environment = builder.Environment;
string envName = environment.EnvironmentName;
string connection = builder.Configuration.GetSection("ConnectionStrings")[envName];

Based on the Environment it will retrive the ConnectionString From the AppSettings.json 

Change the environmental Value as per the environment such as UAT , Production etc on the launchSettings.json


"environmentVariables": {
  "ASPNETCORE_ENVIRONMENT": "Development"
}

It Can be also Modified using Command 
set ASPNETCORE_ENVIRONMENT=Development



 
