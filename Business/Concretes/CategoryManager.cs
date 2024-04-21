using Business.Abstracts;
using DataAccess.Abstracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    //Desin Pattern => Mediator(Servislerin birbirileri ile iletişimini yönetmemizi sağlar..)
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            Category? category = await _categoryRepository.GetAsync(p => p.Id == id);
            return category;
        }
    }
}
//Bağımlılıklar çift taraflı birbiri içerisinde kullanılamaz. Circular Dependency oluşur.Sonsuz döngüye girmiş olur.
