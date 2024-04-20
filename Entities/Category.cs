using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Category : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } //Bir kategorinin birden fazla ürünü olabileceği için(many to many) ICollection olarak verdik

        public Category(int ıd, string name)
        {
            Id = ıd;
            Name = name;
        }

        public Category()
        {
        }
    }
}
//ICollection yaparak gelecek olan veriyi list olarak da INumerable olarak da gelebileceğini belirtmiş oluyoruz.