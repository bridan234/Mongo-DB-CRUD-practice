using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MungoDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Mongo_CRUD db = new Mongo_CRUD("Sales");

            //Consuming insert method
            //db.InsertRecord("Customer_Profile", new CustomerEntity { FirstName="Bridan", LasName ="ODC", CustId= new Guid() });
            //db.InsertRecord("Customer_Profile", new CustomerEntity { FirstName = "Joe", LasName = "Smith", CustId = new Guid() });
            //db.InsertRecord("Customer_Profile", new CustomerEntity { FirstName = "Ngolo", LasName = "Kante", CustId = new Guid() });
            //db.InsertRecord("Customer_Profile", new CustomerEntity { FirstName = "Eden", LasName = "Hazard", CustId = new Guid() });
            //db.InsertRecord("Customer_Profile", new CustomerEntity { FirstName = "Mikel", LasName = "Obinna", CustId = new Guid() });
            //db.InsertRecord("Customer_Profile", new CustomerEntity { FirstName = "Lebron", LasName = "James", CustId = new Guid() });


            //db.InsertRecord("Orders",new OrdersEntity
            //{

            //    OrderId = new Guid(),
            //    Product = "Acer Laptop",
            //    Quantity = 4,
            //    UnitPrice = 1199.99,

            //    Customer = new CustomerEntity()
            //    {
            //        FirstName = "Bisi",
            //        LasName = "Olalekan",
            //        CustId = new Guid()
            //    }
            //}) ;

            //Consuming the Get All method
            //var elements = db.GetAll<OrdersEntity>("Orders");


            //foreach (var item in elements)
            //{
            //    Console.WriteLine($"{item.Customer.FirstName} {item.Customer.LasName}: {item.Quantity} - {item.Product} at {item.UnitPrice} each");
            //}
            //var records = db.GetAll<CustomerEntity>("Customer_Profile");
            //foreach (var item in records)
            //{
            //    Console.WriteLine($"{item.CustId} - {item.FirstName} {item.LasName}");
            //}
            //var record = db.GetSingle<CustomerEntity>("Customer_Profile", c => c.CustId == new Guid("a7a4cf39-39a2-4e7e-bb6f-641c357cc43e"));
            //Console.WriteLine($"{record.CustId} - {record.FirstName} {record.LasName}");

            Guid Id = new Guid("a7a4cf39-39a2-4e7e-bb6f-641c357cc43e");
            //db.UpSertRecord("Customer_Profile",new CustomerEntity {CustId=Id,FirstName ="Mike", LasName = "GoodLuck" }, c => c.CustId == Id);

            db.DeleteRecord<CustomerEntity>("Customer_Profile", c => c.CustId == Id);
                //Console.ReadLine();
        }
    }

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

   
    public class CustomerEntity
    {
        [BsonId]
        public Guid CustId { get; set; }
        public string FirstName { get; set; }
        public string LasName { get; set; }

        
    }

    public class OrdersEntity
    {
        [BsonId]
        public Guid OrderId { get; set; }
        
        public CustomerEntity Customer { get; set; }
        public string Product { get; set; }
        public double UnitPrice { get; set; } 
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public double TotalPrize()
        {
            return UnitPrice * Quantity;
        }
    }
}
