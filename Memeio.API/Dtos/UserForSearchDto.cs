using System;

namespace Memeio.API.Dtos
{
    public class UserForSearchDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
    }
}