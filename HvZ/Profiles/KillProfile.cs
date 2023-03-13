using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.KillDTO;

namespace HvZ.Profiles
{
    public class KillProfile : Profile
    {
        public KillProfile()
        {
            CreateMap<KillCreateDTO, KillDomain>();
            CreateMap<KillDomain, KillReadDTO>();
            CreateMap<KillEditDTO, KillDomain>();
        }
    }
}
