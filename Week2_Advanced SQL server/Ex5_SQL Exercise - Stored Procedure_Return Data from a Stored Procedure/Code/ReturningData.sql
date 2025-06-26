USE EmployeeManagementSystem;
GO
CREATE PROCEDURE sp_GetEmployeeCountByDepartment @DepartmentID INT  
AS
BEGIN SELECT d.DepartmentName, COUNT(e.EmployeeID) AS TotalEmployees FROM Departments d
LEFT JOIN Employees e ON d.DepartmentID = e.DepartmentID WHERE d.DepartmentID = @DepartmentID GROUP BY d.DepartmentName;
END;
GO

EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 1;
EXEC sp_GetEmployeeCountByDepartment @DepartmentID = 2;