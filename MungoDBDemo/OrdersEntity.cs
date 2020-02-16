using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MungoDBDemo
{
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
