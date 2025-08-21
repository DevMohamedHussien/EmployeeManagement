Overview

MyAssessment is an ASP.NET Core MVC project for managing Departments and Employees.
It supports CRUD operations, role-based login, and has a modern UI with AJAX validation and popups.

Features

Login with ASP.NET Core Identity
Manage departments (create, update, delete)
Prevent duplicate department names (AJAX check)
View department details in a popup (SweetAlert, no refresh)
Manage employees and calculate total salary
Search, sort, and paginate using DataTables
A department cannot be deleted if it still has employees.
If an employee is deleted and they have tasks, those tasks will be reassigned to the department manager.
If a manager is deleted and they have tasks, those tasks will be deleted as well.
A manager can only assign tasks to employees in their own department.
An employee can only view their own tasks.

Tech Stack

ASP.NET Core MVC
Entity Framework Core + SQL Server
Bootstrap, jQuery, DataTables, SweetAlert2

Getting Started
Clone the repo
git clone https://github.com/DevMohamedHussien/EmployeeManagement

Update appsettings.json with your SQL Server connection string.

Run database migrations:
update-database

There are 3 roles will be seeding (SuperAdmin,Employee,Mnager),
And SuperAdmin User with credential (Email:Omar@gmail.com, PassWord: P@$$w0rd  )

