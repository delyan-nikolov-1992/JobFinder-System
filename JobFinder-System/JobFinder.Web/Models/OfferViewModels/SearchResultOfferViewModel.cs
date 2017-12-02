namespace JobFinder.Web.Models.OfferViewModels
{
    using System;
    using System.Linq.Expressions;
    using JobFinder.Models;

    public class SearchResultOfferViewModel
    {
        public static Expression<Func<JobOffer, SearchResultOfferViewModel>> FromJobOffer
        {
            get
            {
                return o => new SearchResultOfferViewModel
                {
                    Id = o.Id,
                    Title = o.Title,
                    DateCreated = o.DateCreated,
                    Town = o.Town.Name,
                    CompanyId = o.CompanyId,
                    CompanyName = o.Company.CompanyName
                };
            }
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Town { get; set; }

        public DateTime DateCreated { get; set; }

        public string CompanyId { get; set; }

        public string CompanyName { get; set; }
    }
}
