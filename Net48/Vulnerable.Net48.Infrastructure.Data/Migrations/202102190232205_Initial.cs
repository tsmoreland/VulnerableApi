namespace Vulnerable.Net48.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        ProvinceId = c.Int(),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .Index(t => t.Name)
                .Index(t => t.ProvinceId)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        ContinentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Continents", t => t.ContinentId, cascadeDelete: true)
                .Index(t => t.Name)
                .Index(t => t.ContinentId);
            
            CreateTable(
                "dbo.Continents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, unicode: false),
                        CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.Name)
                .Index(t => t.CountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Provinces", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "ContinentId", "dbo.Continents");
            DropIndex("dbo.Provinces", new[] { "CountryId" });
            DropIndex("dbo.Provinces", new[] { "Name" });
            DropIndex("dbo.Continents", new[] { "Name" });
            DropIndex("dbo.Countries", new[] { "ContinentId" });
            DropIndex("dbo.Countries", new[] { "Name" });
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Cities", new[] { "ProvinceId" });
            DropIndex("dbo.Cities", new[] { "Name" });
            DropTable("dbo.Provinces");
            DropTable("dbo.Continents");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
        }
    }
}
