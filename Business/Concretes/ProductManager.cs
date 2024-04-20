using AutoMapper;
using Azure.Core;
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
    //Eğer başka bir entity'e ihtiyaç duyuluyorsa o entity'nin servisi injection edilir.
    public class ProductManager : IProductService
    {
        private readonly IProductRepository _productRepository;
       // private readonly ICategoryService _categoryService;
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
            if (dto.UnitPrice < 0)
                throw new BusinessExeption("Ürün fiyatı 0'dan küçük olamaz !");

            //Aynı isimde 2. ürün eklenemez.
            Product? productWithSameName = await _productRepository.GetAsync(p => p.Name == dto.Name);
            if (productWithSameName is not null)
                throw new System.Exception("Aynı isimde 2. ürün eklenemez");
            //Kategori verilerine ulaş.
            //Category? category = _categoryService.GetById(request.CategoryId);
            //if (category is null)
            //    throw new BusinessExeption("Böyle bir kategori bulunamadı.");
            Product product = _mapper.Map<Product>(dto);
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
