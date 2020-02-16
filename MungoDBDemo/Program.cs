using System;

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
}
