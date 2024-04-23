using DataAccess.Concretes.EntityFramework;
using Core.CrossCuttingConcerns.Exceptions.Extentions;
using Business;
using DataAccess;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //Baðýmlýlýklarý eklediðimiz container

            //Sigleton-Scoped-Transient ->Lifetime(Yaþam Süresi)

            //Singleton => Üretilen baðýmlýlýk uygulama açýk olduðu sürece tek bir kere newlenir her enjeksiyonda o instance kullanýlýr.
            //Scoped =>(API) Ýstek baþýna bir instance oluþturur.
            //Transient => Her adýmda (her talepte) yeni 1 instance oluþturur.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

          
            builder.Services.AddBusinessServices();
            builder.Services.AddDataAccessServices();

            //Assembly Keywordü => Uygulamanýzýn veya kütüphanenizin derlenmiþ kodunu ve bu kodun çalýþmasý için gereken bileþenlerin bir araya geldiði yapýdýr.(DLL)

            //Middleware => .Net dünyasýnda sistemimiz çalýþýrken Client'ýn attýpý isteðe karþý Server'ýn döndüðü cevab noktasýný ikiye böler.
            //Araya girerek kendi kod bloðunu çalýþtýran.Gerektiðinde dönmesi gereken cevabýn yerine belirli koþullarla cevabý farklý bir þekilde verebilir.


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ConfigureExceptionMiddlewareExtensions();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();           
            app.Run();
        }
    }
}

//Data Access - Entity Framework
//Cross-Cutting-Concerns => Uygulamayý dikine bölen yapýlar.Ex.Handling