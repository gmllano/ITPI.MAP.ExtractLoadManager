# ITPI.MAP.DataExtractManager
Purpose:  

The purpose of this .net application is to load courses for a community college on a nightly basis, the .json files that are loaded are broken down by terms (Fall, Spring, and Summer).  The application deserializes each file into a class object, it then loads the data into a sql server table.

Technologies used:  

Visual Studio 2019, C#, .net framework 4.7.2, Dapper, Log4net, StyleCop, Autofac and SQL Server 2017.

Unit Test Project:

The file ITPI.MAP.ExtractManagerUnitTest.zip contains the unit test project.  The Microsoft Test Platform Framework and the Moq framework was used to create tests.
