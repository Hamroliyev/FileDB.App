using FileDB.App.Models.Users;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FileDB.App.Brokers.Storages
{
    internal class JsonStorageBroker : IStorageBroker
    {
        private const string FilePath = "../../../Assets/Jsons/Users.json";

        public JsonStorageBroker()
        {
            EnsureFileExists();
        }

        public async Task<User> AddUserAsync(User user)
        {
            string usersString = File.ReadAllText(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersString);
            users.Add(user);
            string serializedUsers = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(FilePath, serializedUsers);

            return user;
        }

        public async Task<List<User>> ReadAllUsersAsync()
        {
            string usersString = await File.ReadAllTextAsync(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersString);

            return users;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            string usersString = await File.ReadAllTextAsync(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersString);
            User updatedUser = users.FirstOrDefault(u => u.Id == user.Id);
            updatedUser.Name = user.Name;
            string serializedUsers = JsonSerializer.Serialize(users);
            await File.WriteAllTextAsync(FilePath, serializedUsers);

            return updatedUser;
        }

        public bool DeleteUser(int id)
        {
            string usersString = File.ReadAllText(FilePath);
            List<User> users = JsonSerializer.Deserialize<List<User>>(usersString);
            User user = users.FirstOrDefault(u => u.Id == id);
            users.Remove(user);
            string serializedUsers = JsonSerializer.Serialize(users);
            File.WriteAllText(FilePath, serializedUsers);

            return true;
        }

        private void EnsureFileExists()
        {
            bool fileExists = File.Exists(FilePath);
            if (fileExists is false)
            {
                File.Create(FilePath).Close();
            }
        }
    }
}
