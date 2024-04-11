using FileDB.App.Models.Users;
using System.Collections.Generic;

namespace FileDB.App.Services.Users
{
    internal interface IUserService
    {
        User AddUser(User user);
        User UpdateUser(User user);
        List<User> GetAllUsers();
        bool DeleteUser(int id);
    }
}
