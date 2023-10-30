using BandagingWebApplication.Models;
using BandagingWebApplication.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Web.Mvc.Routing;

namespace BandagingWebApplication.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly Service _service;
        public AdminController(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _service = new();
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult AddCategory()
        {
            return View();
        }

        public IActionResult AddSubCategory()
        {
            return View();
        }

        public IActionResult AddBlog()
        {
            return View();
        }

        public IActionResult ChangePassword()
        {
            var user = _service.GetUser();
            UserViewModel _user = new();
            _user.OldPassword = user.Password;
            return View(_user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserViewModel user)
        {
            try
            {
                var _user = _service.GetUser();

                if (_user.Password == user.OldPassword && user.Password == user.RetypePassword && user.Password != null)
                {
                    _service.ChangePassword(user);
                    await HttpContext.SignOutAsync();
                    return RedirectToAction("Index", "Admin");
                }

                //_service.ChangePassword(user);
                //await HttpContext.SignOutAsync();
                return View();
                //return RedirectToAction("Index", "Admin");
                //return new Result() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return View();
            };
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            try
            {
                _service.AddCategory(category);
                return RedirectToAction("GetAllCategories");
            }
            catch (Exception ex)
            {
                return View();
                //return new Result() { IsSuccess = false };
            };
        }

        [HttpPost]
        public IActionResult AddSubCategory(SubCategory subCategory)
        {
            try
            {
                _service.AddSubCategory(subCategory);
                return RedirectToAction("GetAllSubCategories");
                //return new Result() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return View();
                //return new Result() { IsSuccess = false };
            };
        }

        [HttpPost]
        public ActionResult AddBlog([Bind("Description,CategoryId,Headline,ImageOne,ImageTwo")] BlogViewModel model)
        {
            Blog blog = new()
            {
                Description = model.Description,
                CategoryId = model.CategoryId,
                Headline = model.Headline,
                PublishedDate = DateTime.Now
            };

            string wwwRootPath = _hostEnvironment.WebRootPath;

            //Image One
            if (model.ImageOne != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(model.ImageOne.FileName);
                string extension = Path.GetExtension(model.ImageOne.FileName);
                string name = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);

                if (!Directory.Exists(wwwRootPath + "/Image/"))
                {
                    Directory.CreateDirectory(wwwRootPath + "/Image/");
                }


                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    model.ImageOne.CopyToAsync(fileStream);
                }
                blog.ImageOne = name;


            }
            //Image Two
            if (model.ImageTwo != null)
            {
                string fileNameTwo = Path.GetFileNameWithoutExtension(model.ImageTwo.FileName);
                string extensionTwo = Path.GetExtension(model.ImageTwo.FileName);
                string imageNameTwo = fileNameTwo = fileNameTwo + DateTime.Now.ToString("yymmssfff") + extensionTwo;
                string pathTwo = Path.Combine(wwwRootPath + "/Image/", fileNameTwo);
                using (var fileStream = new FileStream(pathTwo, FileMode.Create))
                {
                    model.ImageTwo.CopyToAsync(fileStream);
                }

                blog.ImageTwo = imageNameTwo;

            }
            _service.AddBlog(blog);
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            var data = _service.GetCategories();
            return View(data);
            //return new Result() { IsSuccess = true, Data = data };
        }


        [HttpGet]
        public IActionResult GetAllSubCategories()
        {
            var data = _service.GetSubCategories();

            foreach (var item in data)
            {
                item.Category = _service.GetCategory(item.CategoryId);
            }

            return View(data);
            //return new Result() { IsSuccess = true, Data = data };
        }



        //Delete

        [HttpGet]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _service.DeleteCategory(id);
                return RedirectToAction("GetAllCategories");
                //return new Result() { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return View();
                //return new Result() { IsSuccess = false };
            };
        }


        [HttpGet]
        public IActionResult DeleteSubCategory(int id)
        {
            try
            {
                _service.DeleteSubCategory(id);
                return RedirectToAction("GetAllSubCategories");
            }
            catch (Exception ex)
            {
                return View();
            };
        }
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteBlog(int id)
        {
            try
            {
                var blog = _service.GetBlog(id);

                if (blog != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;

                    string pathOne = Path.Combine(wwwRootPath + "/Image/", blog.ImageOne);
                    string pathTwo = Path.Combine(wwwRootPath + "/Image/", blog.ImageTwo);

                    System.IO.File.Delete(pathOne);
                    System.IO.File.Delete(pathTwo);
                }

                _service.DeleteBlog(id);
                return View("Index");
            }
            catch (Exception ex)
            {
                return View("Index");
            };
        }
        public IActionResult BlogDetails(int id)
        {
            var data = _service.GetBlog(id);
            return View(data);
        }


    }

}
