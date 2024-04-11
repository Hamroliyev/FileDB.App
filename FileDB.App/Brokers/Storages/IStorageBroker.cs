using FileDB.App.Models.Users;
using System.Collections.Generic;

namespace FileDB.App.Brokers.Storages
{
    internal interface IStorageBroker
    {
        User AddUser(User user);
        List<User> ReadAllUsers();
        User UpdateUser(User user);
        bool DeleteUser(int id);
    }
}
