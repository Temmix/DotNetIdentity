using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using QueueModel;
using QueueModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Queue.Server
{
    class Program
    {
        const string storageAccountName = "temmix";
        const string storageAccountKey = "8mOONKsUsmfAzDm48k2D9tp1+12aLrdLFw9UnCglVnd/lu+nzWDOoNMt+KIpzAm6IWy1phq4BGFgdo/XK9H8lA==";
        static void Main(string[] args)
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), true);
            var client = storageAccount.CreateCloudQueueClient();
            var queue = client.GetQueueReference("new-queue");
            queue.CreateIfNotExists();

            Console.WriteLine("Waiting for messages ...");

            while (true)
            {
                var message = queue.GetMessage();
                if (message != null)
                {
                   var customer = SerializerBaba<Customer>.DeSerialize(message.AsBytes);
                   Console.WriteLine(customer.Firstname + " " + customer.Lastname);
                    queue.DeleteMessage(message);
                }
                 
                Thread.Sleep(1000);
            }
        }
    }
}
