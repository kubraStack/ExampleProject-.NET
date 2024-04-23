using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    //Operasyon Yetkisi
    public class OperationClaim : Entity
    {
        public string Name { get; set; } // Product.Add, Product.Update, Product.Delete, Product.Read, Product.Write gelecek

    }
}
