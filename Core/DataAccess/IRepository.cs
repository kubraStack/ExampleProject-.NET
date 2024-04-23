using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    public interface IRepository<T>
    {
        T? Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        List<T> GetList(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        //List<T> GetList(Expression<Func<T, bool>>? predicate = null, Expression<Func<T, object>>? orderBy = null);
        //Fonksiyon içinde koşul yazabilmemiz için Expression func kullandık. Burada T gelecek olan entity'i bool ise function dan dönecek değer belirtiyor.
        //Predicate ise bunun bir filtre olduğunu belirtmiş oluruz.
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
