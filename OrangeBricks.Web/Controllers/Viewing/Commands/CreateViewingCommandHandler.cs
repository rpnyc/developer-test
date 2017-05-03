using System.Collections.Generic;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewing.Commands
{
    public class CreateViewingCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public CreateViewingCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(CreateViewingCommand command)
        {
            var property = _context.Properties.Find(command.PropertyId);

            var viewing = new Models.Viewing
            {
                Date = command.Date,
                Time = command.Time,
                UserId = command.BuyerUserId,
            };

            if (property.Viewings == null)
            {
                property.Viewings = new List<Models.Viewing>();
            }

            property.Viewings.Add(viewing);

            _context.SaveChanges();
        }
    }
}