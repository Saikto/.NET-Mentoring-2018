using LinqToDB.Mapping;

namespace Northwind_Linq2Db.Entities
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Column("SupplierID")]
        [PrimaryKey]
        [Identity]
        public int SupplierId { get; set; }

        [Column("CompanyName")]
        [NotNull]
        public string CompanyName { get; set; }

        [Column("ContactName")]
        public string ContactName { get; set; }
    }
}
