using System.Web.Mvc;

namespace BandagingWebApplication.ViewModel
{
    public class BlogViewModel
    {
        public string? Headline { get; set; }

        public string? Description { get; set; }

        public IFormFile ImageOne { get; set; }

        public IFormFile ImageTwo { get; set; }

        public int? CategoryId { get; set; }
    }
}
