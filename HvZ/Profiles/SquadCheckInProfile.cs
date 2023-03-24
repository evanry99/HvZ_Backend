using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.SquadCheckInDTO;

namespace HvZ.Profiles
{
    public class SquadCheckInProfile : Profile
    {
        public SquadCheckInProfile()
        {
            CreateMap<SquadCheckInCreateDTO, SquadCheckInDomain>();
            CreateMap<SquadCheckInDomain, SquadCheckInReadDTO>();
            CreateMap<SquadCheckInEditDTO, SquadCheckInDomain>();
        }
    }
}
