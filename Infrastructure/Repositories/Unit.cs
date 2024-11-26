using Application.Interfaces;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class Unit : IUnit
{
    private readonly ApplicationDbContext _applicationDbContext;

    public Unit(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
        UserRepository = new UserRepository(_applicationDbContext);
    }
    public IUserRepository UserRepository { get; }
    
    public async Task SaveChangesAsync(CancellationToken ct)
    {
        await _applicationDbContext.SaveChangesAsync(ct);
    }
}