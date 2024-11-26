using Domain.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetAsync(long userId,CancellationToken ct = default);
        Task<User?> GetAsync(string nationalCode,CancellationToken ct = default);
        Task<List<User?>> GetAllAsync(CancellationToken ct = default);
        Task DeleteAsync(long userId,CancellationToken ct = default);
        Task UpdateAsync(long userId,UpdateUserDto updateUserDto,CancellationToken ct = default);
        Task AddAsync(CreateUserDto userDto,CancellationToken ct = default);
    }
}
