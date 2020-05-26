using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using Memeio.API.Data;
using Memeio.API.Dtos;
using Memeio.API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Memeio.API.Controllers
{
    [Route("api/gallery/")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IMemeioRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IMemeioRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _repo = repo;
            _mapper = mapper;
            Account acc = new Account( //create an account using our account information
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );
            
            _cloudinary = new Cloudinary(acc); // Set up our cloudinary connection
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotosForGalleryDto>(photoFromRepo);

            return Ok(photo);
        }
    }
}