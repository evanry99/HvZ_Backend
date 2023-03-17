using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.MissionDTO;

namespace HvZ.Profiles
{
    public class MissionProfile : Profile
    {
        public MissionProfile() 
        {
            CreateMap<MissionCreateDTO, MissionDomain>();
            CreateMap<MissionDomain, MissionReadDTO>();
            CreateMap<MissionEditDTO, MissionDomain>();
        }
    }
}
