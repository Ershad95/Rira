using Domain.DTO;

namespace Domain.DomainService
{
    public interface IUserDomainService
    {
        Task TryCheckNationalCodeDuplicationAsync(string nationalCode,CancellationToken ct);
    }
}