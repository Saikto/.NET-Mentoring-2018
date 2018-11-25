using System;
using LinqToDB.Mapping;

namespace Northwind_Linq2Db.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Column("OrderID")]
        [Identity]
        [PrimaryKey]
        public int OrderId { get; set; }

        [Column("ShipVia")]
        public int? ShipperId { get; set; }

        [Column("ShippedDate")]
        public DateTime? ShippedDate { get; set; }

        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }
    }
}
