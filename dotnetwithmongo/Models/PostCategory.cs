using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnetwithmongo.Models
{
    public class PostCategory
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CategoryName { get; set; }
    }
}
