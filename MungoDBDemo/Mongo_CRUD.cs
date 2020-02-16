using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MungoDBDemo
{
    public class Mongo_CRUD
    {
        private readonly IMongoDatabase db;

        public Mongo_CRUD(string database)
        {
            var client = new MongoClient();
            db = client.GetDatabase(database);
        }

        public void InsertRecord<TEntity>(string Entity, TEntity Data) where TEntity : class
        {
            var collection = db.GetCollection<TEntity>(Entity);
            collection.InsertOne(Data);
        }

        public List<TEntity> GetAll<TEntity>(string Entity)
        {
            var Collection = db.GetCollection<TEntity>(Entity);

            return Collection.Find(new BsonDocument()).ToList();
        }

        public TEntity GetSingle<TEntity>(string Entity, Func<TEntity, bool> ConditionIsTrue)
        {
            
            return GetAll<TEntity>(Entity).Where(ConditionIsTrue).FirstOrDefault();
        }

        public void UpSertRecord<TEntity>(string Entity, TEntity Data, Expression<Func<TEntity, bool>> ConditionIsTrue)
            // this method Updates a record if exists else create nee record
        {
            var collection = db.GetCollection<TEntity>(Entity);
            collection.ReplaceOne(Builders<TEntity>.Filter.Where(ConditionIsTrue), Data, new ReplaceOptions { IsUpsert = true });
        }

        public void UpdateField<TEntity>(string Entity, BsonDocument Data, Expression<Func<TEntity, bool>> ConditionIsTrue)
        // this method Updates a record if exists else create nee record
        {
            var collection = db.GetCollection<TEntity>(Entity);
           
            collection.UpdateOne(Builders<TEntity>.Filter.Where(ConditionIsTrue), Data);
        }

        public void DeleteRecord<TEntity>(string Entity, Expression<Func<TEntity, bool>> ConditionIsTrue)
        // this method Updates a record if exists else create nee record
        {
            var collection = db.GetCollection<TEntity>(Entity);

            var result = collection.DeleteOne(Builders<TEntity>.Filter.Where(ConditionIsTrue));
        }
    }
}
