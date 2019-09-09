using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;

namespace PhotoAlbums.Services
{
    public class PhotoService : IPhotoService
    {
        private const string AlbumUri = "http://jsonplaceholder.typicode.com/albums";
        private const string PhotoUri = "http://jsonplaceholder.typicode.com/photos";
        
        public async Task<IEnumerable<Album>> GetAlbumsAsync()
        {
            var albums = await AlbumUri.GetJsonAsync<IEnumerable<Album>>();
            var photos = await PhotoUri.GetJsonAsync<IEnumerable<Photo>>();

            if (albums != null && albums.Any() && photos != null && photos.Any())
            {
                IEnumerable<Album> photoAlbums = JoinAlbumsAndPhotos(albums, photos);

                return photoAlbums;
            }

            return albums;
        }

        public async Task<IEnumerable<Album>> GetAlbumsAsync(int id)
        {
            var albums = await AlbumUri.GetJsonAsync<IEnumerable<Album>>();
            var photos = await PhotoUri.GetJsonAsync<IEnumerable<Photo>>();

            if (albums != null && albums.Any())
            {
                albums = albums.Where(x => x.UserId == id);

                if (albums != null && albums.Any() && photos != null && photos.Any())
                {
                    IEnumerable<Album> photoAlbums = JoinAlbumsAndPhotos(albums, photos);
                    return photoAlbums;
                }
            }

            return albums;
        }

        private static IEnumerable<Album> JoinAlbumsAndPhotos(IEnumerable<Album> albums, IEnumerable<Photo> photos)
        {
            return albums.GroupJoin(photos, album => album.Id, photo => photo.AlbumId,
                            (album, photoList) => new Album
                            {
                                Id = album.Id,
                                Title = album.Title,
                                UserId = album.UserId,
                                Photos = photoList
                            });
        }
    }
}