using System;
using System.Collections.Generic;
using Memeio.API.Models;

namespace Memeio.API.Dtos
{
    public class PhotoForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string AuthorPhotoUrl { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; } //Extrapolated from the 'User' item in our model class
        public string DateCreated { get; set; }
        public ICollection<CommentForPost> Comments { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Favorites { get; set; }
        public string PublicId { get; set; }
    }
}