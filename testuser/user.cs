using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace testuser
{
    public class Product
    {
        public string Name { get; set; }
        public string Ks { get; set; }
    }

    public class ProductList
    {
        public List<Product> GetProducts { get; set; }
    } 
}