using AutoMapper;
using SmartEats.DTOs.Menus;
using SmartEats.DTOs.Users;
using SmartEats.Models.Menus;
using SmartEats.Models.Users;

namespace SmartEats.Profiles.Menus
{
    public class MenuProfile: Profile
    {
        public MenuProfile()
        {
            CreateMap<CreateMenuDTO, Menu>();
            CreateMap<Menu, ReadMenuDTO>();
        }
    }
}
