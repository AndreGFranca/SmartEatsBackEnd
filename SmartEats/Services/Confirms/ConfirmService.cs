using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SmartEats.DTOs.Confirms;
using SmartEats.Models.Confirms;
using SmartEats.Repositories.Confirms;

namespace SmartEats.Services.Confirms
{
    public class ConfirmService
    {
        private static readonly TimeZoneInfo BrasiliaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Brazil/East");
        public readonly IConfirmsRepository _confirmsRepository;
        private IMapper _mapper;
        public ConfirmService(IMapper mapper, IConfirmsRepository confirmsRepository)
        {
            _confirmsRepository = confirmsRepository;
            _mapper = mapper;
        }

        public async Task<bool> Register(CreateConfirmDTO createConfirmDTO) {
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

        public async Task GetCountTimes(int idEmpresa) {
            var dataAtual = DateOnly.Parse(DateTime.Now.Date.ToString("yyyy-MM-dd"));
            var a = await _confirmsRepository.Search()
                .Where(a => a.DataConfirmacao == dataAtual && a.Confirmou == true)
                .GroupBy(a => new {  a.Confirmou, a.HoraDeAlmoco })
                .Select(g => new GroupCountTimes
                {                    
                    HorarioAlmoco = g.Key.HoraDeAlmoco,
                    Count = g.Count()
                })
                .ToListAsync();
            
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
    }
}
