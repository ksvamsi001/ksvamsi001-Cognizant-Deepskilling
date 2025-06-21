using System;
using System.Collections.Generic;
namespace ECommerceSearch
{
    public class Program
    {
        public static void Main()
        {
            Product[] products = {
                new Product(105, "Wireless Headphones", "Electronics"),
                new Product(203, "Running Shoes", "Sports"),
                new Product(301, "Coffee Maker", "Kitchen"),
                new Product(102, "Smartphone", "Electronics"),
                new Product(405, "Yoga Mat", "Sports"),
                new Product(507, "Blender", "Kitchen"),
                new Product(101, "Laptop", "Electronics"),
                new Product(609, "Basketball", "Sports"),
                new Product(208, "Toaster", "Kitchen"),
                new Product(304, "Tablet", "Electronics"),
                new Product(156, "Tennis Racket", "Sports"),
                new Product(789, "Microwave", "Kitchen"),
                new Product(432, "Gaming Mouse", "Electronics"),
                new Product(678, "Dumbbells", "Sports"),
                new Product(234, "Food Processor", "Kitchen")
            };
            ECommerceSearchPlatform platform = new ECommerceSearchPlatform(products);
            platform.DisplayComplexityAnalysis();
            platform.DisplayProducts();
            Console.WriteLine("=== SEARCH DEMONSTRATIONS ===");
            Console.WriteLine("Linear Search by ID (203):");
            Product result1 = platform.LinearSearchById(203);
            Console.WriteLine(result1 != null ? result1.ToString() : "Product not found");
            Console.WriteLine();
            Console.WriteLine("Binary Search by ID (203):");
            Product result2 = platform.BinarySearchById(203);
            Console.WriteLine(result2 != null ? result2.ToString() : "Product not found");
            Console.WriteLine();
            Console.WriteLine("Linear Search by Name (Laptop):");
            Product result3 = platform.LinearSearchByName("Laptop");
            Console.WriteLine(result3 != null ? result3.ToString() : "Product not found");
            Console.WriteLine();
            Console.WriteLine("Linear Search by Category (Sports):");
            List<Product> sportsProducts = platform.LinearSearchByCategory("Sports");
            foreach (var product in sportsProducts)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();

            platform.PerformanceAnalysis(301);

            Console.WriteLine("=== TESTING EDGE CASES ===");
            Console.WriteLine("Search for non-existent product (999):");
            Product notFound = platform.BinarySearchById(999);
            Console.WriteLine(notFound != null ? notFound.ToString() : "Product not found");
            Console.WriteLine();

            Console.WriteLine("Performance test with non-existent product:");
            platform.PerformanceAnalysis(999);
        }
    }
}