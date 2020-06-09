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
            CreateMap<ArchivedPhoto, Photo>();
            CreateMap<ArchivedPhoto, PhotosForProfileDto>();
            CreateMap<Photo, ArchivedPhoto>()
                .ForMember(dest => dest.DateCreated,
                opt => opt.MapFrom(src => src.DatePosted.DetermineDate()));
            CreateMap<ArchivedPhotoForProfileDto, ArchivedPhoto>()
                .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.AuthorId));
            CreateMap<ArchivedPhotoForCreationDto, ArchivedPhoto>()
                .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.AuthorId));
            CreateMap<User, UserForSearchDto>();
            CreateMap<User, UserForProfileDto>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<CommentForProfile, CommentForProfileToReturnDto>();
            CreateMap<CommentForProfile, CommentForProfileDto>();
            CreateMap<CommentForProfileDto, CommentForProfile>();
            CreateMap<CommentForPostDto, CommentForPost>();
        }
    }
}