using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.SquadMemberDTO;

namespace HvZ.Profiles
{
    public class SquadMemberProfile : Profile
    {
        public SquadMemberProfile() 
        {
            CreateMap<SquadMemberCreateDTO, SquadMemberDomain>();
            CreateMap<SquadMemberDomain, SquadMemberReadDTO>();
            CreateMap<SquadMemberEditDTO, SquadMemberDomain>();
        }
    }
}
