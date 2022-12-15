using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Controllers;
using BasketballAppSoftuni.DTOs.ArenaDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework.Internal;

namespace BasketballAppSoftuni.Tests.ControllersTests
{
    public class ArenaControllerTests
    {
        private ArenaController _controller;
        private Mock<IArenaService> _arenaServiceMock;
        private IMemoryCache _cache;

        [SetUp]
        public void SetUp()
        {
            _arenaServiceMock = new Mock<IArenaService>();
            _arenaServiceMock.Setup(a => a.GetAllAsync()).ReturnsAsync(new List<ArenaDetailsDTO>());
            _arenaServiceMock.Setup(a => a.GetAsync(1)).ReturnsAsync(new ArenaDetailsDTO());

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            _cache = serviceProvider.GetService<IMemoryCache>();

            _controller = new ArenaController(_arenaServiceMock.Object, _cache);
        }

        [Test]
        public async Task AllArenas_ReturnsView()
        {
            var result = await _controller.AllArenas();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task ArenaDetails_ReturnsView()
        {
            var result = await _controller.ArenaDetails(1);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
