namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Location = c.String(),
                        PriceForOnePerson = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPaymentNecessary = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        HotelId = c.Int(nullable: false),
                        ReservationNumber = c.Guid(nullable: false),
                        Email = c.String(),
                        IsPaid = c.Boolean(nullable: false),
                        IsPaymentSuccessful = c.Boolean(nullable: false),
                        IsBookingSuccessful = c.Boolean(nullable: false),
                        IsEmailSendSuccessful = c.Boolean(nullable: false),
                        IsReservationSuccessful = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "HotelId", "dbo.Hotels");
            DropIndex("dbo.Orders", new[] { "HotelId" });
            DropTable("dbo.Orders");
            DropTable("dbo.Hotels");
        }
    }
}
