using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.DTOs.TeamDTOs;
using BasketballAppSoftuni.Services;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Tests.ServicesTests
{
    public class TeamServiceTests
    {
        private ApplicationDbContext _dbContext;
        private TeamService _teamService;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BasketballAppDB")
                .Options;

            _dbContext = new ApplicationDbContext(contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _teamService = new TeamService(_dbContext);
        }

        [Test]
        public async Task GetAllAsync_WorksProperly()
        {
            var teams = new List<Team>()
           {
               new Team()
               {
                   Id = 1,
                   ArenaId = 1,
                   HomeTown = "",
                   LogoURL = "",
                   Name = ""
               },
                new Team()
               {
                   Id = 2,
                   ArenaId = 2,
                   HomeTown = "",
                   LogoURL = "",
                   Name = ""
               }
           };
            _dbContext.Teams.AddRange(teams);
            await _dbContext.SaveChangesAsync();

            var result = await _teamService.GetAllAsync();

            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(2, result[1].Id);
            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public async Task GetAsync_WorksProperly()
        {
            var arena = new Arena()
            {
                Id = 1,
                Location = "",
                Name = "",
                PictureURL = "",
                Seats = 10
            };
            var team = new Team()
            {
                Id = 1,
                Name = "Team",
                HomeTown = "HomeTown",
                LogoURL = "",
                Loses = 10,
                Wins = 15,
                ArenaId = 1
            };

            _dbContext.Arenas.Add(arena);
            _dbContext.Teams.Add(team);
            await _dbContext.SaveChangesAsync();

            TeamDetailsDTO result = await _teamService.GetAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.Id == team.Id);
            Assert.That(result.Name == team.Name);
            Assert.That(result.HomeTown == team.HomeTown);
            Assert.That(result.Loses == team.Loses);
            Assert.That(result.Wins == team.Wins);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Dispose();
        }
    }
}