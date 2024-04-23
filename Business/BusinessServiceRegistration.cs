using Business.Abstracts;
using Business.Concretes;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Business
{
    public static class BusinessServiceRegistration
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddMediatR(config => {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //Sıralama Önemli
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
                config.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
                config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // "," yaparak kıstaslara uyan tüm tipler olarak belirlemiş olduk.
               
            });
            services.AddScoped<IProductService, ProductManager>();              //Servisleri ilk başta singleton olarak ekledik ancak basedbcontext servisi sürekli var olduğu için hata verdi bu yüzden scoped olarak değiştirdik.
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());           //Eşleşmesi gereken tüm bileşenleri tara ve eşleştirir.
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
           
            
            return services;
        }
    }
}
