using Business.Features.Products.Commands.Create;
using Business.Features.Products.Commands.Delete;
using Business.Features.Products.Commands.Update;
using Business.Features.Products.Queries.GetById;
using Business.Features.Products.Queries.GetList;
using MediatR;
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
        public async Task<IActionResult> Add([FromBody] CreateProductCommand command )
        {
            //Validation, İş  Kuralları, Authentication
            //Veritabanı bağlantısı
            await _mediator.Send(command);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetListQuery query) //Task<IActionResult> => Yazarak metodun dönüş türünün herhangi bir türde olabileceğini belirttik.
        {
            var result =  await _mediator.Send(query);
            return Ok(result); //Http status içine result gövdesini yazdırır.
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) //FromRoute kullandığımız için veriyi alıp içeride atamasını yaparız.
        {
            GetByIdQuery query = new() { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteProductCommand command = new() { Id = id };
            await _mediator.Send(command);  
            return Ok(); //Refactor
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
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