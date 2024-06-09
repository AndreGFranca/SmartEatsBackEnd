using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartEats.DTOs.Confirms;
using SmartEats.Extensions;
using SmartEats.Models.Confirms;
using SmartEats.Models.Users;
using SmartEats.Repositories.Confirms;
using SmartEats.Repositories.Justifies;
using System.Reflection.Metadata.Ecma335;

namespace SmartEats.Services.Confirms
{
    public class ConfirmService
    {
        private static readonly TimeZoneInfo BrasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Brazil/East");
        public readonly IConfirmsRepository _confirmsRepository;
        public readonly IJustifiesRepository _justifiesRepository;
        private IMapper _mapper;
        public ConfirmService(IMapper mapper, IConfirmsRepository confirmsRepository, IJustifiesRepository justifiesRepository)
        {
            _confirmsRepository = confirmsRepository;
            _justifiesRepository = justifiesRepository;
            _mapper = mapper;
        }

        public async Task<bool> Register(CreateConfirmDTO createConfirmDTO)
        {
            try
            {
                var model = _mapper.Map<Confirm>(createConfirmDTO);
                await _confirmsRepository.Add(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> RegisterConfirms(List<CreateConfirmDTO> createConfirmDTO)
        {
            try
            {
                var model = _mapper.Map<List<Confirm>>(createConfirmDTO);
                await _confirmsRepository.AddRange(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<GroupCountTimes>> GetCountTimes(int idEmpresa)
        {
            var dataAtual = DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(DateTime.UtcNow, BrasiliaTimeZone).Date);

            var confirmacoes = await _confirmsRepository.Search()
                .Where(a => a.DataConfirmacao == dataAtual && a.Confirmou == true && a.IdEmpresa == idEmpresa)
                .GroupBy(a => a.HoraDeAlmoco)
                .Select(g => new GroupCountTimes
                {
                    HorarioAlmoco = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var availableTimes = GetAllTimes();

            var result = availableTimes
                .GroupJoin(confirmacoes,
                           time => time,
                           confirmacao => confirmacao.HorarioAlmoco,
                           (time, confirmacaoGroup) => new GroupCountTimes
                           {
                               HorarioAlmoco = time,
                               Count = confirmacaoGroup.Sum(c => c.Count)
                           })
                .ToList();

            return result;
        }
        public List<TimeOnly> GetAllTimes()
        {
            TimeOnly startTime = new TimeOnly(11, 0, 0);

            TimeOnly endTime = new TimeOnly(23, 0, 0);

            List<TimeOnly> availableTimes = new List<TimeOnly>();

            while (startTime <= endTime)
            {
                availableTimes.Add(startTime);
                startTime = startTime.AddMinutes(30);
            }
            return availableTimes;
        }
        public List<TimeOnly> GetAvailableTimes()
        {
            TimeOnly now = TimeOnly.Parse(TimeZoneInfo.ConvertTime(DateTime.UtcNow, BrasiliaTimeZone).TimeOfDay.ToString());

            TimeOnly startTime = new TimeOnly(11, 0, 0);

            TimeOnly endTime = new TimeOnly(23, 0, 0);

            List<TimeOnly> availableTimes = new List<TimeOnly>();

            while (startTime <= endTime)
            {
                if (startTime > now.AddMinutes(30))
                {
                    availableTimes.Add(startTime);
                }
                startTime = startTime.AddMinutes(30);
            }

            return availableTimes;
        }

        public async Task<(int, string)> ConfirmPresenceWorker(string idUser, ConfirmPresenceDTO confirmPresenceDTO)
        {
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.UtcNow.ConvertToBrasiliaTime().Date);
            var funcionario = await _confirmsRepository.Search().Where(a => a.DataConfirmacao == dataAtual && a.IdFuncionario == idUser).FirstOrDefaultAsync();
            if (funcionario == null)
            {
                return (400, "Colaborador não confirmou presença");
            }
            if (funcionario.Compareceu == true)
            {
                return (400, "Colaborador já confirmou comparecimento");
            }
            confirmPresenceDTO.HorarioComparecimento = DateTime.UtcNow.ConvertToBrasiliaTime();
            _mapper.Map(confirmPresenceDTO, funcionario);
            await _confirmsRepository.Update(funcionario);
            return (200, "Comparecimento Confirmado!");
        }

        public async Task<List<ReadConfirmDTO>> NotPresenceOfConfirmedDay(string idFuncionario)
        {
            DateOnly dataAtual = DateOnly.FromDateTime(DateTime.UtcNow.ConvertToBrasiliaTime().Date);            
            var consulta = await _confirmsRepository.Search().Where(
                    a => a.Confirmou == true
                    && a.Compareceu == false
                    && a.DataConfirmacao < dataAtual
                    && a.IdFuncionario == idFuncionario
                    && !_justifiesRepository.Search().Any(j => j.IdConfirmacao == a.Id)
                ).ToListAsync();

            var resultado = _mapper.Map<List<ReadConfirmDTO>>(consulta);

            return resultado;
        }

    }
}
