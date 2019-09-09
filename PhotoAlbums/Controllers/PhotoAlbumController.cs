using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhotoAlbums.Services;

namespace PhotoAlbums.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoAlbumController : ControllerBase
    {
        private IPhotoService service;

        public PhotoAlbumController(IPhotoService service)
        {
            this.service = service;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAsync()
        {
            var albums = await service.GetAlbumsAsync();

            if (!albums.Any())
                return NotFound("No albums found");

            return Ok(albums);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Album>>> GetAsync(int id)
        {
            if (id <= 0)
                return BadRequest("Bad request message");

            var album = await service.GetAlbumsAsync(id);

            if (album == null)
                return NotFound("No album is found");
            
            return Ok(album);
        }
    }
}
