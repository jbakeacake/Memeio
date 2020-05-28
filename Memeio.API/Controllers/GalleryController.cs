using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Memeio.API.Data;
using Memeio.API.Dtos;
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
    }
}