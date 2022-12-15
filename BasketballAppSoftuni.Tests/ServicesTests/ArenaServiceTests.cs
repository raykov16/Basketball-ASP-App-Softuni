using BasketballAppSoftuni.Data;
using BasketballAppSoftuni.Data.Entities;
using BasketballAppSoftuni.Services;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Tests.ServicesTests
{
    public class ArenaServiceTests
    {
        private ApplicationDbContext _dbContext;
        private ArenaService _arenaService;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BasketballAppDB")
                .Options;

            _dbContext = new ApplicationDbContext(contextOptions);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            _arenaService = new ArenaService(_dbContext);
        }

        [Test]
        public async Task GetAllAsync_WorksProperly()
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
                },
            };
            _dbContext.Arenas.AddRange(arenas);
            await _dbContext.SaveChangesAsync();

            var result = await _arenaService.GetAllAsync();

            Assert.That(result.Count == 2);
            Assert.That(result[0].Seats == 10);
            Assert.That(result[1].Seats == 20);
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
            _dbContext.Arenas.Add(arena);
            await _dbContext.SaveChangesAsync();

            var result = await _arenaService.GetAsync(1);

            Assert.IsNotNull(result);
            Assert.That(result.Seats == 10);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext?.Dispose();
        }
    }
}
