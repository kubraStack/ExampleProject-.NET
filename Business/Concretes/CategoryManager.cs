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
    public class CategoryManager : ICategoryService
    {
        ICategoryRepository _categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public void Add(Category category)
        {
            _categoryRepository.Add(category);
        }

        public void Delete(Category category)
        {
            var deletedCategory = _categoryRepository.Get(p=>p.Id == category.Id);
            if (deletedCategory != null)
            {
               _categoryRepository.Delete(deletedCategory);
            }
        }

        public Category? GetByIdCategory(int id)
        {
            return _categoryRepository.Get(p=>p.Id == id);
        }

        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetList(null);
        }

        public void Update(Category category)
        {
            var updatedCategory = _categoryRepository.Get(p=>p.Id == category.Id);
            if (updatedCategory != null)
            {
                updatedCategory.Name = category.Name;
                updatedCategory.Id = category.Id;
            }
        }
    }
}
