using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Controllers;
using BasketballAppSoftuni.DTOs.PlayerDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BasketballAppSoftuni.Tests.ControllersTests
{
    public class PlayerControllerTests
    {
        private PlayerController _controller;
        private Mock<IPlayerService> _playerServiceMock;
        private IMemoryCache _cache;

        [SetUp]
        public void SetUp()
        {
            _playerServiceMock = new Mock<IPlayerService>();
            _playerServiceMock.Setup(a => a.GetAllAsync()).ReturnsAsync(new List<PlayerTeamAndPositionDTO>());
            _playerServiceMock.Setup(a => a.GetAsync(1)).ReturnsAsync(new PlayerFullInfoDTO());

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            _cache = serviceProvider.GetService<IMemoryCache>();

            _controller = new PlayerController(_playerServiceMock.Object, _cache);
        }

        [Test]
        public async Task AllPlayers_ReturnsView()
        {
            var result = await _controller.AllPlayers(null,null);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task PLayerDetails_ReturnsView()
        {
            var result = await _controller.PlayerDetails(1);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
