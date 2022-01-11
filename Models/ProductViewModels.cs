using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JupiterEcoTech.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductBrand { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ProductFile { get; set; }
        public List<Product> Products { get; set; }
    }

    public class ProductDetails
    {
        public int ProductImageId { get; set; }
        public string ProductFile { get; set; }
        public string ProductDescription { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public List<ProductDetails> Details { get; set; }
    }
}