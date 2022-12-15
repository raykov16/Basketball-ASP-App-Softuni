using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.Services;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Tests.ServicesTests
{
    public class MatchServiceTests
    {
        private ApplicationDbContext _dbContext;
        private MatchService _matchService;

        [SetUp]
        public async Task Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BasketballAppDB")
                .Options;

            _dbContext = new ApplicationDbContext(contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

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
            _dbContext.Teams.AddRange(teams);

            _matchService = new MatchService(_dbContext);
        }

        [Test]
        public async Task GetAllMatchesAsync_WorksProperly()
        {
            var matches = new List<Match>()
           {
                new Match()
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    ArenaId = 1,
                    GameDate = DateTime.Now,
                    TicketsAvailable = 10
                },
                 new Match()
                {
                    Id = 2,
                    HomeTeamId = 2,
                    AwayTeamId = 1,
                    ArenaId = 2,
                    GameDate = DateTime.Now,
                    TicketsAvailable = 0,
                    HomeTeamPoints = 125,
                    AwayTeamPoints = 120
                },
           };
            _dbContext.Matches.AddRange(matches);
            await _dbContext.SaveChangesAsync();

            var result = await _matchService.GetAllMatchesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].HomeTeamId);
            Assert.AreEqual(2, result[1].HomeTeamId);
        }

        [Test]
        public async Task GetMatchesWithTicketsAsync_WorksProperly()
        {
            var matches = new List<Match>()
           {
                new Match()
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    ArenaId = 1,
                    GameDate = DateTime.Now,
                    TicketsAvailable = 10
                },
                 new Match()
                {
                    Id = 2,
                    HomeTeamId = 2,
                    AwayTeamId = 1,
                    ArenaId = 2,
                    GameDate = DateTime.Now,
                    TicketsAvailable = 0,
                    HomeTeamPoints = 125,
                    AwayTeamPoints = 120
                },
           };
            _dbContext.Matches.AddRange(matches);
            await _dbContext.SaveChangesAsync();

            var result = await _matchService.GetMatchesWithTicketsAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].MatchId);

        }

        [Test]
        public async Task GetMyMatchesAsync_WorksProperly()
        {
            var matches = new List<Match>()
           {
                new Match()
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    ArenaId = 1,
                    GameDate = DateTime.Now,
                    TicketsAvailable = 10
                },
                 new Match()
                {
                    Id = 2,
                    HomeTeamId = 2,
                    AwayTeamId = 1,
                    ArenaId = 2,
                    GameDate = DateTime.Now,
                    TicketsAvailable = 0,
                    HomeTeamPoints = 125,
                    AwayTeamPoints = 120
                },
           };
            var arenas = new List<Arena>()
            {
                 new Arena()
                {
                    Id = 1,
                    Location = "",
                    Name = "",
                    PictureURL = "",
                    Seats = 10
                },
                new Arena()
                {
                    Id = 2,
                    Location = "",
                    Name = "",
                    PictureURL = "",
                    Seats = 20
                }
            };
            MyUser user = new MyUser()
            {
                Id = "1",
                Email = "test@abv.bg",
                PasswordHash = "test",
                UserName = "test",
            };
            var userMatches = new List<UserMatch>()
            {
                new UserMatch
                {
                    MatchId = 1,
                    UserId = "1"
                },
                new UserMatch
                {
                    MatchId = 2,
                    UserId = "2"
                }
            };

            user.UserMatches = userMatches;
            _dbContext.Matches.AddRange(matches);
            _dbContext.Arenas.AddRange(arenas);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var result = await _matchService.GetMyMatchesAsync("1");

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Rockets", result[0].HomeTeamName);
            Assert.AreEqual("Lakers", result[0].AwayTeamName);
            Assert.AreEqual("Lakers", result[1].HomeTeamName);
            Assert.AreEqual("Rockets", result[1].AwayTeamName);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Dispose();
        }
    }
}
