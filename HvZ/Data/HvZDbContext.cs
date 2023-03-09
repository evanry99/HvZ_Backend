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


            //On deleting Kill
            modelBuilder.Entity<KillDomain>()
                 .HasOne(k => k.Killer)
                 .WithMany(p => p.Kills)
                 .HasForeignKey(k => k.KillerId)
                 .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<KillDomain>()
                .HasOne(k => k.Victim)
                .WithMany()
                .HasForeignKey(k => k.VictimId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}