using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Controllers;
using BasketballAppSoftuni.DTOs.TeamDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BasketballAppSoftuni.Tests.ControllersTests
{
    public class TeamControllerTests
    {
        private TeamController _controller;
        private Mock<ITeamService> _teamServiceMock;
        private IMemoryCache _cache;

        [SetUp]
        public void SetUp()
        {
            _teamServiceMock = new Mock<ITeamService>();
            _teamServiceMock.Setup(a => a.GetAllAsync()).ReturnsAsync(new List<TeamShortInfoDTO>());
            _teamServiceMock.Setup(a => a.GetAsync(1)).ReturnsAsync(new TeamDetailsDTO());

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            _cache = serviceProvider.GetService<IMemoryCache>();

            _controller = new TeamController(_teamServiceMock.Object, _cache);
        }

        [Test]
        public async Task AllTeams_ReturnsView()
        {
            var result = await _controller.AllTeams();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task TeamDetails_RedirectsToErrorView()
        {
            var result = await _controller.TeamDetails(1);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());

            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
        }
    }
}
