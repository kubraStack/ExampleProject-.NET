using Business.Abstracts;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public List<Category> GetAll()
        {
            return categoryService.GetAllCategories();
        }

        [HttpPost]
        public void Add([FromBody] Category category)
        {
            //Validation, İş  Kuralları, Authentication
            //Veritabanı bağlantısı
            categoryService.Add(category);
        }
    }
}
