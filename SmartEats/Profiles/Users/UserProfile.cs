using AutoMapper;
using SmartEats.DTOs.Users;
using SmartEats.Models.Users;

namespace SmartEats.Profiles.Users
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<CreateUserDTO, User>();
            CreateMap<EditUserDTO, User>();
        }
    }
}
