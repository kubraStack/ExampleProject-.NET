using AutoMapper;
using Business.Features.Products.Commands.Create;
using Entities;


namespace Business.Features.Products.Profiles
{
    public class ProductMappingProfiles : Profile
    {
        public ProductMappingProfiles()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            //ForMember(i=>i.UnitPrice, opt => opt.MapFrom(dto => dto.Price));

        }
    }
}
//Eğer mapping yapmak istediğimiz yerlerin alan adları farklı ise mappin yapmadan önce configürasyonu mapping profilde yapmalıyz.
