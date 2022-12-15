using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Services;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Tests.ServicesTests
{
    public class TicketServiceTests
    {
        private ApplicationDbContext _dbContext;
        private TicketService _ticketService;

        [SetUp]
        public async Task Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BasketballAppDB")
                .Options;

            _dbContext = new ApplicationDbContext(contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            var arenas = new List<Arena>()
            {
                 new Arena()
                {
                    Id = 1,
                    Location = "",
                    Name = "RocketsArena",
                    PictureURL = "",
                    Seats = 10
                },
                new Arena()
                {
                    Id = 2,
                    Location = "",
                    Name = "LakersArena",
                    PictureURL = "",
                    Seats = 20
                }
            };
            var teams = new List<Team>()
            {
                new Team()
                {
                    Id = 1,
                    Name = "Rockets",
                    HomeTown = "HomeTown",
                    LogoURL = "",
                    Loses = 10,
                    Wins = 15,
                    ArenaId = 1
                },
                 new Team()
                {
                    Id = 2,
                    Name = "Lakers",
                    HomeTown = "HomeTown",
                    LogoURL = "",
                    Loses = 5,
                    Wins = 20,
                    ArenaId = 2
                }
            };

            _dbContext.Arenas.AddRange(arenas);
            _dbContext.Teams.AddRange(teams);

            _ticketService = new TicketService(_dbContext);
        }

        [Test]
        public async Task CreateTicketAsync_WorksProperly()
        {
            var match = new Match()
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                ArenaId = 1,
                GameDate = DateTime.Now,
                TicketsAvailable = 10,
                TicketPrice = 60
            };
            _dbContext.Matches.Add(match);
            await _dbContext.SaveChangesAsync();

            var result = await _ticketService.CreateTicketAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.MatchId);
            Assert.AreEqual(10, result.TicketsAvailable);
            Assert.AreEqual(60, result.PricePerTicket);
            Assert.AreEqual(match.Arena.Name, result.ArenaName);
        }

        [Test]
        public async Task BuyTicketsAsync_WorksProperly()
        {
            var match = new Match()
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                ArenaId = 1,
                GameDate = DateTime.Now,
                TicketsAvailable = 10,
                TicketPrice = 60
            };
            MyUser user = new MyUser()
            {
                Id = "1",
                Email = "test@abv.bg",
                PasswordHash = "test",
                UserName = "test",
            };
            var userMatch = new UserMatch
            {
                MatchId = 1,
                UserId = "1"
            };
            _dbContext.Matches.Add(match);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            await _ticketService.BuyTicketsAsync("1", 1, 10);

            Assert.That(user.UserMatches.Any(um => um.MatchId == 1 && um.UserId == "1"));
            Assert.AreEqual(1, user.UserMatches.Count);
            Assert.AreEqual(0, match.TicketsAvailable);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Dispose();
        }
    }
}
