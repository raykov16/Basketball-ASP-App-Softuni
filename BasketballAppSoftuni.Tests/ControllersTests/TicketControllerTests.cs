using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.Controllers;
using BasketballAppSoftuni.DTOs.TicketDTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BasketballAppSoftuni.Tests.ControllersTests
{
    public class TicketControllerTests
    {
        private TicketController _controller;
        private Mock<ITicketService> _ticketServiceMock;

        [SetUp]
        public void SetUp()
        {
            _ticketServiceMock = new Mock<ITicketService>();
            _ticketServiceMock.Setup(a => a.CreateTicketAsync(1)).ReturnsAsync(new TicketDTO());

            _controller = new TicketController(_ticketServiceMock.Object);
        }

        [Test]
        public async Task AllArenas_ReturnsView()
        {
            var result = await _controller.BuyTickets(1);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
