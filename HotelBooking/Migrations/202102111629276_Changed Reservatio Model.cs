namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedReservatioModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "IsPaymentNecessary", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "IsPaymentNecessary");
        }
    }
}
