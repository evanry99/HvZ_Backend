# Humans Vs Zombies (HvZ) backend part for Team HvZ.NET 


## Project Description
The goal of this project is to design and implement a software solution for managing the state and communication of one or more concurrent games of HvZ. For the backend, a suitable database has been created using a code-first approach, and a RESTful API service has been developed to allow the frontend to interact with the database. To ensure the security of the backend, a Keycloak instance has been implemented. The database and API are deployed in Azure.

The RESTful API includes seven main API fields: Chat, Game, Kill, Mission, Player, Squad, and User. Documentation for each API endpoint and instructions on how to use them are provided in the GitLab project.
## Table of Contents

- [Technologies](#technologies)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)

## Technologies
* C#
* .NET 6
* Visual Studio 2022
* Microsoft SQL
* SQL Server Management Studio
* API Documentation: Swagger
* API Testing: Swagger
* AutoMapper
* Keycloak
* SignalR

## Installation

Download and install:
* SQL Server Management Studio
* Visual Studio with .NET 6.0 or later

## Usage
Usage guide:

1. Clone this repository with: 
```
git clone git@gitlab.com:experis-case/hvz_backend.git
```

2. In the project folder look for **appsettings.json** and change the connection string in **appsettings.json** to your database server of your choice.

It should look similar like this: 
```
{
    "Logging": {
        "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
        //Set a connection
        "DefaultConnection": "Data Source=DESKTOP-JVE6J0Q\\SQLEXPRESS;Initial Catalog= HvZDB; Integrated Security=True;"
    }

}
```
3. To migrate the database into your server/database. In Visual Studio go to **Tools -> NuGet Package Manager ->Package Manager Console**

In the console use:

```
update-database
```
The command will create a database with tables and initial data.

4. Run the program with IIS Express, it will open a new localhost swagger site.
Here you can test the APIs with CRUD (create, read, update and delete).
You can take a look at the API documentation on how to use the API.

## Contributing

* [Håvard Madland](https://gitlab.com/havardmad/ "Håvard gitlab")
* [Erlend Halsne Dahl](https://gitlab.com/Erlend-Halsne-Dahl "Erlend gitlab")
* [Sondre Eftedal](https://gitlab.com/SondreEftedal "Sondre gitlab")
* [Vilhelm Assersen](https://gitlab.com/Vilhelm-Assersen "Vilhelm gitlab")
* [An Binh Nguyen](https://gitlab.com/anbinhnguy/ "An gitlab")
