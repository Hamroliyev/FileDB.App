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

        public User UpdateUser(User user)
        {
            List<User> users = ReadAllUsers();

            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Id == user.Id)
                {
                    users[i] = user;
                }
            }

            File.WriteAllText(FilePath, string.Empty);

            foreach (User userLine in users)
            {
                AddUserAsync(userLine);
            }

            return user;
        }

        public List<User> ReadAllUsers()
        {
            string[] userLines = File.ReadAllLines(FilePath);
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

        public bool DeleteUser(int id)
        {
            bool isDeleted = false;
            string[] userLines = File.ReadAllLines(FilePath);
            int userLength = userLines.Length;
            File.WriteAllText(FilePath, String.Empty);

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
                    File.AppendAllText(FilePath, userLine);
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
