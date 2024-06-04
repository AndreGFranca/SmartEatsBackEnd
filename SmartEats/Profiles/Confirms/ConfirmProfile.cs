using AutoMapper;
using SmartEats.DTOs.Confirms;
using SmartEats.DTOs.Menus;
using SmartEats.Models.Confirms;
using SmartEats.Models.Menus;

namespace SmartEats.Profiles.Confirms
{
    public class ConfirmProfile: Profile
    {
        public ConfirmProfile()
        {
            CreateMap<CreateConfirmDTO, Confirm>();
            //CreateMap<Menu, ReadMenuDTO>();
        }
    }
}
