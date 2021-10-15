# smart-energy

## Description

Monolithic implementation of distribution network monitoring web application proved to be an insufficient quality solution in terms of aspects such as scalability, sustainability and performance. Migration from monolithic to microservice architecture was applied in order to solve the previously mentioned problems. Successful application of the microservice architecture required proper handling of distributed transactions.

## Required software

- Visual Studio Code
- Visual Studio 2019
- Docker Desktop
- SQL Server Management Studio 2018


## Run back end

- Open SmartEnergy.sln using Visual Studio 2019
- Set docker-compose as Startup Project 
- Run
- Check the status of containers using Docker Desktop, should be Running


## Run front end

- Open power-system-web-app in Visual Studion Code
- Open terminal and run command: npm install
- Run command: ng serve


## Run SQL scripts with test data

- Open SQL Server Management Studio 2018
- Connect to server type: Database Engine and select SQL Server Authentication
- Connect to localhost,1401 with login: SA, password: Your+password123
- Connect to localhost,1402 with login: SA, password: Your+password123
- Connect to localhost,1403 with login: SA, password: Your+password123
- Connect to localhost,1404 with login: SA, password: Your+password123
- Run SQL scripts with data (InsertDevices.sql, InsertLocations.sql, InsertDeviceUsage.sql, InsertSmartEnergy.sql)


 
