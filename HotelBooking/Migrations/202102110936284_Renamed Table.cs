namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedTable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Orders", newName: "Reservations");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Reservations", newName: "Orders");
        }
    }
}
