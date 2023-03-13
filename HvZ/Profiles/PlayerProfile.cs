using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.PlayerDTO;

namespace HvZ.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerCreateDTO, PlayerDomain>();
            CreateMap<PlayerDomain, PlayerReadDTO>();
            CreateMap<PlayerEditDTO, PlayerDomain>();
        }
    }
}
