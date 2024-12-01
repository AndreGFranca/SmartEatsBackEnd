using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SmartEats.DTOs.Users;
using SmartEats.Models.Justifies;
using SmartEats.Models.Users;
using SmartEats.Repositories.Users;

namespace SmartEats.Services.Users
{
    public class PasswordResetService
    {
        private readonly IPasswordResetRepository _repository;
        private readonly IMapper _mapper;
        public PasswordResetService(IPasswordResetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddCode(CreatePasswordCodeDTO passwordResetCode)
        {
            var newPasswordResetCode = _mapper.Map<PasswordResetCode>(passwordResetCode);
            await _repository.Add(newPasswordResetCode);
            _repository.Dispose();
        }
        public async Task<bool> ValidateCode(string email, string code)
        {
            var resetCode = await _repository
                            .Search()
                            .FirstOrDefaultAsync(c =>
                                                c.Email == email &&
                                                c.Code == code &&
                                                c.Expiration > DateTime.Now
                                           );


            return resetCode != null;
        }
    }
}
