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
    [Route("api/v1/[controller]")]
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
        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToUserArchive(int userId, [FromBody]ArchivedPhotoForCreationDto archivePhotoForCreationDto) // we're using this dto mainly to keep consistency among other controllers
        {   
            var userFromRepo = await _repo.GetUser(userId);

            if (userFromRepo == null)
                return BadRequest("User does not exist");

            var archivedPhoto = _mapper.Map<ArchivedPhoto>(archivePhotoForCreationDto);
            userFromRepo.Archived.Add(archivedPhoto);

            if(await _repo.SaveAll())
            {
                return NoContent();
            }

            return BadRequest("Could not archive photo");
        }
    }
}