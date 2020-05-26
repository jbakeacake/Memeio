using Memeio.API.Dtos;
using Memeio.API.Models;
using AutoMapper;

namespace Memeio.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            char[] delimiter = new char[]{';'};
            CreateMap<User, UserForProfileDto>()
                .ForMember(dest => dest.User_Comments_List, opt => opt.MapFrom(src => src.User_Comments_Serialized.Split(delimiter)));
            CreateMap<UserForProfileDto, User>()
                .ForMember(dest => dest.User_Comments_Serialized, opt => opt.MapFrom(src => string.Join(";", src)));

            CreateMap<Photo, PhotosForGalleryDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.User.Username));
        }
    }
}