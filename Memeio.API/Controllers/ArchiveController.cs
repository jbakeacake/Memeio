using System;
using System.Collections.Generic;
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

    [Route("api/v1/{userId}/[controller]")]
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
        /*
        AddToUserArchive(photoId: int, userId: int): Task<IActionResult>

        Upon request, add a photoId to user's archived collection. This is a one user to many photos relationship.
        Whenever a photo is added to a user's archive, it pairs that user's id with the id of the photo being
        archived. This method executes this pairing onto a the database's archive table.

        photoId: int >> id of the photo to be added to the user's archive
        userId: int >> id of the user who is adding the photo its archive

        Return: Task<IActionResult> >> HTTP message to be returned to our browser
        */
        [HttpPost]
        public async Task<IActionResult> AddToUserArchive(int userId, ArchivedPhotoForCreationDto archivePhotoForCreationDto) // we're using this dto mainly to keep consistency among other controllers
        {
            var userFromRepo = await _repo.GetUser(userId);

            if (userFromRepo == null)
                return BadRequest("User does not exist");
            if (await _repo.ArchivedExists(userId, archivePhotoForCreationDto.PhotoId))
                return BadRequest("Photo is already archived!");

            var archivedPhoto = _mapper.Map<ArchivedPhoto>(archivePhotoForCreationDto);
            userFromRepo.Archived.Add(archivedPhoto);

            if (await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Could not archive photo");
        }

        [HttpGet("photos", Name = "GetArchivePhotos")]
        public async Task<IActionResult> GetArchivePhotos(int userId)
        {
            var photosFromRepo = await _repo.P_GetArchivedPhotos(userId);
            var photosToReturn = _mapper.Map<IEnumerable<PhotoForReturnDto>>(photosFromRepo);

            return Ok(photosToReturn);
        }

        [HttpGet("ids", Name = "GetArchiveIds")]
        public async Task<IActionResult> GetArchive(int userId)
        {
            var archivedFromRepo = await _repo.A_GetArchivedPhotos(userId);
            var archivedToReturn = _mapper.Map<IEnumerable<ArchivedPhotoForReturnDto>>(archivedFromRepo);

            return Ok(archivedToReturn);
        }

        [HttpDelete("{id}", Name = "DeleteArchivedPhoto")]
        public async Task<IActionResult> DeleteArchivedPhoto(int userId, int id)
        {
            //Determine if the user adding the photo is authorized:
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) //access the current user's token and compare Ids with the profile being accessed
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(userId);
            //Determine if any posts from the user correlate with the photo we want to delete
            if (!userFromRepo.Archived.Any(a => a.Id == id))
                return Unauthorized();

            var archivedPhotoFromRepo = await _repo.A_GetArchivedPhoto(id);

            _repo.Delete(archivedPhotoFromRepo);
            if (await _repo.SaveAll())
                return Ok();
            return BadRequest("Failed to delete archived photo");
        }
    }
}