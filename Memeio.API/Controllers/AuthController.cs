using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Memeio.API.Data;
using Memeio.API.Dtos;
using Memeio.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Memeio.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //Before registering our user, determine if the username exists in our database:
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if(await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");
            
            //Now create our user:
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username,
                PhotoUrl = "https://res-console.cloudinary.com/jbakeacake/thumbnails/v1/image/upload/v1590411235/Zm1ycmlzenk4OGRhY214bDdmMm0=/preview"
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201); // send up a success code
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            //Determine if the user exists:
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if(userFromRepo == null)
                return Unauthorized();
            //Now build a token for user to use throughout the application:
            var claims = new[] // Our token will contain two claims: the Id and the Username
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            //Generate a hashing Key from our secret:
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));

            //Create a credential for user with a signature generated by our key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Set our options for the token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor); //Create token object
            var user = _mapper.Map<UserForSearchDto>(userFromRepo); //Map our user to search because we only plan on showing a user(s) when viewing the gallery, or when searching

            return Ok(new
            {
                token = tokenHandler.WriteToken(token), //Write our token object into parseable information
                user //Pass up our user as well for use when setting up user-centric views (profile on navbar, followers, etc.)
            });
        }
    }
}