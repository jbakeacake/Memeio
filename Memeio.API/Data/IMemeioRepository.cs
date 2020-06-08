using System.Collections.Generic;
using System.Threading.Tasks;
using Memeio.API.Models;

namespace Memeio.API.Data
{
    public interface IMemeioRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void DeleteArchivedWhere(int photoId);
        Task<bool> SaveAll();
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<Photo> GetPhoto(int id);
        Task<IEnumerable<Photo>> GetPhotoSet();
        Task<IEnumerable<Photo>> GetUserPhotos(int id);
        Task<CommentForProfile> GetUserComment(int id);
        Task<IEnumerable<CommentForProfile>> GetUserComments(int id);
        Task<ArchivedPhoto> A_GetArchivedPhoto(int id);
        Task<IEnumerable<ArchivedPhoto>> A_GetArchivedPhotos(int id);
        Task<IEnumerable<Photo>> P_GetArchivedPhotos(int id);
        Task<bool> ArchivedExists(int id, int PhotoId);
    }
}