using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.ChatDTO;

namespace HvZ.Profiles
{
    public class ChatProfile : Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatCreateDTO, ChatDomain>();
            CreateMap<ChatDomain, ChatReadDTO>();
            CreateMap<ChatEditDTO, ChatDomain>();
        }
    }
}
