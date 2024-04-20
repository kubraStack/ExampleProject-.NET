using AutoMapper;
using Business.Dtos.Product.Requests;
using Business.Dtos.Product.Responses;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MappingProfiles
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles()
        {
            CreateMap<Product, AddProductRequest>().ReverseMap();
            //ForMember(i=>i.UnitPrice, opt => opt.MapFrom(dto => dto.Price));
            CreateMap<Product, ListProductResponse>().ReverseMap();
        }
    }
}
//Eğer mapping yapmak istediğimiz yerlerin alan adları farklı ise mappin yapmadan önce configürasyonu mapping profilde yapmalıyz.
