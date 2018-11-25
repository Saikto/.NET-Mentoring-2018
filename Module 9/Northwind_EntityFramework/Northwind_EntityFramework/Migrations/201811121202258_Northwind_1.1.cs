namespace Northwind_EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Northwind_11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeCreditCards",
                c => new
                    {
                        CreditCardNumber = c.Int(nullable: false, identity: true),
                        ExpirationDate = c.DateTime(nullable: false),
                        CardHolderName = c.String(nullable: false, maxLength: 50),
                        EmployeeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CreditCardNumber)
                .ForeignKey("dbo.Employees", t => t.EmployeeID, cascadeDelete: true)
                .Index(t => t.EmployeeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EmployeeCreditCards", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.EmployeeCreditCards", new[] { "EmployeeID" });
            DropTable("dbo.EmployeeCreditCards");
        }
    }
}
