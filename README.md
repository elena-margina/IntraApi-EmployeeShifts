IntraApi: Indicates a focus on internal architecture and dependency management.

Instructions on how to build and run the application
Build and run the application:
In file folder you can find folder with name “_SQL”, here is the script with initial load of SQL
tables and some DUMMY data.
Open Microsoft SQL Server Management Studio, open a file “_SQL.sql”.



1. Create the Database:

	The script will:

	Create a database named Restaurant.
	Create a login and user named RestaurantUser:
	This user will have the necessary permissions to operate with the database tables used in the API.
	These credentials will be required when setting up the application's connection string.
	Create the required tables, views, and relationships:
	The script will define tables and configure relationships to match the project requirements.
	Insert dummy data:
	Add sample records to the tables for testing purposes.

2. Open Microsoft Visual Studio Community 2022:

	Launch Visual Studio Community 2022 (64-bit) on your machine.

3. Open the API Project:

	Navigate to the project folder where your solution files are located.
	Open the solution IntraApi.Api.sln.

4. Run the API Project:

	After the solution is loaded, run the IntraApi.Api project. This should start up the API that will interact with your database.

5. Open and Run the Blazor Server App Project:

	Now, go back to the project folder and open the solution IntraApi.App.sln in Visual Studio.
	Run the IntraApi.App project, which is the Blazor Server App, to start the front-end application.
