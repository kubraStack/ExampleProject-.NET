using Core.DataAccess;



namespace Core.Entities
{
    public class UserOperationClaim : Entity
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }

      
    }
}
//Virtual alanları Entity Framework otomatik tablo olarak oluşturduğu için sildik.