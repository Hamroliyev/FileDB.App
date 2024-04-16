using FileDB.App.Brokers.Loggings;
using System;
using System.IO;

namespace FileDB.App.Services.Files
{
    internal class FileService
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
                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    this.loggingBroker.LogInformation($"{unauthorizedAccessException.Message}");
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
                                this.loggingBroker.LogInformation($"{fileInfo.FullName}\t\t{fileInfo.Length.ToString("N0")}");
                                totalSize += fileInfo.Length;
                            }
                        }
                        catch (UnauthorizedAccessException unauthorizedAccessException)
                        {
                            this.loggingBroker.LogInformation($"unAuthFile: {unauthorizedAccessException.Message}");
                        }
                    }
                }
                catch (UnauthorizedAccessException unauthorizedAccessException)
                {
                    this.loggingBroker.LogInformation($"unAuthSubDir: {unauthorizedAccessException.Message}");
                }
            }
            this.loggingBroker.LogInformation("Total size of files " + totalSize);
        }
    }
}