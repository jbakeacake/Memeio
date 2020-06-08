using System.Linq;
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
    [Authorize]
    [Route("api/v1/[controller]/{userId}")]
    [ApiController]
    public class ArchiveController : ControllerBase
    {
        private readonly IMemeioRepository _repo;
        private readonly IMapper _mapper;

        public ArchiveController(IMemeioRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArchivedPhoto(int id)
        {
            var archivedPhotoFromRepo = await _repo.GetArchivedPhoto(id);
            var archivedPhotoForReturn = _mapper.Map<ArchivedPhotoForReturnDto>(archivedPhotoFromRepo);

            return Ok(archivedPhotoForReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetArchivedPhotos(int userId)
        {
            var archivedPhotosFromRepo = await _repo.GetArchivedPhotos(userId);
            var archivedPhotosForReturn = _mapper.Map<ArchivedPhotoForReturnDto>(archivedPhotosFromRepo);

            return Ok(archivedPhotosForReturn);
        }

        [HttpPost]
        public async Task<IActionResult> AddToUserArchive(int userId, ArchivedPhotoForCreationDto archivedPhotoForProfileDto)
        {
            //Determine if the user adding the photo is authorized:
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) //access the current user's token and compare Ids with the profile being accessed
                return Unauthorized();
            //Check if the photo is already archived for this user
            if (await _repo.ArchivedPhotoExists(userId, archivedPhotoForProfileDto.PhotoId))
                return BadRequest("Photo is already archived!");

            var userFromRepo = await _repo.GetUser(userId);

            var archivedPhoto = _mapper.Map<ArchivedPhoto>(archivedPhotoForProfileDto);
            userFromRepo.Archived.Add(archivedPhoto);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Could not add photo to archive");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFromUserArchive(int userId, int id)
        {
            //Determine if the user adding the photo is authorized:
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) //access the current user's token and compare Ids with the profile being accessed
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(userId);

            if(!userFromRepo.Archived.Any(a => a.Id == id))
                return Unauthorized();
            
            var archivedPhotoFromRepo = await _repo.GetArchivedPhoto(id);

            _repo.Delete(archivedPhotoFromRepo);
            if(await _repo.SaveAll())
                return Ok();
            
            return BadRequest("Failed to delete archived photo");
        }
    }
}