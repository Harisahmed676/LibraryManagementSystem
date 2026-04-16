Library Management System

A project built using .NET 8 and SQL Server.

Tech Stack: .NET 8 Web API, Entity Framework Core, SQL Server, AutoMapper, Swagger

The solution has three projects - API handles controllers and services, Core has the entities and interfaces, and Infrastructure deals with the database stuff (DbContext, repositories, migrations).

Database has four tables: Libraries, Books, Members, and BorrowRecords. A library has many books, a member can borrow multiple books, and borrow records track who took what and when.

Few business rules worth mentioning: a book can only be borrowed if copies are available, member has to be active with a valid membership, and returning a book brings the available count back up. Deletes are soft so nothing gets permanently removed.

To run locally, update the connection string in appsettings.json and run:

dotnet ef database update --project LibraryManagement.Infrastructure --startup-project LibraryManagement.API

-Project Structure

Split into three projects to keep things clean:

- **LibraryManagement.API** - Controllers, Services, AutoMapper profiles
- **LibraryManagement.Core** - Entities, DTOs, Interfaces
- **LibraryManagement.Infrastructure** - DbContext, Configurations, Repositories
