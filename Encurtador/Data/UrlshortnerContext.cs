using System;
using Encurtador.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Encurtador.Data
{
    public class UrlshortnerContext
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoDatabase _db;
        public string Database => _configuration["MongoDB:Database"];
        public string Host => _configuration["MongoDB:Host"];
        public int Port => Convert.ToInt16(_configuration["MongoDB:Port"]);
        public string User => _configuration["MongoDB:User"];
        public string Password => _configuration["MongoDB:Password"];
        public string ConnectionString
        {
            get 
            {
                if (string.IsNullOrEmpty(User) || string.IsNullOrEmpty(Password))
                    return $@"mongodb://{Host}:{Port}";
                return $@"mongodb://{User}:{Password}@{Host}:{Port}";
            }
        }
        public IMongoCollection<Url> Urls => _db.GetCollection<Url>("Urls");
        public IMongoCollection<Hash> Hashes => _db.GetCollection<Hash>("Hashes");
        public IMongoCollection<User> Users => _db.GetCollection<User>("Users");

        public UrlshortnerContext(IConfiguration configuration)
        {
            _configuration = configuration;

            var client = new MongoClient(ConnectionString);
            _db = client.GetDatabase(Database);
        }
    }
}