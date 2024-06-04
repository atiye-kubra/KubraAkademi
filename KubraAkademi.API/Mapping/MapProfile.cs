using AutoMapper;
using KubraAkademi.API.Dtos;
using KubraAkademi.API.Models;

namespace KubraAkademi.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<AppUser, UserDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Document, DocumentDto>().ReverseMap();
            
        }
    }
}