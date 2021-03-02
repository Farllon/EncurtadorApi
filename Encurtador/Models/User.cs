using System;
using System.Runtime.InteropServices;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Encurtador.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Uuid { get; set; }

        public Guid Id { get; set; }
        
        public string Username { get; set; }
        
        public string Password { get; set; }
        
        public string Role { get; set; }
    }
}