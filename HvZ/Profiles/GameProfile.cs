using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.GameDTO;

namespace HvZ.Profiles
{
    public class GameProfile: Profile
    {
        public GameProfile() {
            CreateMap<GameCreateDTO, GameDomain>();
            CreateMap<GameDomain, GameReadDTO>();
            CreateMap<GameEditDTO, GameDomain>();
        }
    }
}
