using BandagingWebApplication.Data;
using BandagingWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BandagingWebApplication
{
    public class Service
    {
        BandagingContext context = new BandagingContext();
        public bool AddCategory(Category category)
        {
            try
            {
                context.Categories.Add(category);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangePassword(UserViewModel user)
        {
            try
            {
                var _user = context.Users.FirstOrDefault();
                _user.Password = user.Password;
                context.Users.Entry(_user).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddSubCategory(SubCategory category)
        {
            try
            {
                context.SubCategories.Add(category);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public User? Login(string userName, string password)
        {
            try
            {
                var user = context.Users.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
                if (user != null)
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Category? GetCategory(int? id)
        {
            try
            {
                var data = context.Categories.FirstOrDefault(x => x.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<Category>? GetCategories()
        {
            try
            {
                var data = context.Categories.ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SubCategory>? GetSubCategories()
        {
            try
            {
                var data = context.SubCategories.ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SubCategory> GetSubCategoriesByCategory(int id)
        {
            try
            {
                var data = context.SubCategories.Where(x => x.CategoryId == id).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool AddBlog(Blog blog)
        {
            try
            {
                context.Blogs.Add(blog);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public List<Blog>? GetAllBlogs()
        {
            try
            {
                var data = context.Blogs.OrderByDescending(x => x.PublishedDate).ToList();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public User GetUser()
        {
            try
            {
                var data = context.Users.FirstOrDefault();
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public Blog GetBlog(int id)
        {
            try
            {
                var data = context.Blogs.FirstOrDefault(x => x.Id == id);
                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Blog>? GetBlogsByCategory(int categoryId)
        {
            try
            {
                List<Blog> data = new();
                if (categoryId == 0)
                {
                    data = context.Blogs.OrderByDescending(x => x.PublishedDate).ToList();
                }
                else
                    data = context.Blogs.Where(x => x.CategoryId == categoryId).OrderByDescending(x => x.PublishedDate).ToList();

                foreach (var item in data)
                {
                    item.Description = item.Description.Substring(0, Math.Min(item.Description.Length, 100));
                }

                return data;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //Delete
        public void DeleteCategory(int id)
        {
            var category = context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (category != null)
            {

                var subCat = context.SubCategories.Where(x => x.CategoryId == id).ToList();
                foreach (var item in subCat)
                {
                    DeleteSubCategory(item.Id);
                }

                context.Categories.Attach(category);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public void DeleteSubCategory(int id)
        {
            var category = context.SubCategories.Where(x => x.Id == id).FirstOrDefault();
            if (category != null)
            {
                context.SubCategories.Attach(category);
                context.SubCategories.Remove(category);
                context.SaveChanges();
            }
        }

        public void DeleteBlog(int id)
        {
            var blog = context.Blogs.Where(x => x.Id == id).FirstOrDefault();
            if (blog != null)
            {
                context.Blogs.Attach(blog);
                context.Blogs.Remove(blog);
                context.SaveChanges();
            }
        }
    }
}
