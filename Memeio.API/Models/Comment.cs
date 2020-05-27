using System.Collections.Generic;

namespace Memeio.API.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public Photo Post { get; set; } // This is the Post the comment was made on 
        public int PostId { get; set; } // This is the Id of the Post the comment was made on
        public User User { get; set; } // This is the original poster (if the comment was made on a profile)
        public int UserId { get; set; }// The original poster's Id (if the comment was made on a profile)
        // public ICollection<Comment> Replies { get; set; } // For now, let's not worry about nested comments
    }
}