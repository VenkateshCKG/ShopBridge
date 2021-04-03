namespace ShopBridge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDBCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductID = c.Int(nullable: false, identity: true),
                        ProductName = c.String(maxLength: 100),
                        Category = c.String(maxLength: 100),
                        Description = c.String(maxLength: 400),
                        Supplier = c.String(maxLength: 100),
                        CostPrice = c.Int(nullable: false),
                        SellingPrice = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Products");
        }
    }
}
