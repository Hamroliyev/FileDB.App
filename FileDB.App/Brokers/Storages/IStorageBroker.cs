using FileDB.App.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileDB.App.Brokers.Storages
{
    internal interface IStorageBroker
    {
        Task<User> AddUserAsync(User user);
        List<User> ReadAllUsers();
        User UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
