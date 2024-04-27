using FileDB.App.Models.Users;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FileDB.App.Brokers.Storages
{
    internal class FileStorageBroker : IStorageBroker
    {
        private const string FilePath = "../../../Assets/Users.txt";
        public FileStorageBroker()
        {
            EnsureFileExists();
        }

        public async Task<User> AddUserAsync(User user)
        {
            string userLine = $"{user.Id}*{user.Name}\n";
            await File.AppendAllTextAsync(FilePath, userLine);

            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            List<User> users = await ReadAllUsersAsync();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == user.Id)
                {
                    users[i] = user;
                }
            }

            await File.WriteAllTextAsync(FilePath, string.Empty);

            foreach (User userLine in users)
            {
                await AddUserAsync(userLine);
            }

            return user;
        }

        public async Task<List<User>> ReadAllUsersAsync()
        {
            string[] userLines = await File.ReadAllLinesAsync(FilePath);
            List<User> users = new List<User>();

            foreach (string userLine in userLines)
            {
                string[] userProperties = userLine.Split("*");
                User user = new User
                {
                    Id = Convert.ToInt32(userProperties[0]),
                    Name = userProperties[1],
                };
                users.Add(user);
            }

            return users;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            bool isDeleted = false;
            string[] userLines = await File.ReadAllLinesAsync(FilePath);
            int userLength = userLines.Length;
            await File.WriteAllTextAsync(FilePath, String.Empty);

            for (int iterator = 0; iterator < userLength; iterator++)
            {
                string userLine = userLines[iterator];
                string[] contactProperties = userLine.Split("*");

                if (contactProperties[0] == id.ToString())
                {
                    isDeleted = true;
                }
                else
                {
                    await File.AppendAllTextAsync(FilePath, userLine);
                }
            }

            return isDeleted;
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
