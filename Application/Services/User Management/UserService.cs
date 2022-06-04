using Application.Extensions;
using Application.Services.User_Management.Dto;
using Application.Services.User_Management.DTO;
using Infrastructure.Entities;
using Infrastructure.IConfiguration;
using Microsoft.EntityFrameworkCore;
using Shared.Dto;

namespace Application.Services.User_Management
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<string>> CreateUser(string userName)
        {
            var result = new Result<string>();

            var user = await _unitOfWork.User
                .Query(x => x.UserName.ToLower() == userName.ToLower())
                .FirstOrDefaultAsync();


            if (user != null)
            {
                result.Error = new Error()
                { Message = $"User With This UserName = {userName} already exists", Code = 404 };
                return result;
            }

            await _unitOfWork.User.AddAsync(new User() { UserName = userName });
            await _unitOfWork.SaveAsync();
            result.Content = "User Created Successfully";
            return result;
        }

        public async Task<Result<string>> UpdateUser(UpdateUserDto requestDto)
        {
            var result = new Result<string>();

            var user = await _unitOfWork.User
                .Find(requestDto.Id);

            if (user == null)
            {
                result.Error = new Error()
                { Message = $"User With This Id = {requestDto.Id} Doesn't exists", Code = 404 };
                return result;
            }

            if (!string.IsNullOrEmpty(requestDto.UserName))
            {
                if (user.UserName == requestDto.UserName)
                {
                    result.Error = new Error()
                    { Message = $"your username is already applied as a {user.UserName}", Code = 400 };
                    return result;
                }

                user.UserName = requestDto.UserName;
            }


            user.IsDisabled = requestDto.Status;

            await _unitOfWork.SaveAsync();
            result.Content = $"{user.UserName} Updated Successfully";
            return result;
        }

        public async Task<Result<bool>> DeleteUser(int id)
        {
            var result = new Result<bool>();

            if (id <= 0)
            {
                result.Error = new Error()
                {
                    Code = 400,
                    Message = "Invalid Id"
                };

                return result;
            }

            var user = await _unitOfWork.User.Find(id);

            if (user == null)
            {
                result.Error = new Error()
                { Message = $"User With This Id = {id} Doesn't exists", Code = 404 };
                return result;
            }

            _unitOfWork.User.Remove(user);
            await _unitOfWork.SaveAsync();

            result.Content = true;
            return result;
        }

        public async Task<Result<bool>> DisableUser(int id)
        {
            var result = new Result<bool>();

            if (id <= 0)
            {
                result.Error = new Error()
                {
                    Code = 400,
                    Message = "Invalid Id"
                };

                return result;
            }

            var user = await _unitOfWork.User.Find(id);

            if (user == null)
            {
                result.Error = new Error()
                { Message = $"User With This Id = {id} Doesn't exists", Code = 404 };
                return result;
            }

            user.IsDisabled = true;
            result.Content = user.IsDisabled;
            await _unitOfWork.SaveAsync();
            return result;
        }

        public async Task<PagedResult<User>> GetAll(FilterParameters filterQuery)
        {
            var result = new PagedResult<User>();
            var usersQuery = _unitOfWork.User.Query();

            if (!string.IsNullOrEmpty(filterQuery.UserName))
            {
                var filteredUsers = await usersQuery
                    .Where(u => u.UserName.Contains(filterQuery.UserName.ToLowerInvariant()))
                    .PaginateAsync(filterQuery.Page, filterQuery.PageSize);

                result = filteredUsers;
                return result;
            }

            return await usersQuery.PaginateAsync(filterQuery.Page, filterQuery.PageSize);
        }

        public async Task<bool> CheckIfUserExists(string userName)
        {
            var user = await _unitOfWork.User
                .Query(x => x.UserName.ToLower() == userName.ToLower())
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return false;
            }

            return true;
        }
    }
}
