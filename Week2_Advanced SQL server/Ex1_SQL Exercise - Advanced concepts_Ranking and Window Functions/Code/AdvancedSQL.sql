CREATE DATABASE ProductRankingExercise;
USE ProductRankingExercise;

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(100) NOT NULL,
    Category NVARCHAR(50) NOT NULL,
    Price DECIMAL(10,2) NOT NULL
);

INSERT INTO Products (ProductName, Category, Price) VALUES
('iPhone 15', 'Electronics', 999.99),
('Samsung Galaxy S24', 'Electronics', 899.99),
('iPad Pro', 'Electronics', 1099.99),
('MacBook Air', 'Electronics', 1199.99),
('Dell Laptop', 'Electronics', 899.99),
('Nike Air Max', 'Shoes', 150.00),
('Adidas Ultraboost', 'Shoes', 180.00),
('Converse Chuck Taylor', 'Shoes', 65.00),
('New Balance 990', 'Shoes', 180.00),
('Vans Old Skool', 'Shoes', 70.00),
('Levi 501 Jeans', 'Clothing', 89.99),
('Nike Hoodie', 'Clothing', 79.99),
('Polo Ralph Lauren Shirt', 'Clothing', 125.00),
('Uniqlo T-Shirt', 'Clothing', 19.99),
('Champion Sweatshirt', 'Clothing', 79.99);

-- QUERY 1:

SELECT 
    ProductName,
    Category,
    Price,
    ROW_NUMBER() OVER (PARTITION BY Category ORDER BY Price DESC) AS RowNumber
FROM Products
ORDER BY Category, Price DESC;

-- QUERY 2:

SELECT 
    ProductName,
    Category,
    Price,
    RANK() OVER (PARTITION BY Category ORDER BY Price DESC) AS RankNumber
FROM Products
ORDER BY Category, Price DESC;

-- QUERY 3:

SELECT 
    ProductName,
    Category,
    Price,
    DENSE_RANK() OVER (PARTITION BY Category ORDER BY Price DESC) AS DenseRankNumber
FROM Products
ORDER BY Category, Price DESC;

-- FINAL QUERY:

WITH RankedProducts AS (
    SELECT 
        ProductName,
        Category,
        Price,
        DENSE_RANK() OVER (PARTITION BY Category ORDER BY Price DESC) AS DenseRankNum
    FROM Products
)
SELECT 
    ProductName,
    Category,
    Price,
    DenseRankNum
FROM RankedProducts
WHERE DenseRankNum <= 3
ORDER BY Category, DenseRankNum;