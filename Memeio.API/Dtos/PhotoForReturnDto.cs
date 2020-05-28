using System;
using System.Collections.Generic;
using Memeio.API.Models;

namespace Memeio.API.Dtos
{
    public class PhotoForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Author { get; set; } //Extrapolated from the 'User' item in our model class
        public DateTime DatePosted { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Favorites { get; set; }
        public string PublicId { get; set; }
    }
}