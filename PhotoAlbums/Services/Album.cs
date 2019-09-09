using System.Collections.Generic;

namespace PhotoAlbums.Services
{
    public class Album
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Photo> Photos { get; set; }
    }

}

