namespace Northwind_EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Northwind_EntityFramework.NorthwindDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Northwind_EntityFramework.NorthwindDB context)
        {
            context.Categories.AddOrUpdate(c => c.CategoryName,
                new Category { CategoryName = "Beverages" },
                new Category { CategoryName = "Condiments" },
                new Category { CategoryName = "Confections" },
                new Category { CategoryName = "Dairy Products" });

            context.Regions.AddOrUpdate(r => r.RegionID,
                new Region { RegionDescription = "Eastern", RegionID = 1 },
                new Region { RegionDescription = "Western", RegionID = 2 },
                new Region { RegionDescription = "Northern", RegionID = 3 },
                new Region { RegionDescription = "Southern", RegionID = 4 });

            context.Territories.AddOrUpdate(t => t.TerritoryID,
                new Territory { TerritoryID = "01581", TerritoryDescription = "Westboro", RegionID = 1 },
                new Territory { TerritoryID = "01730", TerritoryDescription = "Bedford", RegionID = 1 },
                new Territory { TerritoryID = "01833", TerritoryDescription = "Georgetow", RegionID = 1 },
                new Territory { TerritoryID = "02116", TerritoryDescription = "Boston", RegionID = 1 });
        }
    }
}
