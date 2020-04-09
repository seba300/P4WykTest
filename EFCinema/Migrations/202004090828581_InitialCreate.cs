namespace EFCinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        CinemaId = c.Int(nullable: false),
                        City = c.String(),
                        Street = c.String(),
                        HomeNumber = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.CinemaId)
                .ForeignKey("dbo.Cinema", t => t.CinemaId)
                .Index(t => t.CinemaId);
            
            CreateTable(
                "dbo.Cinema",
                c => new
                    {
                        CinemaId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CinemaId);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SeatNumbers = c.Int(nullable: false),
                        Cinema_CinemaId = c.Int(),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Cinema", t => t.Cinema_CinemaId)
                .Index(t => t.Cinema_CinemaId);
            
            CreateTable(
                "dbo.Place",
                c => new
                    {
                        PlaceId = c.Int(nullable: false, identity: true),
                        Row = c.String(),
                        Number = c.Int(nullable: false),
                        Room_RoomId = c.Int(),
                    })
                .PrimaryKey(t => t.PlaceId)
                .ForeignKey("dbo.Room", t => t.Room_RoomId)
                .Index(t => t.Room_RoomId);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Place_PlaceId = c.Int(),
                        Seance_SeanceId = c.Int(),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Place", t => t.Place_PlaceId)
                .ForeignKey("dbo.Seance", t => t.Seance_SeanceId)
                .Index(t => t.Place_PlaceId)
                .Index(t => t.Seance_SeanceId);
            
            CreateTable(
                "dbo.Seance",
                c => new
                    {
                        SeanceId = c.Int(nullable: false, identity: true),
                        MovieName = c.String(),
                        MovieStart = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SeanceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address", "CinemaId", "dbo.Cinema");
            DropForeignKey("dbo.Ticket", "Seance_SeanceId", "dbo.Seance");
            DropForeignKey("dbo.Ticket", "Place_PlaceId", "dbo.Place");
            DropForeignKey("dbo.Place", "Room_RoomId", "dbo.Room");
            DropForeignKey("dbo.Room", "Cinema_CinemaId", "dbo.Cinema");
            DropIndex("dbo.Ticket", new[] { "Seance_SeanceId" });
            DropIndex("dbo.Ticket", new[] { "Place_PlaceId" });
            DropIndex("dbo.Place", new[] { "Room_RoomId" });
            DropIndex("dbo.Room", new[] { "Cinema_CinemaId" });
            DropIndex("dbo.Address", new[] { "CinemaId" });
            DropTable("dbo.Seance");
            DropTable("dbo.Ticket");
            DropTable("dbo.Place");
            DropTable("dbo.Room");
            DropTable("dbo.Cinema");
            DropTable("dbo.Address");
        }
    }
}
