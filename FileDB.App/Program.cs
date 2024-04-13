using FileDB.App.Brokers.Loggings;
using FileDB.App.Brokers.Storages;
using FileDB.App.Services.Identities;
using FileDB.App.Services.UserProcessings;
using FileDB.App.Services.Users;
using System;
using System.IO;
using System.Reflection;

namespace FileDB.App
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string docPath = "../../../Assets/";
            DirectoryInfo root = new DirectoryInfo(docPath);
            long totalSize = 0;

            try
            {
                foreach (FileInfo fileInfo in root.EnumerateFiles())
                {
                    try
                    {
                        if (fileInfo.Length >= 0)
                        {
                            Console.WriteLine($"{fileInfo.FullName}\t\t{fileInfo.Length.ToString("N0")}");
                            totalSize = totalSize + fileInfo.Length;
                        }
                    }
                    catch (UnauthorizedAccessException unAuthTop)
                    {
                        Console.WriteLine($"{unAuthTop.Message}");
                    }
                }

                foreach (DirectoryInfo directoryInfo in root.EnumerateDirectories("*"))
                {
                    try
                    {
                        foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories))
                        {
                            try
                            {
                                if (fileInfo.Length >= 0)
                                {
                                    Console.WriteLine($"{fileInfo.FullName}\t\t{fileInfo.Length.ToString("N0")}");
                                    totalSize += fileInfo.Length;
                                }
                            }
                            catch (UnauthorizedAccessException unAuthFile)
                            {
                                Console.WriteLine($"unAuthFile: {unAuthFile.Message}");
                            }
                        }
                    }
                    catch (UnauthorizedAccessException unAuthSubDir)
                    {
                        Console.WriteLine($"unAuthSubDir: {unAuthSubDir.Message}");
                    }
                }
                Console.WriteLine("Total size of files " + totalSize);
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine($"{dirNotFound.Message}");
            }
            catch (UnauthorizedAccessException unAuthDir)
            {
                Console.WriteLine($"unAuthDir: {unAuthDir.Message}");
            }
            catch (PathTooLongException longPath)
            {
                Console.WriteLine($"{longPath.Message}");
            }

            //string userChoice;
            //UserProcessingService userProcessingService = RegisterUserProcessingService();

            //do
            //{
            //    PrintMenu();
            //    Console.Write("Enter your choice:");
            //    userChoice = Console.ReadLine();
            //    switch (userChoice)
            //    {
            //        case "1":
            //            Console.Clear();
            //            Console.Write("Enter you name:");
            //            string userName = Console.ReadLine();
            //            User user = new User() { Name = userName };
            //            userProcessingService.CreateNewUser(user);
            //            break;

            //        case "2":
            //            {
            //                Console.Clear();
            //                userProcessingService.DisplayUsers();
            //            }
            //            break;

            //        case "3":
            //            {
            //                Console.Clear();
            //                Console.WriteLine("Enter an id which you want to delete");
            //                Console.Write("Enter id:");
            //                string deleteWithIdStr = Console.ReadLine();
            //                int deleteWithId = Convert.ToInt32(deleteWithIdStr);
            //                userProcessingService.DeleteUser(deleteWithId);
            //            }
            //            break;

            //        case "4":
            //            {
            //                Console.Clear();
            //                Console.WriteLine("Enter an id which you want  to edit");
            //                Console.Write("Enter an id:");
            //                string idStr = Console.ReadLine();
            //                int id = Convert.ToInt32(idStr);
            //                Console.Write("Enter name:");
            //                string name = Console.ReadLine();
            //                User user1 = new User() { Name = name };
            //                userProcessingService.UpdateUser(user1);
            //            }
            //            break;

            //        case "0": break;

            //        default:
            //            Console.WriteLine("You entered wrong input, Try again");
            //            break;
            //    }
            //}
            //while (userChoice != "0");

            //Console.Clear();
            //Console.WriteLine("The app has been finished");
        }

        private static UserProcessingService RegisterUserProcessingService()
        {
            ILoggingBroker loggingBroker = new LoggingBroker();
            IStorageBroker storageBroker = new JsonStorageBroker();
            IUserService userService = new UserService(loggingBroker, storageBroker);
            IdentityService identitiyService = IdentityService.GetInstance(storageBroker);

            UserProcessingService userProcessingService =
                new UserProcessingService(userService,
                        identitiyService);

            return userProcessingService;
        }

        public static void PrintMenu()
        {
            Console.WriteLine("1.Create User");
            Console.WriteLine("2.Display User");
            Console.WriteLine("3.Delete User by id");
            Console.WriteLine("4.Update User by id");
            Console.WriteLine("0.Exit");
        }
    }
}