using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memeio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Memeio.API.Data
{
    public class MemeioRepository : IMemeioRepository
    {
        private readonly DataContext _context;

        public MemeioRepository(DataContext context)
        {
            _context = context;   
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        /*
        GetPhoto(id : int) : Task<Photo>

        Queries our database for a photo provided the Id of the photo. Returns a "Photo" object.

        id : int >> Id of the photo to be queried.

        Return : Task<Photo> >> Photo object translated from Database
        */
        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos_Tbl.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        /*
        GetPhotos() : Task<IEnumerable<Photo>>

        Returns the 500 of the latest photos added to the database. // TODO: Maybe incorporate a method which selects memes from the users with top followers?

        Return : Task<IEnumerable<Photo>> >> List of ALL photos from the database

        */
        public async Task<IEnumerable<Photo>> GetPhotoSet()
        {
            int numRecords = 500;
            var photos = await _context.Photos_Tbl.OrderByDescending(p => p.DatePosted).Take(numRecords).ToListAsync();
            return photos;
        }

        /*
        GetUserPhotos(id : int) : Task<IEnumerable<Photo>>

        Returns the collection of photos made by a specific user.

        id : int >> The Id of the user

        Return : Task<IEnumerable<Photo>> >> The collection of photos specific to a user.
        */
        public async Task<IEnumerable<Photo>> GetUserPhotos(int id)
        {
            var photos = await _context.Photos_Tbl
                .Where(p => p.UserId == id)
                .ToListAsync();
            return photos;
        }

        /*
        GetUser(id : int) : Task<User>

        Queries our database for a user provided the Id of the user. Returns a "User" object. Additionally, this
        "User" object includes information relative to all of their posts, their followers/follows, profile comments, and basic
        user information.

        id : int >> Id of the user to be queried.

        Return : Task<User> >> User object translated from Database
        */
        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users_Tbl.Include(p => p.Posts).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        /*
        GetUsers() : Task<IEnumerable<User>>

        Queries our database for a list of ALL users. Returns an enumerable containing all users.

        Return : Task<IEnumerable<User>> >> A list of users translated from our Database
        */
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users_Tbl.Include(p => p.Posts).ToListAsync();
            return users;
        }

        /*
        GetUser(id : int) : Task<bool>

        Saves any changes made to our database. Returns true if save was successful.

        Return : User >> User object translated from Database
        */
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0; // If positive, changes were saved successfully
        }

        public async Task<Comment> GetUserComment(int id)
        {
            return await _context.Comments_Tbl
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Comment>> GetUserComments(int id)
        {
            return await _context.Comments_Tbl
                .Where(c => c.UserId == id)
                .ToListAsync();
        }
    }
}