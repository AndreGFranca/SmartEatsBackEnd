using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartEats.DTOs.Justifies;
using SmartEats.Models.Justifies;
using SmartEats.Repositories.Justifies;

namespace SmartEats.Services.Justifies
{
    public class JustifyService
    {
        private readonly IJustifiesRepository _justifiesRepository;
        private readonly IMapper _mapper;
        public JustifyService(IMapper mapper, IJustifiesRepository justifiesRepository)
        {
            _justifiesRepository = justifiesRepository;
            _mapper = mapper;
        }
        public async Task<bool> NewJustify(CreateJustifyDTO create)
        {
            var justify = _mapper.Map<Justify>(create);

            await _justifiesRepository.Add(justify);
            _justifiesRepository.Dispose();
            return true;
        }

        public async Task<List<ReadJustifyDTO>> ListJustifies(int company, string idFuncionario)
        {
            var justifies = await _justifiesRepository
                .Search()
                .Where(a => a.IdFuncionario != idFuncionario
                            && a.Funcionario.TypeUser != Enums.Users.TypeUser.Empresa
                            && a.IdEmpresa == company)
                .OrderBy(a => a.Id)
                .ToListAsync();
            _justifiesRepository.Dispose();
            var result = _mapper.Map<List<ReadJustifyDTO>>(justifies);
            return result;

        }

        public async Task<bool> ConfirmJustify(ConfirmJustifyDTO confirmDto)
        {
            var justify = await _justifiesRepository
                .Search()
                .Where(a => a.Id == confirmDto.Id).FirstOrDefaultAsync();

            _mapper.Map(confirmDto, justify);
            await _justifiesRepository.Update(justify);
            _justifiesRepository.Dispose();
            return true;

        }

    }
}
