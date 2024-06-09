using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartEats.DataBase;
using SmartEats.DTOs.Menus;
using SmartEats.Models.Confirms;
using SmartEats.Models.Menus;
using SmartEats.Models.Users;
using SmartEats.Repositories.Confirms;
using SmartEats.Repositories.Menus;

namespace SmartEats.Services.Menus
{
    public class MenuService
    {
        public readonly IMenusRepository _menusRepository;
        public readonly IConfirmsRepository _confirmRepository;
        private IMapper _mapper;
        public MenuService(IMapper mapper, IMenusRepository menusRepository, IConfirmsRepository confirmRepository)
        {
            _menusRepository = menusRepository;
            _confirmRepository = confirmRepository;
            _mapper = mapper;
        }
        //public Menu GetAllMenusCompanys() {

        //}

        public async Task<bool> Register(List<CreateMenuDTO> newListMenu)
        {
            try
            {
                List<Menu> menu = _mapper.Map<List<Menu>>(newListMenu);
                await _menusRepository.AddRange(menu);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<IList<ReadMenuDTO>> GetAllMenusCompany(int Company, DateOnly inicio, DateOnly final)
        {
            var menus = await _menusRepository.Search().Where(a =>
                a.Data >= inicio && a.Data <= final && a.IdEmpresa == Company
            ).ToListAsync();
            //var menus = await _menusRepository.Search().ToListAsync();

            IList<ReadMenuDTO> readMenus = _mapper.Map<IList<ReadMenuDTO>>(menus);
            return readMenus;
        }

        public async Task<IList<ReadMenuDTO>> GetAllMenusWorker(int Company, DateOnly inicio, DateOnly final, string idUser)
        {
            // Materialize confirmacoesUsuario to avoid multiple database hits
            var confirmacoesUsuario = await _confirmRepository.Search()
                .Where(a => a.DataConfirmacao >= inicio && a.IdFuncionario == idUser && a.IdEmpresa == Company)
                .ToListAsync();

            var date = DateOnly.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

            // Get all menus within the date range
            var allMenus = await _menusRepository.Search()
                .Where(a => a.IdEmpresa == Company && a.Data >= inicio && a.Data <= final)
                .ToListAsync();

            // Join in memory
            var menusConfirmados = (from menu in allMenus
                                    join confirmacao in confirmacoesUsuario
                                    on new { menu.Data, menu.IdEmpresa } equals new { Data = confirmacao.DataConfirmacao, confirmacao.IdEmpresa }
                                    select new ReadMenuDTO(menu, _mapper.Map<List<ReadPlateDto>>(menu.PlatesDay),false, confirmacao.HoraDeAlmoco)).ToList();

            var menusVencidos = (from menu in allMenus
                                 where !confirmacoesUsuario.Any(b => b.DataConfirmacao == menu.Data) && menu.Data < date
                                 select new ReadMenuDTO(menu, _mapper.Map<List<ReadPlateDto>>(menu.PlatesDay), false)).ToList();

            var menusNaoConfirmados = (from menu in allMenus
                                       where !confirmacoesUsuario.Any(b => b.DataConfirmacao == menu.Data) && menu.Data >= date
                                       select new ReadMenuDTO(menu, _mapper.Map<List<ReadPlateDto>>(menu.PlatesDay), true)).ToList();

            var readMenus = menusConfirmados.Union(menusNaoConfirmados).Union(menusVencidos).OrderBy(a => a.Data).ToList();
            //var readMenus = menusConfirmados.Union(menusNaoConfirmados).Union(menusVencidos).OrderBy(a => a.Data).ToList();
            return readMenus;
        }
    }
}
