using Core.CrossCuttingConcerns.Exceptions.Extentions;
using Business;
using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Core.Utilities.JWT;
using Core.Utilities.Encryption;
using Core;

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
            TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //Oluþturduðumuz model class üzerinden jwt bilgilerini alýyoruz.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddBusinessServices();
            builder.Services.AddDataAccessServices();
            builder.Services.AddCoreServices(tokenOptions);
            

            //Assembly Keywordü => Uygulamanýzýn veya kütüphanenizin derlenmiþ kodunu ve bu kodun çalýþmasý için gereken bileþenlerin bir araya geldiði yapýdýr.(DLL)

            //Middleware => .Net dünyasýnda sistemimiz çalýþýrken Client'ýn attýpý isteðe karþý Server'ýn döndüðü cevab noktasýný ikiye böler.
            //Araya girerek kendi kod bloðunu çalýþtýran.Gerektiðinde dönmesi gereken cevabýn yerine belirli koþullarla cevabý farklý bir þekilde verebilir.

            //string securityKey =  builder.Configuration.GetSection("TokenOptions").GetValue<string>("SecurityKey"); //.net'de json'dan veri okuma

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //JWT Konfigürasyonlarý...
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "com.tobeto",
                        ValidAudience = "tobeto.studens",
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

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