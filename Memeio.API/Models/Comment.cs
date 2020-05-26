using System.Collections.Generic;

namespace Memeio.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public Photo Post { get; set; }
        public int PostId { get; set; }
        public User User { get; set; } // This is the original poster
        public int UserId { get; set; }// The original poster's Id
        // public ICollection<Comment> Replies { get; set; } // For now, let's not worry about nested comments
    }
}