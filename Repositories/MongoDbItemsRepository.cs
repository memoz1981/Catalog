using System;
using System.Collections.Generic;
using Catalog.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName="Catalog";
        private const string collectionName="items"; 
        private readonly IMongoCollection<Item> itemsCollection;

        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database=mongoClient.GetDatabase(databaseName);
            itemsCollection=database.GetCollection<Item>(collectionName); 
        }
        public void CreateItem(Item item)
        {
            itemsCollection.InsertOne(item);
        }

        public void DeleteItem(Guid id)
        {
            var filter=filterBuilder.Eq(item => item.Id, id);
            itemsCollection.DeleteOne(filter);  
        }

        public IEnumerable<Item> Get()
        {
            return itemsCollection.Find(new BsonDocument()).ToList(); 
        }

        public Item GetItems(Guid id)
        {
            var filter=filterBuilder.Eq(item => item.Id, id);
            return itemsCollection.Find(filter).SingleOrDefault(); 
        }

        public void UpdateItem(Item item)
        {
            var filter=filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            itemsCollection.ReplaceOne(filter, item); 
        }
    }
}