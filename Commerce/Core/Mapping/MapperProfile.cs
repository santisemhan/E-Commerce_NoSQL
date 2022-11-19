using AutoMapper;
using Commerce.Core.DataTransferObjects.Request;
using Commerce.Core.Models;

namespace Commerce.Core.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserRequestDTO>().ReverseMap();
            CreateMap<Product, ProductRequestDTO>().ReverseMap();
        }
    }
}
