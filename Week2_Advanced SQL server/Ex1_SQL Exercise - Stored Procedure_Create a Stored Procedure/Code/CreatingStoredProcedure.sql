CREATE DATABASE EmployeeManagementSystem;
GO

USE EmployeeManagementSystem;
GO

CREATE TABLE Departments (DepartmentID INT PRIMARY KEY,DepartmentName VARCHAR(100)
);

CREATE TABLE Employees (EmployeeID INT PRIMARY KEY,
FirstName VARCHAR(50),
LastName VARCHAR(50),
DepartmentID INT FOREIGN KEY REFERENCES Departments(DepartmentID),
Salary DECIMAL(10,2),
JoinDate DATE
);

INSERT INTO Departments (DepartmentID, DepartmentName) VALUES 
(1, 'HR'), 
(2, 'Finance'), 
(3, 'IT'), 
(4, 'Marketing');

INSERT INTO Employees (EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate) VALUES 
(1, 'John', 'Doe', 1, 5000.00, '2020-01-15'),
(2, 'Jane', 'Smith', 2, 6000.00, '2019-03-22'),
(3, 'Michael', 'Johnson', 3, 7000.00, '2018-07-30'),
(4, 'Emily', 'Davis', 4, 5500.00, '2021-11-05');

--Step 1
CREATE PROCEDURE sp_GetEmployeesByDepartment @DepartmentID INT
AS
BEGIN
--Step 2
SELECT e.EmployeeID,e.FirstName,e.LastName,
d.DepartmentName,
e.Salary,
e.JoinDate
FROM Employees e
INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE e.DepartmentID = @DepartmentID
ORDER BY e.LastName, e.FirstName;
END;
GO

--Step 3
CREATE PROCEDURE sp_InsertEmployee
@FirstName VARCHAR(50),
@LastName VARCHAR(50),
@DepartmentID INT,
@Salary DECIMAL(10,2),
@JoinDate DATE
AS
BEGIN
DECLARE @NextEmployeeID INT;
SELECT @NextEmployeeID = ISNULL(MAX(EmployeeID), 0) + 1 FROM Employees;
INSERT INTO Employees (EmployeeID, FirstName, LastName, DepartmentID, Salary, JoinDate)
VALUES (@NextEmployeeID, @FirstName, @LastName, @DepartmentID, @Salary, @JoinDate);
    
END;
GO
