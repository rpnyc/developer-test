using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Offers.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Offers.Builders
{
    public class OffersOnPropertyViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public OffersOnPropertyViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns the Offers on a property. If the buyer user ID is provided, then 
        /// restricts return to offers made by that buyer
        /// </summary>
        public OffersOnPropertyViewModel Build(int id, string buyerUserID)
        {
            bool isForBuyer = false;

            var property = _context.Properties
                .Include(x => x.Offers)
                .Where(p => p.Id == id )
                .SingleOrDefault();

            var offers = property.Offers ?? new List<Offer>();

            if (buyerUserID != string.Empty)
            {
                offers = offers.Where(o => o.OfferMadeByUserId == buyerUserID).ToList();
                isForBuyer = true;
            }

            return new OffersOnPropertyViewModel
            {
                IsViewForBuyer = isForBuyer,
                HasOffers = offers.Any(),
                Offers = offers.Select(x => new OfferViewModel
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    CreatedAt = x.CreatedAt,

                    IsPending = (x.Status == OfferStatus.Pending),
                    Status = x.Status.ToString()
                }),
                PropertyId = property.Id, 
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                NumberOfBedrooms = property.NumberOfBedrooms
            };
        }

        public OffersOnPropertyViewModel Build(int id)
        {
            return Build(id, string.Empty);

        }
    
    }
}