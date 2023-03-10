using AutoMapper;
using HvZ.Model.Domain;
using HvZ.Model.DTO.UserDTO;

namespace HvZ.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile() {
            CreateMap<UserCreateDTO, UserDomain>();
            CreateMap<UserDomain, UserReadDTO>();
            CreateMap<UserDomain, UserEditDTO>();
        }
    }
}
