namespace Memeio.API.Models
{
    public class CommentForPost
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public Photo Post { get; set; } // This is the Post the comment was made on 
        public int PostId { get; set; } // This is the Id of the Post the comment was made on
    }
}