using AutoMapper;
using SmartEats.DTOs.Confirms;
using SmartEats.DTOs.Justifies;
using SmartEats.Models.Confirms;
using SmartEats.Models.Justifies;

namespace SmartEats.Profiles.Justifies
{
    public class JustifyProfile : Profile
    {
        public JustifyProfile()
        {
            CreateMap<CreateJustifyDTO, Justify>();
            CreateMap<ConfirmJustifyDTO, Justify>();
            CreateMap<Justify, ReadJustifyDTO>();
        }
    }
}
