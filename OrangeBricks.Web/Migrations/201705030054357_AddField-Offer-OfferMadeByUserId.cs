namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldOfferOfferMadeByUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "OfferMadeByUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "OfferMadeByUserId");
        }
    }
}
