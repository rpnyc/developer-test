using System.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using OrangeBricks.Web.Controllers.Viewing.ViewModels;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewing.Builders
{
    public class ScheduleViewingViewModelBuilder
    {
        private readonly IOrangeBricksContext _context;

        public ScheduleViewingViewModelBuilder(IOrangeBricksContext context)
        {
            _context = context;
        }

        public ViewingViewModel Build(int id)
        {
            var property = _context.Properties.Find(id);

            return new ViewingViewModel
            {
                PropertyType = property.PropertyType,
                StreetName = property.StreetName,
                NumberOfBedrooms = property.NumberOfBedrooms,
                PropertyId = id,
                Date = System.DateTime.Today,
                //For now this is hard-coded. This can later be pulled in from a scheduling DB
                AvailableTime = new string[] { "9", "10", "11", "12", "1", "14", "15"}
                .Select(x => new SelectListItem { Value = x, Text = x })
                .AsEnumerable()
            };

        
        
        }
    }
}
