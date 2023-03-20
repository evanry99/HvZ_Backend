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
                    Nw_Lng = 73.983,
                    StartTime = DateTime.Now,
                },
                new GameDomain()
                {
                    Id = 2,
                    Name = "Second Game",
                    GameState = "In Progress",
                    Description = "Very fun game join plz!",
                    Se_Lat = 33.9249,
                    Se_Lng = 18.4241,
                    StartTime = new DateTime(2023, 4, 10),
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
                UserName = "Hav",
                IsAdmin = false,
                },
                new UserDomain()
                {
                    Id = 2,
                    FirstName = "An",
                    LastName = "Nguyen",
                    UserName = "Al",
                    IsAdmin = false,
                },
                new UserDomain()
                {
                    Id = 3,
                    FirstName = "Vilhelm",
                    LastName = "Assersen",
                    UserName = "Will",
                    IsAdmin = false,
                }
            };
            return users;
        }
        public static List<ChatDomain> GetChatDomains()
        {
            List<ChatDomain> chats = new List<ChatDomain>()
            {
                new ChatDomain()
                {
                    Id = 1,
                    Message = "One zombie close to me",
                    IsHumanGlobal= true,
                    IsZombieGlobal= false,
                    ChatTime = DateTime.UtcNow,
                    GameId= 1,
                    PlayerId= 2,
                    SquadId= 1,
                },
                new ChatDomain()
                {
                    Id = 2,
                    Message = "Surround that human",
                    IsHumanGlobal= false,
                    IsZombieGlobal= true,
                    ChatTime = DateTime.UtcNow,
                    GameId= 1,
                    PlayerId= 1,
                    SquadId= 2,
                },
                new ChatDomain()
                {
                    Id = 3,
                    Message = "Need help!",
                    IsHumanGlobal= true,
                    IsZombieGlobal= false,
                    ChatTime = DateTime.UtcNow,
                    GameId= 2,
                    PlayerId= 3,
                    SquadId= 1,
                },

            };
            return chats;
        }
        public static List<MissionDomain> GetMissionDomains()
        {
            List<MissionDomain> missions = new List<MissionDomain>()
            {
                new MissionDomain()
                {
                    Id = 1,
                    Name= "Gather",
                    IsHumanVisible= true,
                    IsZombieVisible= false,
                    Description= "Find a sock to equip yourself",
                    StartTime= DateTime.UtcNow,
                    EndTime= DateTime.UtcNow.AddMinutes(30),
                    GameId= 1,
                },
                new MissionDomain()
                {
                    Id = 2,
                    Name= "Survive",
                    IsHumanVisible= true,
                    IsZombieVisible= false,
                    Description= "Survive the horde of zombies for 15 minutes ",
                    StartTime= DateTime.UtcNow,
                    EndTime= DateTime.UtcNow.AddMinutes(15),
                    GameId= 2,
                },
                new MissionDomain()
                {
                    Id = 3,
                    Name= "Blood bath",
                    IsHumanVisible= false,
                    IsZombieVisible= true,
                    Description= "Kill 5 humans within 45 minutes ",
                    StartTime= DateTime.UtcNow,
                    EndTime= DateTime.UtcNow.AddMinutes(45),
                    GameId= 2,
                },
            };
            return missions;
        }
        public static List<SquadDomain> GetSquadDomains()
        {
            List<SquadDomain> squads = new List<SquadDomain>()
            {
                new SquadDomain()
                {
                    Id= 1,
                    Name = "Dream team",
                    IsHuman = true,
                    GameId= 1,
                },
                new SquadDomain()
                {
                    Id= 2,
                    Name = "Survivors",
                    IsHuman = true,
                    GameId= 1,
                },
                new SquadDomain()
                {
                    Id= 3,
                    Name = "Kill all humans",
                    IsHuman = false,
                    GameId= 1,
                },
                new SquadDomain()
                {
                    Id= 4,
                    Name = "Zombie zombie",
                    IsHuman = false,
                    GameId= 2,
                },
                new SquadDomain()
                {
                    Id= 5,
                    Name = "Win win",
                    IsHuman = true,
                    GameId= 2,
                },

            };
            return squads;
        }
        public static List<SquadMemberDomain> GetSquadMemberDomains()
        {
            List<SquadMemberDomain> squadMembers = new List<SquadMemberDomain>()
            {
                new SquadMemberDomain()
                {
                    Id= 1,
                    Rank = "General",
                    GameId = 1,
                    SquadId= 1,
                    PlayerId = 2,                 
                },
                new SquadMemberDomain()
                {
                    Id= 2,
                    Rank = "Soldier",
                    GameId = 1,
                    SquadId= 2,
                    PlayerId = 3,
                },
                new SquadMemberDomain()
                {
                    Id= 3,
                    Rank = "Super Zombie",
                    GameId = 2,
                    SquadId= 4,
                    PlayerId = 1,
                },

            };
            return squadMembers;
        }
        public static List<SquadCheckInDomain> GetSquadCheckInDomains()
        {
            List<SquadCheckInDomain> squadCheckIns = new List<SquadCheckInDomain>()
            {
                new SquadCheckInDomain()
                {
                    Id = 1,
                    StartTime= DateTime.UtcNow,
                    EndTime= DateTime.UtcNow.AddMinutes(45),
                    Lat = 55.230,
                    Lng = 20.101,
                    GameId= 1,
                    SquadId= 1,
                    SquadMemberId= 1,
                },
                new SquadCheckInDomain()
                {
                    Id = 2,
                    StartTime= DateTime.UtcNow,
                    EndTime= DateTime.UtcNow.AddMinutes(30),
                    Lat = 10.987,
                    Lng = 40.501,
                    GameId= 1,
                    SquadId= 2,
                    SquadMemberId= 2,
                },
                new SquadCheckInDomain()
                {
                    Id = 3,
                    StartTime= DateTime.UtcNow,
                    EndTime= DateTime.UtcNow.AddMinutes(10),
                    Lat = 70.567,
                    Lng = 5.111,
                    GameId= 2,
                    SquadId= 1,
                    SquadMemberId= 3,
                },

            }; 
            return squadCheckIns;
        }

    }
}