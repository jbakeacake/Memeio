namespace Memeio.API.Dtos
{
    public class CommentForProfileToReturnDto
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}