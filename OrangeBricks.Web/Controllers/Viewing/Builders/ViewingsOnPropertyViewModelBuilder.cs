using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OrangeBricks.Web.Controllers.Viewing.ViewModels;
using OrangeBricks.Web.Models;
using Microsoft.AspNet.Identity;

namespace OrangeBricks.Web.Controllers.Viewing.Builders
{
    public class ViewingsOnPropertyViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public ViewingsOnPropertyViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns Viewing filtered by buyer user ID
        /// </summary>
        public ViewingsOnPropertyViewModel Build(int id, string buyerUserID)
        {
            var property = _context.Properties
                .Include(x => x.Viewings) 
                .Where(p => p.Id == id)
                .SingleOrDefault();

            if (property == null)
            {
                return null;
            }
            var viewings = property.Viewings ?? new List<Models.Viewing>();

            //return only those viewings for the user ID (this is the buyer's user ID)  
            if (buyerUserID != string.Empty)
            {
                viewings = viewings.Where(x => x.UserId == buyerUserID).ToList();
            }

            

            return new ViewingsOnPropertyViewModel
            {
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                NumberOfBedrooms = property.NumberOfBedrooms,
                HasViewings = viewings.Any(),
                Viewings = viewings.Select(x => new ViewingViewModel
                {
                    Date  = x.Date,
                    Time = x.Time,
                    PropertyId = property.Id,
                    UserId = x.UserId //TODO: This will have to be enhanced to show the user's name
                    
                }),

            };
        }



        public ViewingsOnPropertyViewModel Build(int id)
        {
            return Build(id, string.Empty);


            //var property = _context.Properties
            //    .Where(p => p.Id == id)
            //    .Include(x => x.Viewings)
            //    .SingleOrDefault();

            //var viewings = property.Viewings ?? new List<Models.Viewing>();

            //return new ViewingsOnPropertyViewModel
            //{
            //    HasViewings = viewings.Any(),
            //    Viewings = viewings.Select(x => new ViewingViewModel
            //    {
            //        Date = x.Date,
            //        Time = x.Time,
            //        PropertyId = property.Id,
            //        PropertyType = property.PropertyType,
            //        StreetName = property.StreetName,
            //        NumberOfBedrooms = property.NumberOfBedrooms

            //    }),
            //    PropertyId = property.Id,
            //};
        }

    
    
    
    }
}