using HvZ.Model.Domain;

namespace HvZ.Data
{
    public class SeedDataHelper
    {
        public static List<GameDomain> GetGameDomains()
        {
            List<GameDomain> games = new List<GameDomain>()
            {
                new GameDomain()
                {
                    Id = 1,
                    Name = "First Game",
                    GameState = "Registration",
                    Description = "Human vs Zombie fun",
                    Nw_Lat = 40.753,
                    Nw_Lng = 73.983
                },
                new GameDomain()
                {
                    Id = 2,
                    Name = "Second Game",
                    GameState = "In Progress",
                    Description = "Very fun game join plz!",
                    Se_Lat = 33.9249,
                    Se_Lng = 18.4241,
                }
            };
            return games;
        }

        public static List<KillDomain> GetKillDomains()
        {
            List<KillDomain> kills = new List<KillDomain>()
            {
                new KillDomain()
                {
                    Id = 1,
                    TimeOfDeath = new DateTime(2023, 3, 9, 6, 30, 0),
                    Story = "Stabbed him very hard",
                    Lat = 33.3213,
                    Lng = 24.223,
                    GameId = 1,
                    KillerId = 1,
                    VictimId = 2
                },
                new KillDomain()
                {
                    Id = 2,
                    TimeOfDeath = new DateTime(2023, 3, 9, 6, 30, 30),
                    Story = "Shot him in the face",
                    Lat = 23.3828,
                    Lng = 82.992,
                    GameId = 2,
                    KillerId = 1,
                    VictimId = 3
                }
            };
            return kills;
        }

        public static List<PlayerDomain> GetPlayerDomains()
        {
            List<PlayerDomain> players = new List<PlayerDomain>()
            {
                new PlayerDomain()
                {
                    Id = 1,
                    BiteCode = "231233",
                    IsPatientZero = true,
                    IsHuman = false,
                    UserId = 1,
                    GameId = 1,
                },
                new PlayerDomain()
                {
                    Id = 2,
                    BiteCode = "112334",
                    IsPatientZero = false,
                    IsHuman = true,
                    UserId = 2,
                    GameId = 1,
                },
                new PlayerDomain()
                {
                    Id = 3,
                    BiteCode = "928475",
                    IsPatientZero = false,
                    IsHuman = true,
                    UserId = 3,
                    GameId = 2,
                }
            };
            return players;
        }

        public static List<UserDomain> GetUserDomains()
        {
            List<UserDomain> users = new List<UserDomain>()
            {
                new UserDomain()
                {
                Id = 1,
                FirstName = "Håvard",
                LastName = "Madland",
                IsAdmin = false,
                },
                new UserDomain()
                {
                    Id = 2,
                    FirstName = "An",
                    LastName = "Nguyen",
                    IsAdmin = false,
                },
                new UserDomain()
                {
                    Id = 3,
                    FirstName = "Vilhelm",
                    LastName = "Assersen",
                    IsAdmin = false,
                }
            };
            return users;
        }
    }
}