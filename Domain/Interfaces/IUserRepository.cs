using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task RegisterUserAsync(User? userDto,CancellationToken ct=default);

        Task<User?> GetAsync(string nationalCode, CancellationToken ct = default);
        Task<User?> GetAsync(long id, CancellationToken ct = default);
        Task<List<User?>> GetAllAsync(CancellationToken ct = default);
    }
}
