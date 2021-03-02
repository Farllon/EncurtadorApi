using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Encurtador.Models
{
    public class Url
    {
        [BsonId]
        public ObjectId Uuid { get; set; }

        public string OriginalUrl { get; set; }

        public string UserId { get; set; }

        public string Hash { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}