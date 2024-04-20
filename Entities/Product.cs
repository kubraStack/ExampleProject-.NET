using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public Product(int ıd, string name, double unitPrice, int stock, int categoryId)
        {
            Id = ıd;
            Name = name;
            UnitPrice = unitPrice;
            Stock = stock;
            CategoryId = categoryId;
        }

        public Product()
        {

        }
    }
}
//ORM'de ilişkileri temsil ederken ilişkili olduğunuz nesneyi direk ekleriz..Ancak virtual(sanal) olarak ekleriz.
//One to one ilişkilerde tekil olarak ekleriz
//Many to many ilişkilerde list ve collection ile ekleriz.
