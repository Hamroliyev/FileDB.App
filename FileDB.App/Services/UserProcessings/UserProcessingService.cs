using FileDB.App.Models.Users;
using FileDB.App.Services.Identities;
using FileDB.App.Services.Users;

namespace FileDB.App.Services.UserProcessings
{
    internal class UserProcessingService
    {
        private readonly IUserService userService;
        private readonly IIdentityService identityService;

        public UserProcessingService(IUserService userService,
                    IIdentityService identitiyService)
        {
            this.userService = userService;
            this.identityService = identitiyService;
        }

        public User CreateNewUser(string name)
        {
            User user = new User();
            user.Id = this.identityService.GetNewId();
            user.Name = name;
            this.userService.AddUser(user);

            return user;
        }

        public void DisplayUsers() =>
            this.userService.GetAllUsers();

        public bool DeleteUser(int id) =>
            this.userService.DeleteUser(id);

        public void UpdateUser(string name)
        {
            User user = new User();
            user.Name = name;
            this.userService.UpdateUser(user);
        }
    }
}
