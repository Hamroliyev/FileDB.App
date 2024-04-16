using FileDB.App.Brokers.Loggings;
using System;
using System.IO;

namespace FileDB.App.Services.Files
{
    internal class FileService
    {
        private readonly ILoggingBroker loggingBroker;
        private string docPath { get; set; }
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
            try
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
                    catch (UnauthorizedAccessException unAuthTop)
                    {
                        this.loggingBroker.LogInformation($"{unAuthTop.Message}");
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
                            catch (UnauthorizedAccessException unAuthFile)
                            {
                                this.loggingBroker.LogInformation($"unAuthFile: {unAuthFile.Message}");
                            }
                        }
                    }
                    catch (UnauthorizedAccessException unAuthSubDir)
                    {
                        this.loggingBroker.LogInformation($"unAuthSubDir: {unAuthSubDir.Message}");
                    }
                }
                this.loggingBroker.LogInformation("Total size of files " + totalSize);
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                this.loggingBroker.LogInformation($"{dirNotFound.Message}");
            }
            catch (UnauthorizedAccessException unAuthDir)
            {
                this.loggingBroker.LogInformation($"unAuthDir: {unAuthDir.Message}");
            }
            catch (PathTooLongException longPath)
            {
                this.loggingBroker.LogInformation($"{longPath.Message}");
            }
        }
    }
}