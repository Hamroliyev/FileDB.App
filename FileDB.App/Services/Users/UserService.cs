using FileDB.App.Brokers.Loggings;
using FileDB.App.Brokers.Storages;
using FileDB.App.Models.Users;
using System;
using System.Collections.Generic;

namespace FileDB.App.Services.Users
{
    internal class UserService : IUserService
    {
        private readonly ILoggingBroker loggingBroker;
        private readonly IStorageBroker storageBroker;

        public UserService(ILoggingBroker loggingBroker,
            IStorageBroker storageBroker)
        {
            this.loggingBroker = loggingBroker;
            this.storageBroker = storageBroker;
        }

        public User AddUser(User user)
        {
            return user is null
                ? CreateAndLogInvalidUser()
                : ValidateAndAddUser(user);
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            try
            {
                users = this.storageBroker.ReadAllUsers();
            }
            catch (Exception exception)
            {
                this.loggingBroker.LogError(exception.Message);

                return new List<User>();
            }

            foreach (User user in users)
            {
                this.loggingBroker.LogInformation($"{user.Id}, {user.Name}");
            }

            this.loggingBroker.LogInformation("=== End of users ===");

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
                this.loggingBroker.LogInformation("User is created successfully");

                return this.storageBroker.AddUserAsync(user);
            }
        }

        public bool DeleteUser(int id)
        {
            bool isDeleted = false;
            List<User> users = new List<User>();

            try
            {
                users = this.storageBroker.ReadAllUsers();
                isDeleted = this.storageBroker.DeleteUser(id);
            }
            catch (Exception exception)
            {
                this.loggingBroker.LogError($"User with ID {id} not found." + "\n" + exception.Message);
            }
                
            return isDeleted;
        }

        public User UpdateUser(User user)
        {
            if (user is null)
            {
                this.loggingBroker.LogError("Your user is empty");
                return new User();
            }

            if (user.Id is 0 || String.IsNullOrEmpty(user.Name))
            {
                this.loggingBroker.LogError("Your user is invalid");
            }

            try
            {
                this.storageBroker.UpdateUser(user);
            }
            catch (Exception exception)
            {
                this.loggingBroker.LogError("Updating failed " + exception.Message);
            }
            
            return user;
        }
    }
}
