using System;
using System.Collections.Generic;
using Memeio.API.Models;

namespace Memeio.API.Dtos
{
    public class PhotosForProfileDto // Identical to our Gallery Dto -- this was created for clarity when transferring data into our controller
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Author { get; set; } //Extrapolated from the 'User' item in our model class
        public DateTime DatePosted { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Favorites { get; set; }
    }
}