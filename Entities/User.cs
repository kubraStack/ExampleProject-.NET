using Core.DataAccess;


namespace Entities
{
    public class User : Entity
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; } //byte[] vermemizin sebebi sorgu atılmasını engellemek
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
// Plain Text => Şifrelerin veri tabanında olduğu gibi kullanılması anlamına gelir.