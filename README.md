# RStudents

RStudents is a web application designed for managing student and group information. The application allows users to perform CRUD (Create, Read, Update, Delete) operations on students and groups, making it ideal for educational institutions and administrative purposes.

## Features

- **Student Management**: Add, view, edit, and delete student records. Each student is associated with a group.
- **Group Management**: Create, view, edit, and delete groups. Each group can have multiple students.
- **Responsive UI**: A user-friendly interface for managing students and groups.
- **Database Integration**: Uses Entity Framework for database operations, ensuring smooth interaction with SQL Server.
- **Automated Testing**: Selenium-based tests to ensure the application's reliability (visit RStudentsTest).

## Getting Started

### Prerequisites

- **.NET 6.0 SDK**: Ensure you have .NET 6.0 installed.
- **SQL Server**: A running instance of SQL Server.
- **Visual Studio**: Recommended for development and debugging.
- **Selenium WebDriver**: For running automated UI tests.

### Installation

1. **Clone the repository**:
   `git clone https://github.com/Tier1Coder/RStudents.git`
  
   
2. **Set up the database**:
- Open SQL Server Management Studio (SSMS).
- Create a new database, e.g., RStudentsDB.
- Update the connection string in appsettings.json with your SQL Server details.


3. **Apply migrations**:
In the Package Manager Console of Visual Studio, run: `Update-Database`


4. **Run the application**:
Use: `dotnet run`.
