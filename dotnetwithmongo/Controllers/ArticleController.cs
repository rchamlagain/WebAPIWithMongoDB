using dotnetwithmongo.Models;
using dotnetwithmongo.MongoHelper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnetwithmongo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
       
        #region Category
        [HttpGet]
        public async Task<IActionResult> GetAllCategory(int offset, int limit)
        {
            var collection = new MongoContext().GetCollection<PostCategory>(MyMongoCollection.PostCategory);
            var result = await collection.Find(new BsonDocument()).Limit(limit).Skip(offset).ToListAsync();
            return new ObjectResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] PostCategory model)
        {
            var context = new MongoContext();
            var collection = context.GetCollection<PostCategory>(MyMongoCollection.PostCategory);
            await collection.InsertOneAsync(model);
            return new OkResult();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCategory([FromBody] PostCategory model)
        {
            var collection = new MongoContext().GetCollection<BlogPost>(MyMongoCollection.PostCategory);
            var filter = Builders<BlogPost>.Filter.Eq(p => p.Id, model.Id);
            var update = Builders<BlogPost>.Update
                .Set(p => p.Title, model.CategoryName);
            var result = await collection.UpdateOneAsync(filter, update);
            return new OkObjectResult(result);
        }
        #endregion Category
        #region BlogPost
        [HttpPost]
        public async Task<IActionResult> GetPostByFilter([FromBody] PostFilter filter)
        {
            var postCollection = new MongoContext().GetCollection<BlogPost>(MyMongoCollection.BlogPost);
            var categoryCollection = new MongoContext().GetCollection<PostCategory>(MyMongoCollection.PostCategory);
            
            var result = await postCollection.Aggregate().Match(post => (filter.Tags.Count == 0 || post.Tags.Any(t => filter.Tags.Contains(t))
                ))               
                .Skip(filter.Offset)
                .Limit(filter.Limit)
                .Lookup<BlogPost, PostCategory, BlogPostItem>(categoryCollection, post => post.Category, category => category.Id, result => result.Categories)
                .Match(post => (string.IsNullOrEmpty(filter.CategoryName) || post.Categories.Any(c => c.CategoryName == filter.CategoryName)))               
                .ToListAsync();

            return new ObjectResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] BlogPost model)
        {
            var collection = new MongoContext().GetCollection<BlogPost>(MyMongoCollection.BlogPost);
            await collection.InsertOneAsync(model);
            return new OkResult();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePost([FromBody] BlogPost model)
        {
            var collection = new MongoContext().GetCollection<BlogPost>(MyMongoCollection.BlogPost);
            var filter = Builders<BlogPost>.Filter.Eq(p => p.Id, model.Id);
            var update = Builders<BlogPost>.Update
                .Set(p => p.Title, model.Title)
                .Set(p => p.DetailContent, model.DetailContent)
                .Set(p => p.Summary, model.Summary)
                .Set(p => p.Tags, model.Tags);
            var result = await collection.UpdateOneAsync(filter, update);
            return new OkObjectResult(result);
        }
        #endregion BlogPost
    }
}
