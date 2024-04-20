using Business.Abstracts;
using Business.Concretes;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductManager>();              //Servisleri ilk başta singleton olarak ekledik ancak basedbcontext servisi sürekli var olduğu için hata verdi bu yüzden scoped olarak değiştirdik.
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());           //Eşleşmesi gereken tüm bileşenleri tara ve eşleştirir.

            return services;
        }
    }
}
