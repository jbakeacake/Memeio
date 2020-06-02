using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Memeio.API.Data;
using Memeio.API.Dtos;
using Memeio.API.Models;
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
        public async Task<IActionResult> UpdateLikes(int photoId, [FromBody]PhotoForGalleryDto photoForGalleryDto)
        {
            //Determine if photo exists:
            // Get photo from repo
            var photoFromRepo = await _repo.GetPhoto(photoId);

            if(photoFromRepo == null)
                return BadRequest("Photo doesn't exist");

            //Update the number of likes: 
            photoFromRepo.Likes += 1;

            var photo = _mapper.Map<Photo>(photoForGalleryDto);
            if(await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetRating", new { photoId = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not update likes");
        }
                [HttpPut("{photoId}/addDislike")]
        public async Task<IActionResult> UpdateDislikes(int photoId, [FromBody]PhotoForGalleryDto photoForGalleryDto)
        {
            //Determine if photo exists:
            // Get photo from repo
            var photoFromRepo = await _repo.GetPhoto(photoId);

            if(photoFromRepo == null)
                return BadRequest("Photo doesn't exist");

            //Update the number of likes: 
            photoFromRepo.Dislikes += 1;

            var photo = _mapper.Map<Photo>(photoForGalleryDto);
            if(await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetRating", new { photoId = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not update likes");
        }
    }
}