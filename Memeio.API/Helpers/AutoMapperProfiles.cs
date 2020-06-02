using Memeio.API.Dtos;
using Memeio.API.Models;
using AutoMapper;

namespace Memeio.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Photo, PhotoForGalleryDto>()
                .ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.AuthorId,
                opt => opt.MapFrom(src => src.User.Id));
            CreateMap<Photo, PhotosForProfileDto>()
                .ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.AuthorId,
                opt => opt.MapFrom(src => src.User.Id));
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<PhotoForGalleryDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>();
            CreateMap<User, UserForSearchDto>();
            CreateMap<User, UserForProfileDto>();
            CreateMap<CommentForProfile, CommentForProfileToReturnDto>();
            CreateMap<CommentForProfile, CommentForProfileDto>();
            CreateMap<CommentForProfileDto, CommentForProfile>();
        }
    }
}