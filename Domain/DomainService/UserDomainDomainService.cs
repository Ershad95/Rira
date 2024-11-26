using Domain.DTO;
using Domain.Exceptions;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainService
{
    public class UserDomainDomainService : IUserDomainService
    {
        private readonly IUserRepository _repository;
        public UserDomainDomainService(IUserRepository userRepository)
        {
            _repository = userRepository;
        }

        public async Task TryCheckNationalCodeDuplicationAsync(string nationalCode,CancellationToken ct)
        {
            var user = await _repository.GetAsync(nationalCode, ct);
            
            var userAlreadyExist = user is not null;
            if(userAlreadyExist)
                throw new NationalCodeAlreadyExistException("user already exist");
        }
    }
}
