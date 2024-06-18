# Forum Question and Answers API

## Project Overview

This project was created to create forums to discusse about anything. You can create post, create answer, create forum, update and delete these items. 
For this project, separete the solution in 5 projects. Each project has your responsability.


## Technologies Used

- **ASP.NET Core Web Api**: For receive request and pass the request for where it can call .
- **Dapper**: For creating queries.
- **C#**: As the programming language.
- **SQL Server**: As the database.

This project does have focus on back end, the front end is in other repository.

## Project Structure

The solution of this back end project is organized :

- **ForumQA.API**: Handle incoming HTTP requests and return responses.
- **ForumQA.Service**: Contain the business logic of the application. Receive the request from ForumQA.API, make the business 
      logic and if is necessary, call the ForumQA.Infrastructure.
- **ForumQA.Infrastructure**: Handle data access and database operations. Create repository for core entities and have one class 
      responsible to execute queries sql.
- **ForumQA.Domain**: Represent the data structures of the business.
- **ForumQA.ServiceTest**: Project contains unit test.

## Dependency Injection

The project uses dependency injection to manage dependencies and expose only interfaces for accessing necessary methods. 
This keeps the project decoupled and easier to maintain and extend in the future. For the keep project decopling, the interfaces of repositories and services classe's , put in project ForumQA.Domain, folder Abstration, for keep main project that use reference is Domain, where have domain classe's.

## Configuration

Details about credentials and other settings are stored in ForumQA.API, file `appsettings.json`. 
As this is a simple project, secure methods for storing sensitive information, such as database credentials, have not been implemented. For a production environment, it is recommended to use secure storage methods.

## How to Run the Project

1. **Clone the repository**:
    ```sh
    git clone git@github.com:dhiegotamanini/ForumQA_API.git
    ```
2. **Navigate to the project directory**:
    ```sh
    cd your repository
    ```
3. **Restore the dependencies**:
    ```sh
    dotnet restore
    ```
4. **Update the database**:
    Have scripts of queries used for it, in the Infrastructure project
5. **Run the project**:
    ```sh
    dotnet run
    ```

## Future Improvements

- **Security**: Began implement secutiry using Jwt in Asp.Net Core in the back end part. Need finish and improve this part. 
- **User Authentication**: Need finish and improve this part.


## 🚀 Tecnologies
<div>
  <img src="https://img.shields.io/badge/-ASP.NET%20Core-fff?style=flat&logo=.net&logoColor=blue">
  <img src="https://img.shields.io/badge/-SQL-fff?style=flat&logo=Microsoft-SQL-Server&logoColor=blue">
  <img src="https://img.shields.io/badge/-Git-fff?style=flat&logo=git">  
</div>
<div>
    <img src="https://img.shields.io/badge/-Git-fff?style=flat&logo=git">  
  <img src="https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=csharp&logoColor=white">  
</div>

