namespace JobFinder.Web.Areas.Company.Models.JobOfferViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class CreateOfferViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int TownId { get; set; }

        [Required]
        public int BusinessSectorId { get; set; }
    }
}
