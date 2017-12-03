namespace JobFinder.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using JobFinder.Data;
    using JobFinder.Web.Controllers;
    using JobFinder.Web.Areas.Admin.Models.DashboardModels;

    [Authorize(Roles = "Admin")]
    public class DashboardController : BaseController
    {
        public DashboardController(IJobFinderData data)
            : base(data)
        {
        }

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            var model = new List<DashboardViewModel>
            {
                new DashboardViewModel
                {
					Name = "IE",
					Y = 56.33
				}, 
                new DashboardViewModel
                {
					Name = "Chrome",
					Y = 24.03,
					Sliced = true,
					Selected =true
				}, 
                new DashboardViewModel
                {
					Name = "Firefox",
					Y = 10.38
				},
                new DashboardViewModel
                {
					Name = "Safari",
					Y = 4.77
				},
                new DashboardViewModel
                {
					Name = "Opera",
					Y = 0.91
				},
                new DashboardViewModel
                {
					Name = "Other",
					Y = 0.2
				}
            };

            return this.View(model);
        }
    }
}
