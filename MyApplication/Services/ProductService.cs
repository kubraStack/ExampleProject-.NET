using MyApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Services
{
    public class ProductService : BaseProductService
    {
        //Servisler genelde metot tanımlarını yapar.
        public override void AddProduct(Product product)
        {
            Console.WriteLine("Ürün MSSQL'de veritabanına eklendi.");
        }

        public override void DeleteProduct(Product product)
        {
            Console.WriteLine("Ürün MSSQL veritabanından silindi.");
        }

        public override void UpdateProduct(Product product)
        {
            Console.WriteLine("Ürün MSSQL'de güncellendi.");
        }
    }
}
