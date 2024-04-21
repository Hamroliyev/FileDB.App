using FileDB.App.Brokers.Loggings;
using FileDB.App.Brokers.Storages;
using FileDB.App.Models.Users;
using FileDB.App.Services.Files;
using FileDB.App.Services.Identities;
using FileDB.App.Services.UserProcessings;
using FileDB.App.Services.Users;
using System;

namespace FileDB.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string userChoice;
            IFileService fileService = new FileService("../../../Assets/", new LoggingBroker());
            IUserProcessingService userProcessingService = RegisterUserProcessingService();

            do
            {
                PrintMenu();
                Console.Write("Enter your choice : ");
                userChoice = Console.ReadLine();
                switch (userChoice)
                {
                    case "1":
                        Console.Clear();
                        Console.Write("Enter you name : ");
                        string userName = Console.ReadLine();
                        User user = new User() { Name = userName };
                        userProcessingService.CreateNewUser(user);
                        break;

                    case "2":
                        {
                            Console.Clear();
                            userProcessingService.RetrieveUsers();
                        }
                        break;

                    case "3":
                        {
                            Console.Clear();
                            Console.WriteLine("Enter an id which you want to delete.");
                            Console.Write("Enter id : ");
                            string deleteWithIdStr = Console.ReadLine();
                            int deleteWithId = Convert.ToInt32(deleteWithIdStr);
                            userProcessingService.DeleteUser(deleteWithId);
                        }
                        break;

                    case "4":
                        {
                            Console.Clear();
                            Console.WriteLine("Enter an id which you want  to edit.");
                            Console.Write("Enter an id : ");
                            string idStr = Console.ReadLine();
                            int id = Convert.ToInt32(idStr);
                            Console.Write("Enter name : ");
                            string name = Console.ReadLine();
                            User user1 = new User() { Name = name };
                            userProcessingService.UpdateUser(user1);
                        }
                        break;

                    case "5":
                        {
                            Console.WriteLine("Calculate files size");
                            fileService.GetFileSize();
                        }
                        break;

                    case "0": break;

                    default:
                        Console.WriteLine("You entered wrong input, Try again");
                        break;
                }
            }
            while (userChoice != "0");

            Console.Clear();
            Console.WriteLine("The app has been finished");
        }

        private static IUserProcessingService RegisterUserProcessingService()
        {
            ILoggingBroker loggingBroker = new LoggingBroker();
            IStorageBroker storageBroker = new JsonStorageBroker();
            IUserService userService = new UserService(loggingBroker, storageBroker);
            IdentityService identitiyService = IdentityService.GetInstance(storageBroker);

            IUserProcessingService userProcessingService =
                new UserProcessingService(userService,
                        identitiyService);

            return userProcessingService;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("\t =====  1.Create User  =====");
            Console.WriteLine("\t =====  2.Display User =====");
            Console.WriteLine("\t =====  3.Delete User by id    =====");
            Console.WriteLine("\t =====  4.Update User by id    =====");
            Console.WriteLine("\t =====  5.Calculate files sizes    =====");
            Console.WriteLine("\t =====  0.Exit =====");
        }
    }
}