﻿namespace JobFinder.Web.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using JobFinder.Data;
    using JobFinder.Web.Areas.Admin.Models.DashboardModels;
    using JobFinder.Web.Controllers;

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
            var jobOffersCount = this.Data.JobOffers.All().Where(o => o.IsActive).Count();

            if (jobOffersCount == 0)
            {
                return this.View();
            }

            var model = this.Data.JobOffers.All().Where(o => o.IsActive).GroupBy(o => o.BusinessSectorId)
                .Select(o => new DashboardViewModel { Name = o.FirstOrDefault().BusinessSector.Name, Y = o.Count() })
                .ToList()
                .Select(o => new DashboardViewModel { Name = o.Name, Y = Math.Round(Convert.ToDouble(o.Y / jobOffersCount) * 100, 2) });

            return this.View(model);
        }
    }
}
