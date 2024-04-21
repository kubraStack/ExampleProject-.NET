using Business.Abstracts;
using Business.Features.Products.Commands.Create;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")] 
    [ApiController] 


    public class ProductsController : ControllerBase
    {
        
        private readonly IMediator _mediator;

        public ProductsController( IMediator mediator)
        {

            
            _mediator = mediator;
        }

        [HttpPost]
        public async Task Add([FromBody] CreateProductCommand command )
        {
            //Validation, İş  Kuralları, Authentication
            //Veritabanı bağlantısı
            await _mediator.Send(command);
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

//CQRS => Command Query Responsibility Segregation => Sorguların  ve Komutların Sorumluluklarının Ayrılması
//CQRS bir servisde yazılan metotları ayrı ayrı yazmamızı ister.Çünkü başka bir servisde kullanılacağı zaman tüm servisi injection etmeye gerek kalmaz.
//Servislerde gereksiz injection yapmaya Dependency Injection Hell denir.

//Commandlar genellikle veriyi manipüle eden sorgular.
//Queriler veriyi sorgulayan yapılardır.