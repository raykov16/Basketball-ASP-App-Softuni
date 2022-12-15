using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Controllers;
using BasketballAppSoftuni.DTOs.MatchDTOs;
using BasketballAppSoftuni.DTOs.TeamDTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BasketballAppSoftuni.Tests.ControllersTests
{
    public class MatchControllerTests
    {
        private MatchController _controller;
        private Mock<IMatchService> _matchServiceMock;
        private Mock<ITeamService> _teamServiceMock;
        private IMemoryCache _cache;

        [SetUp]
        public void SetUp()
        {
            _matchServiceMock = new Mock<IMatchService>();
            _matchServiceMock.Setup(a => a.GetAllMatchesAsync()).ReturnsAsync(new List<MatchTableDTO>());
            _matchServiceMock.Setup(a => a.GetMatchesWithTicketsAsync()).ReturnsAsync(new List<MatchBuyTicketDTO>());

            _teamServiceMock = new Mock<ITeamService>();
            _teamServiceMock.Setup(a => a.GetAllAsync()).ReturnsAsync(new List<TeamShortInfoDTO>());

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            _cache = serviceProvider.GetService<IMemoryCache>();

            _controller = new MatchController(_matchServiceMock.Object,_teamServiceMock.Object, _cache);
        }

        [Test]
        public async Task AllMatches_ReturnsView()
        {
            var result = await _controller.AllMatches(0);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task MatchesWithTickets_ReturnsView()
        {
            var result = await _controller.MatchesWithTickets(0);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
