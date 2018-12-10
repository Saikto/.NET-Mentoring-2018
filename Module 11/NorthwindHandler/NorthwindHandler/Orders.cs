using System;
using System.Data.Linq.Mapping;

namespace NorthwindHandler
{
    [Table(Name = "Orders")]
    public class Orders
    {
        [Column(Name = "CustomerID")]
        public string CustomerID { get; set; }

        [Column(Name = "OrderDate")]
        public DateTime OrderDate { get; set; }

        [Column(Name = "ShipName")]
        public string ShipName { get; set; }

        [Column(Name = "ShipCountry")]
        public string ShipCountry { get; set; }
    }
}