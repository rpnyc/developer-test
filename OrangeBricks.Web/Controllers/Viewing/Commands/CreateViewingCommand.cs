using System;
using System.ComponentModel.DataAnnotations;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Viewing.Commands
{
    public class CreateViewingCommand
    {
        public DateTime Date { get; set; }
        public int Time { get; set; }

        public int PropertyId { get; set; }
        public string BuyerUserId { get; set; }
    }
}