using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Memeio.API.Data;
using Memeio.API.Dtos;
using Memeio.API.Helpers;
using Memeio.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Memeio.API.Controllers
{
    [Authorize]
    [Route("api/v1/user/{userId}/[controller]")]
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

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotosForGalleryDto>(photoFromRepo);

            return Ok(photo);
        }
        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            //Determine if the user adding the photo is authorized:
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) //access the current user's token and compare Ids with the profile being accessed
                return Unauthorized();

            //Get our user
            var userFromRepo = await _repo.GetUser(userId);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult(); // to store the result from cloudinary

            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream()) // use a 'using' statment to dispose of data read
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(400).Crop("scale") // scale our images to a decent viewable size
                    };
                    uploadResult = _cloudinary.Upload(uploadParams); // upload our file and get the link associated with that file
                }; // dispose of photo data in memory / close stream when done
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;
            photoForCreationDto.Author = userFromRepo.Username;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            userFromRepo.Posts.Add(photo);

            if (await _repo.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto", new { userId = userId, id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add photo");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int userId, int id)
        {
            //Determine if the user adding the photo is authorized:
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) //access the current user's token and compare Ids with the profile being accessed
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(userId);

            //Determine if any posts from the user correlate with the id of the photo we want to delete
            if(!userFromRepo.Posts.Any(p => p.Id == id))
                return Unauthorized();
            
            //Get the photo
            var photoFromRepo = await _repo.GetPhoto(id);

            //Check if our photo is on our cloudinary database:
            if(photoFromRepo.PublicId != null)
            {
                //Now delete the photo from cloudinary, and delete the url from our database:
                var deleteParams = new DeletionParams(photoFromRepo.PublicId);

                var result = _cloudinary.Destroy(deleteParams);

                if(result.Result == "ok") // if we have successfully destroyed the cloudinary photo, then it MUST be deleted from our db as well
                {
                    _repo.Delete(photoFromRepo);
                }
            }

            //If our photo is NOT in our cloudinary db, then delete it from repo:
            if(photoFromRepo.PublicId == null)
            {
                _repo.Delete(photoFromRepo);
            }

            if(await _repo.SaveAll())
                return Ok();
            
            return BadRequest("Failed to delete photo");
        }

    }
}