using System;
using System.Collections.Generic;

namespace OrangeBricks.Web.Controllers.Viewing.ViewModels
{

    public class ViewingsOnPropertyViewModel
    {
        public bool HasViewings { get; set; }
        public IEnumerable<ViewingViewModel> Viewings { get; set; }
        public string PropertyType { get; set; }
        public int NumberOfBedrooms { get; set; }
        public string StreetName { get; set; }
    }
}