using FileDB.App.Brokers.Loggings;
using FileDB.App.Brokers.Storages;
using FileDB.App.Models.Users;
using System;
using System.Collections.Generic;

namespace FileDB.App.Services.Users
{
    internal class UserService : IUserService
    {
        private readonly IStorageBroker storageBroker;
        private readonly ILoggingBroker loggingBroker;

        public UserService()
        {
            this.storageBroker = new JsonStorageBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public User AddUser(User user)
        {
            return user is null
                ? CreateAndLogInvalidUser()
                : ValidateAndAddUser(user);
        }

        public List<User> GetAllUsers()
        {
            List<User> users = this.storageBroker.ReadAllUsers();

            foreach (User user in users)
            {
                this.loggingBroker.LogInforamation($"{user.Id}, {user.Name}");
            }

            this.loggingBroker.LogInforamation("===End of users");

            return users;
        }

        private User CreateAndLogInvalidUser()
        {
            this.loggingBroker.LogError("User is invalid");
            return new User();
        }

        private User ValidateAndAddUser(User user)
        {
            if (user.Id is 0 || String.IsNullOrWhiteSpace(user.Name))
            {
                this.loggingBroker.LogError("User details missing.");
                return new User();
            }
            else
            {
                this.loggingBroker.LogInforamation("User is created successfully");
                return this.storageBroker.AddUser(user);
            }
        }

        public bool DeleteUser(int id)
        {
            bool isDeleted = false;
            List<User> users = this.storageBroker.ReadAllUsers();
            isDeleted = this.storageBroker.DeleteUser(id);
            this.loggingBroker.LogError($"User with ID {id} not found.");

            return isDeleted;
        }

        public User UpdateUser(User user)
        {
            if (user is null)
            {
                this.loggingBroker.LogError("Your user is empty");
                return new User();
            }

            if (user.Id == 0 || String.IsNullOrEmpty(user.Name))
            {
                this.loggingBroker.LogError("Your user is invalid");
            }

            this.storageBroker.UpdateUser(user);

            return user;
        }
    }
}
