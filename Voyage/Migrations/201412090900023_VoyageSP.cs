namespace Voyage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoyageSP : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 6, scale: 2),
                        Seats = c.Int(nullable: false),
                        ShowId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.Show", t => t.ShowId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.ShowId)
                .Index(t => t.StatusId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 100, unicode: false),
                        Lastname = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Phone = c.String(nullable: false, maxLength: 12, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Show",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Time = c.DateTime(nullable: false),
                        VIP = c.Boolean(),
                        Price = c.Decimal(nullable: false, precision: 6, scale: 2),
                        MovieId = c.Int(nullable: false),
                        TheatreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Movie", t => t.MovieId)
                .ForeignKey("dbo.Theatre", t => t.TheatreId)
                .Index(t => t.MovieId)
                .Index(t => t.TheatreId);
            
            CreateTable(
                "dbo.Movie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150, unicode: false),
                        PosterPath = c.String(nullable: false, maxLength: 100, unicode: false),
                        BigPosterPath = c.String(nullable: false, maxLength: 100, unicode: false),
                        Duration = c.Int(nullable: false),
                        Embed = c.String(maxLength: 255, unicode: false),
                        Rating = c.Decimal(precision: 3, scale: 1),
                        Actor = c.String(nullable: false, maxLength: 100, unicode: false),
                        _3D = c.Boolean(name: "3D", nullable: false),
                        Language = c.String(nullable: false, maxLength: 100, unicode: false),
                        Premiere = c.DateTime(),
                        Release = c.Int(),
                        GenreId = c.Int(nullable: false),
                        Highlighted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Genre", t => t.GenreId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Theatre",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45, unicode: false),
                        Seats = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false, maxLength: 100, unicode: false),
                        Lastname = c.String(nullable: false, maxLength: 100, unicode: false),
                        Email = c.String(nullable: false, maxLength: 100, unicode: false),
                        Phone = c.String(maxLength: 12, unicode: false),
                        Password = c.String(nullable: false, maxLength: 45, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateStoredProcedure(
                "dbo.Booking_Insert",
                p => new
                    {
                        Price = p.Decimal(precision: 6, scale: 2),
                        Seats = p.Int(),
                        ShowId = p.Int(),
                        StatusId = p.Int(),
                        CustomerId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Booking]([Price], [Seats], [ShowId], [StatusId], [CustomerId])
                      VALUES (@Price, @Seats, @ShowId, @StatusId, @CustomerId)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Booking]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Booking] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Booking_Update",
                p => new
                    {
                        ID = p.Int(),
                        Price = p.Decimal(precision: 6, scale: 2),
                        Seats = p.Int(),
                        ShowId = p.Int(),
                        StatusId = p.Int(),
                        CustomerId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Booking]
                      SET [Price] = @Price, [Seats] = @Seats, [ShowId] = @ShowId, [StatusId] = @StatusId, [CustomerId] = @CustomerId
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Booking_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Booking]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Insert",
                p => new
                    {
                        Firstname = p.String(maxLength: 100, unicode: false),
                        Lastname = p.String(maxLength: 100, unicode: false),
                        Email = p.String(maxLength: 100, unicode: false),
                        Phone = p.String(maxLength: 12, unicode: false),
                    },
                body:
                    @"INSERT [dbo].[Customer]([Firstname], [Lastname], [Email], [Phone])
                      VALUES (@Firstname, @Lastname, @Email, @Phone)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Customer]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Customer] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Update",
                p => new
                    {
                        ID = p.Int(),
                        Firstname = p.String(maxLength: 100, unicode: false),
                        Lastname = p.String(maxLength: 100, unicode: false),
                        Email = p.String(maxLength: 100, unicode: false),
                        Phone = p.String(maxLength: 12, unicode: false),
                    },
                body:
                    @"UPDATE [dbo].[Customer]
                      SET [Firstname] = @Firstname, [Lastname] = @Lastname, [Email] = @Email, [Phone] = @Phone
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Customer_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Customer]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Show_Insert",
                p => new
                    {
                        Time = p.DateTime(),
                        VIP = p.Boolean(),
                        Price = p.Decimal(precision: 6, scale: 2),
                        MovieId = p.Int(),
                        TheatreId = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Show]([Time], [VIP], [Price], [MovieId], [TheatreId])
                      VALUES (@Time, @VIP, @Price, @MovieId, @TheatreId)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Show]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Show] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Show_Update",
                p => new
                    {
                        ID = p.Int(),
                        Time = p.DateTime(),
                        VIP = p.Boolean(),
                        Price = p.Decimal(precision: 6, scale: 2),
                        MovieId = p.Int(),
                        TheatreId = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Show]
                      SET [Time] = @Time, [VIP] = @VIP, [Price] = @Price, [MovieId] = @MovieId, [TheatreId] = @TheatreId
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Show_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Show]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Movie_Insert",
                p => new
                    {
                        Title = p.String(maxLength: 150, unicode: false),
                        PosterPath = p.String(maxLength: 100, unicode: false),
                        BigPosterPath = p.String(maxLength: 100, unicode: false),
                        Duration = p.Int(),
                        Embed = p.String(maxLength: 255, unicode: false),
                        Rating = p.Decimal(precision: 3, scale: 1),
                        Actor = p.String(maxLength: 100, unicode: false),
                        _3D = p.Boolean(name: "3D"),
                        Language = p.String(maxLength: 100, unicode: false),
                        Premiere = p.DateTime(),
                        Release = p.Int(),
                        GenreId = p.Int(),
                        Highlighted = p.Boolean(),
                    },
                body:
                    @"INSERT [dbo].[Movie]([Title], [PosterPath], [BigPosterPath], [Duration], [Embed], [Rating], [Actor], [3D], [Language], [Premiere], [Release], [GenreId], [Highlighted])
                      VALUES (@Title, @PosterPath, @BigPosterPath, @Duration, @Embed, @Rating, @Actor, @3D, @Language, @Premiere, @Release, @GenreId, @Highlighted)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Movie]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Movie] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Movie_Update",
                p => new
                    {
                        ID = p.Int(),
                        Title = p.String(maxLength: 150, unicode: false),
                        PosterPath = p.String(maxLength: 100, unicode: false),
                        BigPosterPath = p.String(maxLength: 100, unicode: false),
                        Duration = p.Int(),
                        Embed = p.String(maxLength: 255, unicode: false),
                        Rating = p.Decimal(precision: 3, scale: 1),
                        Actor = p.String(maxLength: 100, unicode: false),
                        _3D = p.Boolean(name: "3D"),
                        Language = p.String(maxLength: 100, unicode: false),
                        Premiere = p.DateTime(),
                        Release = p.Int(),
                        GenreId = p.Int(),
                        Highlighted = p.Boolean(),
                    },
                body:
                    @"UPDATE [dbo].[Movie]
                      SET [Title] = @Title, [PosterPath] = @PosterPath, [BigPosterPath] = @BigPosterPath, [Duration] = @Duration, [Embed] = @Embed, [Rating] = @Rating, [Actor] = @Actor, [3D] = @3D, [Language] = @Language, [Premiere] = @Premiere, [Release] = @Release, [GenreId] = @GenreId, [Highlighted] = @Highlighted
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Movie_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Movie]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Genre_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 45, unicode: false),
                    },
                body:
                    @"INSERT [dbo].[Genre]([Name])
                      VALUES (@Name)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Genre]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Genre] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Genre_Update",
                p => new
                    {
                        ID = p.Int(),
                        Name = p.String(maxLength: 45, unicode: false),
                    },
                body:
                    @"UPDATE [dbo].[Genre]
                      SET [Name] = @Name
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Genre_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Genre]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Theatre_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 45, unicode: false),
                        Seats = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Theatre]([Name], [Seats])
                      VALUES (@Name, @Seats)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Theatre]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Theatre] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Theatre_Update",
                p => new
                    {
                        ID = p.Int(),
                        Name = p.String(maxLength: 45, unicode: false),
                        Seats = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Theatre]
                      SET [Name] = @Name, [Seats] = @Seats
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Theatre_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Theatre]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Status_Insert",
                p => new
                    {
                        Name = p.String(maxLength: 45, unicode: false),
                    },
                body:
                    @"INSERT [dbo].[Status]([Name])
                      VALUES (@Name)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[Status]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[Status] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.Status_Update",
                p => new
                    {
                        ID = p.Int(),
                        Name = p.String(maxLength: 45, unicode: false),
                    },
                body:
                    @"UPDATE [dbo].[Status]
                      SET [Name] = @Name
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Status_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Status]
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.User_Insert",
                p => new
                    {
                        Firstname = p.String(maxLength: 100, unicode: false),
                        Lastname = p.String(maxLength: 100, unicode: false),
                        Email = p.String(maxLength: 100, unicode: false),
                        Phone = p.String(maxLength: 12, unicode: false),
                        Password = p.String(maxLength: 45, unicode: false),
                    },
                body:
                    @"INSERT [dbo].[User]([Firstname], [Lastname], [Email], [Phone], [Password])
                      VALUES (@Firstname, @Lastname, @Email, @Phone, @Password)
                      
                      DECLARE @ID int
                      SELECT @ID = [ID]
                      FROM [dbo].[User]
                      WHERE @@ROWCOUNT > 0 AND [ID] = scope_identity()
                      
                      SELECT t0.[ID]
                      FROM [dbo].[User] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[ID] = @ID"
            );
            
            CreateStoredProcedure(
                "dbo.User_Update",
                p => new
                    {
                        ID = p.Int(),
                        Firstname = p.String(maxLength: 100, unicode: false),
                        Lastname = p.String(maxLength: 100, unicode: false),
                        Email = p.String(maxLength: 100, unicode: false),
                        Phone = p.String(maxLength: 12, unicode: false),
                        Password = p.String(maxLength: 45, unicode: false),
                    },
                body:
                    @"UPDATE [dbo].[User]
                      SET [Firstname] = @Firstname, [Lastname] = @Lastname, [Email] = @Email, [Phone] = @Phone, [Password] = @Password
                      WHERE ([ID] = @ID)"
            );
            
            CreateStoredProcedure(
                "dbo.User_Delete",
                p => new
                    {
                        ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[User]
                      WHERE ([ID] = @ID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.User_Delete");
            DropStoredProcedure("dbo.User_Update");
            DropStoredProcedure("dbo.User_Insert");
            DropStoredProcedure("dbo.Status_Delete");
            DropStoredProcedure("dbo.Status_Update");
            DropStoredProcedure("dbo.Status_Insert");
            DropStoredProcedure("dbo.Theatre_Delete");
            DropStoredProcedure("dbo.Theatre_Update");
            DropStoredProcedure("dbo.Theatre_Insert");
            DropStoredProcedure("dbo.Genre_Delete");
            DropStoredProcedure("dbo.Genre_Update");
            DropStoredProcedure("dbo.Genre_Insert");
            DropStoredProcedure("dbo.Movie_Delete");
            DropStoredProcedure("dbo.Movie_Update");
            DropStoredProcedure("dbo.Movie_Insert");
            DropStoredProcedure("dbo.Show_Delete");
            DropStoredProcedure("dbo.Show_Update");
            DropStoredProcedure("dbo.Show_Insert");
            DropStoredProcedure("dbo.Customer_Delete");
            DropStoredProcedure("dbo.Customer_Update");
            DropStoredProcedure("dbo.Customer_Insert");
            DropStoredProcedure("dbo.Booking_Delete");
            DropStoredProcedure("dbo.Booking_Update");
            DropStoredProcedure("dbo.Booking_Insert");
            DropForeignKey("dbo.Booking", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Show", "TheatreId", "dbo.Theatre");
            DropForeignKey("dbo.Show", "MovieId", "dbo.Movie");
            DropForeignKey("dbo.Movie", "GenreId", "dbo.Genre");
            DropForeignKey("dbo.Booking", "ShowId", "dbo.Show");
            DropForeignKey("dbo.Booking", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Movie", new[] { "GenreId" });
            DropIndex("dbo.Show", new[] { "TheatreId" });
            DropIndex("dbo.Show", new[] { "MovieId" });
            DropIndex("dbo.Booking", new[] { "CustomerId" });
            DropIndex("dbo.Booking", new[] { "StatusId" });
            DropIndex("dbo.Booking", new[] { "ShowId" });
            DropTable("dbo.User");
            DropTable("dbo.Status");
            DropTable("dbo.Theatre");
            DropTable("dbo.Genre");
            DropTable("dbo.Movie");
            DropTable("dbo.Show");
            DropTable("dbo.Customer");
            DropTable("dbo.Booking");
        }
    }
}
