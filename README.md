# PROG7321_POE

# Municipal Services Application
Overview
The Municipal Services Application is a WPF (Windows Presentation Foundation) application designed to enable citizens to report issues and request services for a South African municipality. The application focuses on providing a user-friendly interface for reporting issues, attaching media, managing user profiles, and being able to view any local events. 

# Features
1. Login and Registration: Users start on the login page and can log in or register for an account. Users can also continue as a guest to report issues.
2. Issue Reporting: Citizens can report various issues such as sanitation, roads, and utilities. The application allows attaching media to the report.
3. Customizable User Profiles: Users can create and customize their profiles. Customization options include profile image, username, address, email, and mobile number. This strategy encourages active participation by allowing users to personalize their experience.
4. Local Events and Announcements: iew and search for upcoming local events by date and category. The screen displays events in two lists: "Events" and "Recommendations."
# Getting Started
# Prerequisites
.NET Framework 8.0 or later
Visual Studio 2022 or later
SQL Server Management Studio (SSMS) for database operations (for the user profile)
Installation
1. Clone the Repository

- Copy code
- git clone https://github.com/Khatija-Essa/PROG7321_POE.git
- cd municipal-services-application

2. Open the Project

- Open the solution file (MunicipalServicesApplication.sln) in Visual Studio.

3. Restore NuGet Packages

- In Visual Studio, restore the NuGet packages by right-clicking on the solution in Solution Explorer and selecting Restore NuGet Packages.
- the NuGet Oackes in this applicatio are:
  - System.Data.SqlClient

4. Set Up the Database

- Ensure SQL Server is installed and running.
- Create a new database named MunicipalServicesDB. (for user profiles)
- Execute the SQL scripts provided in the DatabaseScripts folder to set up the necessary tables and schema.
5. Build and Run the Application

- Build the solution by selecting Build > Build Solution from the menu.
- Run the application by pressing F5 or selecting Debug > Start Debugging.
  
# Application Workflow
1. Login Page

- The application starts on the login page. Users can:
- Log In: Enter their username and password to access their accounts.
- Register: New users can click the "Register" link to navigate to the registration page.
- Continue as Guest: Users can opt to report issues without logging in or registering.

![VCDN-BCA3 - 2024-09-10T200118 - lab-2271f54a-c899-4627-8a1b-0265c056dd29 westeurope cloudapp azure com_7123 - Remote Desktop Connection 2024_09_10 21_27_56](https://github.com/user-attachments/assets/b5bc5430-d64b-4c28-af02-5282ecfec27d)


2. Registration Page

- Username: Enter a desired username.
- Password: Set a secure password.
- Confirm Password: Confirm the password.
- Address: Provide a residential address.
- Email: Enter a valid email address.
- Mobile Number: Enter a mobile contact number.
- Register: Submit the registration form to create an account. Upon successful registration, users are redirected to their profile page for further customization.

![VCDN-BCA3 - 2024-09-10T200118 - lab-2271f54a-c899-4627-8a1b-0265c056dd29 westeurope cloudapp azure com_7123 - Remote Desktop Connection 2024_09_10 21_29_54](https://github.com/user-attachments/assets/ce722956-2b43-4eed-9baa-e2d6e1b6850e)
  

3. MainWindows page:
- Report Issue: be lead to the report issue page.
- ocal Events and Announcements: Feature coming soon but current button not enabled.
- Service Request Status: Feature coming soon but current button not enabled.

![VCDN-BCA3 - 2024-09-10T200118 - lab-2271f54a-c899-4627-8a1b-0265c056dd29 westeurope cloudapp azure com_7123 - Remote Desktop Connection 2024_09_10 21_31_07](https://github.com/user-attachments/assets/8335a258-867d-47a6-a8ca-b18ea414de79)
  
  
4. Reporting Issues

- Location: Enter the location of the issue.
- Category: Select the category of the issue (e.g., sanitation, roads, utilities).
- Description: Provide a detailed description of the issue.
- Attach Media: Optionally, attach images related to the issue. Images are saved in a folder named Images within the application directory.
- Submit: Submit the report. A confirmation message will appear, and the form will reset.
- Go main to main page. user cn return back to the mainwindows form.
- All data is saved in a list on the report an issue form.

![VCDN-BCA3 - 2024-11-17T195846 - lab-2271f54a-c899-4627-8a1b-0265c056dd29 westeurope cloudapp azure com_7123 - Remote Desktop Connection 2024_11_17 20_09_07](https://github.com/user-attachments/assets/c9f51ea0-0204-4d1c-82e4-f8cc7f62b006)


  
5. User Profiles

- Profile Customization: After logging in or registering, users can customize their profile by updating personal details and profile image.
- Navigation back to Main Menu: From the reporting page, users can navigate back to the main menu.

  ![VCDN-BCA3 - 2024-09-10T200118 - lab-2271f54a-c899-4627-8a1b-0265c056dd29 westeurope cloudapp azure com_7123 - Remote Desktop Connection 2024_09_10 21_35_02](https://github.com/user-attachments/assets/73f4fb21-d2da-4229-abbc-45dd3addec1e)

6. Local Events and Announcements

- View and search for upcoming local events by date and category.
- The screen displays events in two lists: "Events" and "Recommendations."
- Events are stored in various data structures, including dictionaries, queues, and hash sets, allowing for efficient searching and categorization.
- Recent search functionality is provided, and searches are stored in a queue for quick access.
- Users can filter events by date, name or category.

![image](https://github.com/user-attachments/assets/4023dfed-59cc-4ba7-a63f-fbe650fa549f)



  
# User Engagement Strategy
- To encourage active participation, the application allows users to customize their profiles. Personalization options include changing the profile image and updating contact details. This engagement strategy aims to enhance user experience and foster a sense of ownership.

# Youtube Video:
- [https://youtu.be/5AMcGV8tzyA](https://youtu.be/U_TdzZMkvSs)

Contact
For any questions or feedback, please contact ST10029778@vcconnect.edu.za
