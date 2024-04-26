using FileDB.App.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FileDB.App.Brokers.Storages
{
    internal interface IStorageBroker
    {
        Task<User> AddUserAsync(User user);
        Task<List<User>> ReadAllUsersAsync();
        Task<User> UpdateUserAsync(User user);
        bool DeleteUser(int id);
    }
}
