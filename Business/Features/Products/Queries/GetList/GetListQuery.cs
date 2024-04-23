using AutoMapper;
using Core.Application.Pipelines.Authorization;
using DataAccess.Abstracts;
using Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Features.Products.Queries.GetList
{
    public class GetListQuery :IRequest<List<GetAllProductResponse>>, ISecuredRequest
    {          //Komut(Sorgu)   //Mediator Request <Geri Dönüş Tipi DTO>

        //Requestin İstediği Alanlar
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string[] RequiredRoles => ["Product.Add","Product.Read"];

        //Request'i handler edecek class                 <Komut(Sorgu), Dönüş Tipi>  
        public class GetListQueryHandler : IRequestHandler<GetListQuery, List<GetAllProductResponse>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetListQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            //Dependencyler,
            //Handle Metodu
            public async Task<List<GetAllProductResponse>> Handle(GetListQuery request, CancellationToken cancellationToken)
            {
                List<Product> products = await _productRepository.GetListAsync();
                List<GetAllProductResponse> response = _mapper.Map<List<GetAllProductResponse>>(products);
                return response;
            }
        }
    }
}
//Queriler geri dönüş tipi istediği için IRequest'in içine geri dönüşü belirten dto yapılır.
//N-Tier Arch
//Clean Arch