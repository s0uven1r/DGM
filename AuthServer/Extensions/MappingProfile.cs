using AuthServer.Entities;
using AuthServer.Models.Users;
using AutoMapper;

namespace AuthServer.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserKYCRequestModel, UserKYC>().ReverseMap();
            CreateMap<UserKYC, UserKYCResponseModel>();
        }
    }
}
