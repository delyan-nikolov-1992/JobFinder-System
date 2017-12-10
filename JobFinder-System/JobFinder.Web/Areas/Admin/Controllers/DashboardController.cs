namespace JobFinder.Web.Areas.Admin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using JobFinder.Data;
    using JobFinder.Web.Areas.Admin.Models.DashboardModels;
    using JobFinder.Web.Controllers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

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
            return this.View(this.GetOffersBySector());
        }

        [HttpPost]
        public ActionResult OffersBySector()
        {
            return this.Json(this.ToJson(this.GetOffersBySector()));
        }

        [HttpPost]
        public ActionResult OffersByTown()
        {
            var townModels = this.Data.JobOffers.All().Where(o => o.IsActive).GroupBy(o => o.TownId)
                .Select(o => new TownViewModel
                {
                    Name = o.FirstOrDefault().Town.Name,
                    Offers = o.GroupBy(off => off.DateCreated.Year).OrderBy(off => off.Key).Select(off =>
                        new OfferViewModel
                        {
                            Year = off.Key,
                            OffersCount = off.Count()
                        })
                }).OrderBy(t => t.Name).ToList();

            var model = new DashboardColumnViewModel();

            foreach (var town in townModels)
            {
                model.Categories.Add(town.Name);

                foreach (var offer in town.Offers)
                {
                    var yearName = string.Format("Year: {0}", offer.Year);
                    var yearSeries = model.Series.FirstOrDefault(s => s.Name == yearName);

                    if (yearSeries == null)
                    {
                        yearSeries = new ColumnViewModel { Name = yearName };
                        model.Series.Add(yearSeries);
                    }

                    yearSeries.Data.Add(offer.OffersCount);
                }
            }

            return this.Json(this.ToJson(model));
        }

        private IEnumerable<DashboardViewModel> GetOffersBySector()
        {
            var jobOffersCount = this.Data.JobOffers.All().Where(o => o.IsActive).Count();

            if (jobOffersCount == 0)
            {
                return new DashboardViewModel[] { };
            }

            var model = this.Data.JobOffers.All().Where(o => o.IsActive).GroupBy(o => o.BusinessSectorId)
                .Select(o => new DashboardViewModel { Name = o.FirstOrDefault().BusinessSector.Name, Y = o.Count() })
                .ToList()
                .Select(o => new DashboardViewModel { Name = o.Name, Y = Math.Round(Convert.ToDouble(o.Y / jobOffersCount) * 100, 2) })
                .ToList();

            return model;
        }

        private object ToJson(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            settings.Converters.Add(new JavaScriptDateTimeConverter());
            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
