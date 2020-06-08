using System;
using System.Collections.Generic;

namespace Memeio.API.Models
{
    public class ArchivedPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int PhotoId { get; set; }
        public string AuthorPhotoUrl { get; set; }
        public string Author { get; set; }
        public User User { get; set; } // User archiving the photo
        public int UserId { get; set; } // Id of the user archiving the photo
        public string DateCreated { get; set; }
        public ICollection<CommentForPost> Comments { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Favorites { get; set; }
    }
}