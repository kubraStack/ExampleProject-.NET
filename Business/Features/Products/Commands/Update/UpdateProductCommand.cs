﻿using AutoMapper;
using Business.Abstracts;
using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstracts;
using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Products.Commands.Update
{
    public class UpdateProductCommand : IRequest<UpdateProductResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }


        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
        {
            private readonly IProductRepository _productRepository;
            private readonly ICategoryService _categoryService;
            private readonly IMapper _mapper;

            public UpdateProductCommandHandler(IProductRepository productRepository, ICategoryService categoryService, IMapper mapper)
            {
                _productRepository = productRepository;
                _categoryService = categoryService;
                _mapper = mapper;
            }

            public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                Category? category = await _categoryService.GetByIdAsync(request.CategoryId);
                if (category is null)
                    throw new BusinessExeption("Böyle bir kategori bulunamadı.");

                Product? product = await _productRepository.GetAsync(p => p.Id == request.Id);
                if (product is null)
                    throw new BusinessExeption("Böyle bir veri bulunamadı");

                Product mappedProduct = _mapper.Map<Product>(request); 

                 await _productRepository.UpdateAsync(mappedProduct);

                UpdateProductResponse response = _mapper.Map<UpdateProductResponse>(mappedProduct);
                return response;
                
                //Güncelleme işlemi için veri tabanındaki instance'ı  requestten gelen bilgilerle güncellememiz gerekir.
                //Mapleyerek aynı objenin maplenmeyecek alanlarını aynı tutarak maplenecek olanları değiştirmesini sağladık.
            }
        }
    }
}
