namespace Memeio.API.Dtos
{
    public class CommentForPostDto
    {
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
    }
}