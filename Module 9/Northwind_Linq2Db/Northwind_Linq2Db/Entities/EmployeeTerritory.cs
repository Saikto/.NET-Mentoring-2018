using LinqToDB.Mapping;

namespace Northwind_Linq2Db.Entities
{
    [Table("EmployeeTerritories")]
    public class EmployeeTerritory
    {
        [Column("EmployeeID")]
        [NotNull]
        public int EmployeeId { get; set; }

        [Column("TerritoryID")]
        [NotNull]
        public int TerritoryId { get; set; }
    }
}
