using Application.Exceptions;
using Application.Interfaces;
using Domain.DomainService;
using Domain.DTO;
using Domain.Entities;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnit _unit;
        private readonly IUserDomainService _userDomainService;
        public UserService(IUnit unit, IUserDomainService userDomainService)
        {
            _unit = unit;
            _userDomainService = userDomainService;
        }
        public async Task AddAsync(CreateUserDto userDto, CancellationToken ct = default)
        {
            await _userDomainService.TryCheckNationalCodeDuplicationAsync(userDto.NationalCode, ct);
            var user = User.Create(userDto);
            await _unit.UserRepository.RegisterUserAsync(user, ct);
            await _unit.SaveChangesAsync(ct);
        }

        public async Task DeleteAsync(long userId, CancellationToken ct = default)
        {
            var user = await _unit.UserRepository.GetAsync(userId, ct);
            IfUserIsNullThrowException(user);

            user!.Delete();
            await _unit.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(long userId, UpdateUserDto updateUserDto, CancellationToken ct = default)
        {
            await _userDomainService.TryCheckNationalCodeDuplicationAsync(updateUserDto.NationalCode, ct);

            var user = await _unit.UserRepository.GetAsync(userId, ct);
            IfUserIsNullThrowException(user);
            
            user!.Update(updateUserDto);
            await _unit.SaveChangesAsync(ct);
        }

        public async Task<List<User?>> GetAllAsync(CancellationToken ct = default)
        {
            var users = await _unit.UserRepository.GetAllAsync(ct);
            return users.ToList();
        }

        public async Task<User?> GetAsync(long userId, CancellationToken ct = default)
        {
            var user = await _unit.UserRepository.GetAsync(userId, ct);
            IfUserIsNullThrowException(user);


            return user;
        }

        public async Task<User?> GetAsync(string nationalCode, CancellationToken ct = default)
        {
            var user = await _unit.UserRepository.GetAsync(nationalCode, ct);
            IfUserIsNullThrowException(user);
            
            return user;
        }

        private static void IfUserIsNullThrowException(User? user)
        {
            if (user is null)
            {
                throw new UserNotFoundException("user not found");
            }
        }
    }
}
