using BasketballAppSoftuni.Areas.Manager.Controllers;
using BasketballAppSoftuni.Areas.Manager.Models;
using BasketballAppSoftuni.Contracts;
using BasketballAppSoftuni.DTOs.ManagerAreaDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BasketballAppSoftuni.Tests.ControllersTests
{
    public class ManagerMatchControllerTests
    {
        private MatchController _controller;
        private Mock<IManagerService> _managerServiceMock;
        private IMemoryCache _cache;

        [SetUp]
        public void SetUp()
        {
            _managerServiceMock = new Mock<IManagerService>();
            _managerServiceMock.Setup(a => a.GetAllTeamNamesAsync()).ReturnsAsync(new List<TeamShortInfoDTO>());
            _managerServiceMock.Setup(a => a.GetUnplayedMatchesAsync()).ReturnsAsync(new List<RescheduleMatchDTO>());
            _managerServiceMock.Setup(a => a.GetMatchesForUpdateAsync()).ReturnsAsync(new List<UpdateMatchResultDTO>());

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            _cache = serviceProvider.GetService<IMemoryCache>();

            ITempDataProvider tempDataProvider = Mock.Of<ITempDataProvider>();
            TempDataDictionaryFactory tempDataDictionaryFactory = new TempDataDictionaryFactory(tempDataProvider);
            ITempDataDictionary tempData = tempDataDictionaryFactory.GetTempData(new DefaultHttpContext());


            _controller = new MatchController(_managerServiceMock.Object, _cache)
            {
                TempData = tempData
            };
        }

        [Test]
        public async Task GetSchedule_ReturnsView()
        {
            var result = await _controller.Schedule();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task PostSchedule_ReturnsView()
        {
            var result = await _controller.Schedule(new ScheduleMatchModel());

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<RedirectToActionResult>());

            var redirectResult = result as RedirectToActionResult;
            Assert.IsNotNull(redirectResult);
        }

        [Test]
        public async Task GetReschedule_ReturnsView()
        {
            var result = await _controller.Reschedule();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task PostReschedule_ReturnsView()
        {
            var result = await _controller.Reschedule(new RescheduleMatchModel());

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task GetUpdateResult_ReturnsView()
        {
            var result = await _controller.UpdateResult();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task PostUpdateResult_ReturnsView()
        {
            var result = await _controller.UpdateResult(new UpdateMatchResultModel());

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task GetRemoveMatch_ReturnsView()
        {
            var result = await _controller.RemoveMatch();

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [Test]
        public async Task PostRemoveMatch_ReturnsView()
        {
            var result = await _controller.RemoveMatch(1);

            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<ViewResult>());

            var viewResult = result as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
