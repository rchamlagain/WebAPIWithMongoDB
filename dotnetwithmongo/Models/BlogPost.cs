using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dotnetwithmongo.Models
{
    public class BlogPost
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRequired]
        public string Title { get; set; }
        [BsonRequired]
        public string Summary { get; set; }
        public string DetailContent { get; set; }
        public List<string> Tags { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Category { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
    }
}
