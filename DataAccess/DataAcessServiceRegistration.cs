using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class DataAcessServiceRegistration
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryRepository, EfCategoryRepository>();
            services.AddScoped<IProductRepository, EfProductRepository>();
            services.AddDbContext<BaseDbContext>();
            return services;
        }
    }
}
