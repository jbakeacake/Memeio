using System.Collections.Generic;
using Memeio.API.Models;

namespace Memeio.API.Dtos
{
    public class ArchivedPhotoForCreationDto
    {
        public string Url { get; set; }
        public int PhotoId { get; set; }
        public string AuthorPhotoUrl { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; } //This will act as the author id
        public string DateCreated { get; set; }
        public ICollection<CommentForPost> Comments { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public int Favorites { get; set; }
    }
}