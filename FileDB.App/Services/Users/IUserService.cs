using FileDB.App.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileDB.App.Services.Users
{
    internal interface IUserService
    {
        Task<User> AddUserAsync(User user);
        User UpdateUser(User user);
        List<User> GetAllUsers();
        bool DeleteUser(int id);
    }
}
