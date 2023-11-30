using E_Commerce_Template.Models.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_Commerce_Template.Models.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> Register(RegisterUserDto data, ModelStateDictionary modelState);

        public Task<UserDto> Authenticate(string username, string password);

        public Task<UserDto> GetUser(string username);
        public Task Logout();
    }
}
