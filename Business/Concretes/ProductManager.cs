using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Product.Requests;
using Business.Dtos.Product.Responses;
using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes
{
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductManager(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        //DTO => Data Transfer Object / Dto'ların tanımlanması business katmanında olur
        //Request-Response Pattern => Her istek ve her cevap farklı nesneye sahip olmalıdır.
        public async Task Add(AddProductRequest dto)
        {
            //ürün ismini kontrol et
            //fiyatını kontrol et

            if (dto.UnitPrice < 0)  
               throw new BusinessExeption("Ürün fiyatı 0'dan küçük olamaz !");

            //Aynı isimde 2. ürün eklenemez.
            Product? productWithSameName = await _productRepository.GetAsync(p=>p.Name == dto.Name);
            if (productWithSameName is not null)
            {
                throw new System.Exception("Aynı isimde 2. ürün eklenemez");
            }


            //Async işlemler
            //Global Ex. Handling => Global Hata Yönetimi
            //İş Kuralları, Validation => Daha temiz yazarız?
            //Pipeline Mediator pattern ??

            //Manuel Mapping(Eşleme)
            //Auto Mapping
            //Product product = new();
            //product.Name = dto.Name;    
            //product.UnitPrice = dto.UnitPrice;
            //product.Stock = dto.Stock;
            //product.CategoryId = dto.CategoryId;
            //product.CreatedDate = DateTime.Now;

            Product product = _mapper.Map<Product>(dto); //Mapper ile maplemek istediğimiz tür ve verilerin transfer edileceği kaynağı belirterek otomatik mapleme yapıldı. 

            await _productRepository.AddAsync(product);
        }

        public void Delete(int id)
        {
            Product? productTodelete = _productRepository.Get(p=>p.Id == id);
            if (productTodelete != null)
            {
                _productRepository.Delete(productTodelete);
            }
        }     

        public async Task<List<ListProductResponse>> GetAll()
        {
            //Cacheleme ?
            List<Product> products = await _productRepository.GetListAsync();

            //List<ProductForListingDto> response = new List<ProductForListingDto>();

            //foreach (Product product in products)
            //{
            //    ProductForListingDto dto = new();
            //    dto.Name = product.Name;
            //    dto.UnitPrice = product.UnitPrice;
            //    dto.Id = product.Id;
            //    response.Add(dto);
            //}

            //Select kullanarak modeli belirlemiş olduk.(Manuel Mapping)
            //List<ProductForListingDto> response = products.Select(p => new ProductForListingDto()
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    UnitPrice = p.UnitPrice,
            //}).ToList();

            List<ListProductResponse> response = _mapper.Map<List<ListProductResponse>>(products);
            return response;
        }

        public Product? GetById(int id)
        {
            return _productRepository.Get(p=>p.Id == id);
        }

        

        //public List<Product> GetList(Expression<Func<Product, bool>>? predicate = null, Expression<Func<Product, object>>? orderBy = null)
        //{
        //    var filteredList = _productRepository.GetList(p => p.Stock >0, p=> p.UnitPrice);
        //    return filteredList;
        //}

        public void Update(Product product)
        {
            _productRepository.Update(product);

        }

        
    }
}
