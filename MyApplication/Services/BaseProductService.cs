using MyApplication.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Services
{
    public abstract class BaseProductService : IProductService
    {
        //Abstract class ile ister soyut istersek somut tanımlar yapabiliriz.Yarı soyut olmasının sebebi hem soyut hem somutları kullanabilmemiz.Abstract class'ı ezmek zorundayız.
        //Virtual  anahtar kelimesi, C# programlama dilinde bir sınıfın veya sınıf üyesinin (metod, özellik, indexer veya olay) miras alındığında yeniden tanımlanabilir olmasını sağlar. virtual anahtar kelimesiyle işaretlenen üye, miras alındığında override anahtar kelimesiyle yeniden tanımlanabilir. Bu, OOP'nin (Nesne Yönelimli Programlama) bir parçası olan polymorphism (çok biçimlilik) kavramını destekler
        public virtual void AddProductWithLoggging(Product product)
        {
            Console.WriteLine("Loglama işlemi yapıldı");
            AddProduct(product);
        }
        public abstract void AddProduct(Product product);

        public abstract void DeleteProduct(Product product);


        public abstract void UpdateProduct(Product product);
       
    }
}
