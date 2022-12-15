using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Controllers;
using BasketballAppSoftuni.DTOs.MatchDTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BasketballAppSoftuni.Tests.ControllersTests
{
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<IMatchService> _matchServiceMock;

        [SetUp]
        public void SetUp()
        {
            _matchServiceMock = new Mock<IMatchService>();

            _matchServiceMock.Setup(a => a.GetMatchesWithTicketsAsync()).ReturnsAsync(new List<MatchBuyTicketDTO>());

            _controller = new HomeController(_matchServiceMock.Object);
        }

        [Test]
        public async Task Index_ReturnsView()
        {
            var result = await _controller.Index();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public void Error_ReturnsView()
        {
            var result = _controller.Error("Some message");

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
            Assert.That(viewResult.ViewData.ContainsKey("Message"));
            Assert.That(viewResult.ViewData["Message"] == "Some message");
        }
    }
}
