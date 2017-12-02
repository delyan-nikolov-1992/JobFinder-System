namespace JobFinder.Web.Models.OfferViewModels
{
    public class SearchOfferViewModel
    {
        public int? Town { get; set; }

        public int[] Sectors { get; set; }

        public string Word { get; set; }

        public int? Page { get; set; }
    }
}
