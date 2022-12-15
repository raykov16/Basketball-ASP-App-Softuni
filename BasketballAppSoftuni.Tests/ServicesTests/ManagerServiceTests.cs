using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.DTOs.ManagerAreaDTOs;
using BasketballAppSoftuni.Services;
using Microsoft.EntityFrameworkCore;
namespace BasketballAppSoftuni.Tests.ServicesTests
{
    public class ManagerServiceTests
    {
        private ApplicationDbContext _dbContext;
        private ManagerService _managerService;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BasketballAppDB")
                .Options;

            _dbContext = new ApplicationDbContext(contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _managerService = new ManagerService(_dbContext);
        }

        [Test]
        public async Task AddMatchAsync_WorksProperly()
        {
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
            await _dbContext.SaveChangesAsync();

            var dto = new ScheduleMatchDTO
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                MatchDate = DateTime.Now,
                MatchTime = "10:20",
                TicketPrice = 60
            };

            await _managerService.AddMatchAsync(dto);

            var result = await _dbContext.Matches.ToListAsync();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].HomeTeamId);
            Assert.AreEqual(2, result[0].AwayTeamId);
            Assert.AreEqual(60, result[0].TicketPrice);
        }

        [Test]
        public async Task GetAllTeamNamesAsync_WorksProperly()
        {
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
                },
                new Team()
                {
                    Id = 2,
                    Name = "Lakers",
                    HomeTown = "HomeTown",
                    LogoURL = "",
                    Loses = 5,
                    Wins = 20,
                }
            };
            _dbContext.Teams.AddRange(teams);
            await _dbContext.SaveChangesAsync();

            var result = await _managerService.GetAllTeamNamesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.That(result.Any(x => x.Name == teams[0].Name));
            Assert.That(result.Any(x => x.Name == teams[1].Name));
        }

        [Test]
        public async Task GetUnplayedMatchesAsync_WorksProperly()
        {
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
            var matches = new List<Match>()
            {
                new Match()
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    ArenaId = 1,
                    GameDate = DateTime.Now.AddDays(1),
                    TicketsAvailable = 10
                },
                new Match()
                {
                    Id = 2,
                    HomeTeamId = 2,
                    AwayTeamId = 1,
                    ArenaId = 2,
                    GameDate = new DateTime(day: 1,month: 1, year: 2022),
                    TicketsAvailable = 100
                }
            };
            _dbContext.Teams.AddRange(teams);
            _dbContext.Matches.AddRange(matches);
            await _dbContext.SaveChangesAsync();

            var result = await _managerService.GetUnplayedMatchesAsync();

            Assert.AreEqual(1, result.Count());
            Assert.That(result.Any(x => x.MatchId == 1));
            Assert.That(!result.Any(x => x.MatchId == 2));
        }

        [Test]
        public async Task RescheduleMatchAsync_WorksProperly()
        {
            var match = new Match()
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                ArenaId = 1,
                GameDate = DateTime.Now.AddDays(1),
                TicketsAvailable = 10
            };
            _dbContext.Matches.Add(match);
            await _dbContext.SaveChangesAsync();
            var date = DateTime.Now;

            await _managerService.RescheduleMatchAsync(1, date);

            Assert.AreEqual(date, match.GameDate);
        }

        [Test]
        public async Task GetMatchesForUpdateAsync_WorksProperly()
        {
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
            var matches = new List<Match>()
            {
                new Match()
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    ArenaId = 1,
                    GameDate = DateTime.Now.AddDays(1),
                    TicketsAvailable = 10
                },
                new Match()
                {
                    Id = 2,
                    HomeTeamId = 2,
                    AwayTeamId = 1,
                    ArenaId = 2,
                    GameDate = new DateTime(day: 1,month: 1, year: 2022),
                    TicketsAvailable = 0,
                },
                new Match()
                {
                    Id = 3,
                    HomeTeamId = 2,
                    AwayTeamId = 1,
                    ArenaId = 2,
                    GameDate = new DateTime(day: 1,month: 1, year: 2022),
                    TicketsAvailable = 0,
                    HomeTeamPoints = 125,
                    AwayTeamPoints = 120

                }
            };
            _dbContext.Teams.AddRange(teams);
            _dbContext.Matches.AddRange(matches);
            await _dbContext.SaveChangesAsync();

            var result = await _managerService.GetMatchesForUpdateAsync();

            Assert.AreEqual(1, result.Count());
            Assert.That(result.Any(x => x.HomeTeamPoints == 0));
            Assert.That(result.Any(x => x.MatchId == 2));
        }

        [Test]
        public async Task UpdateMatchScoreAsync_WorksProperly()
        {
            var match = new Match()
            {
                Id = 2,
                HomeTeamId = 2,
                AwayTeamId = 1,
                ArenaId = 2,
                GameDate = new DateTime(day: 1, month: 1, year: 2022),
                TicketsAvailable = 0,
            };
            _dbContext.Matches.Add(match);
            await _dbContext.SaveChangesAsync();

            await _managerService.UpdateMatchScoreAsync(2, 120, 130);

            Assert.AreEqual(120, match.HomeTeamPoints);
            Assert.AreEqual(130, match.AwayTeamPoints);
        }

        [Test]
        public async Task RemoveMatchAsync_WorksProperly()
        {
            var matches = new List<Match>()
            {
                new Match()
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    ArenaId = 1,
                    GameDate = DateTime.Now.AddDays(1),
                    TicketsAvailable = 10
                },
                new Match()
                {
                    Id = 2,
                    HomeTeamId = 2,
                    AwayTeamId = 1,
                    ArenaId = 2,
                    GameDate = new DateTime(day: 1,month: 1, year: 2022),
                    TicketsAvailable = 100
                }
            };
            _dbContext.Matches.AddRange(matches);
            await _dbContext.SaveChangesAsync();

            await _managerService.RemoveMatchAsync(2);

            Assert.AreEqual(1, _dbContext.Matches.Count());
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Dispose();
        }
    }
}
