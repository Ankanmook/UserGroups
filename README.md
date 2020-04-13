# UserGroups

NLog
Entity Framework Core 
Dapper
Automapper

Razor Pages => functionality of Asp.net MVC Core

Run following command on UserGroup.Web
npm install 


Database
1.The database was built using SQL script and then EF scaffolding was done to generate the model which resides in UserGroup.DataModel along with Context
2.Database script in UserGroup.Sql => Creat.sql. It is idempotent (will delete and recreate DB (provided you don't have active connection to db)
3.Database backup is in the same folder
4.Database also contains search stored procedure and seeding of 2 groups and 5 person

Solution Architecture
I decided to have 3 layer approach
DAL Layer which has entity framework and dapper repositories
Service Layer which has business logic
UI layer which has Razor pages and Api controllers
Reason for that was separation of concenrs and testability.
Each individual projects can be tested separately and if removed then it should not affect the correctned of the system Liskov substitution principle

Api for Person and Group
I decided to build the api first for this page as I like Api driven approach (first build and test api then views in this case)
Advantage of building api is that you can use that to build UI on single page application using something like React/Angular/Vue

Searching option has Entity Framework and Dapper
Reason to include dapper is mentioned in SearchService.cs. I like Dapper

Once I build and tested them then I built the UI using Razor pages which are the new features introduced in asp.net core
Finally I will focus on building the view compnent for search and pagination

Front End Npm install
For front end I have included package.json which builds and dumps the front end files to nodeModules directory
Reason for doing that is I like using node.js to keep control of the files I am adding to my project
Consequentially you can delete the files in wwwroot and it should still work as links to layout are directed from nodeModules
If you publish this then it would build and put all the node files to nodeModules directory, provided you have npm installed

Person List View
There are 3 views to list persons Page page
Reason => it follows an incremental approach
Default View (No pagination)
Jquery Datatable View which gets all the data from back end and then does sorting and filtering on front end (inefficient)
Index View which has pagination and search option which only gets required filtered/paginated data from back end

Next
Build a View component for reusing the search option
Use vuejs to build a front end which would also include building a search component which can be reused across
Why choose vue cause it is lightweight