using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blobs
{
    class Program
    {
        const string storageAccountName = "temmix";
        const string storageAccountKey = "8mOONKsUsmfAzDm48k2D9tp1+12aLrdLFw9UnCglVnd/lu+nzWDOoNMt+KIpzAm6IWy1phq4BGFgdo/XK9H8lA==";
        const string folderPath = @"C:\Users\Fierce PC\Desktop\Images";
        static void Main(string[] args)
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), true);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("hip-hop-stars");

            // This is for uploading blobs
             UploadBlobs(storageAccount, blobClient, container);

            // This is for downloading blobs
            if (container.Exists())
            {
                var blobs = container.ListBlobs();
                DownloadBlobs(storageAccount, blobClient, container, blobs);
            }

            // This is for deleting blobs
            if (container.Exists())
            {
                var blobs = container.ListBlobs();
                DeleteBlobs(storageAccount, blobClient, container, blobs);
            }

            Console.ReadLine(); ;
        }

        private static void UploadBlobs(CloudStorageAccount storageAccount, CloudBlobClient blobClient, CloudBlobContainer container)
        {
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            foreach (var filePath in Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories))
            {
                var azureFileName = Guid.NewGuid().ToString() + ".jpg";
                var blob = container.GetBlockBlobReference(azureFileName);
                blob.UploadFromFile(filePath, FileMode.Open);
                Console.WriteLine("File {0} uploaded as {1} ", filePath, azureFileName);
            }

            Console.WriteLine("Upload Completed");
        }

        private static void DownloadBlobs(CloudStorageAccount storageAccount, CloudBlobClient blobClient, 
            CloudBlobContainer container, IEnumerable<IListBlobItem> blobs)
        {
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            foreach (var blob in blobs)
            {
                if (blob is CloudBlockBlob)
                {
                    var blockBlob = blob as CloudBlockBlob;
                    blockBlob.DownloadToFile(@"C:\Users\Fierce PC\Downloads\" + blockBlob.Name, FileMode.Create);
                    Console.WriteLine("{0} Downloaded", blockBlob.Name);
                }
                else if (blob is CloudBlobDirectory)
                {
                    var blobDirectory = blob as CloudBlobDirectory;
                    Directory.CreateDirectory(blobDirectory.Prefix);
                    Console.WriteLine("Create directory " + blobDirectory.Prefix);
                    DownloadBlobs(storageAccount, blobClient, container, blobDirectory.ListBlobs());
                } 
            }

            Console.WriteLine("Download Completed");
        }

        private static void DeleteBlobs(CloudStorageAccount storageAccount, CloudBlobClient blobClient,
            CloudBlobContainer container, IEnumerable<IListBlobItem> blobs)
        {
            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            foreach (var blob in blobs)
            {
                if (blob is CloudBlockBlob)
                {
                    var blockBlob = blob as CloudBlockBlob;
                    blockBlob.DeleteIfExists();
                    Console.WriteLine("{0} Deleted", blockBlob.Name);
                }
                else if (blob is CloudBlobDirectory)
                {
                    var blobDirectory = blob as CloudBlobDirectory; 
                    DownloadBlobs(storageAccount, blobClient, container, blobDirectory.ListBlobs());
                }
            }

            Console.WriteLine("Delete Completed");
        }

    }
}
