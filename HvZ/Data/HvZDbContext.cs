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
        public DbSet<ChatDomain> Chats { get; set; }
        public DbSet<MissionDomain> Missions { get; set; }
        public DbSet<SquadCheckInDomain> SquadCheckIns { get; set; }
        public DbSet<SquadDomain> Squads { get; set; }
        public DbSet<SquadMemberDomain> SquadMembers { get; set; }

        /// <summary>
        /// Method to seed the database with initial records of data into each table.
        /// Also ignores cascades on delete behaviours between tables.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameDomain>().HasData(SeedDataHelper.GetGameDomains());

            modelBuilder.Entity<KillDomain>().HasData(SeedDataHelper.GetKillDomains());

            modelBuilder.Entity<PlayerDomain>().HasData(SeedDataHelper.GetPlayerDomains());

            modelBuilder.Entity<UserDomain>().HasData(SeedDataHelper.GetUserDomains());

            modelBuilder.Entity<ChatDomain>().HasData(SeedDataHelper.GetChatDomains());

            modelBuilder.Entity<MissionDomain>().HasData(SeedDataHelper.GetMissionDomains());

            modelBuilder.Entity<SquadDomain>().HasData(SeedDataHelper.GetSquadDomains());

            modelBuilder.Entity<SquadMemberDomain>().HasData(SeedDataHelper.GetSquadMemberDomains());

            modelBuilder.Entity<SquadCheckInDomain>().HasData(SeedDataHelper.GetSquadCheckInDomains());

            //On deleting Kill and Victim
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

            // On deleting Chats and Player
            modelBuilder.Entity<ChatDomain>()
                .HasOne(c => c.Player)
                .WithMany(p => p.Chats)
                .HasForeignKey(c => c.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            // On deleting SquadMembers and Players
            modelBuilder.Entity<SquadMemberDomain>()
                .HasOne(p => p.Player)
                .WithOne(s => s.SquadMember)
                .HasForeignKey<SquadMemberDomain>(p => p.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            // On deleting SquadMembers and Squad
            modelBuilder.Entity<SquadMemberDomain>()
                .HasOne(sm => sm.Squad)
                .WithMany(s => s.SquadMembers)
                .HasForeignKey(sm => sm.SquadId)
                .OnDelete(DeleteBehavior.NoAction);

            // On deleting Player and SquadMember
            modelBuilder.Entity<PlayerDomain>()
                .HasOne(p => p.SquadMember)
                .WithOne(sm => sm.Player)
                .HasForeignKey<SquadMemberDomain>(sm => sm.PlayerId)
                .OnDelete(DeleteBehavior.NoAction);

            // On deleting SquadCheckIn and Squad
            modelBuilder.Entity<SquadCheckInDomain>()
                .HasOne(s => s.Squad)
                .WithMany(s => s.SquadCheckIns)
                .HasForeignKey(s => s.SquadId)
                .OnDelete(DeleteBehavior.NoAction);

            // On deleting SquadCheckIn and SquadMember
            modelBuilder.Entity<SquadCheckInDomain>()
                .HasOne(s => s.SquadMember)
                .WithMany(sm => sm.SquadCheckIns)
                .HasForeignKey(s => s.SquadMemberId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}