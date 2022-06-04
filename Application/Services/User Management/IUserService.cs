using Application.Services.User_Management.Dto;
using Application.Services.User_Management.DTO;
using Infrastructure.Entities;
using Shared.Dto;

namespace Application.Services.User_Management
{
    public interface IUserService
    {
        Task<Result<string>> CreateUser(string userName);
        Task<Result<string>> UpdateUser(UpdateUserDto requestDto);
        Task<Result<bool>> DeleteUser(int id);
        Task<Result<bool>> DisableUser(int id);
        Task<PagedResult<User>> GetAll(FilterParameters filterQuery);
        Task<bool> CheckIfUserExists(string userName);
    }
}
