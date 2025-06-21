using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ECommerceSearch
{
    public class ECommerceSearchPlatform
    {
        private Product[] products;
        private Product[] sortedProducts;

        public ECommerceSearchPlatform(Product[] products)
        {
            this.products = products;
            this.sortedProducts = new Product[products.Length];
            Array.Copy(products, sortedProducts, products.Length);
            Array.Sort(sortedProducts);
        }

        public Product LinearSearchById(int productId)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].ProductId == productId)
                {
                    return products[i];
                }
            }
            return null;
        }

        public Product LinearSearchByName(string productName)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    return products[i];
                }
            }
            return null;
        }

        public List<Product> LinearSearchByCategory(string category)
        {
            List<Product> results = new List<Product>();
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].Category.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(products[i]);
                }
            }
            return results;
        }

        public Product BinarySearchById(int productId)
        {
            int left = 0;
            int right = sortedProducts.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;

                if (sortedProducts[mid].ProductId == productId)
                {
                    return sortedProducts[mid];
                }

                if (sortedProducts[mid].ProductId < productId)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid - 1;
                }
            }
            return null;
        }

        public Product RecursiveBinarySearchById(int productId)
        {
            return RecursiveBinarySearchHelper(productId, 0, sortedProducts.Length - 1);
        }

        private Product RecursiveBinarySearchHelper(int productId, int left, int right)
        {
            if (left > right)
            {
                return null;
            }

            int mid = left + (right - left) / 2;

            if (sortedProducts[mid].ProductId == productId)
            {
                return sortedProducts[mid];
            }

            if (sortedProducts[mid].ProductId < productId)
            {
                return RecursiveBinarySearchHelper(productId, mid + 1, right);
            }
            else
            {
                return RecursiveBinarySearchHelper(productId, left, mid - 1);
            }
        }

        public void PerformanceAnalysis(int searchId)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();
            Product linearResult = LinearSearchById(searchId);
            sw.Stop();
            long linearTime = sw.ElapsedTicks;

            sw.Restart();
            Product binaryResult = BinarySearchById(searchId);
            sw.Stop();
            long binaryTime = sw.ElapsedTicks;

            Console.WriteLine("=== PERFORMANCE ANALYSIS ===");
            Console.WriteLine($"Dataset Size: {products.Length} products");
            Console.WriteLine($"Search Target: Product ID {searchId}");
            Console.WriteLine();
            
            Console.WriteLine("LINEAR SEARCH:");
            Console.WriteLine($"Time Complexity: O(n)");
            Console.WriteLine($"Space Complexity: O(1)");
            Console.WriteLine($"Execution Time: {linearTime} ticks");
            Console.WriteLine($"Result: {(linearResult != null ? "Found" : "Not Found")}");
            Console.WriteLine();

            Console.WriteLine("BINARY SEARCH:");
            Console.WriteLine($"Time Complexity: O(log n)");
            Console.WriteLine($"Space Complexity: O(1) iterative, O(log n) recursive");
            Console.WriteLine($"Execution Time: {binaryTime} ticks");
            Console.WriteLine($"Result: {(binaryResult != null ? "Found" : "Not Found")}");
            Console.WriteLine();

            Console.WriteLine("COMPARISON:");
            if (binaryTime > 0)
            {
                double speedup = (double)linearTime / binaryTime;
                Console.WriteLine($"Binary search is {speedup:F2}x faster");
            }
            Console.WriteLine($"Binary search requires sorted data");
        }

        public void DisplayComplexityAnalysis()
        {
            Console.WriteLine("=== BIG O NOTATION ANALYSIS ===");
            Console.WriteLine();
            
            Console.WriteLine("LINEAR SEARCH:");
            Console.WriteLine("• Best Case: O(1) - target is first element");
            Console.WriteLine("• Average Case: O(n/2) = O(n) - target is in middle");
            Console.WriteLine("• Worst Case: O(n) - target is last element or not found");
            Console.WriteLine("• Space Complexity: O(1) - constant extra space");
            Console.WriteLine();

            Console.WriteLine("BINARY SEARCH:");
            Console.WriteLine("• Best Case: O(1) - target is middle element");
            Console.WriteLine("• Average Case: O(log n) - logarithmic divisions");
            Console.WriteLine("• Worst Case: O(log n) - maximum log n comparisons");
            Console.WriteLine("• Space Complexity: O(1) iterative, O(log n) recursive");
            Console.WriteLine("• Prerequisite: Data must be sorted");
            Console.WriteLine();

            Console.WriteLine("SCALABILITY ANALYSIS:");
            Console.WriteLine("Dataset Size | Linear Search | Binary Search");
            Console.WriteLine("-------------|---------------|---------------");
            Console.WriteLine("100          | 100 ops       | 7 ops");
            Console.WriteLine("1,000        | 1,000 ops     | 10 ops");
            Console.WriteLine("10,000       | 10,000 ops    | 14 ops");
            Console.WriteLine("100,000      | 100,000 ops   | 17 ops");
            Console.WriteLine("1,000,000    | 1,000,000 ops | 20 ops");
            Console.WriteLine();

            Console.WriteLine("RECOMMENDATION FOR E-COMMERCE:");
            Console.WriteLine("• Use Binary Search for Product ID lookups (frequently accessed, numeric)");
            Console.WriteLine("• Use Linear Search for Category/Name searches (text-based, flexible)");
            Console.WriteLine("• Consider indexing strategies for large datasets");
            Console.WriteLine("• Hash tables may be optimal for exact matches");
        }

        public void DisplayProducts()
        {
            Console.WriteLine("=== PRODUCT CATALOG ===");
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
            Console.WriteLine();
        }
    }
}