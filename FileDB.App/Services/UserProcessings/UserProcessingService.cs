using FileDB.App.Models.Users;
using FileDB.App.Services.Identities;
using FileDB.App.Services.Users;

namespace FileDB.App.Services.UserProcessings
{
    internal class UserProcessingService : IUserProcessingService
    {
        private readonly IUserService userService;
        private readonly IIdentityService identityService;

        public UserProcessingService(IUserService userService,
                    IIdentityService identitiyService)
        {
            this.userService = userService;
            this.identityService = identitiyService;
        }

        public User CreateNewUser(User user)
        {
            user.Id = this.identityService.GetNewId();
            this.userService.AddUser(user);

            return user;
        }

        public void RetrieveUsers() =>
            this.userService.GetAllUsers();

        public bool DeleteUser(int id) =>
            this.userService.DeleteUser(id);

        public void UpdateUser(User user) =>
            this.userService.UpdateUserAsync(user);
    }
}
