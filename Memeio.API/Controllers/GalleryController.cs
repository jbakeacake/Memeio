using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Memeio.API.Data;
using Memeio.API.Dtos;
using Memeio.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Memeio.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GalleryController : ControllerBase
    {
        private readonly IMemeioRepository _repo;
        private readonly IMapper _mapper;
        public GalleryController(IMemeioRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotoSet()
        {
            var photosFromRepo = await _repo.GetPhotoSet();
            var photosToReturn = _mapper.Map<IEnumerable<PhotoForReturnDto>>(photosFromRepo);

            return Ok(photosToReturn);
        }

        [HttpGet("{photoId}", Name="GetRating")]
        public async Task<IActionResult> GetLikes(int photoId)
        {
            var photoFromRepo = await _repo.GetPhoto(photoId);
            var photo = _mapper.Map<PhotoForGalleryDto>(photoFromRepo);
            return Ok(photo);
        }

        [HttpPut("{photoId}/addLike")]
        public async Task<IActionResult> UpdateLikes(int photoId, PhotoForGalleryDto photoForGalleryDto)
        {
            //Determine if photo exists:
            // Get photo from repo
            var photoFromRepo = await _repo.GetPhoto(photoId);

            if(photoFromRepo == null)
                return BadRequest("Photo doesn't exist");

            //Update the number of likes: 
            photoForGalleryDto.Likes += 1;
            _mapper.Map(photoForGalleryDto, photoFromRepo);

            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating photo {photoId} failed on save");
        }
        [HttpPut("{photoId}/addDislike")]
        public async Task<IActionResult> UpdateDislikes(int photoId, PhotoForGalleryDto photoForGalleryDto)
        {
            //Determine if photo exists:
            // Get photo from repo
            var photoFromRepo = await _repo.GetPhoto(photoId);

            if(photoFromRepo == null)
                return BadRequest("Photo doesn't exist");

            //Update the number of likes: 
            photoForGalleryDto.Dislikes += 1;
            _mapper.Map(photoForGalleryDto, photoFromRepo);

            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            throw new Exception($"Updating photo {photoId} failed on save");
        }

        [HttpPost("{id}/addComment")]
        public async Task<IActionResult> AddPhotoComment(int id, CommentForPostDto commentForPostDto)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            if(photoFromRepo == null)
                return BadRequest("Photo doesn't exist");
            commentForPostDto.Author = User.FindFirst(ClaimTypes.Name).Value;
            commentForPostDto.AuthorId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            commentForPostDto.PostId = id;
            var comment = _mapper.Map<CommentForPost>(commentForPostDto);
            photoFromRepo.Comments.Add(comment);

            if(await _repo.SaveAll())
                return NoContent();
            
            return BadRequest("Could not add comment");
        }
    }
}