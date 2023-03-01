Requirements: SQL Server Express

Contents:
	SqlScripts/ - directory containing SQL scripts to create the required database and tables
	CustomerApplication/ - directory containing CustomerApplication solution file and relevant projects

Summary: 
	Before project launch, run sql scripts in SqlScripts folder in their numbered order.
	Note - database creation may fail depending on local system SQL server express installation path. 

	If this is the case, a database named "Consulting" should be created with default settings. 

	The table creation and default data scripts can then be executed. 
	At a minimum, the SQL tables must be created for the application to function in a meaningful way. 
	
	Visual Studio Startup Project: CustomerApplication.WebApp
 