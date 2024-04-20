using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleUI
{
    //Erişim Belirteci 
    //public => Tüm dış dünyaya açık
    //private => ilgili nesneye özel
    public class Product //Genel tanımların olduğu alanlara class deriz.
    {
        //Field(Alanlar)
        public int Id { get; set; }
        public string Name { get; set; }

        public Product(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }
        public Product()
        {
            
        }

        //Constructor -> Nesnenin içinde tanımlanabilen nesne ismi ile aynı oluşturulan metotdur.
        //Metotdan tek farkı geri dönüş tipi yoktur.
        //Nesnenin oluşturulacağı zaman çağırılan metotdur.
        //İgili class hiçbir constructor içermiyorsa boş parametreli ctor default olarak eklenir.
        //Ctor da overload edebiliriz.Yani bir metot veya imzanın birden fazla imzası olabilir.Bu durumda, hangi versiyonun çağrılacağını belirlemek için kullanılır.
        //Ctor'ları genellikle propertileri nesneyi oluşturma anında set etmek için kullanılırız.

        //Ctor'ları classlarda fiel'ları set etmek için kullanılırız.
        //Managerlarda ctor' Bağımlılıkların Yönetimi: Constructor injection, bir sınıfın diğer sınıflara veya servislere olan bağımlılıklarını enjekte etmek için kullanılır. Bu, bağımlılıkların daha esnek ve test edilebilir olmasını sağlar.

        //Genelde her class'da All Args Ctor ve No Args Ctor kullanılır
        //All Args Ctor => Tüm argümanlarla çalışacak ctor
        //No Args Ctor => Argümansız çalışacak ctor.
    }
}
