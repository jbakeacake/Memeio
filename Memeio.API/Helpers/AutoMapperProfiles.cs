using Memeio.API.Dtos;
using Memeio.API.Models;
using AutoMapper;

namespace Memeio.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Photo, PhotosForGalleryDto>()
                .ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.User.Username));
            CreateMap<User, UserForSearchDto>();
        }
    }
}