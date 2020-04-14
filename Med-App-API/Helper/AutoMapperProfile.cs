using AutoMapper;
using Med_App_API.Dto;
using Med_App_API.Models;

namespace Med_App_API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User,UserForListDto>();
            CreateMap<User,UserForDetailedDto>();
            CreateMap<UserForRegisterDto,User>();
        }       
    }
}