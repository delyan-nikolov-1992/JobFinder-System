namespace JobFinder.Web.Areas.Admin.Models.DashboardModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class DashboardViewModel
    {
        public string Name { get; set; }

        public double Y { get; set; }

        public bool Sliced { get; set; }

        public bool Selected { get; set; }
    }
}
