using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartEats.DataBase;
using SmartEats.DTOs.Users;
using SmartEats.Enums.Users;
using SmartEats.Models.Users;
using SmartEats.Repositories.Users;
using System.Security.Claims;

namespace SmartEats.Services.Users
{
    public class UserService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private TokenService _tokenService;
        public readonly IUsersRepository _usersRepository;


        public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService, IUsersRepository usersRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _usersRepository = usersRepository;
        }

        public async Task RegisterAsync(CreateUserDTO usuarioDto)
        {
            User usuario = _mapper.Map<User>(usuarioDto);
            IdentityResult resultado = await _userManager.CreateAsync(usuario, usuarioDto.Password);
            if (resultado.Succeeded)
                return;
            throw new ApplicationException("Houve uma falha ao cadastrar o usuário");
        }

        public async Task<string> Login(LoginUserDTO dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Usuário ou/e senha invalidos");
            }
            var user = _signInManager
                                .UserManager
                                .Users.FirstOrDefault(userdb => userdb.NormalizedUserName == dto.Email.ToUpper());
            if(!user.Ativo)
            {
                throw new UnauthorizedAccessException("Usuário Inativado");
            }

            var token = _tokenService.GenerateToken(user);
            return token;
        }

        public async Task<bool> UpdateUser(string id, EditUserDTO editUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                _mapper.Map(editUser, user);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;

            }catch(Exception ex)
            {
                return false;
            }

        }
        public async Task<bool> UpdateUser(string id, UpdateUserDTO editUser)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                _mapper.Map(editUser, user);
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public async Task<IList<ReadUserDTO>> ListUsersCompany(int id)
        {
            var listaDeUsuarios = await _usersRepository.Search().Where(user => user.Id_Company == id && user.TypeUser != TypeUser.Administrador && user.TypeUser != TypeUser.Empresa).ToListAsync();
            var readListDto = _mapper.Map<List<ReadUserDTO>>(listaDeUsuarios);
            return readListDto;
        }
        public async Task<bool> ChangePassword(string id, PasswordChangeDTO passwordChangeDTO)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var result = await _userManager.ChangePasswordAsync(user!,passwordChangeDTO.CurrentPassword,passwordChangeDTO.NewPassword);                
                
                if (result.Succeeded)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<ReadUserDTO> GetUser(string idUsuario)
        {
            var user = await _userManager.FindByIdAsync(idUsuario);
            var result = _mapper.Map<ReadUserDTO>(user);
            return result;
        }
    }
}
