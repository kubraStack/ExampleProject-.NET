using AutoMapper;
using Business.Abstracts;
using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.Exceptions.Types;
using DataAccess.Abstracts;
using Entities;
using FluentValidation;
using FluentValidation.Results;
using MediatR;


namespace Business.Features.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<CreateProductResponse>, ILoggableRequest
    {
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Stock { get; set; }
        public int CategoryId { get; set; }

        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
        {
            private readonly IProductRepository _productRepository;
            private readonly ICategoryService _categoryService; 
            private readonly IMapper _mapper;
            

            public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ICategoryService categoryService)
            {
                _productRepository = productRepository;
                _mapper = mapper;
                _categoryService = categoryService;
            }

            public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                IValidator<CreateProductCommand> validator = new CreatProductCommandValidator();
               // validator.ValidateAndThrow(request); // Kendi ex. fırlatacak.
                ValidationResult result =  validator.Validate(request); //Validation'ı yapıcak sonucu verecek. Exception'ı ben fırlatacağım
                if (!result.IsValid)
                    throw new Core.CrossCuttingConcerns.Exceptions.Types.ValidationException(result.Errors.Select<ValidationFailure, string>(i=> i.ErrorMessage).ToList());

                Product? productWithSameName = await _productRepository.GetAsync(p => p.Name == request.Name);
                if (productWithSameName is not null)
                    throw new System.Exception("Aynı isimde 2. ürün eklenemez");
                Category? category = await _categoryService.GetByIdAsync(request.CategoryId);
                if (category is null)
                    throw new BusinessExeption("Böyle bir kategori bulunamadı.");

                Product product = _mapper.Map<Product>(request);
                await _productRepository.AddAsync(product);

                CreateProductResponse response = _mapper.Map<CreateProductResponse>(product);
                return response;
            }

        }
    }
}

//Pipeline => MediatR ile Komutlar arasına logicler yerleştirilir ve bu iletişim bunun üzerinden devam eder.
//ValidationBehavior => AuthBehavior => LogBehavior
