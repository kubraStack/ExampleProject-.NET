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

        public Task AddAsync(Category category)
        {
            // Kategori eklenirken içerisinde 3 adet ürün gönderilmelidir.

            //CategoryManager => ProductManager
            throw new NotImplementedException(); 
        }

        public Category? GetById(int id)
        {
            return _categoryRepository.Get(p => p.Id == id);
        }
    }
}
//Bağımlılıklar çift taraflı birbiri içerisinde kullanılamaz. Circular Dependency oluşur.Sonsuz döngüye girmiş olur.
