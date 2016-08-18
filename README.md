##Setup Entity Framework Project
* https://docs.efproject.net/en/latest/platforms/aspnetcore/new-db.html
* https://damienbod.com/2016/01/11/asp-net-5-with-postgresql-and-entity-framework-7/
- Migrations
    * dotnet ef --startup-project ../MyConsoleApplication/ migrations list
    * http://benjii.me/2016/05/dotnet-ef-migrations-for-asp-net-core/
    * dotnet ef --project ../TextAnalysis.Web.Domain/ --startup-project . migrations add InitialCreate
    * https://github.com/aspnet/EntityFramework/issues/5320#issuecomment-236395136