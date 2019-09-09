using NUnit.Framework;
using PhotoAlbums.Services;
using System.Collections.Generic;
using Moq;
using PhotoAlbums.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoAlumbs.Test.UnitTests.Controller
{
    [TestFixture]
    public class PhotoAlbumControllerTests
    {
        private const int albumId = 1;
        private const string photoTitle = "photo title";
        private const string photoUrl = "url";
        private const string FirstItemTitle = "First item title";
        private const int userId = 5;
        private Mock<IPhotoService> service;
        private PhotoAlbumController controller;

        [SetUp]
        public void Setup()
        {
            service = new Mock<IPhotoService>();
            controller = new PhotoAlbumController(service.Object);

            Album album = new Album
            {
                Id = albumId,
                Title = FirstItemTitle,
                UserId = userId,
                Photos = new List<Photo>{ new Photo {
                                                AlbumId = albumId,
                                                Title = photoTitle,
                                                Url = photoUrl
                                                    }
                                         }
            };
            List<Album> albums = new List<Album>
                                {
                                    album
                                };
            service.Setup(x => x.GetAlbumsAsync()).ReturnsAsync(albums);
            service.Setup(x => x.GetAlbumsAsync(userId)).ReturnsAsync(albums);
        }

        [Test]
        public async Task Return404NotFoundWhenNoPhotosAreFoundAsync()
        {
            service.Setup(x => x.GetAlbumsAsync()).ReturnsAsync(new List<Album>());

            var result = await controller.GetAsync();

            var notfound = result.Result as NotFoundObjectResult;
            result.Should().NotBeNull();
            notfound.Should().BeOfType<NotFoundObjectResult>();
            notfound.Value.Should().NotBeNull();
        }

        [Test]
        public void ReturnAlbumsWhenResultsAreFound()
        {
            var result = controller.GetAsync().Result;
            
            var ok = result.Result as OkObjectResult;
            ok.Should().BeOfType<OkObjectResult>();
            var value = ok.Value as IEnumerable<Album>;
            value.Should().NotBeEmpty();

            var firstItem = value.FirstOrDefault();
            firstItem.Should().NotBeNull();
            firstItem.Title.Should().Be(FirstItemTitle);
            firstItem.Id.Should().Be(1);
            firstItem.Photos.Should().NotBeEmpty();

            var firstPhoto = firstItem.Photos.FirstOrDefault();
            firstPhoto.AlbumId.Should().Be(albumId);
            firstPhoto.Title.Should().BeEquivalentTo(photoTitle);
            firstPhoto.Url.Should().BeEquivalentTo(photoUrl);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public async Task Return400BadRequestWhenNoCriteriaIsDefinedFotGetWithIdAsync(int id)
        {
            var result = await controller.GetAsync(id);

            var asyncResult = result.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            asyncResult.Should().BeOfType<BadRequestObjectResult>();
        }

        public void ReturnAlbumWhenResultsAreFound()
        {
            var result = controller.GetAsync(userId).Result;

            var ok = result.Result as OkObjectResult;
            ok.Should().BeOfType<OkObjectResult>();
            var item = result.Value as Album;
            
            item.Should().NotBeNull();
            item.Title.Should().Be(FirstItemTitle);
            item.Id.Should().Be(1);
            item.Photos.Should().NotBeEmpty();

            var firstPhoto = item.Photos.FirstOrDefault();
            firstPhoto.AlbumId.Should().Be(albumId);
            firstPhoto.Title.Should().BeEquivalentTo(photoTitle);
            firstPhoto.Url.Should().BeEquivalentTo(photoUrl);
        }
    }
}
