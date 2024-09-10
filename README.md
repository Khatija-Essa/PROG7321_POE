# PROG7321_POE

# Municipal Services Application
Overview
The Municipal Services Application is a WPF (Windows Presentation Foundation) application designed to enable citizens to report issues and request services for a South African municipality. The application focuses on providing a user-friendly interface for reporting issues, attaching media, and managing user profiles.

# Features
Login and Registration: Users start on the login page and can log in or register for an account. Users can also continue as a guest to report issues.
Issue Reporting: Citizens can report various issues such as sanitation, roads, and utilities. The application allows attaching media to the report.
Customizable User Profiles: Users can create and customize their profiles. Customization options include profile image, username, address, email, and mobile number. This strategy encourages active participation by allowing users to personalize their experience.
# Getting Started
# Prerequisites
.NET Framework 8.0 or later
Visual Studio 2022 or later
SQL Server Management Studio (SSMS) for database operations
Installation
1. Clone the Repository

- Copy code
- git clone https://github.com/Khatija-Essa/PROG7321_POE.git
- cd municipal-services-application

2. Open the Project

- Open the solution file (MunicipalServicesApplication.sln) in Visual Studio.

3. Restore NuGet Packages

- In Visual Studio, restore the NuGet packages by right-clicking on the solution in Solution Explorer and selecting Restore NuGet Packages.

4. Set Up the Database

- Ensure SQL Server is installed and running.
- Create a new database named MunicipalServicesDB.
- Execute the SQL scripts provided in the DatabaseScripts folder to set up the necessary tables and schema.
5. Build and Run the Application

- Build the solution by selecting Build > Build Solution from the menu.
- Run the application by pressing F5 or selecting Debug > Start Debugging.
  
# Application Workflow
1. Login Page

The application starts on the login page. Users can:

- Log In: Enter their username and password to access their accounts.
- Register: New users can click the "Register" link to navigate to the registration page.
- Continue as Guest: Users can opt to report issues without logging in or registering.
2. Registration Page

- Username: Enter a desired username.
- Password: Set a secure password.
- Confirm Password: Confirm the password.
- Address: Provide a residential address.
- Email: Enter a valid email address.
- Mobile Number: Enter a mobile contact number.
- Register: Submit the registration form to create an account. Upon successful registration, users are redirected to their profile page for further customization.

3. MainWindows page:
- Report Issue: be lead to the report issue page.
- ocal Events and Announcements: Feature coming soon but current button not enabled.
- Service Request Status: Feature coming soon but current button not enabled.
  
4. Reporting Issues

- Location: Enter the location of the issue.
- Category: Select the category of the issue (e.g., sanitation, roads, utilities).
- Description: Provide a detailed description of the issue.
- Attach Media: Optionally, attach images related to the issue. Images are saved in a folder named Images within the application directory.
- Submit: Submit the report. A confirmation message will appear, and the form will reset.
- go main to main page. user cn return back to the mainwindows form.
  
5. User Profiles

- Profile Customization: After logging in or registering, users can customize their profile by updating personal details and profile image.
  
6. Navigation

- Back to Main Menu: From the reporting page, users can navigate back to the main menu.
# User Engagement Strategy
- To encourage active participation, the application allows users to customize their profiles. Personalization options include changing the profile image and updating contact details. This engagement strategy aims to enhance user experience and foster a sense of ownership.

# Youtube Video:
- 

Contact
For any questions or feedback, please contact ST10029778@vcconnect.edu.za
