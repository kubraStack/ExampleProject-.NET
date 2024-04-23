using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class BaseUser : Entity
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; } //byte[] vermemizin sebebi sorgu atılmasını engellemek
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
