using AutoMapper;
using Entities;
using Business.Features.Auth.Command.Register;


namespace Business.Features.Auth.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
        }
    }
}
