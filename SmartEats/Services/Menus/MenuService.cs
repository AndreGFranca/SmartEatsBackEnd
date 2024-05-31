﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartEats.DataBase;
using SmartEats.DTOs.Menus;
using SmartEats.Models.Menus;
using SmartEats.Models.Users;
using SmartEats.Repositories.Menus;

namespace SmartEats.Services.Menus
{
    public class MenuService
    {
        public readonly IMenusRepository _menusRepository;
        private IMapper _mapper;
        public MenuService(IMapper mapper, IMenusRepository menusRepository) {
            _menusRepository = menusRepository;
            _mapper = mapper;
        }
        //public Menu GetAllMenusCompanys() {
            
        //}

        public async Task<bool> Register(CreateMenuDTO newMenu)
        {
            try
            {
                Menu menu = _mapper.Map<Menu>(newMenu);
                await _menusRepository.Add(menu);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }     
            
        }

        public async Task<IList<ReadMenuDTO>> GetAllMenusCompany(int Company,DateOnly inicio, DateOnly final)
        {
            var menus = await _menusRepository.Search().Where(a => a.Data >= inicio && a.Data <= final).ToListAsync();
            IList<ReadMenuDTO> readMenus = _mapper.Map<IList<ReadMenuDTO>>(menus);
            return readMenus;
        }
    }
}
