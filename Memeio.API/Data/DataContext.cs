using Memeio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Memeio.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}
        public DbSet<User> Users_Tbl { get; set; }
        public DbSet<Photo> Photos_Tbl { get; set; }
        public DbSet<ArchivedPhoto> Archived_Tbl { get; set; }
        public DbSet<CommentForProfile> Profile_Comments_Tbl { get; set; }
        public DbSet<CommentForPost> Post_Comments_Tbl { get; set; }
    }
}