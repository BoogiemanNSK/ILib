Ilib - A Library Management System

System requirements:
 - Windows 7(max), 8, 8.1, 10
 - .NET Framework 4.7
 - SQL Express https://go.microsoft.com/fwlink/?LinkID=799012
 
###                                     ###
To start the application go to
  [Project path]\I2P-Project\bin\Debug\bin\
    and run I2P-Project.exe
###                                     ###

/\/\/\/\/\/\/\/\/\ IF APP CRASHES /\/\/\/\/\/\/\/\/\

 Make sure your SQL Server service is running.
 
 If that was not the problem, try these steps:
 1) Kill all SQL Server processess with Task Manager
 2) Delete DataBases folder (inside folder with .exe)
 3) Delete all files in
 	C://Users/{Your User}/AppData/Local/Microsoft/Microsoft SQL Server Data/SQLEXPRESS
 4) Start SQL Server (you can use SQL Configuration Manager installed bu SQL Express)
 5) Try launching the app again
 
 AppData folder may be hidden, in that case, turn on showing hidden folders:
 	https://support.microsoft.com/en-us/help/14201/windows-show-hidden-files

\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/

For users:
 - To use the system you have to register a new account by submitting info about you and creating password
 - After creating an account you would be able to log into the system
 - With Ilib LMS you will be able to search through system to find books and media you like and check them out to your account
 - Please, do not overdue documents since other users may also want to use books you took
 - System holds several requirements for users:
	a) For students deadline to return books is 3 weeks after checkout
	b) For faculty members book returning deadline is 4 weeks
	c) If the book is bestseller deadline for everyone is 2 weeks
	d) AV materials and journals can only be checked out for 2 weeks for everyone
 - Please be advised that for overduing docs you have to pay the fine, specified by system

For librarians:
 - To use the system as librarian you have to register AND fill serial number field at registration page
 - Serial number can be obtained from system developers
 - After logging in as librarian you will be able to:
	a) Check overdued documents
	b) Manage patrons
	c) Adding / deleting / modifying documents

System Developers:
 - Peter Zakharkin  | Architecture & Logic    | @DanielFaraday28    
 - Anton Krylov     | Back end                | @kartofaan
 - Valery Borisov   | Front end & DB          | @valer_xenus
 - Yuriy Sukhorukov | Testing & documentation | @realnigger
