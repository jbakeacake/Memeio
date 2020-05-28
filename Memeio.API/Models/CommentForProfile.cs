using System.Collections.Generic;

namespace Memeio.API.Models
{
    public class CommentForProfile
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public User User { get; set; } // This is the original poster (i.e. the user receiving the comment)
        public int UserId { get; set; }// The original poster's Id (i.e. the user receiving the comment)
        // public ICollection<Comment> Replies { get; set; } // For now, let's not worry about nested comments
    }
}