using System;
using System.Linq;
using System.Threading.Tasks;
using Memeio.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Memeio.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            //First determine if the user exists:
            var user = await _context.Users_Tbl.FirstOrDefaultAsync(x => x.Username == username);
            if(user == null)
                return null;
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //Generate our key given the passwordSalt
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) //Create a using statement so we can dispose of this data when we finish
            {
                //Use our key to generate the hash, and compare the generated hash against the actual hash:
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            //Given a new user, generate a passwordHash + salt for them and add to our db
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            //Append hash + salt to our new user:
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            //Generate our key and hash
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users_Tbl.AnyAsync(x => x.Username == username))
                return true;
            
            return false;
        }
    }
}