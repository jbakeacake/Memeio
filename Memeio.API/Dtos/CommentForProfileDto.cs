namespace Memeio.API.Dtos
{
    public class CommentForProfileDto
    {
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}