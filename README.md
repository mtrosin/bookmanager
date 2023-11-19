# Book Manager API

## Overview

The Book Manager is a .NET Core web API designed to manage books. It provides CRUD (Create, Read, Update, Delete) operations for book entities and integrates with Amazon S3 for storing book-related images.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
- [Usage](#usage)
  - [API Endpoints](#api-endpoints)
  - [Amazon S3 Integration](#amazon-s3-integration)
  - [Unit Testing](#unit-testing)
- [ToDo](#todo)

## Features

- CRUD operations for managing books.
- Integration with Amazon S3 for storing book images.
- RESTful API design for easy consumption.
- Repository pattern

## Getting Started

### Prerequisites

Before running the application, ensure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/mtrosin/bookmanager.git

2. Open up the project on Visual Studio
3. Build the project

### Configuration
1. Open up "appsettings.json" file and change the "ConnectionStrings" to match your MSSQL database.
2. In order to use the S3 bucket to store images, open up "appsettings.json" file and edit "AWS" key with your data.

## Usage
### API endpoints
Open http://localhost:5071/swagger to check and use the routes.

### Amazon S3 Integration
To use S3 to store images a repository called "S3ImageRepository" was created and used by default. If you wish to just store images locally just change the image Repository being used inside "Program.cs" to "LocalImageRepository" then it will be stored on "/Images" folder on the root folder.

### Unit Testing
To run the tests go to "Test -> Run all tests" inside Visual Studio.

## ToDo
Finish docker files.
Add pagination to Books and Authors.