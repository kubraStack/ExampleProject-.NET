using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface ICategoryService
    {
        Category GetByIdCategory(int id);
        List<Category> GetAllCategories();
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
