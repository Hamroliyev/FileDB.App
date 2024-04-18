using FileDB.App.Brokers.Loggings;
using System;
using System.IO;

namespace FileDB.App.Services.Files
{
    internal class FileService : IFileService
    {
        private readonly ILoggingBroker loggingBroker;
        private string docPath;
        private readonly DirectoryInfo root;
        private long totalSize;
        public FileService(string docPath, ILoggingBroker loggingBroker)
        {
            root = new DirectoryInfo(docPath);
            totalSize = 0;
            this.loggingBroker = loggingBroker;
        }

        public void GetFileSize()
        {
            foreach (FileInfo fileInfo in root.EnumerateFiles())
            {
                try
                {
                    if (fileInfo.Length >= 0)
                    {
                        this.loggingBroker.LogInformation($"{fileInfo.FullName}\t\t{fileInfo.Length.ToString("N0")}");
                        totalSize = totalSize + fileInfo.Length;
                    }
                }
                catch (Exception exception)
                {
                    this.loggingBroker.LogError($"{exception.Message}");
                }
            }

            foreach (DirectoryInfo directoryInfo in root.EnumerateDirectories("*"))
            {
                try
                {
                    foreach (FileInfo fileInfo in directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories))
                    {
                        if (fileInfo.Length >= 0)
                        {
                            this.loggingBroker.LogInformation($"{fileInfo.FullName}\t\t{fileInfo.Length.ToString("N0")}");
                            totalSize += fileInfo.Length;
                        }
                    }
                }
                catch (Exception exception)
                {
                    this.loggingBroker.LogInformation($"Error: {exception.Message}");
                }
            }

            this.loggingBroker.LogInformation("Total size of files " + totalSize);
        }
    }
}