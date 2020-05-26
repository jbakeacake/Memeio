using System.Collections.Generic;

namespace Memeio.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int OriginalPosterId { get; set; }
        public User OriginalPoster { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        // public ICollection<Comment> Replies { get; set; } // For now, let's not worry about nested comments
    }
}