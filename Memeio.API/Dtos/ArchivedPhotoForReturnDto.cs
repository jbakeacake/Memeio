using System;
using System.Collections.Generic;
using Memeio.API.Models;

namespace Memeio.API.Dtos
{
    public class ArchivedPhotoForReturnDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string AuthorPhotoUrl { get; set; }
        public string Author { get; set; }
        public int UserId { get; set; } //This will act as the author id
        public DateTime DatePosted { get; set; }
        public ICollection<CommentForPost> Comments { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Favorites { get; set; }
    }
}