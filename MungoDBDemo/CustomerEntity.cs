using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MungoDBDemo
{
    public class CustomerEntity
    {
        [BsonId]
        public Guid CustId { get; set; }
        public string FirstName { get; set; }
        public string LasName { get; set; }

        
    }
}
