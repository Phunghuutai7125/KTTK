using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using StudentApp.Models;

namespace StudentApp.Data
{
    public class MongoDbContext
    {
        public IMongoCollection<Student> Students { get; }

        public MongoDbContext()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config["MongoDbSettings:ConnectionString"] ?? "";
            string databaseName = config["MongoDbSettings:DatabaseName"] ?? "";

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            Students = database.GetCollection<Student>("Students");
        }
    }
}