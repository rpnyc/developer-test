namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeOfferCommand
    {
        public int PropertyId { get; set; }

        public int Offer { get; set; }

        //User Id of the buyer who made the offer. TODO: Could consider renaming this to just BuyerUserId or UserId 
        public string OfferMadeByUserId { get; set; }
    }
}