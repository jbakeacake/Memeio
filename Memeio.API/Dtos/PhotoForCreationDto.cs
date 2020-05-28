using System;
using System.Collections.Generic;
using Memeio.API.Models;
using Microsoft.AspNetCore.Http;

namespace Memeio.API.Dtos
{
    public class PhotoForCreationDto
    {
        public IFormFile File { get; set; }
        public string PublicId { get; set; } // Cloudinary comes in the form of characters and numbers, so let pub id be a string
        public string Url { get; set; }
        public string Author { get; set; }
        public DateTime DatePosted { get; set; }
        public ICollection<CommentForPost> Comments { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Favorites { get; set; }

        public PhotoForCreationDto()
        {
            DatePosted = DateTime.Now;
        }
    }
}