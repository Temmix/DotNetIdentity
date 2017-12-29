using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tables
{
    class Program
    {
        const string storageAccountName = "temmix";
        const string storageAccountKey = "8mOONKsUsmfAzDm48k2D9tp1+12aLrdLFw9UnCglVnd/lu+nzWDOoNMt+KIpzAm6IWy1phq4BGFgdo/XK9H8lA==";
        static void Main(string[] args)
        {
            var storageAccount = new CloudStorageAccount(new StorageCredentials(storageAccountName, storageAccountKey), true);
            var client = storageAccount.CreateCloudTableClient();
            var table = client.GetTableReference("scores");
            
            // Insert Entities
            InsertEntities(table); 
           
            // Insert Entities in batch
            InsertEntitiesInBatch(table);

            // Retrieve an Entity
            var tableResult = table.Execute(TableOperation.Retrieve<ScoreEntity>("Smug", "Temi"));
            var scoreEntity = (ScoreEntity)tableResult.Result;
            Console.WriteLine(scoreEntity);
            Console.WriteLine();

            // Querying the table
            var topScores = table.CreateQuery<ScoreEntity>().Where(x => x.PartitionKey == "Smug" && x.TopScore > 1000);
            foreach (var topScore in topScores)
            {
                Console.WriteLine(topScore);
            }

            Console.WriteLine("Table operation completed");
            Console.ReadLine();
        }

        private static void InsertEntitiesInBatch(CloudTable table)
        {
            table.CreateIfNotExists();
            table.ExecuteBatch(new TableBatchOperation() {
                 TableOperation.InsertOrReplace(new ScoreEntity("Smug", "Temi", 1000)),
                 TableOperation.InsertOrReplace(new ScoreEntity("Smug", "Ollie", 2000)),
                 TableOperation.InsertOrReplace(new ScoreEntity("Smug", "Ileri", 3000))
            });
        }

        private static void InsertEntities(CloudTable table)
        {
            table.CreateIfNotExists();
            table.Execute(TableOperation.InsertOrReplace(new ScoreEntity("Snail", "Temi", 1000)));
            table.Execute(TableOperation.InsertOrReplace(new ScoreEntity("Snail", "Ollie", 2000)));
        }
    }


    public class ScoreEntity : TableEntity
    {
        public ScoreEntity() { }
        public ScoreEntity(string gameName, string userName, int topScore)
        {
            PartitionKey = gameName;
            RowKey = userName;
            TopScore = topScore;
        }

        public string Game => PartitionKey;
        public string UserName => RowKey;
        public int TopScore { get; set; }

        public override string ToString() => $"{Game} {UserName} {TopScore}";

    }
}
