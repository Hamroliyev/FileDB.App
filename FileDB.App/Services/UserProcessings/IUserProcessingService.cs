using FileDB.App.Models.Users;

namespace FileDB.App.Services.UserProcessings
{
    internal interface IUserProcessingService
    {
        User CreateNewUser(User user);
        bool DeleteUser(int id);
        void DisplayUsers();
        void UpdateUser(User user);
    }
}