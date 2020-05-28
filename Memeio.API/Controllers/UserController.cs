using System.Collections.Generic;
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
    public class UserController : ControllerBase
    {
        private readonly IMemeioRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IMemeioRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /*
        GetUsers(void) : Task<IActionResult>

        Sends a request to our database via our repository to get a list of all users, and returns them as an http get request with mapped data.

        Return : Task<IActionResult> >> An http get containing a mapped to DTO list of all users in our database
        */
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForSearchDto>>(users);

            return Ok(usersToReturn);
        }

        /*
        GetUser(id : int) : Task<IActionResult>

        Sends a request to our database via our repository to get a specific users, and returns it as an http get request with mapped data.

        Return : Task<IActionResult> >> An http get containing a mapped to DTO user in our database
        */
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);
            var userToReturn = _mapper.Map<UserForProfileDto>(user);

            return Ok(userToReturn);
        }

        [HttpGet("{userId}/comment/{id}", Name="GetComment")]
        public async Task<IActionResult> GetComment(int id)
        {
            var commentFromRepo = await _repo.GetUserComment(id);
            var comment = _mapper.Map<CommentForProfileDto>(commentFromRepo);
            return Ok(comment);
        }

        // TODO : Fix null ref exception when mapping from commentForProfileDto to Comment
        [HttpPut("{userId}/comment")]
        public async Task<IActionResult> AddCommentForUser(int userId, [FromForm] CommentForProfileDto commentForProfileDto) // TODO: Add form/CommentForProfileDto // for now lets just add a simple
        {
            //Determine if user exists:
            var userFromRepo = await _repo.GetUser(userId);

            if(userFromRepo == null)
                return BadRequest("User doesn't exist");
            
            commentForProfileDto.UserId = userId;

            var comment = _mapper.Map<CommentForProfile>(commentForProfileDto);

            userFromRepo.Comments.Add(comment);

            if(await _repo.SaveAll())
            {
                var commentToReturn = _mapper.Map<CommentForProfileToReturnDto>(comment);
                return CreatedAtRoute("GetComment", new { userId = userId, id = comment.Id }, commentToReturn);
            }

            return BadRequest("Could not add comment");
        }
    }
}