using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Queue;
using QueueModel;
using QueueModel.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queues
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
            foreach (var customer in GetCustomers())
            {
                queue.AddMessage(new CloudQueueMessage(SerializerBaba<Customer>.Serialize(customer)));
            }

            Console.WriteLine("Queue completed");
            Console.ReadLine();
        }

        private static List<Customer> GetCustomers()
        {
            return new List<Customer> {
                new Customer(1, "Temi", "Makinde","Temi.Makinde@GMail.com"),
                new Customer(2, "Olamide", "Makinde","Olamide.Makinde@GMail.com"),
                new Customer(3, "Ileri", "Makinde","Ileri.Makinde@GMail.com"),
                new Customer(4, "Ife", "Makinde","Ife.Makinde@GMail.com"),
                new Customer(5, "Temi Snr", "Makinde","Temi.Snr.Makinde@GMail.com"),
            };
        }
    }
}
