using System;

namespace FileDB.App.Brokers.Loggings
{
    internal class LoggingBroker : ILoggingBroker
    {
        public void LogInforamation(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void LogError(string userMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(userMessage);
            Console.ResetColor();
        }
    }
}
