using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task RegisterUserAsync(User? user, CancellationToken ct = default)
    {
       await _applicationDbContext.Users.AddAsync(user, ct);
    }

    public async Task<User?> GetAsync(string nationalCode, CancellationToken ct = default)
    {
        return await _applicationDbContext.Users.FirstOrDefaultAsync(x => x != null && x.NationalCode == nationalCode, ct);
    }

    public async Task<User?> GetAsync(long id, CancellationToken ct = default)
    {
        return await _applicationDbContext.Users.FirstOrDefaultAsync(x => x != null && x.Id == id, ct);
    }

    public async Task<List<User?>> GetAllAsync(CancellationToken ct = default)
    {
        return await _applicationDbContext.Users.ToListAsync(ct);
    }
}