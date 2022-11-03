using BasketballAppSoftuni.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasketballAppSoftuni.Data
{
    public class ApplicationDbContext : IdentityDbContext<MyUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Arena> Arenas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Player>(p =>
            {
                p.Property(pl => pl.PointsPerGame)
                .HasDefaultValue(0);

                p.Property(pl => pl.AssistsPerGame)
                .HasDefaultValue(0);

                p.Property(pl => pl.ReboundsPerGame)
                .HasDefaultValue(0);
            });

            base.OnModelCreating(builder);
        }
    }
}