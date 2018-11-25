using LinqToDB.Mapping;

namespace Northwind_Linq2Db.Entities
{
    [Table("Region")]
    public class Region
    {
        [Column("RegionID")]
        [Identity]
        [PrimaryKey]
        public int RegionId { get; set; }

        [Column("RegionDescription")]
        [NotNull]
        public string RegionDescription { get; set; }
    }
}
