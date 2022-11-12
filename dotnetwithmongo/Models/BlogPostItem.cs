using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace dotnetwithmongo.Models
{
    public class BlogPostItem : BlogPost
    {
        public List<PostCategory> Categories { get; set; }
    }
}
