using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //Generic yapılara kısıtlayıcı(Constraint) verebiliriz.
    public class EfRepositoryBase<TEntity, TContext> : IRepository<TEntity>, IAsyncRepository<TEntity>
        where TContext : DbContext
        where TEntity : Entity
    {
        private readonly TContext Context;

        public EfRepositoryBase(TContext context)
        {
            Context = context;
        }

        public void Add(TEntity entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            Context.Remove(entity); 
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        //Filtreleme
        public List<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> data = Context.Set<TEntity>();
            if (predicate != null)
            {
                data = data.Where(predicate);

            }
            return data.ToList();

        }


        public TEntity? Get(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> data = Context.Set<TEntity>();
            
            return data.FirstOrDefault(predicate);
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> data = Context.Set<TEntity>();

            return await data.FirstOrDefaultAsync(predicate);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> data = Context.Set<TEntity>();
            if (predicate != null)
            {
                data = data.Where(predicate);

            }
            return await data.ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            //Silme ve Güncelleme de işlem öncesinde veritabanına istek atılmadığı için asyn olmasına gerek yok kaydederken async yapılır.
             Context.Update(entity);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }


        //Sıralama
        //public List<TEntity> GetList(Expression<Func<TEntity, bool>>? predicate = null, Expression<Func<TEntity, object>>? orderBy = null)
        //{
        //   IQueryable<TEntity> data = Context.Set<TEntity>();

        //    if (predicate != null)
        //    {
        //        data = data.Where(predicate);
        //    }

        //    if (orderBy != null)
        //    {
        //        data = data.OrderBy(orderBy);
        //    }

        //    return data.ToList();
        //}
    }

    //IQueryable tipinde ise belirtilen sorgular direk olarak server üzerinde çalıştırılır ve dönüş sağlar.
    //Ayrıca bu tip IEnumerable tipini implement ettiği için IEnumerable’ın tüm özelliklerini kullanabilir.
    //IQueryable verdiğimiz sorguyu çalıştırır buna uygun olanları database'den belleğe çeker.IEnumerable ise bütün verileri db'den belleğe çekip sonra sorgusunu gerçekleştirir.
}

//public List<TEntity> GetAll()
//{
//    //EntityFramework den gelen Entry metodu içine verilen veri tipindekiler üzerinde işlem yapılacağını belirtir.
//    //Context.Entry<TEntity>()

//    //Set<> İle veri setini belirtmiş oluyoruz.
//   return Context.Set<TEntity>().ToList();
//}