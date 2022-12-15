using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.Services;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Tests.ServicesTests
{
    public class PlayerServiceTests
    {
        private ApplicationDbContext _dbContext;
        private PlayerService _playerService;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BasketballAppDB")
                .Options;

            _dbContext = new ApplicationDbContext(contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            var team = new Team()
            {
                Id = 1,
                Name = "Team",
                HomeTown = "HomeTown",
                LogoURL = "",
                Loses = 10,
                Wins = 15,
            };
            _dbContext.Teams.Add(team);

            _playerService = new PlayerService(_dbContext);
        }

        [Test]
        public async Task GetAllAsync_WorksProperly()
        {
            var players = new List<Player>()
            {
              new Player()
              {
                 Id = 1,
                 FirstName = "Martin",
                 LastName = "Raykov",
                 PictureURL = "",
                 Height = "",
                 Position = "",
                 Salary = "",
                 TeamId = 1
              },
                new Player()
              {
                 Id = 2,
                 FirstName = "Steph",
                 LastName = "Curry",
                 PictureURL = "",
                 Height = "",
                 Position = "",
                 Salary = "",
                 TeamId = 1
              },
            };
            _dbContext.Players.AddRange(players);
            await _dbContext.SaveChangesAsync();

            var result = await _playerService.GetAllAsync();

            Assert.AreEqual(2, result.Count);
            Assert.That(result[0].Id == 1);
            Assert.That(result[0].FullName == "Martin Raykov");
            Assert.That(result[1].Id == 2);
            Assert.That(result[1].FullName == "Steph Curry");
        }

        [Test]
        public async Task GetAsync_WorksProperly()
        {
            var player = new Player()
            {
                Id = 1,
                FirstName = "Martin",
                LastName = "Raykov",
                PictureURL = "",
                Height = "",
                Position = "",
                Salary = "",
                TeamId = 1
            };
            _dbContext.Players.Add(player);
            await _dbContext.SaveChangesAsync();

            var result = await _playerService.GetAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(player.FirstName + " " + player.LastName, result.FullName);
            Assert.AreEqual(player.TeamId, result.TeamId);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Dispose();
        }
    }
}
