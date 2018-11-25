using LinqToDB.Mapping;

namespace Northwind_Linq2Db.Entities
{
    [Table("Shippers")]
    public class Shipper
    {
        [Column("ShipperID")]
        [Identity]
        [PrimaryKey]
        public int ShipperId { get; set; }

        [Column("CompanyName")]
        [NotNull]
        public string CompanyName { get; set; }
    }
}
