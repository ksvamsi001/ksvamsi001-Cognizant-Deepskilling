using System;
namespace ECommerceSearch
{
    public class Product : IComparable<Product>
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public Product(int productId, string productName, string category)
        {
            ProductId = productId;
            ProductName = productName;
            Category = category;
        }
        public int CompareTo(Product other)
        {
            if (other == null) return 1;
            return this.ProductId.CompareTo(other.ProductId);
        }
        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {ProductName}, Category: {Category}";
        }
    }
}