# Transaction-Management-System-Backend

## Overview
The Transaction Management System Backend is a RESTful API designed to handle transactions. It provides endpoints for creating, reading, updating, and deleting transactions, as well as generating reports and managing user accounts.

## Features
- Create, read, update, and delete transactions
- User authentication and authorization
- Generate financial reports
- Error handling and validation
- Secure API with JWT authentication

## Technologies Used
- .Net 8
- Entity Framework
- PostgreSQL
- EP Plus
- JWT for authentication

## Installation
1. Clone the repository:
    ```bash
    git clone https://github.com/sornwari/Transaction-Management-System-Backend.git
    ```
## Running the Application
1. Open Project TMS-Backend.sln via Visual Studio 2022.
2. Start TMS-Backend as Start up Project
3. Run project using https

## API Endpoints
- `POST /Auth/login` - Login
- `GET /Role/getRoles` - Get all roles
- `POST /Dashboard/searchDashboard` - Search dashboard data

- `POST /User/searchUser` - Search user by filter
- `POST /User/createUser` - Create user
- `PUT /User/updateUser` - Update user by UserId
- `DELETE /User/deleteUser/{Id}` - Delete user by UserId

- `POST /Account/searchAccount` - Search account by filter
- `POST /Account/createAccount` - Create account
- `PUT /Account/updateAccount` - Update account by AccountId
- `DELETE /Account/deleteAccount/{Id}` - Delete account by AccountId

- `POST /Transaction/searchTransaction` - Search transaction by filter
- `POST /Transaction/createTransaction` - Create transaction
- `PUT /Transaction/updateTransaction` - Update transaction by TransactionId
- `DELETE /Transaction/deleteTransaction/{Id}` - Delete transaction by TransactionId

- `POST /Report/getReportUser` - Get user report
- `POST /Report/getReportAccount` - Get account report
- `POST /Report/getReportTransaction` - Get transaction report

## Database schema

CREATE SCHEMA IF NOT EXISTS tms_schema;

CREATE TABLE tms_schema.Account (
    Id SERIAL PRIMARY KEY,
    AccountNo VARCHAR(50) NOT NULL,
    UserId INT NOT NULL,
    Balance DECIMAL(18, 2) NOT NULL,
    Deposit DECIMAL(18, 2) NOT NULL,
    Withdraw DECIMAL(18, 2) NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    CreateBy VARCHAR(50) NOT NULL,
    UpdateDate TIMESTAMP,
    UpdateBy VARCHAR(50)
);

CREATE TABLE tms_schema.Role (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    CreateBy VARCHAR(50) NOT NULL,
    UpdateDate TIMESTAMP,
    UpdateBy VARCHAR(50)
);

CREATE TABLE tms_schema.Transaction (
    Id SERIAL PRIMARY KEY,
    AccountId INT NOT NULL,
    TransactionName VARCHAR(100) NOT NULL,
    TransactionType VARCHAR(50) NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    TotalBeforeTransaction DECIMAL(18, 2) NOT NULL,
    TotalAfterTransaction DECIMAL(18, 2) NOT NULL,
    Status VARCHAR(50) NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    CreateBy VARCHAR(50) NOT NULL,
    UpdateDate TIMESTAMP,
    UpdateBy VARCHAR(50)
);

CREATE TABLE tms_schema.User (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    UserName VARCHAR(100) NOT NULL UNIQUE,
    Password VARCHAR(255) NOT NULL,
    RoleId INT NOT NULL,
    CreateDate TIMESTAMP NOT NULL,
    CreateBy VARCHAR(50) NOT NULL,
    UpdateDate TIMESTAMP,
    UpdateBy VARCHAR(50)
);
