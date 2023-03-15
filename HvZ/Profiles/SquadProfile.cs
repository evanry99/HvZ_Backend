using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.SquadDTO;

namespace HvZ.Profiles
{
    public class SquadProfile: Profile
    {
        public SquadProfile() 
        {
            CreateMap<SquadCreateDTO, SquadDomain>();
            CreateMap<SquadDomain, SquadReadDTO>();
            CreateMap<SquadEditDTO, SquadDomain>();
        }
    }
}
