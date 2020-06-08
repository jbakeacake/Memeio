using System;
using System.Collections.Generic;

namespace Memeio.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastActive { get; set; }
        public string PhotoUrl { get; set; }
        public string Introduction { get; set; }
        public ICollection<CommentForProfile> Comments { get; set; }
        public ICollection<ArchivedPhoto> Archived { get; set; }
        public ICollection<Photo> Posts { get; set; }
        public int Followers { get; set; }
        public int Follows { get; set; }
    }
}