using MyApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Services
{
    public class ProductServiceMysql : BaseProductService
    {
        //ovveride olarak belirtip BaseProductService'den gelen metotların ezdiğimizi belirtiyoruz.
        //BaseProductService abstract class olduğu için sadece içindeki soyut metotları inherit ediyor.
        public override void AddProductWithLoggging(Product product)
        {
            Console.WriteLine("Logging ezildi.");
        }
        public override void AddProduct(Product product)
        {
            Console.WriteLine("Ürün MySql'de veritabanına eklendi.");
        }

        public override void DeleteProduct(Product product)
        {
            Console.WriteLine("Ürün MySql veritabanından silindi.");
        }

        public override void UpdateProduct(Product product)
        {
            Console.WriteLine("Ürün MySql'de güncellendi.");
        }
    }
}
