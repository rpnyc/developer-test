using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using OrangeBricks.Web.Models;
using OrangeBricks.Web.Attributes;
using OrangeBricks.Web.Controllers.Viewing.ViewModels;
using OrangeBricks.Web.Controllers.Viewing.Builders;
using OrangeBricks.Web.Controllers.Viewing.Commands;

namespace OrangeBricks.Web.Controllers.Viewing
{
    public class ViewingController : Controller
    {
        private readonly IOrangeBricksContext _context;

        public ViewingController(IOrangeBricksContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns ALL viewings for a property. Accessible only to sellers of the property
        /// </summary>
        [OrangeBricksAuthorize(Roles = "Seller")]
        public ActionResult OnProperty(int id)
        {
            var builder = new ViewingsOnPropertyViewModelBuilder(_context);
            var viewModel = builder.Build(id);

            return View(viewModel);
        }


        /// <summary>
        /// Returns all viewings scheduled for the property by the current user 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult OnPropertyByMe(int id)
        {
            var builder = new ViewingsOnPropertyViewModelBuilder(_context);
            var viewModel = builder.Build(id, User.Identity.GetUserId());
            
            return View("OnProperty", viewModel);
        }

        [Authorize]
        public ActionResult ScheduleViewing(int id)
        {
            var builder = new ScheduleViewingViewModelBuilder(_context);

            var viewModel = builder.Build(id);
            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        [HandleError]
        public ActionResult ScheduleViewing(CreateViewingCommand command)
        {
            //Note: Ideally, the date validation against current date would be in the business logic layer
            if (!ModelState.IsValid || command.Date < System.DateTime.Today)
            {
                TempData["error"] = 1;
                return RedirectToAction("ScheduleViewing", "Viewing", new { id = command.PropertyId });
                
            }
            command.BuyerUserId = User.Identity.GetUserId();

            var handler = new CreateViewingCommandHandler(_context);

            handler.Handle(command);

            //Return to the listing of all Viewings scheduled for the current user
            return RedirectToAction("OnPropertyByMe", "Viewing", new { id = command.PropertyId });
        }
    }
}