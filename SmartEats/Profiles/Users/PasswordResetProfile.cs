using AutoMapper;
using SmartEats.DTOs.Users;
using SmartEats.Models.Users;

namespace SmartEats.Profiles.Users
{
    public class PasswordResetProfile : Profile
    {
        public PasswordResetProfile()
        {
            CreateMap<CreatePasswordCodeDTO, PasswordResetCode>();
        }
    }
}
