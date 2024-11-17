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

7. Service Request Status
The "Service Request Status" feature manages and tracks the progress of service requests, allowing users to see dependencies, change statuses, and engage with a dynamic set of tasks. This feature is created with a variety of data structures, including graphs, binary search trees, heaps, lists, dictionaries, and combo boxes, to ensure optimal performance, user experience, and efficiency.

![VCDN-BCA3 - 2024-11-17T195846 - lab-2271f54a-c899-4627-8a1b-0265c056dd29 westeurope cloudapp azure com_7123 - Remote Desktop Connection 2024_11_17 21_22_06](https://github.com/user-attachments/assets/44b25508-ca34-452b-b0f2-ab7d47c3d2a2)


![VCDN-BCA3 - 2024-11-17T195846 - lab-2271f54a-c899-4627-8a1b-0265c056dd29 westeurope cloudapp azure com_7123 - Remote Desktop Connection 2024_11_17 21_23_23](https://github.com/user-attachments/assets/d0368cfb-717f-4b56-9b9c-e549d81b41f3)

1. Graph (for Service Request Dependencies)
- Role:
A graph is used to represent the dependencies between service requests. In this setting, each service request is modelled as a node, with directed edges representing the relationships (dependencies) between them. This graph helps us see which service requests are dependent on others.
- Contribution to Efficiency:
Efficient Traversal: Graphs have a structure that enables rapid traversal of dependent nodes. Using graph techniques such as depth-first or breadth-first search, we can quickly locate all tasks that are reliant on a single request.
Fast Dependency Lookup: By representing the graph using a dictionary or adjacency list, we can quickly locate and visualise all of the dependents for a service request in the TreeView.

```bash
foreach (var request in requestGraph.GetAllNodes())
{
    var dependencies = requestGraph.GetNeighbors(request);
    if (dependencies.Count > 0)
    {
        var rootNode = new TreeViewItem { Header = new TextBlock { Text = $"{request.Description} depends on:" } };
        foreach (var dependentRequest in dependencies)
        {
            var childNode = new TreeViewItem { Header = new TextBlock { Text = $"• {dependentRequest.Description}" } };
            rootNode.Items.Add(childNode);
        }
        DependencyTreeView.Items.Add(rootNode);
    }
}
```

2. Binary Search Tree (for Service Request Ordering):
- Role:
A binary search tree (BST) is a technique for efficiently storing and retrieving service requests in sorted order based on certain criteria, such as request priority, date created, or progress status. BSTs enable for fast insertion, deletion, and searching in O(log n) time.
- Contribution to Efficiency:
Efficient Sorting and Searching: The binary search tree ensures that service requests are always maintained in an ordered format, making it simple to obtain them based on certain criteria, such as the most recent or highest priority requests.
Optimised Performance: The BST has logarithmic time complexity for search operations, which means that even as the number of service requests increases, the system can still run efficiently.
```bash
Example: 
// Example of adding a service request to a binary search tree
binarySearchTree.Add(new ServiceRequest { Priority = 1, Description = "Fix plumbing issue" });
binarySearchTree.Add(new ServiceRequest { Priority = 3, Description = "Electrical repairs" });
binarySearchTree.Add(new ServiceRequest { Priority = 2, Description = "Paint the wall" });
```

4. List (for Service Requests):
- Role: 
A List<ServiceRequest> stores all service requests in memory. This collection offers fast access and editing features, allowing users to quickly examine and update service request statuses.
- Contribution to Efficiency:
Dynamic Data Handling: The list offers efficient dynamic resizing and constant-time indexing. This makes it an excellent data structure for storing a list of requests that are regularly updated.
Simple Management: Lists enable simple management of requests, such as adding new requests, removing requests, or changing their status.

```bash
Example: 
// Accessing and modifying a service request's status
var selectedRequest = ServiceRequestDataGrid.SelectedItem as ServiceRequest;
selectedRequest.Status = "In Progress";
ServiceRequestDataGrid.Items.Refresh();
```

5. Dictionary (for Service Request Graph):
Role:
A dictionary is used to describe the graph of service requests, with each key-value pair denoting a service request and its dependent requests. This enables faster lookups and more efficient depiction of task dependencies.
- Contribution to Efficiency:
  Fast Dependency Access: By using a dictionary for the graph's adjacency list representation, accessing a service request's dependencies takes only O(1) time on average.
  Efficient Graph Representation: This structure makes it easier to build and traverse graphs, allowing for more efficient investigation of dependencies and task linkages.

```bash
example:
// Getting the neighbors (dependencies) of a specific request using a dictionary
var dependencies = requestGraph.GetNeighbors(request);
```
6. ComboBox (for Status Selection):
- Role:
  The ComboBox is used to pick a new status for a service request from predefined alternatives such as "Pending", "In Progress", or "Completed".
- Contribution to Efficiency:
  User-Friendly Interface: The ComboBox simplifies status selection by guaranteeing that the user chooses only valid statuses. This prevents invalid status entries and enhances the user experience.
  Efficient Status Updates: When a status is selected, the accompanying service request can be swiftly changed, and the system will reflect the change right away.

```bash
Example:
// Setting the status of a selected service request based on ComboBox selection
var selectedStatus = (StatusComboBox.SelectedItem as ComboBoxItem)?.Content as string;
selectedRequest.Status = selectedStatus;
```

7. DataGrid (for Displaying Service Requests):
-Role:
The DataGrid displays the list of service requests in a tabular style. It enables the sorting, filtering, and altering of service request data.
- Contribution to Efficiency:
Interactive User Interface: The DataGrid allows users to easily interact with the list of service requests. It offers real-time updates and refreshes when a service request's status changes.
Sorting and Filtering: Users can sort and filter the DataGrid's data, making it easier to identify and handle requests based on criteria such as status or priority.

```bash
Example:
// Refreshing the DataGrid after status change
ServiceRequestDataGrid.Items.Refresh();
```

Role of Each Data Structure in the Feature’s Efficiency
Each data structure contributes to the "Service Request Status" feature in the following ways:

- Graph: Efficiently manages and visualizes dependencies between service requests.
- Binary Search Tree (BST): Ensures efficient storage, sorting, and retrieval of service requests.
- Heap (Priority Queue): Allows for efficient scheduling and management of service requests based on priority.
- List: Provides efficient dynamic management and updates of service requests.
- Dictionary: Enables fast lookups and management of service request dependencies.
- ComboBox: Provides a simple and user-friendly interface for updating the status of service requests.
- DataGrid: Displays service requests interactively with support for sorting, filtering, and dynamic updates.

  
# User Engagement Strategy
- To encourage active participation, the application allows users to customize their profiles. Personalization options include changing the profile image and updating contact details. This engagement strategy aims to enhance user experience and foster a sense of ownership.

# Youtube Video:
- [https://youtu.be/5AMcGV8tzyA](https://youtu.be/U_TdzZMkvSs)

Contact
For any questions or feedback, please contact ST10029778@vcconnect.edu.za
