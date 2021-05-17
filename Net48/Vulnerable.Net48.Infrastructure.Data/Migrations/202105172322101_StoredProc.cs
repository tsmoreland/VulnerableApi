namespace Vulnerable.Net48.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProc : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.City_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 100, unicode: false),
                        ProvinceId = p.Int(),
                        CountryId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Cities]([Name], [ProvinceId], [CountryId])
                      VALUES (@Name, @ProvinceId, @CountryId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Cities]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Cities] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.City_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(maxLength: 100, unicode: false),
                        Name_Original = p.String(maxLength: 100, unicode: false),
                        ProvinceId = p.Int(),
                        CountryId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Cities]
                      SET [Name] = @Name, [ProvinceId] = @ProvinceId, [CountryId] = @CountryId
                      WHERE (([Id] = @Id) AND (([Name] = @Name_Original) OR ([Name] IS NULL AND @Name_Original IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.City_Delete",
                p => new
                    {
                        Id = p.Int(),
                        Name_Original = p.String(maxLength: 100, unicode: false),
                    },
                body:
                    @"DELETE [dbo].[Cities]
                      WHERE (([Id] = @Id) AND (([Name] = @Name_Original) OR ([Name] IS NULL AND @Name_Original IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.Country_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 100, unicode: false),
                        ContinentId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Countries]([Name], [ContinentId])
                      VALUES (@Name, @ContinentId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Countries]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Countries] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Country_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(maxLength: 100, unicode: false),
                        Name_Original = p.String(maxLength: 100, unicode: false),
                        ContinentId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Countries]
                      SET [Name] = @Name, [ContinentId] = @ContinentId
                      WHERE (([Id] = @Id) AND (([Name] = @Name_Original) OR ([Name] IS NULL AND @Name_Original IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.Country_Delete",
                p => new
                    {
                        Id = p.Int(),
                        Name_Original = p.String(maxLength: 100, unicode: false),
                    },
                body:
                    @"DELETE [dbo].[Countries]
                      WHERE (([Id] = @Id) AND (([Name] = @Name_Original) OR ([Name] IS NULL AND @Name_Original IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.Continent_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 100, unicode: false),
                    },
                body:
                    @"INSERT [dbo].[Continents]([Name])
                      VALUES (@Name)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Continents]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Continents] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Continent_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(maxLength: 100, unicode: false),
                        Name_Original = p.String(maxLength: 100, unicode: false),
                    },
                body:
                    @"UPDATE [dbo].[Continents]
                      SET [Name] = @Name
                      WHERE (([Id] = @Id) AND (([Name] = @Name_Original) OR ([Name] IS NULL AND @Name_Original IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.Continent_Delete",
                p => new
                    {
                        Id = p.Int(),
                        Name_Original = p.String(maxLength: 100, unicode: false),
                    },
                body:
                    @"DELETE [dbo].[Continents]
                      WHERE (([Id] = @Id) AND (([Name] = @Name_Original) OR ([Name] IS NULL AND @Name_Original IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.Province_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 100, unicode: false),
                        CountryId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Provinces]([Name], [CountryId])
                      VALUES (@Name, @CountryId)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Provinces]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Provinces] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            CreateStoredProcedure(
                "dbo.Province_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(maxLength: 100, unicode: false),
                        Name_Original = p.String(maxLength: 100, unicode: false),
                        CountryId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Provinces]
                      SET [Name] = @Name, [CountryId] = @CountryId
                      WHERE (([Id] = @Id) AND (([Name] = @Name_Original) OR ([Name] IS NULL AND @Name_Original IS NULL)))"
            );
            
            CreateStoredProcedure(
                "dbo.Province_Delete",
                p => new
                    {
                        Id = p.Int(),
                        Name_Original = p.String(maxLength: 100, unicode: false),
                    },
                body:
                    @"DELETE [dbo].[Provinces]
                      WHERE (([Id] = @Id) AND (([Name] = @Name_Original) OR ([Name] IS NULL AND @Name_Original IS NULL)))"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Province_Delete");
            DropStoredProcedure("dbo.Province_Update");
            DropStoredProcedure("dbo.Province_Insert");
            DropStoredProcedure("dbo.Continent_Delete");
            DropStoredProcedure("dbo.Continent_Update");
            DropStoredProcedure("dbo.Continent_Insert");
            DropStoredProcedure("dbo.Country_Delete");
            DropStoredProcedure("dbo.Country_Update");
            DropStoredProcedure("dbo.Country_Insert");
            DropStoredProcedure("dbo.City_Delete");
            DropStoredProcedure("dbo.City_Update");
            DropStoredProcedure("dbo.City_Insert");
        }
    }
}
