namespace HotelBooking.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedReservationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reservations", "HowManyDays", c => c.Int(nullable: false));
            AddColumn("dbo.Reservations", "NumberOfPeople", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reservations", "NumberOfPeople");
            DropColumn("dbo.Reservations", "HowManyDays");
        }
    }
}
