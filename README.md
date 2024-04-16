# FileDB.App
This development is about using files as a database (I know this is ridiculous idea)

# .NET Console App - Text/JSON Database CRUD

This .NET console application allows you to use either a text file or a JSON file as a database to perform full CRUD (Create, Read, Update, Delete) operations. 

Installation and Setup

1. Clone the repository to your local machine using the following command:
      git clone [here](https://github.com/Hamroliyev/FileDB.App.git)
   

2. Open the solution in your preferred IDE (Visual Studio, VS Code, etc.)

3. Build the solution to restore the dependencies.

4. Run the application.

Usage

- Upon running the application, you will be prompted to choose the type of file database you would like to use (txt or JSON).
- Follow the on-screen instructions to perform CRUD operations on the selected file database.
- You can create new records, read existing records, update records, and delete records.

File Database

The application allows you to use either a text file or a JSON file as the database for storing records. The choice of using a text file (.txt) or a JSON file (.json) will determine the structure of the database.

Contributing

Contributions are welcome! If you find any issues or would like to add new features, feel free to open a pull request.

If you have any questions or suggestions, please feel free to contact the maintainer at hamroliyevahmadjon1909@gmail.com.

Thank you for using our .NET Console App for CRUD operations with text and JSON file databases!

Demo
![result](https://github.com/Hamroliyev/FileDB.App/assets/90793925/bb4b3bc5-ef06-40a5-b50f-c4b649ea93cc)

=======

```cs
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
                  FileService fileService = new FileService("../../../Assets/", new LoggingBroker());
                  UserProcessingService userProcessingService = RegisterUserProcessingService();
      
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
                                  userProcessingService.DisplayUsers();
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
                  Console.WriteLine("\t =====  1.Create User  =====");
                  Console.WriteLine("\t =====  2.Display User =====");
                  Console.WriteLine("\t =====  3.Delete User by id    =====");
                  Console.WriteLine("\t =====  4.Update User by id    =====");
                  Console.WriteLine("\t =====  5.Calculate files sizes    =====");
                  Console.WriteLine("\t =====  0.Exit =====");
              }
          }
      }
```
