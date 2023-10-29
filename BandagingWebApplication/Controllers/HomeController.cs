using BandagingWebApplication.Models;
using BandagingWebApplication.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BandagingWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Service service;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            service = new();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BlogDetails(int id)
        {
            var data = service.GetBlog(id);
            return View(data);
        }

        public Result GetAllBlogs(int id)
        {
            var data = service.GetBlogsByCategory(id);
            return new Result() { IsSuccess = true, Data = data };
        }

        [HttpGet]
        public Result GetAllCategories()
        {
            var data = service.GetCategories();
            return new Result() { IsSuccess = true, Data = data };
        }


        [HttpGet]
        public Result GetAllSubCategoriesCategories()
        {
            var data = service.GetSubCategories();
            return new Result() { IsSuccess = true, Data = data };
        }


        [HttpGet]
        public Result GetAllSubCategoriesByCategory(int id)
        {
            var data = service.GetSubCategoriesByCategory(id);
            return new Result() { IsSuccess = true, Data = data };
        }

        [HttpGet]
        public Result GetAllCategoriesWithSubCategories()
        {
            var categories = service.GetCategories();
            List<CategoryViewModel> categoryViewModels = new();
            if (categories != null)
            {
                foreach (var item in categories)
                {

                    var subCats = service.GetSubCategoriesByCategory(item.Id);
                    List<SubCategoryViewModel> subCategories = new();
                    foreach (var subC in subCats)
                    {
                        subCategories.Add(new() { Id = subC.Id, Name = subC.Name });
                    }
                    CategoryViewModel categoryView = new()
                    {
                        Name = item.Name,
                        SubCategories = subCategories
                    };
                    categoryViewModels.Add(categoryView);
                }
            }

            return new Result() { Data = categoryViewModels, IsSuccess = true };
        }




        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}