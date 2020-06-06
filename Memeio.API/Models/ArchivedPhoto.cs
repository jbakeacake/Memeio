using System;
using System.Collections.Generic;

namespace Memeio.API.Models
{
    public class ArchivedPhoto
    {
        public int Id { get; set; }
        public User User { get; set; } //This will act as the author
        public int UserId { get; set; }
        public int PhotoId { get; set; }
    }
}