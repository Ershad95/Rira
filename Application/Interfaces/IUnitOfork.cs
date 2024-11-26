using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IUnit
    {
        IUserRepository UserRepository { get; }

        Task SaveChangesAsync(CancellationToken ct);
    }
}
