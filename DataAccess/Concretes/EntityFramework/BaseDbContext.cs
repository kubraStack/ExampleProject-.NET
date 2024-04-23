using Core.Entities;
using Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Concretes.EntityFramework
{
    //Veritabanını temsil eden dosya
    public class BaseDbContext : DbContext //DbContext'den inherit ederek bu class'ın EntityFramework tarafından bir veritabanı gibi tanınmasını sağlıyoruz
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
        


        //OnConfiguring metodunun temel amacı, veritabanı bağlantısı için gerekli olan ayarların tanımlandığı yerdir.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=KUBRA;" + // SQLInstanceName'i kendi SQL Server isminizle değiştirin
                "Initial Catalog=TobetoDb;" + // Initial Catalog, Database'nin diğer bir adıdır
                "Integrated Security=True;" + // Windows kimlik doğrulamasını aktifleştir
                "Encrypt=False;" + // Şiflemeyi devre dışı bırak
                "TrustServerCertificate=True;" // Sunucu sertifikasına güven
            );
            base.OnConfiguring(optionsBuilder);
        }

        //OnModelCreating => veritabanı modelinin (veritabanı şemasının) nasıl oluşturulacağını ve yapılandırılacağını belirtir. Bu metod genellikle veritabanı tablolarının, ilişkilerin ve diğer veritabanı yapılarının nasıl oluşturulacağını ayarlamak için kullanılır.
        //Genellikle OnModelCreating işlemlerini migration eklemeden yapmak gerekir.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Product>().ToTable("ProductTable"); //Var olan tablonun adını değiştirebiliriz.
            //modelBuilder.Entity<Product>().HasOne(i=>i.Category); //Her product'ın bir categorisi olduğunu belirttik.
            modelBuilder.Entity<Product>().Property(i => i.Name).HasColumnName("Name").HasMaxLength(50); //Alanların özelliğini değiştirebiliriz.
            modelBuilder.Entity<User>().ToTable("Users");
            //modelBuilder.Entity<UserOperationClaim>().HasMany(i=>i.)
            base.OnModelCreating(modelBuilder);



            //Entity Framework Seed Data => CodeFirst'de çok kullanılır.Veritabanı oluştururken kullanabileceğimiz test verilerini otomatik eklemek.
            //Seed Data genellikle development alanındaki veritabanları için kullanılır.

            Category category1 = new Category(1, "Giyim");
            Category category2 = new Category(2, "Elektronik");
            Category category3 = new Category(3, "Ev Eşyaları");

            Product product1 = new Product(1, "Etek", 200, 5 ,1);
            Product product2 = new Product(2, "Laptop", 25000, 7, 2);
            Product product3 = new Product(3, "Blender", 7000, 8, 3);

            modelBuilder.Entity<Category>().HasData(category1, category2, category3);
            modelBuilder.Entity<Product>().HasData(product1, product2, product3);
        }

    }
}
//Db First (Database First)=> Bu yaklaşımla var olan veritabanı şemasını kullanarak model sınıflarını oluşturabiliriz.
//Code First: Bu yaklaşım ile veritabanınızı ORM kodunuzu yazarak oluşturabilirsiniz. Sınıflarınızı oluşturduktan sonra, Entity Framework veritabanı şemasını otomatik olarak oluşturur.