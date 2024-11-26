using Application;
using Application.Interfaces;
using Domain.DTO;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Rira.Users.GrpcServer.Protos;
using static Rira.Users.GrpcServer.Protos.UserService;
using Empty = Rira.Users.GrpcServer.Protos.Empty;

namespace GrpcServices
{
    public class UserGrpcService : UserServiceBase
    {
        private readonly IUserService _userService;
        public UserGrpcService(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task<Empty> RegisterAsync(UserRequest request, ServerCallContext context)
        {
            TryValidateRequest(request);

            // todo  : use auto mapper
            var createUser = new CreateUserDto
            {
                BirthDate = request.BirthDate.ToDateTime(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                NationalCode = request.NationalCode
            };
            
            await _userService.AddAsync(createUser, context.CancellationToken);

            return new Empty();
        }


        public override async Task<Empty> UpdateAsync(UserUpdateRequest request, ServerCallContext context)
        {
            TryValidateRequest(request);
            
            // todo  : use auto mapper

            var userInfo = new UpdateUserDto
            {
                BirthDate = request.UserRequest.BirthDate.ToDateTime(),
                FirstName = request.UserRequest.FirstName,
                LastName = request.UserRequest.LastName,
                NationalCode = request.UserRequest.NationalCode
            };
            await _userService.UpdateAsync(request.UserId,userInfo, context.CancellationToken);

            return new Empty();
        }


        public override async Task<Empty> DeleteAsync(UserIdRequest request, ServerCallContext context)
        {
            TryValidateRequest(request);
            await _userService.DeleteAsync(request.Id,context.CancellationToken);
            return new Empty();
        }


        public override async Task<UserReply> GetByNationalCodeAsync(UserNationalCodeRequest request, ServerCallContext context)
        {
            TryValidateRequest(request);

            var user = await _userService.GetAsync(request.NationalCode);

            return new UserReply()
            {
                NationalCode = user.NationalCode,
                BirthDate = user.BirthDate.ToUniversalTime().ToTimestamp(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            };
        }

        public override async Task<UserReply> GetByUserIdAsync(UserIdRequest request, ServerCallContext context)
        {
            TryValidateRequest(request);

            var user = await _userService.GetAsync(request.Id);
            // todo  : use auto mapper

            return new UserReply()
            {
                NationalCode = user.NationalCode,
                BirthDate = user.BirthDate.ToUniversalTime().ToTimestamp(),
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id
            };
        }

        public override async Task<UsersReply> GetAllAsync(Empty request, ServerCallContext context)
        {
            var users = await _userService.GetAllAsync(context.CancellationToken);
            // todo  : use auto mapper

            var userReplies = users.Select(x => new UserReply()
            {
                BirthDate = x.BirthDate.ToUniversalTime().ToTimestamp(),
                FirstName = x.FirstName,
                LastName = x.LastName,
                NationalCode = x.NationalCode,
                Id= x.Id
            });

            var usersReply = new UsersReply();
            usersReply.Items.AddRange(userReplies);
            return usersReply;
        }

       
        
        private static void TryValidateRequest(UserRequest userRequest)
        {
            if (string.IsNullOrWhiteSpace(userRequest.FirstName))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.FirstName)} is invalid"));
            }

            if (string.IsNullOrWhiteSpace(userRequest.LastName))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.LastName)} is invalid"));
            }

            if (string.IsNullOrWhiteSpace(userRequest.NationalCode))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.NationalCode)} is invalid"));
            }

            if (userRequest.BirthDate is null || userRequest.BirthDate.ToDateTime() == default)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.BirthDate)} is invalid"));
            }
        }

        private static void TryValidateRequest(UserUpdateRequest userRequest)
        {
            if (string.IsNullOrWhiteSpace(userRequest.UserRequest.FirstName))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.UserRequest.FirstName)} is invalid"));
            }

            if (string.IsNullOrWhiteSpace(userRequest.UserRequest.LastName))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.UserRequest.LastName)} is invalid"));
            }

            if (string.IsNullOrWhiteSpace(userRequest.UserRequest.NationalCode))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.UserRequest.NationalCode)} is invalid"));
            }

            if (userRequest.UserRequest.BirthDate is null || userRequest.UserRequest.BirthDate.ToDateTime() == default)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.UserRequest.BirthDate)} is invalid"));
            }
        }

        private static void TryValidateRequest(UserIdRequest userRequest)
        {
            if (userRequest.Id <=0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.Id)} is invalid"));
            }
        }

        private static void TryValidateRequest(UserNationalCodeRequest userRequest)
        {
            if (string.IsNullOrWhiteSpace(userRequest.NationalCode))
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, $"{nameof(userRequest.NationalCode)} is invalid"));
            }
        }
    }
}
