using HvZ.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace HvZ.Data
{
    public class HvZDbContext : DbContext
    {
        public HvZDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<GameDomain> Games { get; set; }
        public DbSet<KillDomain> Kills { get; set; }
        public DbSet<PlayerDomain> Players { get; set; }
        public DbSet<UserDomain> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDomain>().HasData(SeedDataHelper.GetGameDomains());

            modelBuilder.Entity<KillDomain>().HasData(SeedDataHelper.GetKillDomains());

            modelBuilder.Entity<PlayerDomain>().HasData(SeedDataHelper.GetPlayerDomains());

            modelBuilder.Entity<UserDomain>().HasData(SeedDataHelper.GetUserDomains());
        }
    }
}