using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoAlbums.Services
{
    public interface IPhotoService
    {
        Task<IEnumerable<Album>> GetAlbumsAsync();
        Task<IEnumerable<Album>> GetAlbumsAsync(int id);
    }
}