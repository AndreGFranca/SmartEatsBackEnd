using SmartEats.Models.Menus;
using System.ComponentModel.DataAnnotations;

namespace SmartEats.DTOs.Menus
{
    public class ReadMenuDTO
    {
        public ReadMenuDTO()
        {
            
        }

        public ReadMenuDTO(Menu menu, List<ReadPlateDto> lista, bool editable)
        {
            Data = menu.Data;
            IdEmpresa = menu.IdEmpresa;
            PlatesDay = lista;
            Data = menu.Data;
            Editable = editable;
        }
        public ReadMenuDTO(Menu menu, List<ReadPlateDto> lista, bool editable, TimeOnly? horarioAlmoco)
        {
            Data = menu.Data;
            IdEmpresa = menu.IdEmpresa;
            PlatesDay = lista;
            Data = menu.Data;
            Editable = editable;
            HorarioAlmoco = horarioAlmoco;

        }
        public DateOnly Data { get; set; }
        public int IdEmpresa { get; set; }
        public IList<ReadPlateDto> PlatesDay { get; set; }
        public bool Editable { get; set; } = false;
        public TimeOnly? HorarioAlmoco{ get; set; }
    }
}
