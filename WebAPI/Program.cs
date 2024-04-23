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

            //Ba��ml�l�klar� ekledi�imiz container

            //Sigleton-Scoped-Transient ->Lifetime(Ya�am S�resi)

            //Singleton => �retilen ba��ml�l�k uygulama a��k oldu�u s�rece tek bir kere newlenir her enjeksiyonda o instance kullan�l�r.
            //Scoped =>(API) �stek ba��na bir instance olu�turur.
            //Transient => Her ad�mda (her talepte) yeni 1 instance olu�turur.
            TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>(); //Olu�turdu�umuz model class �zerinden jwt bilgilerini al�yoruz.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddBusinessServices();
            builder.Services.AddDataAccessServices();
            builder.Services.AddCoreServices(tokenOptions);
            

            //Assembly Keyword� => Uygulaman�z�n veya k�t�phanenizin derlenmi� kodunu ve bu kodun �al��mas� i�in gereken bile�enlerin bir araya geldi�i yap�d�r.(DLL)

            //Middleware => .Net d�nyas�nda sistemimiz �al���rken Client'�n att�p� iste�e kar�� Server'�n d�nd��� cevab noktas�n� ikiye b�ler.
            //Araya girerek kendi kod blo�unu �al��t�ran.Gerekti�inde d�nmesi gereken cevab�n yerine belirli ko�ullarla cevab� farkl� bir �ekilde verebilir.

            //string securityKey =  builder.Configuration.GetSection("TokenOptions").GetValue<string>("SecurityKey"); //.net'de json'dan veri okuma

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //JWT Konfig�rasyonlar�...
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
//Cross-Cutting-Concerns => Uygulamay� dikine b�len yap�lar.Ex.Handling