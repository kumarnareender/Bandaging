using BandagingWebApplication.Models;

namespace BandagingWebApplication.ViewModel
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public List<SubCategoryViewModel> SubCategories { get; set; }

    }
    public class SubCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
