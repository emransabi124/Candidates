# Candidates 

## Overview

This project is built using ASP.NET Core 6 API, providing a robust and scalable framework for building web applications and APIs. The application is designed to manage candidates efficiently with features such as adding, updating candidate information. It also integrates with Entity Framework Core for data management, and supports Swagger for API documentation and testing.

## Features

- **ASP.NET Core 6 API**: Utilizes the latest ASP.NET Core framework for building RESTful APIs.
- **Entity Framework Core**: Integrated with EF Core for database interactions and management.
- **Swagger**: Provides interactive API documentation and testing interface.
- **Dependency Injection**: Follows best practices with Dependency Injection for managing services and repository patterns.
- **Serilog Logging**: Configured Serilog for structured and easy-to-read logging.
- **Automapper**: Uses Automapper for object-to-object mapping, simplifying data transfer between layers.
- **Custom Middleware**: Includes custom middleware for handling exceptions and global error handling.

## Setup

To get started with this project, follow these steps:
1. **Clone the repository**:

   You can clone the repository using either HTTPS or SSH:

   - **HTTPS**:
     ```bash
       git clone https://github.com/emransabi124/Candidates.git.
     ```

   - **Run Project**:
     ```bash
     dotnet run --project ./Candidates.csproj
     ```

   - **Host**:
     ```bash
      https://localhost:7174/swagger/index.html
     ```
     ![image](https://github.com/user-attachments/assets/c3d17788-c8b6-48b6-a46d-2ea72ce3fa08)

 
 
2. **Technologies Used**:
   - **ASP.NET Core 6**
   - **Entity Framework Core**
    - **AutoMapper**
    - **Serilog**
    - **Swagger**


3. **List of improvements**:
- **We can add unit action to prevent concurrency or multiple transactions**
- **Add all CRUD operations**
- **Add login and authentication JWT for more security**
