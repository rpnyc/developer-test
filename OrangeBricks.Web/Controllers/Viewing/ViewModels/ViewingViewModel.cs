using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace OrangeBricks.Web.Controllers.Viewing.ViewModels
{
    public class ViewingViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        [Range(9, 17)]
        [Display(Name = "Time")]
        public int Time { get; set; }

        [Required]
        public int PropertyId { get; set; }

        //TODO: Change this to display name of the buyer rather than user ID. Possible performance impact if 
        // getting details using ASP.NET UserManager
        [Required]
        public string UserId { get; set; }


        public string PropertyType { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string StreetName { get; set; }
        public IEnumerable<SelectListItem> AvailableTime { get; set; }
    }
}