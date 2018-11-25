using LinqToDB.Mapping;

namespace Northwind_Linq2Db.Entities
{
    [Table("Territories")]
    public class Territory
    {
        [Column("TerritoryID")]
        [PrimaryKey]
        [Identity]
        public int TerritoryId { get; set; }

        [Column("RegionID")]
        [NotNull]
        public int RegionId { get; set; }
    }
}
