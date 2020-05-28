using System;
using System.Collections.Generic;
using Memeio.API.Models;

namespace Memeio.API.Dtos
{
    public class UserForProfileDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
        public string Introduction { get; set; }
        public ICollection<CommentForProfile> Comments { get; set; }
        public ICollection<PhotosForProfileDto> Posts { get; set; }
        public int Followers { get; set; }
        public int Follows { get; set; }
    }
}