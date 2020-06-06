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
                opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.AuthorPhotoUrl,
                opt => opt.MapFrom(src => src.User.PhotoUrl))
                .ForMember(dest => dest.DateCreated,
                opt => opt.MapFrom(src => src.DatePosted.DetermineDate()));
            CreateMap<Photo, PhotosForProfileDto>()
                .ForMember(dest => dest.Author,
                opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.AuthorId,
                opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.DateCreated,
                opt => opt.MapFrom(src => src.DatePosted.DetermineDate()));
            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<PhotoForGalleryDto, Photo>();
            CreateMap<Photo, PhotoForReturnDto>()
                .ForMember(dest => dest.AuthorId,
                opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DateCreated,
                opt => opt.MapFrom(src => src.DatePosted.DetermineDate()));
            CreateMap<ArchivedPhotoForCreationDto, ArchivedPhoto>();
            CreateMap<ArchivedPhoto, ArchivedPhotoForCreationDto>();
            CreateMap<User, UserForSearchDto>();
            CreateMap<User, UserForProfileDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<CommentForProfile, CommentForProfileToReturnDto>();
            CreateMap<CommentForProfile, CommentForProfileDto>();
            CreateMap<CommentForProfileDto, CommentForProfile>();
        }
    }
}