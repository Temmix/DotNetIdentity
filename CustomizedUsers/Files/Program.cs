using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Files
{
    class Program
    {
        const string storageAccountName = "temmix";
        const string storageAccountKey = "8mOONKsUsmfAzDm48k2D9tp1+12aLrdLFw9UnCglVnd/lu+nzWDOoNMt+KIpzAm6IWy1phq4BGFgdo/XK9H8lA==";
 
        static void Main(string[] args)
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), true);
            var fileClient = storageAccount.CreateCloudFileClient();
            var share = fileClient.GetShareReference("temmixfiles");
            share.CreateIfNotExists();

            // create file in root Directory
            var rootDir = share.GetRootDirectoryReference();
            rootDir.GetFileReference("readMe.txt").UploadText("-------- READ ME FIRST --------");

            // create folder with files
            var folder1 = rootDir.GetDirectoryReference("folder1");
            folder1.CreateIfNotExists();

            folder1.GetFileReference("file1.txt").UploadText("File1 contents");
            folder1.GetFileReference("file2.txt").UploadText("File2 contents");

            var folder2 = rootDir.GetDirectoryReference("folder2");
            folder2.CreateIfNotExists();

            folder2.GetFileReference("file1.txt").UploadText("File1 contents");
            folder2.GetFileReference("file2.txt").UploadText("File2 contents");

             DownLoadFiles(rootDir, @"C:\Users\Fierce PC\Downloads\temp");
            Console.WriteLine("File Operations completed");
            Console.ReadLine();
        }

        private static void DownLoadFiles(CloudFileDirectory rootDir, string path)
        {
            foreach (var fileItem  in rootDir.ListFilesAndDirectories())
            {
                if (fileItem is CloudFile)
                {
                    var file = ((CloudFile)fileItem);
                    file.DownloadToFile(Path.Combine(path, file.Name), FileMode.Create);
                    Console.WriteLine("{0} is downloaded ", file.Name);
                }
                else if (fileItem is CloudFileDirectory)
                {
                    var dir = ((CloudFileDirectory)fileItem);
                    var dirPath = Path.Combine(path, dir.Name);
                    Directory.CreateDirectory(dirPath);
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                        Console.WriteLine("{0} directory is created ", dir.Name);
                    }
                    DownLoadFiles(dir, dirPath);
                }
            }
        }
    }

}
