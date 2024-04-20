using Business.Abstracts;
using Business.Dtos.Product;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    
    [Route("api/[controller]")] 
    [ApiController] 


    public class ProductsController : ControllerBase
    {
        IProductService _productService;


        public ProductsController(IProductService productService)
        {
            
            _productService = productService;
        }

        [HttpGet]
        public async Task<List<ProductForListingDto>> GetAll()
        {
            return await _productService.GetAll();
        }

        [HttpPost]
        public async Task Add([FromBody] ProductForAddDto dto)
        {
            //Validation, İş  Kuralları, Authentication
            //Veritabanı bağlantısı
            await _productService.Add(dto);
        }

        [HttpGet("Senkron")]
        public string Sync()
        {
            Thread.Sleep(5000); // Thread => Çalışan işlemci parçacığını 5 saniye uyutuyoruz.
            return "Senkron endpiont";
        }

        [HttpGet("Asenkron")]
        public async Task<string> Asenkron() 
        {
            await  Task.Delay(5000); //Eğer bir olayı bekletmek istersek await yazıyoruz.
            return  "Asenkron endpoint";
        }
        
    }
}

//Senkron => Bir methodun çalışma anında bir satırı çalıştırırken o satırı bitirmeden alt satıra geçmemesine senktron işlem denir
//Asenkron => Bir methodun çalışma anında bir satırı çalıştırırken o satırı bitirmeden alt satıra geçebilmesine ve işlemi bloklamayan yapılara asenkron denir.
//Asenkron işlemler paralel olarak çalışabilir . Ancak senkron çalışamaz tüm threadleri bloklar.


//Task<T> tipi asenkron programalama modelinde en sık kullanılan dönüş tipidir.
// TASK KULLANILMA SEBEBİ:
// Asenkron Operasyonları temsil eder
// Hata yönetimini kolaylaştırır.
// Süreç İlerlemesi
// Birleştirme ve Sıralama => Birden fazla asenkron operasyonu birleştirmek veya sıralamak için Task ve Task<T> tipleri kullanılır. Örneğin, Task.WhenAll veya Task.WhenAny gibi metodlarla birden fazla asenkron işlemi koordine edebilirsiniz.