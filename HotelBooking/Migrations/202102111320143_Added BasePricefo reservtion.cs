namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBasePriceforeservtion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "BasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "BasePrice");
        }
    }
}
