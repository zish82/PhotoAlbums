using FluentAssertions;
using NUnit.Framework;
using PhotoAlbums.Services;
using System.Threading.Tasks;
using System.Linq;

namespace PhotoAlumbs.Test.IntegrationTests
{
    [TestFixture]
    public class PhotoServiceTests
    {
        private IPhotoService service;

        [SetUp]
        public void Setup()
        {
            service = new PhotoService();
        }

        [Test]
        public async Task AlbumsFoundAsync()
        {
            var result = await service.GetAlbumsAsync();

            result.Should().NotBeEmpty();
            var firstItem = result.FirstOrDefault();
            firstItem.Should().NotBeNull();
        }

        [Test]
        public async Task AlbumsAreFoundWhenUserIdIsPassedAsync()
        {
            var result = await service.GetAlbumsAsync(1);

            result.Should().NotBeEmpty();
            var firstItem = result.FirstOrDefault();
            firstItem.Should().NotBeNull();
        }
    }
}
