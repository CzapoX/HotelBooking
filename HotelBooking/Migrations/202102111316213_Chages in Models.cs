namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChagesinModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "PriceToPay", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Reservations", "IsPaid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reservations", "IsPaid", c => c.Boolean(nullable: false));
            DropColumn("dbo.Reservations", "PriceToPay");
        }
    }
}
