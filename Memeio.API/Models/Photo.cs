using System;
using System.Collections.Generic;

namespace Memeio.API.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public User User { get; set; } //This will act as the author
        public int UserId { get; set; }
        public DateTime DatePosted { get; set; }
        public string Photo_Comments_Serialized { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Favorites { get; set; }
    }
}