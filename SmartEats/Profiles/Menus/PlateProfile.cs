using AutoMapper;
using SmartEats.DTOs.Menus;
using SmartEats.Models.Menus;

namespace SmartEats.Profiles.Menus
{
    public class PlateProfile : Profile
    {
        public PlateProfile()
        {
            CreateMap<CreatePlateDTO, PlateDay>();
            CreateMap<PlateDay, ReadPlateDto>();
        }
    }
}
