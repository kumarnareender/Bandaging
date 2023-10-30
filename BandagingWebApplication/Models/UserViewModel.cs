using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace BandagingWebApplication.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Display(Name ="Old Password")]
        public string? OldPassword { get; set; }
        [Display(Name = "Retype Password")]
        public string? RetypePassword { get; set; }
        [Display(Name = "New Password")]
        public string? Password { get; set; }
    }
}
