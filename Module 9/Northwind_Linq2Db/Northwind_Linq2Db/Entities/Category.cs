using LinqToDB.Mapping;

namespace Northwind_Linq2Db.Entities
{
    [Table("Categories")]
    public class Category
    {
        [PrimaryKey]
        [Identity]
        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [Column("CategoryName")]
        public string CategoryName { get; set; }
    }
}
