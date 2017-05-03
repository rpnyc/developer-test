using System;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Data.Entity;
using OrangeBricks.Web.Controllers.Property.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Builders
{
    public class PropertiesViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public PropertiesViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public PropertiesViewModel Build(PropertiesQuery query, string currentUserID)
        {
            var properties = _context.Properties
                .Include(a => a.Offers)
                .Include(a => a.Viewings) //RP: TODO: Overhead to take this for every property
                .Where(p => p.IsListedForSale);

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                properties = properties.Where(x => x.StreetName.Contains(query.Search) 
                    || x.Description.Contains(query.Search));
            }

            return new PropertiesViewModel
            {
                Properties = properties
                    .ToList()
                      .Select(p => new PropertyViewModel
                      {
                          Id = p.Id,
                          StreetName = p.StreetName,
                          Description = p.Description,
                          NumberOfBedrooms = p.NumberOfBedrooms,
                          PropertyType = p.PropertyType,
                          //Flags indicating if current user has any offers on or viewing scheduled on this property
                          HasOfferFromCurrentUser = p.Offers.Any(x => x.OfferMadeByUserId == currentUserID),
                          HasViewings = p.Viewings.Any(x => x.UserId == currentUserID)
                      }
                    )
                    .ToList(),
                Search = query.Search
            };
        }

    }
}